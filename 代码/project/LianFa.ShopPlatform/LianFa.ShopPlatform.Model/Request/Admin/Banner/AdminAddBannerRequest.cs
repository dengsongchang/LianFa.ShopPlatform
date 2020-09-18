using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Banner
{
    /// <summary>
    /// 后台添加轮播请求类
    /// </summary>
    public class AdminAddBannerRequest
    {
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
