using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 退款订单列表请求类
    /// </summary>
    public class RefundOrderListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
    }
}
