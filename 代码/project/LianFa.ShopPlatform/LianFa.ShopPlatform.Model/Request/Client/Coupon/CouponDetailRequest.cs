using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 获取礼品卡详情请求类
    /// </summary>
    public class CouponDetailRequest
    {
        /// <summary>  
        /// 礼品卡类型id  
        /// </summary>
        public int CouponTypeId { get; set; }
    }
}
