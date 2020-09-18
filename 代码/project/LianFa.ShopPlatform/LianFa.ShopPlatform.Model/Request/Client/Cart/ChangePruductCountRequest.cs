using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Cart
{
    /// <summary>
    /// 修改购物车商品数量
    /// </summary>
    public class ChangePruductCountRequest
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
        /// 选中的购物车项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }
    }
}