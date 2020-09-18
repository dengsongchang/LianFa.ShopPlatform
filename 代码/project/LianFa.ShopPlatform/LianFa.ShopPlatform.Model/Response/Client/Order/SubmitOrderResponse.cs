
using LianFa.ShopPlatform.Model.Payment.Wxpay;

namespace LianFa.ShopPlatform.Model.Response.Client.Order
{
    /// <summary>
    /// 提交订单
    /// </summary>
    public class SubmitOrderResponse
    {
        /// <summary>
        /// 微信支付参数
        /// </summary>
        public MWebParameter WechatParameter { get; set; }

        /// <summary>
        /// 微信支付参数
        /// </summary>
        public JsApiParameter JsApiParameter { get; set; }
    }

    public class OrderModelT

    {
        public decimal CostPrice { get; set; }
        public decimal ShopPrice { get; set; }
        public int IsCostPrice { get; set; }
        public int RealCount { get; set; }
        public int PId { get; set; }

    }

}
