using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Banner
{
    /// <summary>
    /// 后台删除轮播请求类
    /// </summary>
    public class AdminDelBannerRequest
    {
        /// <summary>
        /// 轮播Id列表
        /// </summary>
        public List<int> BannerIdList { get; set; }
    }
}
