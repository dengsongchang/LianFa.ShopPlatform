using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.IndexData
{
    /// <summary>
    /// 首页获取礼品卡列表响应类
    /// </summary>
    public class GetCouponListResponse
    {
        /// <summary>
        /// 礼品卡列表
        /// </summary>
        public List<CouponListInfo> CouponList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 礼品卡列表信息类
    /// </summary>
    public class CouponListInfo
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
        /// 礼品卡图片
        /// </summary>
        public string CouponImg { get; set; }
    }
}