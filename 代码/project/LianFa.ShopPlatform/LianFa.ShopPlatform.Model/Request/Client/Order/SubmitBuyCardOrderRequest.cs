using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 提交购买礼品卡订单
    /// </summary>
    public class SubmitBuyCardOrderRequest
    {
        /// <summary>
        /// 优惠卷
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
    }
}
