using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 获取优惠券列表响应类
    /// </summary>
    public class CouponListResponse
    {
        /// <summary>
        /// 用户优惠券列表
        /// </summary>
        public List<CouponPartInfo> List { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 未使用数量
        /// </summary>
        public int UnUseCount { get; set; }

        /// <summary>
        /// 已使用数量
        /// </summary>
        public int HasUseCount { get; set; }

        /// <summary>
        /// 已过期数量
        /// </summary>
        public int HasExpiredCount { get; set; }
    }

    /// <summary>
    /// 优惠券部分类
    /// </summary>
    public class CouponPartInfo
    {
        /// <summary>  
    	/// 优惠劵id  
    	/// </summary>
        public int CouponId { get; set; }
        /// <summary>  
    	/// 优惠劵类型id  
    	/// </summary>
        public int CouponTypeId { get; set; }
        /// <summary>  
    	/// 优惠劵金额  
    	/// </summary>
        public int Money { get; set; }
        /// <summary>  
    	/// 最低订单金额  
    	/// </summary>
        public int OrderAmountLower { get; set; }
        /// <summary>  
    	/// 是否限制商品  
    	/// </summary>
        public byte LimitProduct { get; set; }
        /// <summary>  
    	/// 最低用户等级  
    	/// </summary>
        public short UserRankLower { get; set; }
        /// <summary>  
        /// 激活时间  
        /// </summary>
        public string ActivateTime { get; set; }

        /// <summary>  
        /// 激活时间  
        /// </summary>
        public System.DateTime ActivateTimeToDate { get; set; }
        /// <summary>  
    	/// 使用相对时间  
    	/// </summary>
        public int UseExpireTime { get; set; }
        /// <summary>  
        /// 使用开始时间  
        /// </summary>
        public string UseStartTime { get; set; }
        /// <summary>  
        /// 使用结束时间  
        /// </summary>
        public string UseEndTime { get; set; }

        /// <summary>  
        /// 使用开始时间  
        /// </summary>
        public System.DateTime UseStartTimeToDate { get; set; }
        /// <summary>  
        /// 使用结束时间  
        /// </summary>
        public System.DateTime UseEndTimeToDate { get; set; }
        /// <summary>  
        /// 使用标识  
        /// </summary>
        public int Use { get; set; }

        /// <summary>
        /// 类型(0代表全部，1代表未使用，2代表已使用，3代表已过期)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

    }

    /// <summary>
    /// 优惠券时间格式化类
    /// </summary>
    public class CouponTimeToStringInfo
    {
        /// <summary>  
        /// 优惠劵id  
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>  
        /// 优惠劵类型id  
        /// </summary>
        public int CouponTypeId { get; set; }
        /// <summary>  
        /// 优惠劵金额  
        /// </summary>
        public int Money { get; set; }
        /// <summary>  
        /// 最低订单金额  
        /// </summary>
        public int OrderAmountLower { get; set; }
        /// <summary>  
        /// 是否限制商品  
        /// </summary>
        public byte LimitProduct { get; set; }
        /// <summary>  
        /// 最低用户等级  
        /// </summary>
        public short UserRankLower { get; set; }
        /// <summary>  
        /// 激活时间  
        /// </summary>
        public string ActivateTime { get; set; }
        /// <summary>  
        /// 使用相对时间  
        /// </summary>
        public int UseExpireTime { get; set; }
        /// <summary>  
        /// 使用开始时间  
        /// </summary>
        public string UseStartTime { get; set; }
        /// <summary>  
        /// 使用结束时间  
        /// </summary>
        public string UseEndTime { get; set; }
        /// <summary>  
        /// 使用标识  
        /// </summary>
        public int Use { get; set; }
    }
}
