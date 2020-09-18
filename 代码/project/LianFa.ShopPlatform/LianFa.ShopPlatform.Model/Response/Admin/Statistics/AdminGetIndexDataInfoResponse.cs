namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 获取首页数据详情 响应类
    /// </summary>
    public class AdminGetIndexDataInfoResponse
    {
        /// <summary>
        /// 今日订单金额
        /// </summary>
        public decimal TodayOrderAmount { get; set; }

        /// <summary>
        /// 今日订单总数
        /// </summary>
        public int TodayOrderCount { get; set; }

        /// <summary>
        /// 今日新增会员
        /// </summary>
        public int TodayUsers { get; set; }

        /// <summary>
        /// 会员总数
        /// </summary>
        public int TotalUsers { get; set; }

        /// <summary>
        /// 商品总数
        /// </summary>
        public int TotalProducts { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal TotalOrderAmount { get; set; }

        /// <summary>
        /// 今天较昨天新增金额
        /// </summary>
        public decimal AddOrderAmount { get; set; }

        /// <summary>
        /// 今天较昨天新增订单数
        /// </summary>
        public int AddOrderCount { get; set; }

        /// <summary>
        /// 今天较昨天新增会员
        /// </summary>
        public int AddUsers { get; set; }

        /// <summary>
        /// 礼品卡获取今日兑换数
        /// </summary>
        public int AddCouponsUsed { get; set; }

        /// <summary>
        /// 礼品卡总个数
        /// </summary>
        public int TotalCoupons { get; set; }

        /// <summary>
        /// 礼品卡已经使用个数
        /// </summary>
        public int TotalCouponsIsUsed { get; set; }
    }
}
