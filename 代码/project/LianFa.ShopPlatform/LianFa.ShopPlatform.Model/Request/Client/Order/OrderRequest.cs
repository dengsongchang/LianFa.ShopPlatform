using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Client.Order
{
    /// <summary>
    /// 订单列表请求类
    /// </summary>
    public class OrderListRequest
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public int orderState { get; set; }

        /// <summary>
        /// 类型0-普通订单-卡片订单
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }

    /// <summary>
    /// 订单列表请求类
    /// </summary>
    public class CouponOrderListRequest
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
    
    /// <summary>
    /// 订单详情请求类
    /// </summary>
    public class OrderInfoRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int oid { get; set; }
    }

    /// <summary>
    /// 兑换详情请求类
    /// </summary>
    public class CouponsOrderInfoRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int oid { get; set; }

    }

    
    /// <summary>
    /// 订单物流请类
    /// </summary>
    public class OrderLogisticsRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int Oid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OLId { get; set; }
    }

    /// <summary>
    /// 取消订单请求类
    /// </summary>
    public class CancelOrderRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int oid { get; set; }

    }

    /// <summary>
    /// 确认收货请求类
    /// </summary>
    public class ReceiveOrderRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "订单ID大小不能小于1")]
        public int oid { get; set; }
    }

    /// <summary>
    /// 各订单状态统计请求类
    /// </summary>
    public class SelOrderStateStatisticsRequest
    {

    }

    /// <summary>
    /// 待成团订单列表请求类
    /// </summary>
    public class StayTogetherOrdersRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int uid { get; set; }
    }

    /// <summary>
    /// 团购订单详情请求类
    /// </summary>
    public class GroupBuyingOrdersInfoRequest
    {
        /// <summary>
        /// 团购记录id
        /// </summary>
        public int OgrId { get; set; }
    }
    /// <summary>
    /// 订单物流列表请求类
    /// </summary>
    public class OrderLogisticsListRequest
    {
        /// <summary>
        /// 订单id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "订单id不能小于1")]
        public int OId { get; set; }
    }
    /// <summary>
    /// 团购订单列表请求类
    /// </summary>
    public class GroupBuysOrderListRequest
    {
        /// <summary>
        /// 团状态，0全部，-1未开团，10待成团，20拼团失败，30拼团成功
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "类型错误")]
        public int OrderState { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        [Required]
        public PageModel Page { get; set; }
    }
}
