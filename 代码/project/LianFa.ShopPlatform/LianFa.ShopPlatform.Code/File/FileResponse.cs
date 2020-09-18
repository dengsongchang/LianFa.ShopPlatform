using HuCheng.Util.Core.Helper;

namespace LianFa.ShopPlatform.Code.File
{
    public class FileResponse
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 完整
        /// </summary>
        public string FullUrl { get; set; }
    }

    public static class FullHelper
    {
        public static FileResponse GetFileResponse(this string url)
        {
            return new FileResponse
            {
                FullUrl = FileHelper.GetFileFullUrl(url),
                Url = url
            };
        }
    }
}
