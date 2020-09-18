using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Coupon
{
    /// <summary>
    /// 验证配送地址请求类
    /// </summary>
    public class VerifyShipAddressRequest
    {
        /// <summary>
        /// 配送地址Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "配送地址Id大于1")]
        public int SaId { get; set; }

        /// <summary>
        /// 礼品卡类型Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "礼品卡类型Id大于1")]
        public int CouponTypeId { get; set; }
    }
}
