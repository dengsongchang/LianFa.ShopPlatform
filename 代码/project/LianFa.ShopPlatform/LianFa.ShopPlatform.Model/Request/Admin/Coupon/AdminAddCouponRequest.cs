using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台添加礼品卡请求类
    /// </summary>
    public class AdminAddCouponRequest
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        [Required(ErrorMessage = "序列号不能为空")]
        public string CouponSn { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        [Required(ErrorMessage = "截止日期不能为空")]
        public DateTime EndTime { get; set; }
    }
}
