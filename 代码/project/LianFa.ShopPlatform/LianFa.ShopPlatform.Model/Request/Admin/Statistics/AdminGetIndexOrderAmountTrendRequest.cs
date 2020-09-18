namespace LianFa.ShopPlatform.Model.Request.Admin.Statistics
{
    /// <summary>
    /// 获取首页订单金额趋势数据 请求类
    /// </summary>
    public class AdminGetIndexOrderAmountTrendRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
