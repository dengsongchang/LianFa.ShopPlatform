using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台编辑礼品卡类型请求类
    /// </summary>
    public class AdminEditCouponTypeRequest
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "礼品卡类型id须大于0")]
        public int CouponTypeId { get; set; }
        /// <summary>
        /// 礼品卡名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
        /// <summary>  
        /// 状态  （0-下架，1-上架）
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择有效的状态")]
        public byte State { get; set; }
        /// <summary>
        /// 礼品卡类型编号
        /// </summary>
        [StringLength(3, ErrorMessage = "类型编号长度须在1-3之间", MinimumLength = 1)]
        public string CouponTypeSn { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Range(0.01, int.MaxValue, ErrorMessage = "价格最低为0.01元")]
        public decimal Money { get; set; }
        /// <summary>
        /// 配送范围id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择配送范围")]
        public int DeliveryAreaId { get; set; }
        /// <summary>
        /// 礼品卡图片
        /// </summary>
        public string CouponImg { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImg { get; set; }
        /// <summary>
        /// 礼品卡内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否开启特价
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择正确的状态")]
        public int IsCostPrice { get; set; }

        /// <summary>
        /// 特价
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "价格最低为0元")]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 运费模板id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择运费模板")]
        public int TemplateId { get; set; }
    }
}
