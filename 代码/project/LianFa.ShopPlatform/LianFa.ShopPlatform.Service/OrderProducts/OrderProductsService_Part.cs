using System;
using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Client.Cart;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;

namespace LianFa.ShopPlatform.Service
{
    public partial class OrderProductsService
    {
        #region Fields
        /// <summary>
        /// 商品仓储类
        /// </summary>
        private readonly IRepository<LF_Products> _productRepository;

        /// <summary>
        /// 商品库存仓储类
        /// </summary>
        private readonly IRepository<LF_ProductStocks> _productStocksRepository;

        /// <summary>
        /// 分类仓储类
        /// </summary>
        private readonly IRepository<LF_Categories> _categoriesRepository;

        /// <summary>
        /// 订单仓储类
        /// </summary>
        private readonly IRepository<LF_Orders> _orderRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="orderProductsRepository">订单商品仓储类</param>
        /// <param name="productRepository">商品仓储类</param>
        /// <param name="categoriesRepository">分类仓储类</param>
        /// <param name="orderRepository">订单仓储类</param>
        public OrderProductsService(IRepository<LF_OrderProducts> orderProductsRepository, IRepository<LF_Products> productRepository,
            IRepository<LF_Categories> categoriesRepository, IRepository<LF_Orders> orderRepository, IRepository<LF_ProductStocks> productStocksRepository)
        {
            _productRepository = productRepository;
            _categoriesRepository = categoriesRepository;
            _orderProductsRepository = orderProductsRepository;
            _orderRepository = orderRepository;
            _productStocksRepository = productStocksRepository;
        }

        #endregion

        /// <summary>
        /// 整理订单商品列表
        /// </summary>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public CartInfo TidyOrderProductList(List<int> selectedCartItemKeyList, List<LF_OrderProducts> orderProductList)
        {
            //声明一个购物车
            var cartInfo = new CartInfo();
            if (orderProductList.Count < 1)
                return cartInfo;

            //判断购物车商品是否选中
            foreach (var item in orderProductList)
            {
                bool isSelected;
                int isCostPrice = _productRepository.LoadEntitiesNoTracking(p => p.PId == item.PId)
                                      .FirstOrDefault()
                                      ?.IsCostPrice ?? 0;
                if (IsSelectedCartItem(item.PId, selectedCartItemKeyList))
                {
                    cartInfo.SelectedOrderProductList.Add(item);
                    isSelected = true;
                }
                else
                {
                    cartInfo.RemainedOrderProductList.Add(item);
                    isSelected = false;
                }

                //判断是否失效(规格是否存在或者商品是否为上架状态)
                var failure = !_productRepository.Exist(m => m.PId == item.PId && m.State == (int)ProductsStatus.OnSale);

                cartInfo.CartProductList.Add(new CartProductInfo
                {
                    IsSelected = isSelected,
                    IsCostPrice = isCostPrice,
                    OrderProductInfo = item,
                    Failure = failure
                });
                
            }

            cartInfo.IsSelected = cartInfo.SelectedOrderProductList.Count > 0;

            return cartInfo;
        }

        /// <summary>
        /// 判断购物车项是否被选中
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <returns></returns>
        public bool IsSelectedCartItem(int id, List<int> selectedCartItemKeyList)
        {
            if (selectedCartItemKeyList == null || selectedCartItemKeyList.Count == 0)
                return false;
            return selectedCartItemKeyList.Any(x => x == id);
        }

        /// <summary>
        /// 获取产品商品库存
        /// </summary>
        /// <returns></returns>
        public int GetProductStockNumberByPid(int pId)
        {
            var model = _productStocksRepository.LoadEntitiesNoTracking(x => x.PId == pId).FirstOrDefault();
            return model?.Number ?? 0;
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="orderProductList"></param>
        public void DecreaseProductStockNumber(List<LF_OrderProducts> orderProductList)
        {
            if (orderProductList == null) return;

            foreach (var item in orderProductList)
            {
                _productStocksRepository.BatchUpdate(x => item.PId == x.PId, x => new LF_ProductStocks()
                {
                    Number = x.Number - item.RealCount
                });
            }
        }

        /// <summary>
        /// 计算商品合计金额
        /// </summary>
        /// <param name="selectedOrderProductList"></param>
        /// <returns></returns>
        public decimal GetProductAmount(List<LF_OrderProducts> selectedOrderProductList,int uId)
        {
            return selectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);
        }

        /// <summary>
        /// 计算直接购买商品合计金额
        /// </summary>
        /// <param name="selectedOrderProductList"></param>
        /// <returns></returns>
        public decimal GetDirectBuyProductAmount(List<LF_OrderProducts> selectedOrderProductList)
        {
            var pIdList = selectedOrderProductList.Select(x => x.PId).ToList();
            var data = _productRepository.GetDbSetNoTracking()
                .Where(x => pIdList.Contains(x.PId))
                .Select(x => new OrderModelT
                {
                    PId = x.PId,
                    CostPrice = x.CostPrice,
                    ShopPrice = x.ShopPrice,
                    IsCostPrice = x.IsCostPrice,
                    RealCount = 0
                })
                .ToList();
            if (data.Any())
            {
                foreach (var item in data)
                {
                    item.RealCount = selectedOrderProductList.FirstOrDefault(u => u.PId == item.PId).RealCount;
                }
            }
            decimal amount = 0;
            foreach (var item in data)
            {
                if (item.IsCostPrice == (int) WhetherType.No)
                {
                    amount += item.ShopPrice * item.RealCount;
                }
                else
                {
                    amount += item.CostPrice * item.RealCount;
                }
            }

            return amount;
        }

    }
}
