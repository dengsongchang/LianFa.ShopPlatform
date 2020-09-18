namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 重新提交订单接口
    /// </summary>
    public class ReSubmitOrderRequest
    {
        /// <summary>
        /// 小程序Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public int OId { get; set; }
    }
}
