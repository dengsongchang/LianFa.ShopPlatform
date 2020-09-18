namespace LianFa.ShopPlatform.Code.Deppon.Models.Response
{
    /// <summary>
    /// 新标准轨迹查询响应类
    /// </summary>
    public class NewTraceQueryResponse
    {
        /// <summary>
        /// 轨迹列表
        /// </summary>
        public TraceInfo[] trace_list { get; set; }

        /// <summary>
        /// 德邦运单号
        /// </summary>
        public string tracking_number { get; set; }
    }

    /// <summary>
    /// 轨迹信息
    /// </summary>
    public class TraceInfo
    {
        public string city { get; set; }
        public string description { get; set; }
        public string site { get; set; }
        public string status { get; set; }
        public string time { get; set; }
    }

}
