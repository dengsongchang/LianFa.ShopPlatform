namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 评价订单请求类
    /// </summary>
    public class ReviewProductRequest
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int Oid { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 订单记录Id
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 星星
        /// </summary>
        public int Star { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        public string Message { get; set; }
    }
}