using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Payment.Wxpay;

namespace LianFa.ShopPlatform.Model.Response.Client.Coupon
{
    /// <summary>
    /// 提交礼品卡订单
    /// </summary>
    public class SubmitCouponOrderResponse
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int OId { get; set; }
    }
}
