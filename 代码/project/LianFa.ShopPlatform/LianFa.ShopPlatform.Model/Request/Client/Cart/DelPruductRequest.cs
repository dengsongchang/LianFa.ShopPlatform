using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Cart
{
    /// <summary>
    /// 删除购物车商品
    /// </summary>
    public class DelPruductRequest
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public List<int> PIds { get; set; }


        /// <summary>
        /// 选中的购物车项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }
    }
}