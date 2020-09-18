using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 提交订单请求类
    /// </summary>
    public class SubmitOrderRequest
    {
        /// <summary>
        /// 是否直接购买
        /// </summary>
        public int IsDirectBuy { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }

        /// <summary>
        /// 支付插件名称
        /// </summary>
        public byte PayType { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 选中的购物车项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }

        /// <summary>
        /// 支付积分
        /// </summary>
        public int PayCreditCount { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// skuId
        /// </summary>
        public int PId { get; set; }
    }
}