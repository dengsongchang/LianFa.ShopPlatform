using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 购买礼品卡请求类
    /// </summary>
    public class BuyCouponRequest
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 支付插件名称
        /// </summary>
        public byte PayType { get; set; }
    }
}
