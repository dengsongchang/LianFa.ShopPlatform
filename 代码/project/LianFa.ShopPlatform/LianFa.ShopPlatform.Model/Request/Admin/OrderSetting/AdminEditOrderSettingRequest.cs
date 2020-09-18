using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.OrderSetting
{
    /// <summary>
    /// 后台编辑订单设置请求类
    /// </summary>
    public class AdminEditOrderSettingRequest
    {
        /// <summary>
        /// 抢购订单付款限时
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "抢购订单付款限时参数不正确")]
        public string GroupBuylimitTime { get; set; }
        /// <summary>
        /// 下单付款限时
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "下单付款限时参数不正确")]
        public string OrderLimitTime { get; set; }
        /// <summary>
        /// 发货限时
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "发货限时参数不正确")]
        public string SendTime { get; set; }
        /// <summary>
        /// 订单自动结束交易时间
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "订单自动结束交易时间参数不正确")]
        public string CompleteOrder { get; set; }
        /// <summary>
        ///  自动五星好评时间
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "自动五星好评时间参数不正确")]
        public string AutoGoodComments { get; set; }
        /// <summary>
        /// 是否自动处理退款，0否，1是
        /// </summary>
        [RegularExpression(@"^[0-1]*$", ErrorMessage = "是否处理退款参数不正确")]
        public string Autorefund { get; set; }
        /// <summary>
        /// 订单短信通知，0否，1是
        /// </summary>
        [RegularExpression(@"^[0-1]*$", ErrorMessage = "订单短信通知参数不正确")]
        public string AutoSendTip { get; set; }
    }
}
