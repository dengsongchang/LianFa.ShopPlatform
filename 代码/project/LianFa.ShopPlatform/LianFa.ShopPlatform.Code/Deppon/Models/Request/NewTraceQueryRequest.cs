using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Code.Deppon.Models.Request
{
    /// <summary>
    /// 新标准轨迹查询请求类
    /// </summary>
    public class NewTraceQueryRequest
    {
        /// <summary>
        /// 运单号
        /// </summary>
        [JsonProperty("mailNo")]
        public string MailNo { get; set; }
    }
}
