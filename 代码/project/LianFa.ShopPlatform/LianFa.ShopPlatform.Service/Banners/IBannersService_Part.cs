using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Model.Response.Admin.Banner;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IBannersService
    {
        /// <summary>
        /// 首页轮播图列表
        /// </summary>
        /// <returns></returns>
        List<BannerListInfo> BannerList();

        /// <summary>
        /// 后台轮播列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<AdminBannerListInfo> AdminBannerList(PageModel page, out int total);

        /// <summary>
        /// 后台轮播信息
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        AdminBannerInfo AdminBannerInfo(int bannerId);

    }
}
