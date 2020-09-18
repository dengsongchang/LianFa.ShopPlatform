using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.OrderSetting
{
    /// <summary>
    /// 后台订单设置列表响应类
    /// </summary>
    public class AdminOrderSettingListResponse
    {
        /// <summary>
        /// 订单设置列表
        /// </summary>
        public List<OrderSettingInfo> OrderSettingList { get; set; }
    }

    /// <summary>
    /// 订单设置类
    /// </summary>
    public class OrderSettingInfo
    {
        /// <summary>
        /// 抢购订单付款限时
        /// </summary>
        public int GroupBuylimitTime { get; set; }
        /// <summary>
        /// 下单付款限时
        /// </summary>
        public int OrderLimitTime { get; set; }
        /// <summary>
        /// 发货限时
        /// </summary>
        public int SendTime { get; set; }
        /// <summary>
        /// 订单自动结束交易时间
        /// </summary>
        public int CompleteOrder { get; set; }
        /// <summary>
        ///  自动五星好评时间
        /// </summary>
        public int AutoGoodComments { get; set; }
        /// <summary>
        /// 是否自动处理退款，0否，1是
        /// </summary>
        public string Autorefund { get; set; }
        /// <summary>
        /// 订单短信通知，0否，1是
        /// </summary>
        public string AutoSendTip { get; set; }
    }
}
