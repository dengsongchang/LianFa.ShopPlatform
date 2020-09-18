using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Service
{
    public partial interface ICouponDeliveryAreasService
    {
        /// <summary>
        /// 后台获取配送区域列表
        /// </summary>
        /// <returns></returns>
        List<DeliveryAreaInfo> GetDeliveryAreaList();

        /// <summary>
        /// 获取已有的区域编号
        /// </summary>
        /// <returns></returns>
        List<string> GetDeliveryAreaSnList();
    }
}
