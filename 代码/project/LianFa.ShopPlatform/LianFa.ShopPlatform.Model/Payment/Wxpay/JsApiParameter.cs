namespace LianFa.ShopPlatform.Model.Payment.Wxpay
{
    /// <summary>
    /// 微信支付 JS Api 参数
    /// </summary>
    public class JsApiParameter
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary> 
        /// 
        /// package
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// 支付签名
        /// </summary>
        public string PaySign { get; set; }
    }
}
