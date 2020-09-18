using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡列表响应类
    /// </summary>
    public class CouponListResponse
    {
        /// <summary>
        /// 礼品卡列表
        /// </summary>
        public List<CouponInfo> CouponList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }
    }
    /// <summary>
    /// 礼品卡列表
    /// </summary>
    public class CouponInfo
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
        /// 购买时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public string AddTimes { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime UseEndTime { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public string UseEndTimes { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public string UseTimes { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateDec { get; set; }
    }
}
