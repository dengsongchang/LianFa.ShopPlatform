using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡类型列表请求类
    /// </summary>
    public class CouponTypeListRequest
    {
        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 配送范围id
        /// </summary>
        public int DeliveryAreaId { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }

    /// <summary>
    /// 后台导入礼品卡Excel数据请求类
    /// </summary>
    public class AdminUploadCouponListRequest
    {
        /// <summary>
        /// excel路径
        /// </summary>
        public string ExcelUrl { get; set; }
    }
}
