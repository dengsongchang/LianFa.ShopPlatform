using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Cart
{
    /// <summary>
    /// 取消或选中购物车项 请求类
    /// </summary>
    public class CancelOrSelectCartItemRequest
    {
        /// <summary>
        /// 选中的购物车项键列表
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }
    }
}
