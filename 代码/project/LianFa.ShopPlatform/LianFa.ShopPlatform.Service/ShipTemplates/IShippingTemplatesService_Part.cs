using System.Collections.Generic;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Model.Response.Admin.Templates;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IShippingTemplatesService
    {
        /// <summary>
        /// 获得配送模板列表
        /// </summary>
        /// <param name="page">分页模型</param>
        /// <param name="total">总数</param>
        /// <returns>配送模板列表</returns>
        List<TemplatesListInfo> GetTemplatesList(PageModel page, out int total);

        /// <summary>
        /// 计算运费
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="defaultFullShipAddressInfo">用户完整地址信息</param>
        /// <returns>计算运费</returns>
        decimal CalculateFreight(List<LF_OrderProducts> orderProductList, FullShipAddressInfo defaultFullShipAddressInfo);

        /// <summary>
        /// 计算单种商品运费
        /// </summary>
        /// <param name="orderProducts">商品</param>
        /// <param name="defaultFullShipAddressInfo"></param>
        /// <returns>计算运费</returns>
        decimal ProductCalculateFreight(LF_OrderProducts orderProducts, FullShipAddressInfo defaultFullShipAddressInfo);

        /// <summary>
        /// 计算单种礼品卡运费
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <param name="defaultFullShipAddressInfo"></param>
        /// <returns>计算运费</returns>
        decimal CouponCalculateFreight(LF_OrderProducts orderProduct, FullShipAddressInfo defaultFullShipAddressInfo);

        /// <summary>
        /// 获得配送地区价格表
        /// </summary>
        /// <param name="id">运费模板id</param>
        /// <returns>配送地区价格表</returns>
        List<ShippingRegionsGroups> GetRegionsGroupsList(int id);
    }
}
