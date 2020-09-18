using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Code.Deppon.Models.Request
{
    /// <summary>
    /// 标准订单查询请求类
    /// </summary>
    public class QueryOrderRequest
    {
        /// <summary>
        /// 物流公司ID
        /// </summary>
        [JsonProperty("logisticCompanyID")]
        public string LogisticCompanyId { get; set; }

        /// <summary>
        /// 渠道单号
        /// </summary>
        [JsonProperty("logisticID")]
        public string LogisticId { get; set; }
    }
}
