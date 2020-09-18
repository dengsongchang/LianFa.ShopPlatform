namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 直接确认扫描订单
    /// </summary>
    public class DirectConfirmScanOrderRequest
    {
        /// <summary>
        ///商品Id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
