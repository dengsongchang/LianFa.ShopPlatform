namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 提交扫描订单
    /// </summary>
    public class SubmitScanOrderRequest
    {
        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }

        /// <summary>
        /// 支付插件名称 1支付宝 2微信
        /// </summary>
        public byte PayType { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// PId
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 卡片Id
        /// </summary>
        public int CardId { get; set; }
    }
}
