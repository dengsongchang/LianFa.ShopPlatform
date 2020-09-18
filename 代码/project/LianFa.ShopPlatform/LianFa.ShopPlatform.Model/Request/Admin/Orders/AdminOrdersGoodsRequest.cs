namespace LianFa.ShopPlatform.Model.Request.Admin.Orders
{
    /// <summary>
    /// 订单发货请求类
    /// </summary>
    public class AdminOrdersGoodsRequest
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipCode { get; set; }

        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string ShipFriendName { get; set; }
    }
}
