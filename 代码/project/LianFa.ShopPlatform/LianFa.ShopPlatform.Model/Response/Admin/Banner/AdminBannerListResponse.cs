using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Banner
{
    /// <summary>
    /// 后台轮播列表响应类
    /// </summary>
    public class AdminBannerListResponse
    {
        /// <summary>
        /// 后台轮播列表
        /// </summary>
        public List<AdminBannerListInfo> AdminBannerList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 后台轮播列表信息类
    /// </summary>
    public class AdminBannerListInfo
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
        /// 跳转链接
        /// </summary>
        public string Link { get; set; }
    }
}
