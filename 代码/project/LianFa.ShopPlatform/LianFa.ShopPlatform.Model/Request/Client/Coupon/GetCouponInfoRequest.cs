using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 移动端兑换礼品卡请求类
    /// </summary>
    public class GetCouponInfoRequest
    {
        /// <summary>
        /// 礼品卡id
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }
    }
}
