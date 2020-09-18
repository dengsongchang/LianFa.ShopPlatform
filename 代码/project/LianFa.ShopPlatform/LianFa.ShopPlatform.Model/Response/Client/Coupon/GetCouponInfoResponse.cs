using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Response.Client.Order;

namespace LianFa.ShopPlatform.Model.Response.Client.Coupon
{
    /// <summary>
    /// 移动端兑换礼品卡需要类
    /// </summary>
    public class GetCouponInfoResponse
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 礼品卡id
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 礼品卡图片
        /// </summary>
        public string CouponImg { get; set; }

        /// <summary>
        /// 礼品卡内容
        /// </summary>
        public List<string> Content { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImg { get; set; }

        /// <summary>
        /// 礼品卡价格
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>  
        /// 成本价  
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 是否开启特价
        /// </summary>
        public int IsCostPrice { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public FullShipAddressInfo Address { get; set; }

        /// <summary>
        /// 是否在配送范围内
        /// </summary>
        public bool IsDeliveryArea { get; set; }
    }
}
