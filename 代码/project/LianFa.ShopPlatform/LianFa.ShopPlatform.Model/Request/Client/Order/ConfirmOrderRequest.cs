using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 确认订单请求类
    /// </summary>
    public class ConfirmOrderRequest
    {
        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> SelectedCartItemKeyList { get; set; }
    }
}