using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台批量删除礼品卡请求类
    /// </summary>
    public class AdminDelCouponRequest
    {
        /// <summary>  
        /// 优惠券id组 
        /// </summary>
        public int[] CouponId { get; set; }
    }

    /// <summary>
    /// 后台批量删除礼品卡类型请求类
    /// </summary>
    public class AdminDelCouponTypeRequest
    {
        /// <summary>  
        /// 优惠券类型id组 
        /// </summary>
        public int[] CouponTypeId { get; set; }
    }
    
    /// <summary>
    /// 后台批量延期礼品卡请求类
    /// </summary>
    public class AdminEditCouponTimeRequest
    {
        /// <summary>  
        /// 优惠券id组 
        /// </summary>
        public int[] CouponId { get; set; }

        /// <summary>  
        /// 使用截止时间 
        /// </summary>
        public DateTime UseEndTime { get; set; }
    }
}
