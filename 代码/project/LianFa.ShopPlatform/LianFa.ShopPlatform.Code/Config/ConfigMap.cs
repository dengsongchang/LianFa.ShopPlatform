using System;
using System.Collections.Generic;
using HuCheng.Util.Configs.Base;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.TypeConvert;

namespace LianFa.ShopPlatform.Code.Config
{
    public class ConfigMap
    {
        /// <summary>
        /// 基础配置文件管理(config文件夹)
        /// </summary>
        private static readonly ConfigBaseManager ConfigManager = new ConfigBaseManager();

        #region 接口签名相关

        /// <summary>
        /// 接口签名密钥
        /// </summary>
        public static string SignKey => ConfigManager.ReadConfig("Settings", "configuration/SignKey");

        /// <summary>
        /// 后台接口签名密钥
        /// </summary>
        public static string AdminSignKey => ConfigManager.ReadConfig("Settings", "configuration/AdminSignKey");

        /// <summary>
        /// 身份验证时间
        /// </summary>
        public static int CheckTime => Convert.ToInt32(ConfigManager.ReadConfig("Settings", "configuration/CheckTime"));

        #endregion

        #region 用户相关

        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword => ConfigManager.ReadConfig("Settings", "configuration/DefaultPassword");

        #endregion

        #region 微信小程序相关

        /// <summary>  
        /// AppId  
        /// </summary>
        public static string WxMiniAppId => ConfigHelper.GetAppSettings("WxMiniAppId");

        /// <summary>  
        /// AppSecret  
        /// </summary>
        public static string WxMiniAppSecret => ConfigHelper.GetAppSettings("WxMiniAppSecret");

        #endregion

        #region 基本设置相关

        /// <summary>
        /// 弹屏图片
        /// </summary>
        public static string ScreenImg => ConfigManager.ReadConfig("BasicSetting", "configuration/ScreenImg");

        /// <summary>
        /// 弹屏链接
        /// </summary>
        public static string ScreenLink => ConfigManager.ReadConfig("BasicSetting", "configuration/ScreenLink");

        /// <summary>
        /// 公司地址
        /// </summary>
        public static string Address => ConfigManager.ReadConfig("BasicSetting", "configuration/Address");

        /// <summary>
        /// 客服热线电话
        /// </summary>
        public static string CustomerPhone => ConfigManager.ReadConfig("BasicSetting", "configuration/CustomerPhone");

        /// <summary>
        /// 默认头像
        /// </summary>
        public static string InitialAvatar => ConfigManager.ReadConfig("BasicSetting", "configuration/InitialAvatar");

        /// <summary>
        /// 礼品卡兑换banner图
        /// </summary>
        public static string CouponImg => ConfigManager.ReadConfig("BasicSetting", "configuration/CouponImg");

        #endregion

        #region 订单设置规则相关
        /// <summary>
        /// 抢购订单付款限时
        /// </summary>
        public static int GroupBuylimitTime => Convert.ToInt32(ConfigManager.ReadConfig("OrderSetting", "configuration/GroupBuylimitTime"));

        /// <summary>
        /// 下单付款限时
        /// </summary>
        public static int OrderLimitTime => Convert.ToInt32(ConfigManager.ReadConfig("OrderSetting", "configuration/OrderLimitTime"));

        /// <summary>
        /// 发货限时
        /// </summary>
        public static int SendTime => Convert.ToInt32(ConfigManager.ReadConfig("OrderSetting", "configuration/SendTime"));

        /// <summary>
        ///  自动五星好评时间
        /// </summary>
        public static int AutoGoodComments => Convert.ToInt32(ConfigManager.ReadConfig("OrderSetting", "configuration/AutoGoodComments"));

        /// <summary>
        /// 是否自动处理退款，0否，1是
        /// </summary>
        public static string Autorefund => ConfigManager.ReadConfig("OrderSetting", "configuration/Autorefund");

        /// <summary>
        /// 订单短信通知，0否，1是
        /// </summary>
        public static string AutoSendTip => ConfigManager.ReadConfig("OrderSetting", "configuration/AutoSendTip");

