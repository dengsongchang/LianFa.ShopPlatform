using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.Coupon
{
    /// <summary>
    /// 移动端获取礼品卡banner响应类
    /// </summary>
    public class GetCouponImgInfoResponse
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string CouponImg { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string CouponFullImg { get; set; }
    }
}
