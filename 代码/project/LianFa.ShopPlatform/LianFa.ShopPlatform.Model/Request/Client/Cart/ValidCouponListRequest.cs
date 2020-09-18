using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Cart
{
    /// <summary>
    /// 获取可使用优惠券
    /// </summary>
    public class ValidCouponListRequest
    {
        /// <summary>
        /// 是否直接购买
        /// </summary>
        public int IsDirectBuy { get; set; }

        /// <summary>
        /// skuId
        /// </summary>
        public int PSkuRId { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 选中项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }
    }
}
