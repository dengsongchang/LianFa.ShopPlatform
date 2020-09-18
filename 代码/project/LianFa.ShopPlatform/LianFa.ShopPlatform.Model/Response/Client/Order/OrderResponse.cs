using System;
using System.Collections.Generic;
using LianFa.ShopPlatform.Code.KuaiDi100;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Client.Order
{
    /// <summary>
    /// 订单列表响应类
    /// </summary>
    public class OrderListResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<OrderList> OrderList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 订单列表类
    /// </summary>
    public class OrderList
    {
        /// <summary>  
        /// 订单id  
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 记录id
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public byte OrderState { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public string OrderStateDec { get; set; }

        /// <summary>  
        /// 商品合计  
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>  
        /// 订单合计  
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>  
        /// 支付金额  
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// 真实购买数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 商品商城价
        /// </summary>
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 是否评价
        /// </summary>
        public int IsReview { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTimeStr { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public byte PayMode { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public byte Type { get; set; }
    }


    /// <summary>
    /// 卡片订单列表响应类
    /// </summary>
    public class CouponOrderListResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<CouponOrderList> CouponOrderList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 卡片订单列表类
    /// </summary>
    public class CouponOrderList
    {
        /// <summary>  
        /// 订单id  
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public byte Type { get; set; }

        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string OSn { get; set; }

        /// <summary>  
        /// 快递单号  
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        ///卡片类型
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public System.DateTime UseTime { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public string UseTimeStr { get; set; }

        /// <summary>
        /// 卡片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡片图片
        /// </summary>
        public string CouponImg { get; set; }

        /// <summary>
        /// 卡片图片
        /// </summary>
        public string CouponImgFull { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public byte OrderState { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public string OrderStateDec { get; set; }

        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerRemark { get; set; }
    }


    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderInfoResponse
    {
        /// <summary>
        /// 订单详情
        /// </summary>
        public OrderInfo OrderInfo { get; set; }

        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<ProductsList> ProductsList { get; set; }

        /// <summary>
        /// 订单收货地址信息
        /// </summary>
        public OrderAddress OrderAddressInfo { get; set; }
    }


    /// <summary>
    /// 兑换详情
    /// </summary>
    public class CouponsOrderInfoResponse
    {
        /// <summary>
        /// 卡片详情
        /// </summary>
        public CouponInfos CouponInfo { get; set; }

        /// <summary>
        /// 订单详情
        /// </summary>
        public OrderInfo OrderInfo { get; set; }

        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<ProductsList> ProductsList { get; set; }

        /// <summary>
        /// 订单收货地址信息
        /// </summary>
        public OrderAddress OrderAddressInfo { get; set; }

        /// <summary>
        /// 订单物流信息
        /// </summary>
        public OrdersLogisticsInfo OrdersLogisticsInfo { get; set; }
    }

    /// <summary>
    /// 礼品卡详情
    /// </summary>
    public class CouponInfos
    {
        /// <summary>
        /// 礼品劵id
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 礼品券序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 礼品券订单id
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 礼品卡密码/验证码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 礼品卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 礼品卡内容列表
        /// </summary>
        public List<CouponTypeContents> ContentList { get; set; }

        /// <summary>
        /// 礼品卡主图
        /// </summary>
        public string CouponImg { get; set; }
    }

    /// <summary>
    /// 卡片内容详情类
    /// </summary>
    public class CouponTypeContents
    {
        /// <summary>
        /// 礼品卡内容id
        /// </summary>
        public int CouponContentId { get; set; }
        /// <summary>
        /// 礼品卡种类id
        /// </summary>
        public int CouponTypeId { get; set; }
        /// <summary>
        ///礼品卡内容
        /// </summary>
        public string CouponContent { get; set; }
    }

    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderInfo
    {
        /// <summary>  
        /// 订单id  
        /// </summary>
        public int OId { get; set; }

        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string OSn { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public byte OrderState { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public string OrderStateDesc { get; set; }

        /// <summary>  
        /// 支付金额  
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>  
        /// 添加时间  
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipFee { get; set; }

        /// <summary>  
        /// 添加时间  
        /// </summary>
        public string AddTimes { get; set; }

        /// <summary>  
        /// 收货人  
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>  
        /// 手机  
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>  
        /// 收货地址  
        /// </summary>
        public string Address { get; set; }

        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string ShipSystemName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public short RegionId { get; set; }
    }

    /// <summary>
    /// 订单商品列表类
    /// </summary>
    public class ProductsList
    {
        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public byte OrderState { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public string OrderStateDec { get; set; }

        /// <summary>  
        /// 商品合计  
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>  
        /// 订单合计  
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>  
        /// 支付金额  
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// 真实购买数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 商品商城价
        /// </summary>
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sku { get; set; }
    }

    /// <summary>
    /// 订单物流信息
    /// </summary>
    public class OrderLogisticsResponse
    {
        /// <summary>
        /// 订单物流信息
        /// </summary>
        public OrdersLogisticsInfo OrdersLogisticsInfo { get; set; }
    }
    /// <summary>
    /// 订单物流信息类
    /// </summary>
    public class OrdersLogisticsInfo
    {
        /// <summary>
        /// 物流信息
        /// </summary>
        public KuaiDiModel Logistics { get; set; }

        /// <summary>
        /// 物流状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 订单收货地址信息
        /// </summary>
        public OrderAddress OrderAddressInfo { get; set; }
    }
    /// <summary>
    /// 订单收货地址信息类
    /// </summary>
    public class OrderAddress
    {
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 快递名称
        /// </summary>
        public string ShiPsyStemName { get; set; }

        /// <summary>
        /// 是否卡片订单(0普通，1卡片)
        /// </summary>
        public int IsCoupons { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string StoreName { get; set; }
    }

    /// <summary>
    /// 订单物流图片类
    /// </summary>
    public class ImageLogists
    {
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 图片全路径
        /// </summary>
        public string ImageFull { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string StoreName { get; set; }
    }

    /// <summary>
    /// 各状态订单统计响应类
    /// </summary>
    public class SelOrderStateStatisticsResponse
    {
        /// <summary>
        ///  各状态订单统计
        /// </summary>
        public SelOrderStateStatistics SelOrderStateStatistics { get; set; }
    }

    /// <summary>
    /// 各状态订单统计类
    /// </summary>
    public class SelOrderStateStatistics
    {
        /// <summary>
        /// 待付款
        /// </summary>
        public int waitPaying { get; set; }

        /// <summary>
        /// 待发货
        /// </summary>
        public int stayShipments { get; set; }

        /// <summary>
        /// 待收货
        /// </summary>
        public int stayReceive { get; set; }

        /// <summary>
        /// 待评价
        /// </summary>
        public int Evaluate { get; set; }

        /// <summary>
        /// 售后
        /// </summary>
        public int afterSales { get; set; }
    }

    /// <summary>
    /// 订单物流信息响应类
    /// </summary>
    public class OrderLogisticsListResponse
    {
        /// <summary>
        /// 订单物流信息列表
        /// </summary>
        public OrderLogistics OrderLogisticsList { get; set; }
    }

    /// <summary>
    /// 订单物流信息类
    /// </summary>
    public class OrderLogistics
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 订单物流商品列表
        /// </summary>
        public List<OrderLogisticsProduct> OrderLogisticsProductList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShipSn { get; set; }
    }

    /// <summary>
    /// 订单物流商品类
    /// </summary>
    public class OrderLogisticsProduct
    {
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 时代编号
        /// </summary>
        public string ProductSku { get; set; }

        /// <summary>
        /// 商品物流id
        /// </summary>
        public int OLId { get; set; }

        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductList> OrderProductList { get; set; }
    }
    /// <summary>
    /// 订单商品列表类
    /// </summary>
    public class OrderProductList
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProdutcsName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LOId { get; set; }
    }
    /// <summary>
    /// 待成团订单类
    /// </summary>
    public class StayTogetherOrdersList
    {
        /// <summary>  
    	/// 订单id  
    	/// </summary>
        public int oid { get; set; }

        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string osn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 商品展示图片
        /// </summary>
        public string showImg { get; set; }

        /// <summary>
        ///商城价
        /// </summary>
        public decimal fixedPrice { get; set; }

        /// <summary>
        /// 真实购买数量
        /// </summary>
        public int realCount { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal surplusMoney { get; set; }

        /// <summary>
        /// 还差多少拼团人数
        /// </summary>
        public int groupBuyingPersonCount { get; set; }

        /// <summary>
        /// 团购状态
        /// </summary>
        public int gbState { get; set; }

        /// <summary>
        /// 团购id
        /// </summary>
        public int grbuid { get; set; }

        /// <summary>
        /// 团购满足总数量
        /// </summary>
        public int groupBuyingCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OgrId { get; set; }
    }

    /// <summary>
    /// 待成团订单响应类
    /// </summary>
    public class StayTogetherOrdersListResponse
    {
        /// <summary>
        /// 待成团订单列表
        /// </summary>
        public List<StayTogetherOrdersList> StayTogetherOrdersList { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }
    }

    /// <summary>
    /// 团购订单详情类
    /// </summary>
    public class GroupBuyingOrdersInfo
    {
        /// <summary>  
        /// 订单id  
        /// </summary>
        public int oid { get; set; }

        /// <summary>
        /// 团购id
        /// </summary>
        public int grBuid { get; set; }

        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string osn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 商品展示图片
        /// </summary>
        public string showImg { get; set; }

        /// <summary>
        /// 真实购买数量
        /// </summary>
        public int realCount { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal surplusMoney { get; set; }

        /// <summary>
        /// 团购状态
        /// </summary>
        public int gbState { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal shipFee { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string addTime { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime addTimed { get; set; }

        /// <summary>
        /// 一口价
        /// </summary>
        public decimal fixedPrice { get; set; }
        ///<summary>
        /// 还差多少拼团人数
        /// </summary>
        public int groupBuyingPersonCount { get; set; }

        /// <summary>
        /// 满足团购人数
        /// </summary>
        public int groupBuyingCount { get; set; }

        /// <summary>
        /// 成团时间
        /// </summary>
        public string clusteringTime { get; set; }

        /// <summary>
        /// 成团时间
        /// </summary>
        public DateTime clusteringTimed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OgrId { get; set; }
    }

    /// <summary>
    /// 团购订单详情响应类
    /// </summary>
    public class GroupBuyingOrdersInfoResponse
    {
        /// <summary>
        /// 团购订单详情
        /// </summary>
        public GroupBuyingOrdersInfo GroupBuyingOrdersInfo { get; set; }

        /// <summary>  
        /// 收货人  
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>  
        /// 手机  
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>  
    	/// 收货地址  
    	/// </summary>
        public string Address { get; set; }
    }
    /// <summary>
    /// 团购订单列表响应类
    /// </summary>
    public class GroupBuyOrderListResponse
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public List<GroupBuyOrderPartList> GroupBuyOrderList { get; set; }

        /// <summary>
        /// 总信息数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 订单部分信息
    /// </summary>
    public class GroupBuyOrderPartList
    {
        /// <summary>  
        /// 订单ID  
        /// </summary>
        public int OId { get; set; }

        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string OSn { get; set; }

        /// <summary>  
        /// 订单状态  
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>  
        /// 订单状态描述  
        /// </summary>
        public string OrderStateDes { get; set; }

        /// <summary>
        /// 团状态
        /// </summary>
        public int GroupState { get; set; }

        /// <summary>
        /// 团状态
        /// </summary>
        public string GroupStates { get; set; }

        /// <summary>  
        /// 支付金额  
        /// </summary>
        public decimal PayMoney { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 还差多少人
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 团活动id
        /// </summary>
        public int GBAId { get; set; }

        /// <summary>
        /// 拼团id
        /// </summary>
        public int OGRId { get; set; }
    }
}
