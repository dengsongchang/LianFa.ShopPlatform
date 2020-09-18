using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Model.Response.Client.Order;

namespace LianFa.ShopPlatform.Model.Response.Admin.Orders
{
    /// <summary>
    /// 卡片订单详情
    /// </summary>
   public class AdminCouponsOrderInfoModelResponse
    {
        /// <summary>
        /// 卡片详情
        /// </summary>
        public AdminCouponsInfo CouponInfo { get; set; }

        /// <summary>
        /// 订单详情
        /// </summary>
        public OrderInfo OrderInfo { get; set; }

        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<ProductsList> ProductsList { get; set; }

        /// <summary>
        /// 订单收货地址信息
        /// </summary>
        public OrderAddress OrderAddressInfo { get; set; }

        /// <summary>
        /// 订单物流信息
        /// </summary>
        public OrdersLogisticsInfo OrdersLogisticsInfo { get; set; }
    }

    /// <summary>
    /// 礼品卡详情
    /// </summary>
    public class AdminCouponsInfo
    {
        /// <summary>
        /// 礼品卡编号
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 礼品卡类型
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 对应订单号
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 礼品卡密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商家商品列表
        /// </summary>
        public List<CouponTypeContents> ContentList { get; set; }

        /// <summary>
        /// 礼品卡主图
        /// </summary>
        public string CouponImg { get; set; }

        /// <summary>
        /// 卡片价格
        /// </summary>
        public decimal Money { get; set; }
    }
}
