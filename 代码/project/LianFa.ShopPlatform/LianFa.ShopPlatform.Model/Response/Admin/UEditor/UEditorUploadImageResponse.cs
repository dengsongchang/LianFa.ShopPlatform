using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Model.Response.Admin.UEditor
{
    /// <summary>
    /// 百度编辑器上传图片响应类
    /// </summary>
    public class UEditorUploadImageResponse
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 图片文件名
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 图片源文件名
        /// </summary>
        [JsonProperty("original")]
        public string Original { get; set; }
    }
}
