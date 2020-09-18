using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial interface ICouponTypesService
    {
        /// <summary>
        /// 首页礼品卡列表
        /// </summary>
        /// <returns></returns>
        List<CouponListInfo> CouponTypeList(PageModel page, out int total);

        /// <summary>
        /// 后台获取礼品卡类型列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="couponName"></param>
        /// <param name="areaId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CouponTypeInfo> GetCouponTypeList(PageModel page, string couponName, int areaId, out int total);

        /// <summary>
        /// 后台获取礼品卡类型列表
        /// </summary>
        /// <returns></returns>
        List<MiniCouponTypeInfo> GetMiniCouponTypeList();

        /// <summary>
        /// 获取礼品卡类型详情
        /// </summary>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        CouponTypeDetailInfo AdminGetCouponTypeDetail(int couponTypeId);
    }
}
