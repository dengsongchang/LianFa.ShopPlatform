using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡列表请求类
    /// </summary>
    public class CouponListRequest
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态（0-未兑换1-已兑换2-已过期3-全部）
        /// </summary>
        [Range(0, 3, ErrorMessage = "状态不能小于0和大于3")]
        public int State { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
    /// <summary>
    /// 后台获取管理员兑换礼品卡列表请求类
    /// </summary>
    public class AdminCouponListRequest
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
}
