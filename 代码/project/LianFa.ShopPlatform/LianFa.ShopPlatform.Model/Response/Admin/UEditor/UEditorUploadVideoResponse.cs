using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Model.Response.Admin.UEditor
{
    /// <summary>
    /// 百度编辑器上传视频响应类
    /// </summary>
    public class UEditorUploadVideoResponse
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 视频文件名
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 视频源文件名
        /// </summary>
        [JsonProperty("original")]
        public string Original { get; set; }
    }
}
