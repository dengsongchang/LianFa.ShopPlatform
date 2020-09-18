using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台获取管理员兑换礼品卡列表
    /// </summary>
    public class AdminCouponListResponse
    {
        /// <summary>
        /// 礼品卡列表
        /// </summary>
        public List<AdminCouponInfo> CouponList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 礼品卡列表
    /// </summary>
    public class AdminCouponInfo
    {
        /// <summary>
        /// 礼品卡id
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 礼品卡编号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public string AddTimes { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public string UseTimes { get; set; }

        /// <summary>
        /// 兑换管理员
        /// </summary>
        public string AdminUser { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public int OId { get; set; }
    }
}
