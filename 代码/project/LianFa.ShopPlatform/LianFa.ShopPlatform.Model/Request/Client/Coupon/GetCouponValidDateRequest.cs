using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 礼品卡有效期查询请求类
    /// </summary>
    public class GetCouponValidDateRequest
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        public string CouponSn { get; set; }
    }
}
