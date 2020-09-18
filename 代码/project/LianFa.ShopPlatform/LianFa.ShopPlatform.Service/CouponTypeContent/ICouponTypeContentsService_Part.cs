using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Service
{
    public partial interface ICouponTypeContentsService
    {
        /// <summary>
        /// 后台获取礼品卡内容
        /// </summary>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        string GetCouponContentList(int couponTypeId);
    }
}
