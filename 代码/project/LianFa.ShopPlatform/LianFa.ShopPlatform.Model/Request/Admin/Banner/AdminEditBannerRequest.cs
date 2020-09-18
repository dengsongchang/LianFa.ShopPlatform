using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Banner
{
    /// <summary>
    /// 后台编辑轮播请求类
    /// </summary>
    public class AdminEditBannerRequest
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
        /// 轮播图链接
        /// </summary>
        public string Link { get; set; }
    }
}
