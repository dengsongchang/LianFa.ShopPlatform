using System.Collections.Generic;
using LianFa.ShopPlatform.Model.Response.Client.Cart;

namespace LianFa.ShopPlatform.Model.Response.Client.Order
{
    /// <summary>
    /// 确认订单响应类
    /// </summary>
    public class ConfirmOrderResponse
    {
        /// <summary>
        /// 选中的购物车项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }

        /// <summary>
        /// 默认完整用户配送地址
        /// </summary>
        public FullShipAddressInfo DefaultFullShipAddressInfo { get; set; }

        /// <summary>
        /// 用户支付积分
        /// </summary>
        public decimal UserPayCredits { get; set; }
        /// <summary>
        /// 最大使用支付积分
        /// </summary>
        public decimal MaxUsePayCredits { get; set; }

        /// <summary>
        /// 全部配送费用
        /// </summary>
        public decimal ShipFee { get; set; }

        /// <summary>
        /// 全部商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// 全部订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 全部商品总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 购物车信息
        /// </summary>
        public CartInfo CartInfo { get; set; }

        /// <summary>
        /// 满减金额
        /// </summary>
        public int FullCut { get; set; }

        /// <summary>
        /// 卡片Id
        /// </summary>
        public int CardId { get; set; }
    }
    /// <summary>
    /// 取消订单
    /// </summary>
    public class CancelOrderResponse
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public int orderstate { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int Oid { get; set; }
        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string orderstateDesc { get; set; }
    }
}