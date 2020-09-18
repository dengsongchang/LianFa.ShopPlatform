using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Banner
{
    /// <summary>
    /// 后台轮播信息响应类
    /// </summary>
    public class AdminBannerInfoResponse
    {
        /// <summary>
        /// 后台轮播信息
        /// </summary>
        public AdminBannerInfo AdminBannerInfo { get; set; }
    }

    /// <summary>
    /// 后台轮播信息类
    /// </summary>
    public class AdminBannerInfo
    {
        /// <summary>
        /// 轮播图Id
        /// </summary>
        public int BannerId { get; set; }

        /// <summary>
        /// 轮播图
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 轮播图
        /// </summary>
        public string ShowImgFull { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Link { get; set; }
    }
}
