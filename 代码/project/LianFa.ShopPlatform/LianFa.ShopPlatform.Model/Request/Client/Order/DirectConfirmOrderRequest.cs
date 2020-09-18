using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 直接确认订单请求类
    /// </summary>
    public class DirectConfirmOrderRequest
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// 选中的购物车项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }
    }
}