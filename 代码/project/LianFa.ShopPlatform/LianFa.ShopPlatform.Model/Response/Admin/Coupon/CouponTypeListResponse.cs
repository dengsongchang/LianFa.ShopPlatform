using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡类型列表响应类
    /// </summary>
    public class CouponTypeListResponse
    {
        /// <summary>
        /// 礼品卡类型列表
        /// </summary>
        public List<CouponTypeInfo> CouponTypeList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }

    }

    /// <summary>
    /// 礼品卡类型信息
    /// </summary>
    public class CouponTypeInfo
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 礼品卡类型编号
        /// </summary>
        public string CouponTypeSn { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 配送范围
        /// </summary>
        public string DeliveryArea { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StateDec { get; set; }
    }
}
