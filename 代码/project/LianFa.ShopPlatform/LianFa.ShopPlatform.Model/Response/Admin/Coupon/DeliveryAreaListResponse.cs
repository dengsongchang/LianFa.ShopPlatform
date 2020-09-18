using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台获取配送区域列表响应类
    /// </summary>
    public class DeliveryAreaListResponse
    {
        /// <summary>
        /// 配送区域列表
        /// </summary>
        public List<DeliveryAreaInfo> DeliveryAreaList { get; set; }
    }

    /// <summary>
    /// 配送区域信息
    /// </summary>
    public class DeliveryAreaInfo
    {
        /// <summary>
        /// 配送id
        /// </summary>
        public int DeliveryAreaId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
