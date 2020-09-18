using LianFa.ShopPlatform.Model.Payment.Wxpay;
using Senparc.Weixin.TenPay.V3;

namespace LianFa.ShopPlatform.Service.Payment.Wxpay
{
    public interface IWxpayService
    {
        /// <summary>
        /// 获取JsApi参数列表
        /// </summary>
        /// <returns></returns>
        JsApiParameter GetJsApiParams(string openId, string productName, decimal price, string createIp, string notifyUrl, string spBillNo = "");

        /// <summary>
        /// 获取app参数列表
        /// </summary>
        /// <param name="productName">订单名称</param>
        /// <param name="price">付款金额</param>
        /// <param name="createIp">用户的公网ip，不是商户服务器IP</param> 
        /// <param name="notifyUrl">回调地址</param>
        /// <param name="spBillNo">商家订单号</param>
        /// <returns></returns>
        MWebParameter GetWebAppParams(string productName, decimal price, string createIp, string notifyUrl,
            string spBillNo = "");
        /// <summary>
        /// 退款申请接口
        /// </summary>
        /// <returns></returns>
        RefundResult Refund(string transactionId, int refundFee, int totalFee);

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="openId">openId</param>
        /// <param name="amount"></param>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        TransfersResult Transfers(string openId, decimal amount, string remark, string ip, string name = "");

        /// <summary>
        /// 退款申请接口
        /// </summary>
        /// <returns></returns>
        RefundResult AppRefund(string transactionId, int refundFee, int totalFee);

    }
}
