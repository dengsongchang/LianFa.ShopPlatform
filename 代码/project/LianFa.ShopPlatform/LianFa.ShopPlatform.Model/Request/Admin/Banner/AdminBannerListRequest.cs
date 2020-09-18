using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Banner
{
    /// <summary>
    /// 后台轮播列表请求类
    /// </summary>
    public class AdminBannerListRequest
    {
        /// <summary>
        /// 分页
        /// </summary>
        public PageModel Page { get; set; }
    }
}
