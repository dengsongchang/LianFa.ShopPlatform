using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Model.Response.Admin.UEditor
{
    /// <summary>
    /// 百度编辑器不支持类型响应类
    /// </summary>
    public class UEditorNotSupportedResponse
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
