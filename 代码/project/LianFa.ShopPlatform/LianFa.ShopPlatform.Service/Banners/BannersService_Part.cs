using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Model.Response.Admin.Banner;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial class BannersService
    {
        /// <summary>
        /// 首页轮播图列表
        /// </summary>
        /// <returns></returns>
        public List<BannerListInfo> BannerList()
        {
            var list = _bannersRepository.GetDbSetNoTracking()
                .Select(d => new BannerListInfo
                {
                    BannerId = d.BannerId,
                    ShowImg = d.ShowImg,
                    BannerLink = d.Url
                })
                .ToList();

            if (!list.Any())
                return null;

            list.ForEach(m => m.ShowImg = FileHelper.GetFileFullUrl(m.ShowImg));

            return list;
        }

        /// <summary>
        /// 后台轮播列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<AdminBannerListInfo> AdminBannerList(PageModel page, out int total)
        {
            var list = _bannersRepository.GetDbSetNoTracking()
                .Select(d => new AdminBannerListInfo
                {
                    BannerId = d.BannerId,
                    ShowImg = d.ShowImg,
                    Link = d.Url
                })
                .OrderByDescending(m => m.BannerId)
                .LoadPage(page, out total)
                .ToList();

            if (!list.Any())
                return null;

            foreach (var info in list)
            {
                info.ShowImg = FileHelper.GetFileFullUrl(info.ShowImg);
            }
            return list;
        }

        /// <summary>
        /// 后台轮播信息
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        public AdminBannerInfo AdminBannerInfo(int bannerId)
        {
            var info = _bannersRepository.GetDbSetNoTracking()
                .Where(m => m.BannerId == bannerId)
                .Select(d => new AdminBannerInfo
                {
                    BannerId = d.BannerId,
                    ShowImg = d.ShowImg,
                    Link = d.Url
                })
                .FirstOrDefault();

            if (info == null)
            {
                return null;
            }

            info.ShowImgFull = FileHelper.GetFileFullUrl(info.ShowImg);

            return info;
        }
    }
}