        /// <summary>
        /// 订单自动结束交易时间
        /// </summary>
        public static int CompleteOrder => Convert.ToInt32(ConfigManager.ReadConfig("OrderSetting", "configuration/CompleteOrder"));

        /// <summary>
        /// 100积分换算金额
        /// </summary>
        public static int PayCreditPrice => Conv.ToInt(ConfigManager.ReadConfig("OrderSetting", "configuration/PayCreditPrice"));

        /// <summary>
        /// 每笔订单最大用多少积分
        /// </summary>
        public static int OrderMaxUsePayCredits => Conv.ToInt(ConfigManager.ReadConfig("OrderSetting", "configuration/OrderMaxUsePayCredits"));

        /// <summary>
        /// 查询退款原因列表
        /// </summary>
        public static List<string> CancelReasons => ConfigManager.ReadConfigList("OrderSetting", "configuration/CancelReasons");

        #endregion

        #region 支付设置相关

        /// <summary>  
        /// AppId  
        /// </summary>
        public static string AppId => ConfigManager.ReadConfig("PaymentSetting", "configuration/AppId");

        /// <summary>  
        /// APP支付应用ID  
        /// </summary>
        public static string AppPayAppId => ConfigManager.ReadConfig("PaymentSetting", "configuration/AppPayAppId");
        /// <summary>  
        /// AppPayKey  
        /// </summary>
        public static string AppPayKey => ConfigManager.ReadConfig("PaymentSetting", "configuration/AppPayKey");
        /// <summary>  
        /// AppPayCertificateMicro  
        /// </summary>
        public static string AppPayCertificateMicro => ConfigManager.ReadConfig("PaymentSetting", "configuration/AppPayCertificateMicro");

        /// <summary>  
        /// AppSecret  
        /// </summary>
        public static string AppSecret => ConfigManager.ReadConfig("PaymentSetting", "configuration/AppSecret");
        /// <summary>  
        /// AppMchId  
        /// </summary>
        public static string AppMchId => ConfigManager.ReadConfig("PaymentSetting", "configuration/AppMchID");

        /// <summary>  
        /// WxOpenAppId  
        /// </summary>
        public static string WxOpenAppId => ConfigManager.ReadConfig("PaymentSetting", "configuration/WxOpenAppId");

        /// <summary>  
        /// WxOpenSecret  
        /// </summary>
        public static string WxOpenSecret => ConfigManager.ReadConfig("PaymentSetting", "configuration/WxOpenSecret");

        /// <summary>  
        /// 微信证书  
        /// </summary>
        public static string CertificateMicroLetter => ConfigManager.ReadConfig("PaymentSetting", "configuration/CertificateMicroLetter");

        /// <summary>  
        /// Key  
        /// </summary>
        public static string Key => ConfigManager.ReadConfig("PaymentSetting", "configuration/Key");

        /// <summary>  
        /// Mch_ID  
        /// </summary>
        public static string MchId => ConfigManager.ReadConfig("PaymentSetting", "configuration/MchID");

        /// <summary>
        /// 微信支付回调地址
        /// </summary>
        public static string WeXinPayNotifyUrl => ConfigManager.ReadConfig("PaymentSetting", "configuration/WeXinPayNotifyUrl");
        #endregion

        #region 阿里云短信设置相关

        /// <summary>  
        /// AccessKeyId  
        /// </summary>
        public static string AccessKeyId => ConfigManager.ReadConfig("SmsSetting", "configuration/AccessKeyId");

        /// <summary>  
        /// AccessKeySecret  
        /// </summary>
        public static string AccessKeySecret => ConfigManager.ReadConfig("SmsSetting", "configuration/AccessKeySecret");

        /// <summary>  
        /// SignName  
        /// </summary>
        public static string SignName => ConfigManager.ReadConfig("SmsSetting", "configuration/SignName");

        /// <summary>  
        /// 验证码模板  
        /// </summary>
        public static string TemplateCode => ConfigManager.ReadConfig("SmsSetting", "configuration/TemplateCode");

        #endregion
    }
}
