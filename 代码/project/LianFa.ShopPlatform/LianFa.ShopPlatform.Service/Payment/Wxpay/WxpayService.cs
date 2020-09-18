using System;
using HuCheng.Util.Core.Dependency;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Model.Payment.Wxpay;
using Senparc.Weixin.TenPay;
using Senparc.Weixin.TenPay.V3;

namespace LianFa.ShopPlatform.Service.Payment.Wxpay
{
    [DependencyRegister]
    public class WxpayService : IWxpayService
    {

        /// <summary>
        /// 日志管理器
        /// </summary>
        public readonly ILog ApiLogger;

        /// <summary>
        /// 注入日志
        /// </summary>
        /// <param name="apiLogger"></param>
        public WxpayService(ILog apiLogger)
        {
            ApiLogger = apiLogger;
        }

        /// <summary>
        /// 获取JsApi参数列表
        /// </summary>
        /// <param name="openId">openId</param>
        /// <param name="productName">订单名称</param>
        /// <param name="price">付款金额</param>
        /// <param name="createIp">用户的公网ip，不是商户服务器IP</param 
        /// <param name="notifyUrl">回调地址</param>
        /// <param name="spBillNo">商家订单号</param>
        /// <returns></returns>
        public JsApiParameter GetJsApiParams(string openId, string productName, decimal price, string createIp, string notifyUrl, string spBillNo = "")
        {
            //如果订单序列号为空，则生成
            if (string.IsNullOrEmpty(spBillNo))
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                spBillNo = $"{ConfigMap.MchId}{DateTime.Now:yyyyMMddHHmmss}{TenPayV3Util.BuildRandomStr(6)}";
            }
            else
            {
                spBillNo = spBillNo + TenPayV3Util.BuildRandomStr(8);
            }

            ApiLogger.Info("GetJsApiParams", $"微信支付基本参数：openId:{openId},spBillNo:{spBillNo},productName:{productName},price:{price},createIp:{createIp}");

            //获取请求参数
            var timeStamp = TenPayV3Util.GetTimestamp();
            var nonceStr = TenPayV3Util.GetNoncestr();

            //订单名称
            var body = productName;

            //付款金额
            var totalFee = (int)(price * 100);

            ApiLogger.Info("GetJsApiParams", $"微信支付package订单参数：timeStamp:{timeStamp},nonceStr:{nonceStr},totalFee:{totalFee},notifyUrl:{notifyUrl},openId:{openId}");

            //获取预下单XML数据
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(ConfigMap.AppId, ConfigMap.MchId, body, spBillNo, totalFee, createIp, notifyUrl, TenPayV3Type.JSAPI, openId, ConfigMap.Key, nonceStr);

            ApiLogger.Info("GetJsApiParams", $"微信支付预下单XML数据：xmlDataInfo:{xmlDataInfo.ToJson()}");

            //调用统一订单接口
            var result = TenPayV3.Unifiedorder(xmlDataInfo);
            ApiLogger.Info("GetJsApiParams", $"微信支付统一订单接口返回数据：result:{result.ToJson()}");

            //获取预下单Id
            var package = $"prepay_id={result.prepay_id}";

            //获取支付签名
            var paySign = TenPayV3.GetJsPaySign(ConfigMap.AppId, timeStamp, nonceStr, package, ConfigMap.Key); ;

            ApiLogger.Info("GetJsApiParams", $"微信支付参数：package:{package},paySign:{paySign}");

            //返回JsApi参数
            var apiParams = new JsApiParameter
            {
                TimeStamp = timeStamp,
                NonceStr = nonceStr,
                Package = package,
                PaySign = paySign
            };

            return apiParams;
        }

        /// <summary>
        /// 获取app参数列表
        /// </summary>
        /// <param name="productName">订单名称</param>
        /// <param name="price">付款金额</param>
        /// <param name="createIp">用户的公网ip，不是商户服务器IP</param> 
        /// <param name="notifyUrl">回调地址</param>
        /// <param name="spBillNo">商家订单号</param>
        /// <returns></returns>
        public MWebParameter GetWebAppParams(string productName, decimal price, string createIp, string notifyUrl, string spBillNo = "")
        {
            //如果订单序列号为空，则生成
            if (string.IsNullOrEmpty(spBillNo))
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                spBillNo = $"{ConfigMap.AppMchId}{DateTime.Now:yyyyMMddHHmmss}{TenPayV3Util.BuildRandomStr(6)}";
            }
            else
            {
                spBillNo = spBillNo + TenPayV3Util.BuildRandomStr(8);
            }

            ApiLogger.Info("GetAppParams", $"微信支付基本参数：spBillNo:{spBillNo},productName:{productName},price:{price},createIp:{createIp}");

            //获取请求参数
            var timeStamp = TenPayV3Util.GetTimestamp();
            var nonceStr = TenPayV3Util.GetNoncestr();

            //订单名称
            var body = productName;

            //付款金额
            var totalFee = (int)(price * 100);

