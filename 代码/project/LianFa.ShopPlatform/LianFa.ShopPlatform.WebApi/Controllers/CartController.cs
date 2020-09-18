using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Client.Cart;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.Cart;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 购物车控制器
    /// </summary>
    [Signature]
    [ControllerGroup("购物车相关接口", "用于购物车查询")]
    public class CartController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 用户上下文
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// 购物车服务
        /// </summary>
        private readonly IOrderProductsService _orderProducts;

        /// <summary>
        /// 商品服务
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 商品库存服务
        /// </summary>
        private readonly IProductStocksService _productStocks;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 购物车控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="orderProducts"></param>
        /// <param name="products"></param>
        /// <param name="workContext"></param>
        /// <param name="productStocks"></param>
        /// <param name="users"></param>
        public CartController(IUnitOfWork unitOfWork, IOrderProductsService orderProducts, IProductsService products, 
            IWorkContext workContext, IProductStocksService productStocks, IUsersService users)
        {
            _unitOfWork = unitOfWork;
            _orderProducts = orderProducts;
            _products = products;
            _workContext = workContext;
            _productStocks = productStocks;
            _users = users;
        }

        #region 查询购物车列表

        /// <summary>
        /// 查询购物车列表
        /// </summary>
        /// <returns></returns>
        public BaseResponse<CartResponse> QueryCardList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user =_users.LoadEntitieNoTracking(u=>u.UId==_workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                    {
                        //返回未登录响应
                        return BuildResponse.FailResponse<CartResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    var orderProductList = _orderProducts.GetList(x => x.UId == user.UId && x.OId == 0).ToList();

                    foreach (var orderProduct in orderProductList)
                    {
                        var product = _products.LoadEntitieNoTracking(x => x.PId == orderProduct.PId);

                        if (product == null) continue;
                        if (orderProduct.ShopPrice != Math.Round(product.ShopPrice, 2, MidpointRounding.AwayFromZero) ||
                            orderProduct.CostPrice != Math.Round(product.CostPrice, 2, MidpointRounding.AwayFromZero) ||
                                orderProduct.ShowImg != product.ShowImg)
                        {
                            //更新价格

                            var shopPrice = product.IsCostPrice == (int)WhetherType.Yes
                                ? Math.Round(product.CostPrice, 2, MidpointRounding.AwayFromZero)
                                : Math.Round(product.ShopPrice, 2, MidpointRounding.AwayFromZero);
                            orderProduct.ShopPrice = shopPrice;
                            orderProduct.CostPrice = product.CostPrice;
                            orderProduct.ShowImg = product.ShowImg;
                        }

                        _orderProducts.UpdateOrderProducts(orderProduct);

                        var result = _unitOfWork.Commit();

                        ApiLogger.Debug("QueryCardList", result > 0 ? "查询购物车更新价格成功" : "查询购物车更新价格失败", orderProduct.ToJson());
                    }

                    orderProductList.ForEach(m => m.ShowImg = FileHelper.GetFileFullUrl(m.ShowImg));

                    //购物车信息
                    var cartInfo = _orderProducts.TidyOrderProductList(null, orderProductList);

                    //商品总数量
                    var totalCount = cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);

                    //计算商品合计金额
                    var productAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);
                    //var productAmount = cartInfo.SelectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);

                    //满减折扣
                    var fullCut = cartInfo.CartFullCutList.Where(x => x.IsEnough).Sum(x => x.CutMoney);

                    //订单合计
                    var orderAmount = productAmount - fullCut;

                    var model = new CartResponse
                    {
                        TotalCount = totalCount,
                        ProductAmount = productAmount,
                        FullCut = fullCut,
                        OrderAmount = orderAmount,
                        CartInfo = cartInfo
                    };

                    return BuildResponse.SuccessResponse(model);

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CartResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("QueryCardList", "", ex.Message, ex);
                return BuildResponse.FailResponse<CartResponse>("获取购物车列表错误");
            }
        }

        #endregion

        #region 添加商品到购物车

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        public BaseResponse<object> AddProduct(AddProductRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        //获得当前用户
                        var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                        //当商城不允许游客使用购物车时
                        if (user == null || user.UId < 1)
                            return BuildResponse.FailResponse<object>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        var orderProductList = _orderProducts.LoadEntitiesNoTracking(x => x.UId == user.UId && x.OId == 0).ToList();

                        var product = _products.LoadEntitieNoTracking(x => x.PId == request.PId);
                        if (product == null)
                            return BuildResponse.FailResponse<object>("商品不存在");

                        //判断库存
                        var stock = _orderProducts.GetProductStockNumberByPid(request.PId);
                        if (stock < request.Count)
                            return BuildResponse.FailResponse<object>("库存不足");

                        var buyCount = request.Count;//购买数量

                        //购买数量不能小于1
                        if (buyCount < 1)
                            return BuildResponse.FailResponse<object>("购买数量不能小于1");

                        var orderProductInfo = orderProductList.FirstOrDefault(x => x.PId == product.PId);

                        var shopPrice = product.IsCostPrice == (int)WhetherType.Yes
                            ? Math.Round(product.CostPrice, 2, MidpointRounding.AwayFromZero)
                            : Math.Round(product.ShopPrice, 2, MidpointRounding.AwayFromZero);

                        if (orderProductInfo != null)
                        {
                            //商品库存  
                            if (stock < request.Count + orderProductInfo.BuyCount)
                                return BuildResponse.FailResponse<object>("库存不足");
                            //更新
                            //提交事务
                            var result = _orderProducts.BatchUpdate(x => x.RecordId == orderProductInfo.RecordId,
                                x => new LF_OrderProducts()
                                {
                                    BuyCount = x.BuyCount + buyCount,
                                    RealCount = x.RealCount + buyCount,
                                    ShopPrice = shopPrice
                                });
                        }
                        else
                        {
                            //商品库存
                            if (stock < request.Count)
                                return BuildResponse.FailResponse<object>("库存不足");

                            var model = new LF_OrderProducts()
                            {
                                OId = 0,
                                SId = "",
                                CostPrice = product.CostPrice,
                                ShopPrice = shopPrice,
                                ShowImg = product.ShowImg,
                                BrandId = product.BrandId,
                                CateId = product.CateId,
                                Weight = product.Weight,
                                Name = product.Name,
                                PId = product.PId,
                                UId = user.UId,
                                RealCount = buyCount,
                                SendCount = 0,
                                BuyCount = buyCount,
                                AddTime = DateTime.Now,
                                ShipFee = 0,
                                PSn = product.PSn,
                            };

                            _orderProducts.AddOrderProducts(model);
                        }
                        _unitOfWork.Commit();
                        //成功提交事务
                        scope.Complete();

                        //返回成功结果
                        return BuildResponse.SuccessResponse<object>("加入购物车成功");
                    }

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AddProduct", "", ex.Message, ex);
                return BuildResponse.FailResponse<object>("加入购物车错误");
            }
        }

        #endregion

        #region 修改购物车中商品数量

        /// <summary>
        /// 修改购物车中商品数量
        /// </summary>
        public BaseResponse<object> ChangeProductCount(ChangePruductCountRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                        return BuildResponse.FailResponse<object>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    var pSku = _products.LoadEntitieNoTracking(x => x.PId == request.PId);
                    if (pSku == null)
                        return BuildResponse.FailResponse<object>("商品不存在");

                    //判断库存
                    var stock = _orderProducts.GetProductStockNumberByPid(request.PId);
                    if (stock < request.Count)
                        return BuildResponse.FailResponse<object>("库存不足");

                    //购物车中已经存在的商品列表
                    var orderProductList = _orderProducts.LoadEntitiesNoTracking(x => x.UId == user.UId && x.OId == 0).ToList();

                    foreach (var orderProductImgInfo in orderProductList)
                    {
                        orderProductImgInfo.ShowImg = FileHelper.GetFileFullUrl(orderProductImgInfo.ShowImg);
                    }

                    var buyCount = request.Count;//购买数量
                    var selectedCartItemKeyList = request.SelectedCartItemKeyList;//选中的购物车项键列表

                    var orderProduct = orderProductList.Where(x => x.PId == request.PId).ToList();
                    var orderProductInfo = orderProduct.FirstOrDefault();
                    if (orderProduct.Any() && orderProductInfo != null)
                    {
                        if (buyCount < 1)//当购买数量小于1时，删除此商品
                        {
                            var deleteId = new List<int> { orderProductInfo.RecordId };
                            _orderProducts.BatchDelete(x => deleteId.Any(y => y == x.RecordId));
                        }
                        else if (buyCount != orderProductInfo.BuyCount)
                        {
                            orderProductInfo.BuyCount = buyCount;
                            orderProductInfo.RealCount = buyCount;
                            _orderProducts.BatchUpdate(x => x.RecordId == orderProductInfo.RecordId, x => new LF_OrderProducts()
                            {
                                BuyCount = buyCount,
                                RealCount = buyCount
                            });
                        }
                    }
                    //购物车信息
                    var cartInfo = _orderProducts.TidyOrderProductList(selectedCartItemKeyList, orderProductList);

                    //商品总数量
                    var totalCount = cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    //商品合计
                    var productAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);
                    //满减折扣
                    var fullCut = cartInfo.CartFullCutList.Where(x => x.IsEnough).Sum(x => x.CutMoney);
                    //订单合计
                    var orderAmount = productAmount - fullCut;

                    var model = new CartResponse
                    {
                        TotalCount = totalCount,
                        ProductAmount = productAmount,
                        FullCut = fullCut,
                        OrderAmount = orderAmount,
                        CartInfo = cartInfo,
                    };

                    return BuildResponse.SuccessResponse<object>(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ChangeProductCount", "", ex.Message, ex);
                return BuildResponse.FailResponse<object>("系统错误");
            }
        }

        #endregion

        #region 删除购物车中商品

        /// <summary>
        /// 删除购物车中商品
        /// </summary>
        public BaseResponse<object> DelProduct(DelPruductRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                        return BuildResponse.FailResponse<object>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    //购物车中已经存在的商品列表
                    var orderProductList = _orderProducts.LoadEntitiesNoTracking(x => x.UId == user.UId && x.OId == 0).ToList();

                    for (int i = 0; i < orderProductList.Count; i++)
                    {
                        orderProductList[i].ShowImg = FileHelper.GetFileFullUrl(orderProductList[i].ShowImg);
                    }

                    var selectedCartItemKeyList = request.SelectedCartItemKeyList;//选中的购物车项键列表

                    var orderProduct = orderProductList.Where(x => request.PIds.Any(y => y == x.PId)).ToList();
                    if (orderProduct.Any())
                    {
                        var deleteId = new List<int>();
                        deleteId.AddRange(orderProduct.Select(x => x.RecordId).ToList());
                        orderProductList.RemoveAll(x => deleteId.Any(y => y == x.RecordId));
                        _orderProducts.BatchDelete(x => deleteId.Any(y => y == x.RecordId));
                    }

                    //商品数量
                    int pCount = orderProductList.Sum(x => x.RealCount);

                    //购物车信息
                    CartInfo cartInfo = _orderProducts.TidyOrderProductList(selectedCartItemKeyList, orderProductList);


                    //商品总数量
                    int totalCount = cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    //商品合计
                    //decimal productAmount = cartInfo.SelectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);
                    decimal productAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);

                    //满减折扣
                    int fullCut = cartInfo.CartFullCutList.Where(x => x.IsEnough).Sum(x => x.CutMoney);
                    //订单合计
                    decimal orderAmount = productAmount - fullCut;

                    CartResponse model = new CartResponse
                    {
                        TotalCount = totalCount,
                        ProductAmount = productAmount,
                        FullCut = fullCut,
                        OrderAmount = orderAmount,
                        CartInfo = cartInfo,
                    };

                    return BuildResponse.SuccessResponse<object>(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("DelProduct", "", ex.Message, ex);
                return BuildResponse.FailResponse<object>("系统错误");
            }
        }

        #endregion

        #region 取消或选中购物车项

        /// <summary>
        /// 取消或选中购物车项
        /// </summary>
        /// <returns></returns>
        public BaseResponse<CartResponse> CancelOrSelectCartItem(CancelOrSelectCartItemRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                        return BuildResponse.FailResponse<CartResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    //购物车中已经存在的商品列表
                    var orderProductList = _orderProducts.LoadEntitiesNoTracking(x => x.UId == user.UId && x.OId == 0).ToList();

                    foreach (var orderProduct in orderProductList)
                    {
                        orderProduct.ShowImg = FileHelper.GetFileFullUrl(orderProduct.ShowImg);
                    }

                    var selectedCartItemKeyList = request.SelectedCartItemKeyList;//选中的购物车项键列表

                    //购物车信息
                    var cartInfo = _orderProducts.TidyOrderProductList(selectedCartItemKeyList, orderProductList);

                    //商品总数量
                    var totalCount = cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    //商品合计
                    //var productAmount = cartInfo.SelectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);
                    decimal productAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);

                    //满减折扣
                    var fullCut = cartInfo.CartFullCutList.Where(x => x.IsEnough).Sum(x => x.CutMoney);
                    //订单合计
                    var orderAmount = productAmount - fullCut;

                    var model = new CartResponse
                    {
                        TotalCount = totalCount,
                        ProductAmount = productAmount,
                        FullCut = fullCut,
                        OrderAmount = orderAmount,
                        CartInfo = cartInfo,
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CartResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CancelOrSelectCartItem", "", ex.Message, ex);
                return BuildResponse.FailResponse<CartResponse>("系统错误");
            }
        }

        #endregion

        #region 选中全部购物车项

        /// <summary>
        /// 选中全部购物车项
        /// </summary>
        /// <returns></returns>
        public BaseResponse<CartResponse> SelectAllCartItem()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user.UId < 1)
                        return BuildResponse.FailResponse<CartResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    //购物车中已经存在的商品列表
                    var orderProductList = _orderProducts.LoadEntitiesNoTracking(x => x.UId == user.UId && x.OId == 0).ToList();

                    foreach (var orderProduct in orderProductList)
                    {
                        orderProduct.ShowImg = FileHelper.GetFileFullUrl(orderProduct.ShowImg);
                    }

                    //购物车信息
                    var cartInfo = _orderProducts.TidyOrderProductList(null, orderProductList);


                    //商品总数量
                    var totalCount = cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    //商品合计
                    //var productAmount = cartInfo.SelectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);
                    decimal productAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);

                    //满减折扣
                    var fullCut = cartInfo.CartFullCutList.Where(x => x.IsEnough).Sum(x => x.CutMoney);
                    //订单合计
                    var orderAmount = productAmount - fullCut;

                    var model = new CartResponse
                    {
                        TotalCount = totalCount,
                        ProductAmount = productAmount,
                        FullCut = fullCut,
                        OrderAmount = orderAmount,
                        CartInfo = cartInfo,
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CartResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("SelectAllCartItem", "", ex.Message, ex);
                return BuildResponse.FailResponse<CartResponse>("系统错误");
            }

        }

        #endregion

        /// <summary>
        /// 获取模型验证错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        [NonAction]
        private string GetModelErrorMsg()
        {
            //错误信息
            var errorMessage = string.Empty;

            //获取所有错误的Key
            var key = ModelState.Keys.FirstOrDefault();
            if (key != null)
            {
                //获取第一个key对应的ModelStateDictionary的第一条错误信息
                var error = ModelState[key].Errors.FirstOrDefault();

                //将错误描述添加到sb中
                errorMessage = error?.ErrorMessage;
            }

            //返回错误信息
            return errorMessage;
        }
    }
}
