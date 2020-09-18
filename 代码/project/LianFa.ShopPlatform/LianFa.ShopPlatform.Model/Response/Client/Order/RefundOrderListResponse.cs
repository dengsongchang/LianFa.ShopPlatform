using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.Order
{
    /// <summary>
    /// 退款订单列表响应类
    /// </summary>
    public class RefundOrderListResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<OrderList> OrderList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
