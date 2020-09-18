using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 获取礼品卡类型下拉列表响应类
    /// </summary>
    public class MiniCouponTypeListResponse
    {
        /// <summary>
        /// 礼品卡类型列表
        /// </summary>
        public List<MiniCouponTypeInfo> CouponTypeList { get; set; }
    }
    /// <summary>
    /// 礼品卡类型信息
    /// </summary>
    public class MiniCouponTypeInfo
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }
    }
}
