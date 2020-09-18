using System;
using System.Collections.Generic;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Orders;
using LianFa.ShopPlatform.Model.Response.Admin.AfterSalesService;
using LianFa.ShopPlatform.Model.Response.Admin.Orders;
using LianFa.ShopPlatform.Model.Response.Admin.Statistics;
using LianFa.ShopPlatform.Model.Response.Client.Order;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IOrdersService
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="uId">用户信息</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="fullShipAddressInfo">配送地址</param>
        /// <param name="payCreditCount">支付积分数</param>
        /// <param name="buyerRemark">买家备注</param>
        /// <param name="ip">ip地址</param>
        /// <param name="payModel"></param>
        /// <param name="type">0-普通订单 1-礼品卡订单</param>
        LF_Orders CreateOrder(int uId, List<LF_OrderProducts> orderProductList,
            FullShipAddressInfo fullShipAddressInfo, int payCreditCount, string buyerRemark, string ip, byte payModel, byte type,decimal productAmount);


        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="page">分页模型</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="uid"></param>
        /// <param name="total">总数</param>
        ///  <returns></returns>
        List<OrderList> GetOrderList(PageModel page, byte type, int orderState, int uid, out int total);

        /// <summary>
        /// 获取卡片订单列表
        /// </summary>
        /// <param name="page">分页模型</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="uid"></param>
        /// <param name="total">总数</param>
        ///  <returns></returns>
        List<CouponOrderList> GetCouponOrderList(PageModel page, int orderState, int uid, out int total);

        /// <summary>
        /// 获取卡片详情
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        CouponInfos GetCouponsInfo(int oId);

        /// <summary>
        /// 后台获取卡片详情
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        AdminCouponsInfo GetAdminCouponsInfo(int oId);

        /// <summary>
        /// 获取小程序订单详情信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        OrderInfo GetOrderInfo(int oid);

        /// <summary>
        ///  获取订单收货地址信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        OrderAddress GetOrderAddressInfo(int oid);

        /// <summary>
        /// 获取订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<ProductsList> GetProductsList(int oid);

        /// <summary>
        /// 获取订单物流信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="uid">用户id</param>
        /// <param name="olid">商品订单物流id</param>
        /// <returns></returns>
        OrdersLogisticsInfo GetOrdersLogisticsInfo(int oid, int uid, int olid);

        /// <summary>
        /// 获取兑换订单物流信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="uid">用户id</param>
        /// <param name="olid">商品订单物流id</param>
        /// <returns></returns>
        OrdersLogisticsInfo GetOrderLogisticsInfo(int oid);

        /// <summary>
        /// 创建卡片订单
        /// </summary>
        /// <param name="uId">用户信息</param>
        /// <param name="orderProduct">订单商品列表</param>
        /// <param name="fullShipAddressInfo">配送地址</param>
        /// <param name="buyerRemark">买家备注</param>
        /// <param name="ip">ip地址</param>
        /// <param name="payModel"></param>
        LF_Orders CreateCouponOrder(int uId, List<LF_OrderProducts> orderProductList,
            FullShipAddressInfo fullShipAddressInfo, int payCreditCount, string buyerRemark, string ip, byte payModel, byte type);

        /// <summary>
        /// 校验卡号密码
        /// </summary>
        /// <returns></returns>
        LF_Coupons CheckCard(int pId, string code, string password);

        /// <summary>
        /// 取消订单
        /// </summary>
        void CancelOrder(int oId, int uId);

        /// <summary>
        /// 创建订单行为
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderActionType">订单处理类型</param>
        /// <param name="actionDes">行为</param>
        void CreateOrderAction(int oid, int uid, OrderActionType orderActionType, string actionDes);

        /// <summary>
        /// 收货
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="receiveTime">收货时间</param>
        /// <param name="ip">ip</param>
        void ReceiveOrder(LF_Orders orderInfo, DateTime receiveTime, string ip);

        /// <summary>
        /// 获取各订单状态统计
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        SelOrderStateStatistics GetSelOrderStateStatistics(int uid);

        /// <summary>
        /// 获取订单物流列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        OrderLogistics GetOrderLogistics(int oid, int uid);

        /// <summary>
        /// 后台获取所有订单
        /// </summary>
        /// <returns></returns>
        List<Orderlist> GetOrdersList(AdminOrdertListRequest request, out int total);


        /// <summary>
        /// 获取待发货订单导出列表
        /// </summary>
        /// <returns></returns>
        List<OrderGoodsExplist> GetOrderGoodsExplist();

        /// <summary>
        /// 获取待发货订单导出列表
        /// </summary>
        /// <returns></returns>
        List<OrderGoodsExplist> GetOrderCardExplist();
        /// <summary>
        /// 后台获取卡片订单
        /// </summary>
        /// <returns></returns>
        List<CouponsOrderlist> GetAdminCouponsOrderList(AdminCouponsOrdertListReques request, out int total);

        /// <summary>
        /// 获取导出优惠券订单列表
        /// </summary>
        /// <param name="exdata">请求参数</param>
        /// <returns></returns>
        List<CouponOrderExplist> GetAdminExportCouponOrderList(AdminCouponsOrdertListReques exdata);

        /// <summary>
        /// 获取导出订单列表
        /// </summary>
        /// <param name="exdata">请求参数</param>
        /// <returns></returns>
        List<OrderExplist> GetAdminExportOrderList(AdminExportAdminOrdersListRequest exdata);

        /// <summary>
        /// 根据订单id获取订单动作数据
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<OrderActionInfoList> GetOrderActionList(int oid);

        /// <summary>
        /// 订单基本信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<TakeOutOrderInfo> GetTakeOutOrderInfo(int oid);

        /// <summary>
        /// 获取订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<AdminOrderProductsList> GetAdminOrderProductsList(int oid);

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        void UpOrderState(int oid, OrderState orderState);

        /// <summary>
        /// 创建订单行为
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderActionType">订单处理类型</param>
        /// <param name="actionDes">行为</param>
        void CreateOrderAction(int oid, OrderActionType orderActionType, string actionDes);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="shipSN">配送单号</param>
        /// <param name="shipTime">配送时间</param>
        void SendOrder(int oid, OrderState orderState, string shipSN, string shipCom, DateTime shipTime);

        /// <summary>
        /// 获取发货基本信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<DeliveryList> GetDeliveryList(int oid);

        /// <summary>
        /// 获取卡片订单发货基本信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<DeliveryList> GetCouponsDeliveryList(int oid);


        /// <summary>
        /// 后台获取订单物流信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        AdminOrdersLogisticsInfo GetAdminOrdersLogisticsInfo(int oid);


        /// <summary>
        /// 获取用户消费订单总额
        /// </summary>
        /// <param name="uId">用户id</param>
        /// <returns></returns>
        decimal GetUserOrderAmount(int uId);

        /// <summary>
        /// 获取用户消费订单次数
        /// </summary>
        /// <param name="uId">用户id</param>
        /// <returns></returns>
        int GetUserOrderSum(int uId);

        /// <summary>
        /// 获取当前订单总额
        /// </summary>
        /// <returns></returns>
        decimal GetOrderAmountByTime(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取当前订单总数
        /// </summary>
        /// <returns></returns>
        int GetOrderCountByTime(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取订单总额
        /// </summary>
        /// <returns></returns>
        decimal GetOrderAmountTotal();

        /// <summary>
        /// 获取订单总额每天的数据
        /// </summary>
        /// <returns></returns>
        List<DataDetail> GetOrderAmountGroupByDate(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取订单数量每天的数据
        /// </summary>
        /// <returns></returns>
        List<DataDetail> GetOrderCountGroupByDate(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取商品购买统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>商品列表</returns>
        List<Data> AdminGetProductByCount(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取订单交易分析详情
        /// </summary>
        /// <returns></returns>
        List<OrderAnalysisDetal> GetOrderTransactionAnalysisDetail(DateTime startTime, DateTime endTime, int sId, bool payed);

        /// <summary>
        /// 获取商品分析列表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<AdminProductAnalysisInfo> GetAdminGetProductAnalysisList(DateTime startTime, DateTime endTime, string name,
            PageModel page, out int total);

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>订单编号</returns>
        string GenerateOsn(int uid);

    }
}
