namespace LianFa.ShopPlatform.Model.Payment.Wxpay
{
    /// <summary>微信app支付</summary>
    public class MWebParameter : JsApiParameter
    {
        /// <summary>预支付交易会话ID</summary>
        public string PrepayId { get; set; }

        /// <summary>mweb_url为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付,mweb_url的有效期为5分钟。</summary>
        public string MWebUrl { get; set; }
    }
}
