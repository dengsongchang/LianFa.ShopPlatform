using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台兑换礼品卡
    /// </summary>
    public class AdminSubmitCouponOrderRequest
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        [Required(ErrorMessage = "序列号不能为空")]
        public string CouponSn { get; set; }
        /// <summary>
        /// 验证码/密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>  
        /// 区域id  
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "不是有效的区域Id")]
        public short RegionId { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        [StringLength(20, ErrorMessage = "收货人不是有效的字符长度", MinimumLength = 1)]
        public string Consignee { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        [StringLength(150, ErrorMessage = "地址不是有效的字符长度", MinimumLength = 1)]
        public string Address { get; set; }

        /// <summary>
        /// 收货手机
        /// </summary>
        [Required(ErrorMessage = "手机号码不能为空")]
        public string ShipMobile { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }
    }
}
