using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.Coupon
{
    /// <summary>
    /// 礼品卡有效期查询响应类
    /// </summary>
    public class GetCouponValidDateResponse
    {
        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 礼品卡有效期
        /// </summary>
        public DateTime ValidDate { get; set; }

        /// <summary>
        /// 礼品卡有效期
        /// </summary>
        public string ValidDates { get; set; }

        /// <summary>
        /// 礼品卡状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 礼品卡状态
        /// </summary>
        public string StateDec { get; set; }

    }
}