            ApiLogger.Info("GetAppParams", $"微信支付package订单参数：timeStamp:{timeStamp},nonceStr:{nonceStr},totalFee:{totalFee},notifyUrl:{notifyUrl}");

            //获取预下单XML数据
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(ConfigMap.AppPayAppId, ConfigMap.AppMchId, body, spBillNo, totalFee, createIp, notifyUrl, TenPayV3Type.MWEB, "", ConfigMap.AppPayKey, nonceStr);

            ApiLogger.Info("GetAppParams", $"微信支付预下单XML数据：xmlDataInfo:{xmlDataInfo.ToJson()}");

            //调用统一订单接口
            var result = TenPayV3.Unifiedorder(xmlDataInfo);
            ApiLogger.Info("GetAppParams", $"微信支付统一订单接口返回数据：result:{result.ToJson()}");
            //获取预下单Id
            var package = "Sign=WXPay";

            var paySign = GetPaySign(ConfigMap.AppPayAppId, timeStamp, ConfigMap.AppMchId, result.prepay_id, nonceStr,
                package, ConfigMap.AppPayKey);

            ApiLogger.Info("GetJsApiParams", $"微信支付参数：package:{package},paySign:{paySign}");


            //返回app参数
            var appParams = new MWebParameter()
            {
                TimeStamp = timeStamp,
                NonceStr = nonceStr,
                Package = package,
                PaySign = paySign,
                MWebUrl = result.mweb_url,
                PrepayId = result.prepay_id
            };

            return appParams;
        }

        /// <summary>
        /// 获取UI使用的JS支付签名
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="timeStamp"></param>
        /// <param name="prepayId"></param>
        /// <param name="nonceStr"></param>
        /// <param name="package">格式：prepay_id={0}</param>
        /// <param name="key"></param>
        /// <param name="signType"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public static string GetPaySign(string appId, string timeStamp, string partnerId, string prepayId, string nonceStr, string package, string key,
            string signType = "MD5")
        {
            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler();
            paySignReqHandler.SetParameter("appid", appId);
            paySignReqHandler.SetParameter("timestamp", timeStamp);
            paySignReqHandler.SetParameter("partnerid", partnerId);
            paySignReqHandler.SetParameter("prepayid", prepayId);
            paySignReqHandler.SetParameter("noncestr", nonceStr);
            paySignReqHandler.SetParameter("package", package);
            var paySign = paySignReqHandler.CreateMd5Sign("key", key);
            return paySign;
        }

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="openId">openId</param>
        /// <param name="amount"></param>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public TransfersResult Transfers(string openId, decimal amount, string remark, string ip, string name = "")
        {
            //var nonceStr = TenPayV3Util.GetNoncestr();

            //var outNo = "Withdraw-" + DateTime.Now.Ticks;

            //var dataInfo = new TenPayV3TransfersRequestData(ConfigMap.TransfersAppId, ConfigMap.TransfersMchId, null, nonceStr, outNo, openId, ConfigMap.TransfersKey, ConfigMap.TransfersCheckName, name, amount, remark, ip);

            //var password = ConfigMap.TransfersMchId; //默认为商户号，建议修改

            ////获取支付签名
            //var transfersResult = TenPayV3.Transfers(dataInfo, ConfigMap.CertificateMicroLetter, password);

            //ApiLogger.Info("Transfers", $"微信支付参数：dataInfo:{dataInfo},transfersResult:{transfersResult}");

            return new TransfersResult("");
        }

        /// <summary>
        /// 退款申请接口
        /// </summary>
        /// <returns></returns>
        public RefundResult Refund(string transactionId, int refundFee, int totalFee)
        {
            var nonceStr = TenPayV3Util.GetNoncestr();

            var outRefundNo = "OutRefunNo-" + DateTime.Now.Ticks;

            var opUserId = ConfigMap.MchId;
            var dataInfo = new TenPayV3RefundRequestData(ConfigMap.AppId, ConfigMap.MchId, ConfigMap.Key,
                null, nonceStr, transactionId.Trim(), null, outRefundNo, totalFee, refundFee, opUserId, null);

            var password = ConfigMap.MchId; //默认为商户号，建议修改
            var result = TenPayV3.Refund(dataInfo, ConfigMap.CertificateMicroLetter, password);
            return result;
        }

        /// <summary>
        /// 退款申请接口
        /// </summary>
        /// <returns></returns>
        public RefundResult AppRefund(string transactionId, int refundFee, int totalFee)
        {
            var nonceStr = TenPayV3Util.GetNoncestr();

            var outRefundNo = "OutRefunNo-" + DateTime.Now.Ticks;

            var opUserId = ConfigMap.AppMchId;
            var dataInfo = new TenPayV3RefundRequestData(ConfigMap.AppPayAppId, ConfigMap.AppMchId, ConfigMap.AppPayKey,
                null, nonceStr, transactionId.Trim(), null, outRefundNo, totalFee, refundFee, opUserId, null);

            var password = ConfigMap.AppMchId; //默认为商户号，建议修改
            var result = TenPayV3.Refund(dataInfo, ConfigMap.AppPayCertificateMicro, password);
            return result;
        }
    }
}
