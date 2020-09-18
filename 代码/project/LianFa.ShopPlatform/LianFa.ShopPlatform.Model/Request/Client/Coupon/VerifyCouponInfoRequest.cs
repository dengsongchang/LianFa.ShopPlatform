using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 移动端校验礼品卡请求类
    /// </summary>
    public class VerifyCouponInfoRequest
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        public string CouponSn { get; set; }
        /// <summary>
        /// 验证码/密码
        /// </summary>
        public string Password { get; set; }
    }
}
