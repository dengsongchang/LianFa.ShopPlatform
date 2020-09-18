using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡详情请求类
    /// </summary>
    public class AdminGetCouponDetailRequest
    {
        /// <summary>
        /// 礼品卡id
        /// </summary>
        public int CouponId { get; set; }
    }
}
