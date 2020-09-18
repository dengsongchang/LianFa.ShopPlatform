using System;
using System.Collections.Generic;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Code.KuaiDi100;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Admin.Orders
{
    /// <summary>
    /// 后台订单列表响应类
    /// </summary>
    public class AdminOrderListModelResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<Orderlist> OrderList { get; set; }

        /// <summary>
        /// 订单列表总条数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// 订单列表
    /// </summary>
    public class Orderlist
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 完整单号
        /// </summary>
        public string OutOSn { get; set; }

        /// <summary>
        /// 自提门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public string PayTimes { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string Addtimes { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStateDesc { get; set; }

        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 支付友好方式
        /// </summary>
        public string PayfriendName { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 销售店铺编号
        /// </summary>
        public string SellStoreSn { get; set; }

        /// <summary>
        /// 销售店铺名称
        /// </summary>
        public string SellStoreName { get; set; }

        /// <summary>
        /// 是否能发货
        /// </summary>
        public int IsLogistics { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponMoney { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }
    }

    /// <summary>
    /// 导出发货订单列表
    /// </summary>
    public class OrderGoodsExplist
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 物流公司
        /// </summary>
        public string ShipFriendName { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipSn { get; set; }
    }

    /// <summary>
    /// 导出发货订单列表
    /// </summary>
    public class BatchUploadOrderGoods
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string 订单编号 { get; set; }

        /// <summary>
        /// 物流公司
        /// </summary>
        public string 物流公司 { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string 快递单号 { get; set; }
    }
    /// <summary>
    /// 后台卡片订单列表响应类
    /// </summary>
    public class AdminCouponsOrderListModelResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<CouponsOrderlist> OrderList { get; set; }

        /// <summary>
        /// 订单列表总条数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// 卡片订单详情
    /// </summary>
    public class CouponsOrderlist
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        public byte Type { get; set; }

        /// <summary>  
        /// 快递单号  
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

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
        public string OrderStateDec { get; set; }

        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 自提门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStateDesc { get; set; }

        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 支付友好方式
        /// </summary>
        public string PayfriendName { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否能发货
        /// </summary>
        public int IsLogistics { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 卡片金额
        /// </summary>
        public decimal CouponsMoney { get; set; }


    }

    /// <summary>
    /// 导出订单列表
    /// </summary>
    public class OrderExplist
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSn { get; set; }
        ///// <summary>
        ///// 商家订单编号
        ///// </summary>
        //public string OutOSn { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderState { get; set; }

        /// <summary>
        /// 收款日期
        /// </summary>
        public string PaymentTime { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        public string SendDeliveryTime { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        public string CompleteTime { get; set; }

        /// <summary>
        /// 销售平台
        /// </summary>
        public string SalesPlatform { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 货款
        /// </summary>
        public decimal PaymentGoods { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipFee { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 买家帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
    }

    /// <summary>
    /// 导出订单列表 模型
    /// </summary>
    public class OrderExplistModel
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
        /// 商家订单编号
        /// </summary>
        public string OutOSn { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderState { get; set; }

        /// <summary>
        /// 动作列表
        /// </summary>
        public List<OrderActionsModel> OrderActionses { get; set; }

        /// <summary>
        /// 销售平台
        /// </summary>
        public string SalesPlatform { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 货款
        /// </summary>
        public decimal PaymentGoods { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipFee { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 买家帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
    }

    /// <summary>
    /// 订单动作模型
    /// </summary>
    public class OrderActionsModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime ActionTime { get; set; }
        /// <summary>
        /// 动作类型
        /// </summary>
        public byte ActionType { get; set; }
    }

    /// <summary>
    /// 后台订单信息响应类
    /// </summary>
    public class AdminOrderInfoModelResaponse
    {
        /// <summary>
        /// 订单基本信息
        /// </summary>
        public List<TakeOutOrderInfo> TakeOutOrderInfo { get; set; }

        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<AdminOrderProductsList> OrderProductList { get; set; }

        /// <summary>
        /// 订单动作信息列表
        /// </summary>
        public List<OrderActionInfoList> OrderActionInfo { get; set; }

        /// <summary>
        /// 订单物流信息列表
        /// </summary>
        public AdminOrderLogistics OrderLogisticsList { get; set; }

        /// <summary>
        /// 快递信息
        /// </summary>
        public KuaiDiModel Logistics { get; set; }
    }

    /// <summary>
    /// 外卖订单详细信息
    /// </summary>
    public class TakeOutOrderInfo
    {
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 优惠券金额
        /// </summary>
        public int CouponMoney { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string UseName { get; set; }

        /// <summary>
        /// 用户名字
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }

        /// <summary>
        /// 配送地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 支付友好名称
        /// </summary>
        public string PayFriendName { get; set; }

        /// <summary>
        /// 枚举描述
        /// </summary>
        public string OrderStateChinese { get; set; }

        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal ShipFeeAmount { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public int OrderSource { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string OrderSourceDesc { get; set; }
    }

    /// <summary>
    /// 订单区域详细信息
    /// </summary>
    public class RegionsInfo : TakeOutOrderInfo
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RName { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
    }

    /// <summary>
    /// 订单动作列表
    /// </summary>
    public class OrderActionInfoList
    {
        /// <summary>
        /// 处理时间
        /// </summary>
        public string ActionTime { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        public string ActionTypes { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        public int ActionType { get; set; }
    }

    /// <summary>
    /// 后台操作订单响应类
    /// </summary>
    public class AdminOperateOrderModelResponse
    {
        /// <summary>
        /// 订单信息类
        /// </summary>
        public LF_Orders OrderInfo { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public OrderActionType OrderActionType { get; set; }

        /// <summary>
        /// 行动
        /// </summary>
        public string ActionDes { get; set; }
    }
    /// <summary>
    /// 订单物流信息
    /// </summary>
    public class AdminOrderLogisticsResponse
    {
        /// <summary>
        /// 订单物流信息
        /// </summary>
        public AdminOrdersLogisticsInfo OrdersLogisticsInfo { get; set; }
    }

    /// <summary>
    /// 订单物流信息类
    /// </summary>
    public class AdminOrdersLogisticsInfo
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
        public AdminOrderAddress OrderAddressInfo { get; set; }
    }
    /// <summary>
    /// 订单收货地址信息类
    /// </summary>
    public class AdminOrderAddress
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
    }
    /// <summary>
    /// 订单物流信息类
    /// </summary>
    public class AdminOrderLogistics
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
        public List<AdminOrderLogisticsProduct> OrderLogisticsProductList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShipSn { get; set; }
    }

    /// <summary>
    /// 订单物流商品类
    /// </summary>
    public class AdminOrderLogisticsProduct
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
        public List<AdminOrderProductList> OrderProductList { get; set; }
    }

    /// <summary>
    /// 订单商品列表类
    /// </summary>
    public class AdminOrderProductList
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
        /// 自营类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 商品简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 时代编码
        /// </summary>
        public string KMSDSn { get; set; }
    }

    /// <summary>
    /// 订单商品列表
    /// </summary>
    public class AdminOrderProductsList
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 商品图
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductsName { get; set; }

        /// <summary>
        /// 商品简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 商城价
        /// </summary>
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 真实购买数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 小计合计
        /// </summary>
        public decimal ProductsTogether { get; set; }

        /// <summary>
        /// 销售类型
        /// </summary>
        public int ProductsType { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string MName { get; set; }

        /// <summary>
        /// 商品运费
        /// </summary>
        public decimal ShipFee { get; set; }
    }

    /// <summary>
    /// 导出礼品卡订单列表
    /// </summary>
    public class CouponOrderExplist
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
        public string OrderState { get; set; }

        /// <summary>
        /// 收款日期
        /// </summary>
        public string PaymentTime { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        public string SendDeliveryTime { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        public string CompleteTime { get; set; }

        /// <summary>
        /// 销售平台
        /// </summary>
        public string SalesPlatform { get; set; }

        /// <summary>
        /// 优惠卡名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string CouponPwd { get; set; }

        /// <summary>
        /// 买家帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
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
        /// 礼品内容
        /// </summary>
        public string CouponContent { get; set; }
    }

    /// <summary>
    /// 导出礼品卡订单列表 模型
    /// </summary>
    public class CouponOrderExplistModel
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
        public string OrderState { get; set; }

        /// <summary>
        /// 动作列表
        /// </summary>
        public List<OrderActionsModel> OrderActionses { get; set; }

        /// <summary>
        /// 销售平台
        /// </summary>
        public string SalesPlatform { get; set; }

        /// <summary>
        /// 优惠卡名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string CouponSn { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string CouponPwd { get; set; }

        /// <summary>
        /// 买家帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }
    }
}
