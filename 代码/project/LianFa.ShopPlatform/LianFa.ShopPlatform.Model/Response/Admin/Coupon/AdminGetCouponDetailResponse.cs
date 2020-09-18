using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡详情响应类
    /// </summary>
    public class AdminGetCouponDetailResponse
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
        /// 礼品卡密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

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
        /// 礼品卡状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 礼品卡状态
        /// </summary>
        public string StateDec { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int? OrderState { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStateDec { get; set; }

    }
}
