using System.Collections.Generic;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Client.Cart;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IOrderProductsService
    {
        /// <summary>
        /// 整理订单商品列表
        /// </summary>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        CartInfo TidyOrderProductList(List<int> selectedCartItemKeyList, List<LF_OrderProducts> orderProductList);

        /// <summary>
        /// 判断购物车项是否被选中
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <returns></returns>
        bool IsSelectedCartItem(int id, List<int> selectedCartItemKeyList);


        /// <summary>
        /// 获取产品商品库存
        /// </summary>
        /// <returns></returns>
        int GetProductStockNumberByPid(int pId);

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="orderProductList"></param>
        void DecreaseProductStockNumber(List<LF_OrderProducts> orderProductList);

        /// <summary>
        /// 计算商品合计金额
        /// </summary>
        /// <param name="selectedOrderProductList"></param>
        /// <returns></returns>
        decimal GetProductAmount(List<LF_OrderProducts> selectedOrderProductList, int uId);

        /// <summary>
        /// 计算直接购买商品合计金额
        /// </summary>
        /// <param name="selectedOrderProductList"></param>
        /// <returns></returns>
        decimal GetDirectBuyProductAmount(List<LF_OrderProducts> selectedOrderProductList);
    }
}
