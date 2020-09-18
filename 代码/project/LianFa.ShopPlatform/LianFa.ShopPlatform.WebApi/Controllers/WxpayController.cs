using System;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Service;
using Senparc.Weixin.TenPay.V3;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 微信支付控制器
    /// </summary>
    public class WxpayController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 订单管理
        /// </summary>
        private readonly IOrdersService _orders;

        /// <summary>
        /// 订单动作服务
        /// </summary>
        private readonly IOrderActionsService _orderActions;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object Locker = new object();
        /// <summary>
        /// 订单商品服务
        /// </summary>
        private readonly IOrderProductsService _orderProducts;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="orders">订单管理</param> 
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="orderActions">订单动作服务</param>
        /// <param name="orderProducts"></param>
        /// <param name="users"></param>
        /// <param name="products"></param>
        public WxpayController(IOrdersService orders, IUnitOfWork unitOfWork,
            IOrderActionsService orderActions, IOrderProductsService orderProducts, IUsersService users, IProductsService products)
        {
            _unitOfWork = unitOfWork;
            _orders = orders;
            _orders = orders;
            _orderActions = orderActions;
            _orderProducts = orderProducts;
            _users = users;
            _products = products;
        }

        /// <summary>
        /// 通知调用
        /// </summary>
        public IHttpActionResult Notify()
        {
            lock (Locker)
            {
                try
                {
                    var resHandler = new ResponseHandler(null);

                    //获取微信回调接口错误码及信息
                    var returnCode = resHandler.GetParameter("return_code");
                    var returnMsg = resHandler.GetParameter("return_msg");

                    #region 微信回调参数

                    //微信分配的公众账号 ID
                    var appid = resHandler.GetParameter("appid");

                    //以下字段在 return_code 为 SUCCESS 的时候有返回--------------------------------
                    //微信支付分配的商户号
                    var mchId = resHandler.GetParameter("mch_id");
                    //微信支付分配的终端设备号
                    var deviceInfo = resHandler.GetParameter("device_info");
                    //微信分配的公众账号 ID
                    var nonceStr = resHandler.GetParameter("nonce_str");
                    //业务结果 SUCCESS/FAIL
                    var resultCode = resHandler.GetParameter("result_code");
                    //错误代码 
                    var errCode = resHandler.GetParameter("err_code");
                    //结果信息描述
                    var errCodeDes = resHandler.GetParameter("err_code_des");

                    //以下字段在 return_code 和 result_code 都为 SUCCESS 的时候有返回---------------
                    //-------------业务参数---------------------------------------------------------
                    //用户在商户 appid 下的唯一标识
                    var openid = resHandler.GetParameter("openid");
                    //用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
                    var isSubscribe = resHandler.GetParameter("is_subscribe");
                    //JSAPI、NATIVE、MICROPAY、APP
                    var tradeType = resHandler.GetParameter("trade_type");
                    //银行类型，采用字符串类型的银行标识
                    var bankType = resHandler.GetParameter("bank_type");

                    //货币类型，符合 ISO 4217 标准的三位字母代码，默认人民币：CNY
                    var feeType = resHandler.GetParameter("fee_type");
                    //微信支付订单号
                    var transactionId = resHandler.GetParameter("transaction_id");
                    //商户系统的订单号，与请求一致。
                    var outTradeNo = resHandler.GetParameter("out_trade_no");
                    //商家数据包，原样返回
                    var attach = resHandler.GetParameter("attach");
                    //支 付 完 成 时 间 ， 格 式 为yyyyMMddhhmmss，如 2009 年12 月27日 9点 10分 10 秒表示为 20091227091010。时区为 GMT+8 beijing。该时间取自微信支付服务器
                    var timeEnd = resHandler.GetParameter("time_end");

                    #endregion

                    #region 验证回调

                    //订单总金额，单位为分
                    var totalFee = Convert.ToDecimal(resHandler.GetParameter("total_fee"));

                    resHandler.SetKey(ConfigMap.Key);

                    //验证请求是否从微信发过来（安全）
                    if (!resHandler.IsTenpaySign() || returnCode.ToUpper() != "SUCCESS")
                    {
                        ApiLogger.Error("Notify", $"outTradeNo:{outTradeNo}", "通知调用参数");
                        return Ok();
                    }

                    #endregion

                    ApiLogger.Info("微信回调支付成功", "支付信息：商家订单号：" + outTradeNo + "、支付金额(分)：" + totalFee + "、自定义参数：" + attach, "");

                    #region 支付成功逻辑

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            #region 1. 更新订单

                            #region 1.1 更新支付订单

                            var oSn = outTradeNo.Substring(0, outTradeNo.Length - 8);
                            //获取订单
                            var order = _orders.Get(X => X.OSn == oSn);
                            ApiLogger.Info("支付宝回调支付成功", order.ToJson());
                            if (order == null)
                            {
                                ApiLogger.Error("Notify", $"{oSn}", $"找不到订单编号为：{oSn}的订单");
                                return Ok();
                            }

                            if (order.OrderState != (int)OrderState.WaitPaying)
                            {
                                return Ok();
                            }

                            //更新订单
                            order.PaySn = transactionId;
                            order.OrderState = (byte)OrderState.PreProduct;
                            order.PayTime = DateTime.Now;
                            _orders.UpdateOrders(order);


                            #endregion

                            #region 1.2 创建订单动作记录

                            //创建订单处理
                            var orderAction = new LF_OrderActions
                            {
                                OId = order.OId,
                                UId = order.UId,
                                RealName = "本人",
                                AdminGId = 0,
                                AdminGTitle = "非管理员",
                                ActionType = (int)OrderActionType.Pay,
                                ActionTime = DateTime.Now,
                                ActionDes = "你使用微信支付订单成功，微信交易号为:" + transactionId
                            };
                            _orderActions.AddOrderActions(orderAction);

                            #endregion

                            #endregion

                            _unitOfWork.Commit();

                            //成功提交所有事务
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            ApiLogger.Error("Notify", "", "更新订单错误", ex);
                            return InternalServerError();
                        }
                        finally
                        {
                            scope.Dispose();
                        }
                    }

                    #endregion

                    //返回成功xml报文
                    var xml = $@"<xml>
                            <return_code><![CDATA[{returnCode}]]></return_code>
                            <return_msg><![CDATA[{returnMsg}]]></return_msg>
                            </xml>";

                    return Ok(xml);
                }
                catch (Exception ex)
                {
                    ApiLogger.Error("Notify", "", ex);
                    return InternalServerError();
                }
            }
        }
    }
}