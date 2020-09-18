using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Service
{
    public partial class CouponDeliveryAreasService
    {
        /// <summary>
        /// 后台获取配送区域列表
        /// </summary>
        /// <returns></returns>
        public List<DeliveryAreaInfo> GetDeliveryAreaList()
        {
            return _couponDeliveryAreasRepository.GetDbSetNoTracking()
                .Where(c => c.DeliveryAreaId > 0)
                .Select(x => new DeliveryAreaInfo
                {
                    DeliveryAreaId = x.DeliveryAreaId,
                    Name = x.Name
                })
                .ToList();
        }

        /// <summary>
        /// 获取已有的区域编号
        /// </summary>
        /// <returns></returns>
        public List<string> GetDeliveryAreaSnList()
        {
            return _couponDeliveryAreasRepository.GetDbSetNoTracking()
                .Select(x => x.DeliveryAreaSn.Trim())
                .ToList();
        }
    }
}
