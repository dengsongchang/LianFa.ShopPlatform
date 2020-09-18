using System;
using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.Order
{
    /// <summary>
    /// 获得有效的优惠券列表响应类
    /// </summary>
    public class GetValidCouponListResponse
    {
        /// <summary>
        /// 有效的优惠券列表
        /// </summary>
        public List<ValidCouponInfo> ValidCouponList { get; set; }

    }

    /// <summary>
    /// 有效的优惠券
    /// </summary>
    public class ValidCouponInfo
    {
        /// <summary>
        /// 优惠券Id
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int OrderAmountlower { get; set; }

        /// <summary>
        /// 是否限制商品
        /// </summary>
        public int LimitProduct { get; set; }

        /// <summary>
        /// 使用方式
        /// </summary>
        public int UseMode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime UseStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime UseEndTime { get; set; }


    }
}