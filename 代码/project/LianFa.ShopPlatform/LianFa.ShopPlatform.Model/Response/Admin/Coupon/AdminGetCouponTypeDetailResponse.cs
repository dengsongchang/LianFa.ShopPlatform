using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台获取礼品卡类型详情响应类
    /// </summary>
    public class AdminGetCouponTypeDetailResponse
    {
        /// <summary>
        /// 礼品卡信息
        /// </summary>
        public CouponTypeDetailInfo CouponTypeDetail { get; set; }

        /// <summary>
        /// 礼品卡内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 礼品卡详情
    /// </summary>
    public class CouponTypeDetailInfo
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
        /// 成本价  
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 是否开启特价
        /// </summary>
        public int IsCostPrice { get; set; }
        /// <summary>
        /// 配送范围id
        /// </summary>
        public int DeliveryAreaId { get; set; }
        /// <summary>
        /// 兑换开始时间
        /// </summary>
        public string UseStartTimes { get; set; }
        /// <summary>
        /// 兑换结束时间
        /// </summary>
        public string UseEndTimes { get; set; }
        /// <summary>
        /// 礼品卡图片
        /// </summary>
        public string CouponImg { get; set; }
        /// <summary>
        /// 礼品卡图片
        /// </summary>
        public string CouponImgFull { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImg { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImgFull { get; set; }

        /// <summary>
        /// 运费模板id
        /// </summary>
        public int TemplateId { get; set; }

    }

    /// <summary>
    /// 礼品卡内容信息类
    /// </summary>
    public class CouponContentInfo
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string CouponContent { get; set; }
    }
}
