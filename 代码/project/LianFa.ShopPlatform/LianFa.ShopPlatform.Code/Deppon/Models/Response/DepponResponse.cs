using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Code.Deppon.Models.Response
{
    /// <summary>
    /// 泛型响应类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class DepponResponse<T>
    {
        /// <summary>
        /// 响应结果
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// 响应编码
        /// </summary>
        [JsonProperty("resultCode")]
        public int ResultCode { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        [JsonProperty("responseParam")]
        public T ResponseParam { get; set; }
    }
}
