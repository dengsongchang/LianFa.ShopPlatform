using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 提交礼品卡订单请求类
    /// </summary>
    public class SubmitCouponOrderRequest
    {
        /// <summary>
        /// 礼品卡Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "礼品卡ID大于1")]
        public int CouponId { get; set; }

        /// <summary>
        /// 配送地址id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "配送地址ID大于1")]
        public int SaId { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }

    }
}
