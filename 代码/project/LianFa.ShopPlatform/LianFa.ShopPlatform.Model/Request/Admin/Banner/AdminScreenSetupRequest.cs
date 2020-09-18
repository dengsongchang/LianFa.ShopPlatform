namespace LianFa.ShopPlatform.Model.Request.Admin.Banner
{
    /// <summary>
    /// 广告位设置请求类
    /// </summary>
    public class AdminScreenSetupRequest
    {
        /// <summary>
        /// 广告位图片
        /// </summary>
        public string ScreenImg { get; set; }

        /// <summary>
        /// 广告位链接
        /// </summary>
        public string ScreenLink { get; set; }

    }
}
