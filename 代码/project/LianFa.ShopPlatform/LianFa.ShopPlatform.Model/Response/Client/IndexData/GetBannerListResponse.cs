using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.IndexData
{
    /// <summary>
    /// 获取首页轮播图列表响应类
    /// </summary>
    public class GetBannerListResponse
    {
        /// <summary>
        /// 轮播图列表
        /// </summary>
        public List<BannerListInfo> BannerList { get; set; }
    }

    /// <summary>
    /// 轮播图列表信息类
    /// </summary>
    public class BannerListInfo
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
        public string BannerLink { get; set; }
    }
}
