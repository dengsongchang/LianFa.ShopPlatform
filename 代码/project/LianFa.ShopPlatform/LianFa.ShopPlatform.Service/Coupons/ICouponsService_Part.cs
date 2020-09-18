using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Service
{
    public partial interface ICouponsService
    {
        /// <summary>
        /// 后台获取礼品卡列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="couponSn"></param>
        /// <param name="state"></param>
        /// <param name="name"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CouponInfo> GetCouponList(PageModel page, string couponSn, int state, string name, out int total);

        /// <summary>
        /// 礼品卡获取今日兑换数
        /// </summary>
        /// <returns></returns>
        int GetTotalCouponsUsedByTime(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 卡片总数
        /// </summary>
        /// <returns></returns>
        int GetTotalCoupons();
        
        /// <summary>
        /// 卡片兑换总数
        /// </summary>
        /// <returns></returns>
        int GetTotalCouponsIsUsed();

        /// <summary>
        /// 后台获取管理员兑换礼品卡列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="couponSn"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<AdminCouponInfo> GetAdminCouponList(PageModel page, string couponSn, out int total);
    }
}
