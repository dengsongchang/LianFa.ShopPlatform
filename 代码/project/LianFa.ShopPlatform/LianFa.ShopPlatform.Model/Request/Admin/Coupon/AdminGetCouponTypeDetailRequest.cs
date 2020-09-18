using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡类型详情请求类
    /// </summary>
    public class AdminGetCouponTypeDetailRequest
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }
    }
}
