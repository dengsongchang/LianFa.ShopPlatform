using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Service
{
    public partial class CouponTypeContentsService
    {
        /// <summary>
        /// 后台获取礼品卡内容
        /// </summary>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        public string GetCouponContentList(int couponTypeId)
        {
            var data = _couponTypeContentsRepository.GetDbSetNoTracking()
                .Where(c => c.CouponTypeId == couponTypeId)
                .Select(x => x.CouponContent)
                .ToList();
            var content = "";
            if (data.Any() && data.Count > 0)
            {
                content = data.Aggregate(content,
                    (current, couponContent) => current + (couponContent + ","));
                content = content.TrimEnd(',');
            }
            return content;
        }
    }
}
