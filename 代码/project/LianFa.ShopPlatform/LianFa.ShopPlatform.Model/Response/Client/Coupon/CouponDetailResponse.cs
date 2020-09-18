using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.Coupon
{
    /// <summary>
    /// 移动端获取礼品卡类型详情响应类
    /// </summary>
    public class CouponDetailResponse
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
    }
}
