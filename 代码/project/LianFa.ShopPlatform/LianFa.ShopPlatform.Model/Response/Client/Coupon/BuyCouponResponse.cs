using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Payment.Wxpay;

namespace LianFa.ShopPlatform.Model.Response.Client.Coupon
{
    /// <summary>
    /// 购买礼品卡响应类
    /// </summary>
    public class BuyCouponResponse
    {
        /// <summary>
        /// 微信支付参数
        /// </summary>
        public MWebParameter WechatParameter { get; set; }

        /// <summary>
        /// 支付宝支付参数
        /// </summary>
        public string ALiParameter { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
