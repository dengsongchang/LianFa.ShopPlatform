using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.SqlServer;
using System.Linq;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Express.KuaiDi100;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Code.Deppon;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Code.KuaiDi100;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Orders;
using LianFa.ShopPlatform.Model.Response.Admin.AfterSalesService;
using LianFa.ShopPlatform.Model.Response.Admin.Orders;
using LianFa.ShopPlatform.Model.Response.Admin.Statistics;
using LianFa.ShopPlatform.Model.Response.Client.Order;

namespace LianFa.ShopPlatform.Service
{
    public partial class OrdersService
    {
        /// <summary>
        /// 商品仓储类
        /// </summary>
        private readonly IRepository<LF_Users> _usersRepository;

        /// <summary>
        /// 卡片类型仓储类
        /// </summary>
        private readonly IRepository<LF_CouponTypes> _couponTypesRepository;

        /// <summary>
        /// 订单商品仓储类
        /// </summary>
        private readonly IRepository<LF_OrderProducts> _orderproductRepository;

        /// <summary>
        /// 订单动作商品仓储类
        /// </summary>
        private readonly IRepository<LF_OrderActions> _orderActionsRepository;

        /// <summary>
        /// 商品库存仓储类
        /// </summary>
        private readonly IRepository<LF_ProductStocks> _productStocksRepository;

        /// <summary>
        /// 优惠券仓储类
        /// </summary>
        private readonly IRepository<LF_Coupons> _couponsRepository;

        /// <summary>
        /// 优惠券仓储类
        /// </summary>
        private readonly IRepository<LF_CouponTypeContents> _coupontypeContentRepository;

        /// <summary>
        /// 商品仓储类
        /// </summary>
        private readonly IRepository<LF_Products> _productsRepository;

        /// <summary>
        /// 区域仓储类
        /// </summary>
        private readonly IRepository<LF_Regions> _regionsRepository;

        /// <summary>
        ///品牌仓储类
        /// </summary>
        private readonly IRepository<LF_Brands> _brandsRepository;

        /// <summary>
        /// 订单仓储
        /// </summary>
        /// <param name="ordersRepository"></param>
        /// <param name="usersRepository"></param>
        /// <param name="orderproductRepository"></param>
        /// <param name="orderActionsRepository"></param>
        /// <param name="productStocksRepository"></param>
        /// <param name="couponsRepository"></param>
        /// <param name="productsRepository"></param>
        /// <param name="regionsRepository"></param>
        /// <param name="brandsRepository"></param>
        public OrdersService(IRepository<LF_Orders> ordersRepository, IRepository<LF_Users> usersRepository,
                             IRepository<LF_OrderProducts> orderproductRepository, IRepository<LF_OrderActions> orderActionsRepository,
                             IRepository<LF_ProductStocks> productStocksRepository, IRepository<LF_Coupons> couponsRepository,
                             IRepository<LF_Products> productsRepository, IRepository<LF_Regions> regionsRepository,
                             IRepository<LF_Brands> brandsRepository, IRepository<LF_CouponTypes> couponTypesRepository, IRepository<LF_CouponTypeContents> coupontypeContentRepository)
        {
            _ordersRepository = ordersRepository;
            _usersRepository = usersRepository;
            _orderproductRepository = orderproductRepository;
            _orderActionsRepository = orderActionsRepository;
            _productStocksRepository = productStocksRepository;
            _couponsRepository = couponsRepository;
            _productsRepository = productsRepository;
            _regionsRepository = regionsRepository;
            _brandsRepository = brandsRepository;
            _couponTypesRepository = couponTypesRepository;
            _coupontypeContentRepository = coupontypeContentRepository;
        }

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
        public LF_Orders CreateOrder(int uId, List<LF_OrderProducts> orderProductList, FullShipAddressInfo fullShipAddressInfo,
            int payCreditCount, string buyerRemark, string ip, byte payModel, byte type, decimal productAmount)
        {
            LF_Orders orderInfo = new LF_Orders();

            //如果地址信息为空则赋空
            if (fullShipAddressInfo == null)
            {
                fullShipAddressInfo = new FullShipAddressInfo
                {
                    RegionId = 0,
                    Consignee = string.Empty,
                    Mobile = string.Empty,
                    Phone = string.Empty,
                    Email = string.Empty,
                    ZipCode = string.Empty,
                    Address = string.Empty,
                };
            }

            orderInfo.OSn = GenerateOsn(uId);
            orderInfo.UId = uId;

            orderInfo.Weight = orderProductList?.Sum(x => x.RealCount * x.Weight);
            orderInfo.ProductAmount = productAmount;

            orderInfo.ShipFee = orderProductList?.Sum(x => x.ShipFee) == null ? 0 : orderProductList.Sum(x => x.ShipFee);
            orderInfo.OrderAmount = orderInfo.ProductAmount + orderInfo.ShipFee;
            orderInfo.SurplusMoney = orderInfo.OrderAmount;
            if (orderInfo.SurplusMoney < 0)
                orderInfo.SurplusMoney = 0;

            orderInfo.OrderState = orderInfo.SurplusMoney <= 0 ? (byte)OrderState.PreProduct : (byte)OrderState.WaitPaying;

            orderInfo.AddTime = DateTime.Now;
            orderInfo.Type = type;
            orderInfo.PaySystemName = "";
            orderInfo.PaySn = "";
            orderInfo.ShiPsyStemName = "";
            orderInfo.ShipFriendName = "";
            orderInfo.ShipSn = "";
            orderInfo.PayMode = payModel;
            orderInfo.RegionId = (short)fullShipAddressInfo.RegionId;
            orderInfo.Consignee = fullShipAddressInfo.Consignee;
            orderInfo.Mobile = fullShipAddressInfo.Mobile;
            orderInfo.Address = fullShipAddressInfo.Address;
            orderInfo.BuyerRemark = buyerRemark;
            orderInfo.Ip = ip;
            orderInfo.ShipTime = new DateTime(1900, 1, 1);
            orderInfo.IsDel = false;
            orderInfo.OrderSource = (int)OrderSource.WxMini;

            _ordersRepository.Add(orderInfo);
            return orderInfo;
        }

        /// <summary>
        /// 创建卡片订单
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="orderProduct"></param>
        /// <param name="fullShipAddressInfo"></param>
        /// <param name="buyerRemark"></param>
        /// <param name="ip"></param>
        /// <param name="payModel"></param>
        /// <returns></returns>
        public LF_Orders CreateCouponOrder(int uId, List<LF_OrderProducts> orderProductList,
            FullShipAddressInfo fullShipAddressInfo, int payCreditCount, string buyerRemark, string ip, byte payModel, byte type)
        {
            LF_Orders orderInfo = new LF_Orders();

            //如果地址信息为空则赋空
            if (fullShipAddressInfo == null)
            {
                fullShipAddressInfo = new FullShipAddressInfo
                {
                    RegionId = 0,
                    Consignee = string.Empty,
                    Mobile = string.Empty,
                    Phone = string.Empty,
                    Email = string.Empty,
                    ZipCode = string.Empty,
                    Address = string.Empty,
                };
            }

            orderInfo.OSn = GenerateOsn(uId);
            orderInfo.UId = uId;

            orderInfo.Weight = 0;
            orderInfo.ProductAmount = 0;

            orderInfo.ShipFee = 0;
            orderInfo.OrderAmount = 0;
            if (orderInfo.SurplusMoney < 0)
                orderInfo.SurplusMoney = 0;

            orderInfo.OrderState = orderInfo.SurplusMoney <= 0 ? (byte)OrderState.PreProduct : (byte)OrderState.WaitPaying;

            orderInfo.AddTime = DateTime.Now;
            orderInfo.Type = (byte)OrderType.CardOrder;
            orderInfo.PaySystemName = "";
            orderInfo.PaySn = "";
            orderInfo.ShiPsyStemName = "";
            orderInfo.ShipFriendName = "";
            orderInfo.ShipSn = "";
            orderInfo.PayMode = payModel;
            orderInfo.RegionId = (short)fullShipAddressInfo.RegionId;
            orderInfo.Consignee = fullShipAddressInfo.Consignee;
            orderInfo.Mobile = fullShipAddressInfo.Mobile;
            orderInfo.Address = fullShipAddressInfo.Address;
            orderInfo.BuyerRemark = buyerRemark;
            orderInfo.Ip = ip;
            orderInfo.ShipTime = new DateTime(1900, 1, 1);
            _ordersRepository.Add(orderInfo);
            return orderInfo;
        }

        /// <summary>
        /// 获取订单列表 
        /// </summary>
        /// <param name="page">分页模型</param>   
        /// <param name="orderState">订单状态</param>    
        /// <param name="uid"></param>    
        /// <param name="total">总数</param>    
        /// <returns></returns>      
        public List<OrderList> GetOrderList(PageModel page, byte type, int orderState, int uid, out int total)
        {
            var data = _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.UId == uid)
                .Where(x => x.IsDel == false)
                .WhereIf(x => x.OrderState == orderState, orderState > 0)
                .Where(u => u.Type != (byte)OrderType.CardOrder)
                .GroupJoin(_orderproductRepository.GetDbSetNoTracking(), x => x.OId, y => y.OId, (x, y) => new OrderList
                {
                    OrderState = x.OrderState,
                    OId = x.OId,
                    RecordId = y.Any() ? y.FirstOrDefault().RecordId : 0,
                    ShopPrice = y.Any() ? y.FirstOrDefault().ShopPrice : 0,
                    ShowImg = y.Any() ? y.FirstOrDefault().ShowImg : "",
                    SurplusMoney = x.SurplusMoney,
                    BuyerRemark = x.BuyerRemark,
                    OrderAmount = x.OrderAmount,
                    PayMode = x.PayMode,
                    Type = x.Type,
                    OSn = x.OSn,
                    ProductAmount = x.ProductAmount,
                    ProductName = y.Any() ? y.FirstOrDefault().Name : "",
                    RealCount = y.Any() ? y.Sum(z => z.RealCount) : 0,
                    Weight = 0,
                    IsReview = 0,
                    PId = y.Any() ? y.FirstOrDefault().PId : 0,
                    Sku = y.Any() ? "my" : "",
                    AddTime = x.AddTime,
                    AddTimeStr = ""
                })
                .OrderByDescending(d => d.OId)
                .LoadPage(page, out total)
                .ToList();

            return data.Select(d => new OrderList
            {
                OrderState = d.OrderState,
                OrderStateDec = ((OrderState)d.OrderState).GetDescription(),
                OId = d.OId,
                RecordId = d.RecordId,
                ShopPrice = d.ShopPrice,
                ShowImg = FileHelper.GetFileFullUrl(d.ShowImg),
                SurplusMoney = d.SurplusMoney,
                Type = d.Type,
                PayMode = d.PayMode,
                BuyerRemark = d.BuyerRemark,
                OrderAmount = d.OrderAmount,
                OSn = d.OSn.Trim(),
                ProductAmount = d.ProductAmount,
                ProductName = d.ProductName,
                RealCount = d.RealCount,
                Weight = d.Weight,
                IsReview = d.IsReview,
                PId = d.PId,
                Sku = d.Sku,
                AddTime = d.AddTime,
                AddTimeStr = d.AddTime.ToDateTimeString()
            }).ToList();
        }


        /// <summary>
        /// 获取卡片订单列表 
        /// </summary>
        /// <param name="page">分页模型</param>   
        /// <param name="orderState">订单状态</param>    
        /// <param name="uid"></param>    
        /// <param name="total">总数</param>    
        /// <returns></returns>      
        public List<CouponOrderList> GetCouponOrderList(PageModel page, int orderState, int uid, out int total)
        {
            var data = _couponsRepository.GetDbSetNoTracking()
                .Where(x => x.UseUId == uid)
                .Join(_ordersRepository.GetDbSetNoTracking(), x => x.OId, y => y.OId, (x, y) => new
                {
                    OId = y.OId,
                    OSn = y.OSn,
                    CouponSn = "",
                    UId = x.UId,
                    CouponTypeId = x.CouponTypeId,
                    UseTime = x.UseTime,
                    OrderStateDec = "",
                    BuyerRemark = y.BuyerRemark,
                    OrderState = y.OrderState,
                    y.ShipSn,
                    y.Type
                }).Join(_couponTypesRepository.GetDbSetNoTracking(), x => x.CouponTypeId, y => y.CouponTypeId, (x, y) => new CouponOrderList
                {
                    OId = x.OId,
                    OSn = x.OSn,
                    CouponSn = "",
                    UId = x.UId,
                    CouponTypeId = x.CouponTypeId,
                    UseTime = x.UseTime,
                    OrderStateDec = "",
                    BuyerRemark = x.BuyerRemark,
                    OrderState = x.OrderState,
                    Name = y.Name,
                    CouponImg = y.CouponImg,
                    Type = x.Type,
                    ShipSn = x.ShipSn
                })
                .Where(x => x.Type == (byte)OrderType.CardOrder)
                .WhereIf(x => x.OrderState == orderState, orderState > 0)
                .OrderByDescending(d => d.OId)
                .LoadPage(page, out total)
                .ToList();

            return data.Select(x => new CouponOrderList
            {
                OId = x.OId,
                OSn = x.OSn,
                CouponSn = "",
                UId = x.UId,
                CouponTypeId = x.CouponTypeId,
                UseTime = x.UseTime,
                OrderStateDec = ((OrderState)x.OrderState).GetDescription(),
                BuyerRemark = x.BuyerRemark,
                OrderState = x.OrderState,
                Name = x.Name,
                CouponImg = x.CouponImg,
                UseTimeStr = x.UseTime.ToDateTimeString(),
                CouponImgFull = FileHelper.GetFileFullUrl(x.CouponImg),
                ShipSn = x.ShipSn
            }).ToList();
        }

        /// <summary>
        /// 校验卡号密码
        /// </summary>
        /// <returns></returns>
        public LF_Coupons CheckCard(int oId, string code, string password)
        {
            return _couponsRepository.LoadEntitiesNoTracking(x => x.OId == oId && x.CouponSn == code && x.PassWord == password).FirstOrDefault();
        }

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>订单编号</returns>
        public string GenerateOsn(int uid)
        {
            return "X" + DateTime.Now.ToString("yyMMddHHmmssfff") + uid;
        }

        /// <summary>
        /// 获取订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public List<ProductsList> GetProductsList(int oid)
        {
            var data = (from or in _ordersRepository.GetDbSetNoTracking()
                        join op in _orderproductRepository.GetDbSetNoTracking() on or.OId equals op.OId
                        where or.OId == oid
                        select new ProductsList
                        {
                            OrderState = or.OrderState,
                            ShopPrice = op.ShopPrice,
                            ShowImg = op.ShowImg,
                            SurplusMoney = or.SurplusMoney,
                            BuyerRemark = or.BuyerRemark,
                            OrderAmount = or.OrderAmount,
                            OSn = or.OSn,
                            ProductAmount = or.ProductAmount,
                            ProductName = op.Name,
                            RealCount = op.RealCount,
                            SendCount = op.SendCount,
                            Weight = op.Weight,
                            RecordId = op.RecordId,
                            //Sku = op.Sku
                        }).AsNoTracking().ToList();
            return data.Select(d => new ProductsList
            {
                OrderState = d.OrderState,
                OrderStateDec = ((OrderState)d.OrderState).GetDescription(),
                ShopPrice = d.ShopPrice,
                ShowImg = FileHelper.GetFileFullUrl(d.ShowImg),
                SurplusMoney = d.SurplusMoney,
                BuyerRemark = d.BuyerRemark,
                OrderAmount = d.OrderAmount,
                OSn = d.OSn,
                ProductAmount = d.ProductAmount,
                ProductName = d.ProductName,
                RealCount = d.RealCount,
                SendCount = d.SendCount,
                Weight = d.Weight,
                RecordId = d.RecordId,
                StoreName = d.StoreName,
                //Sku = d.Sku
            }).ToList();
        }

        /// <summary>
        ///  获取订单收货地址信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public OrderAddress GetOrderAddressInfo(int oid)
        {
            OrderAddress data = _ordersRepository.GetDbSetNoTracking()
                .Where(d => d.OId == oid)
                .Select(d => new OrderAddress
                {
                    Address = d.Address,
                    Consignee = d.Consignee,
                    Mobile = d.Mobile
                }).AsNoTracking().First();

            return data;
        }
        /// <summary>
        /// 获取小程序订单详情信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public OrderInfo GetOrderInfo(int oid)
        {
            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        where o.OId == oid
                        select new OrderInfo
                        {
                            SurplusMoney = o.SurplusMoney,
                            AddTime = o.AddTime,
                            RegionId = o.RegionId,
                            BuyerRemark = o.BuyerRemark,
                            Address = o.Address,
                            Consignee = o.Consignee,
                            OrderState = o.OrderState,
                            OId = o.OId,
                            OSn = o.OSn,
                            ShipFee = o.ShipFee,
                            ShipSn = o.ShipSn,
                            Mobile = o.Mobile,
                            OrderAmount = o.OrderAmount,
                            ProductAmount = o.ProductAmount,
                            ShipSystemName = o.ShiPsyStemName
                        }).AsNoTracking().First();
            if (data != null)
            {
                data.AddTimes = data.AddTime.ToDateTimeString();

                var r = _regionsRepository.LoadEntitiesNoTracking(x => x.RegionId == data.RegionId).FirstOrDefault();
                if (r != null)
                    data.Address = r.ProvinceName.Trim() + r.CityName.Trim() + r.Name.Trim() + data.Address.Trim();
            }
            return data;
        }

        /// <summary>
        /// 获取卡片信息详情
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        public CouponInfos GetCouponsInfo(int oId)
        {
            var data = _couponsRepository.GetDbSetNoTracking().Where(u => u.OId == oId)
                .Join(_couponTypesRepository.GetDbSetNoTracking(), x => x.CouponTypeId, y => y.CouponTypeId, (x, y) =>
                    new
                    {
                        CouponId = x.CouponId,
                        CouponSn = x.CouponSn,
                        CouponTypeId = y.CouponTypeId,
                        OId = x.OId,
                        PassWord = x.PassWord,
                        Name = y.Name,
                        CouponImg = y.CouponImg
                    }).GroupJoin(_coupontypeContentRepository.GetDbSetNoTracking(), x => x.CouponTypeId,
                    y => y.CouponTypeId, (x, y) => new CouponInfos
                    {
                        CouponId = x.CouponId,
                        CouponSn = x.CouponSn,
                        CouponTypeId = x.CouponTypeId,
                        OId = x.OId,
                        PassWord = x.PassWord,
                        Name = x.Name,
                        CouponImg = x.CouponImg,
                        ContentList = y.Select(z => new CouponTypeContents
                        {
                            CouponTypeId = z.CouponTypeId,
                            CouponContentId = z.CouponContentId,
                            CouponContent = z.CouponContent
                        }).OrderByDescending(z => z.CouponContentId)
                            .ToList()
                    }).AsNoTracking().First();
            data.CouponImg = FileHelper.GetFileFullUrl(data.CouponImg);
            return data;
        }

        /// <summary>
        /// 获取卡片信息详情
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        public AdminCouponsInfo GetAdminCouponsInfo(int oId)
        {
            var data = _couponsRepository.GetDbSetNoTracking().Where(u => u.OId == oId)
                .Join(_couponTypesRepository.GetDbSetNoTracking(), x => x.CouponTypeId, y => y.CouponTypeId, (x, y) =>
                    new
                    {
                        CouponId = x.CouponId,
                        CouponSn = x.CouponSn,
                        CouponTypeId = y.CouponTypeId,
                        OId = x.OId,
                        PassWord = x.PassWord,
                        Name = y.Name,
                        CouponImg = y.CouponImg,
                        y.Money
                    }).GroupJoin(_coupontypeContentRepository.GetDbSetNoTracking(), x => x.CouponTypeId,
                    y => y.CouponTypeId, (x, y) => new AdminCouponsInfo
                    {
                        Money = x.Money,
                        CouponId = x.CouponId,
                        CouponSn = x.CouponSn,
                        CouponTypeId = x.CouponTypeId,
                        OId = x.OId,
                        PassWord = x.PassWord,
                        Name = x.Name,
                        CouponImg = x.CouponImg,
                        ContentList = y.Select(z => new CouponTypeContents
                        {
                            CouponTypeId = z.CouponTypeId,
                            CouponContentId = z.CouponContentId,
                            CouponContent = z.CouponContent
                        }).OrderByDescending(z => z.CouponContentId)
                            .ToList()
                    }).AsNoTracking().First();
            data.CouponImg = FileHelper.GetFileFullUrl(data.CouponImg);
            return data;
        }

        /// <summary>
        /// 获取订单物流信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="uid">用户id</param>
        /// <param name="olid">商品订单物流id</param>
        /// <returns></returns>
        public OrdersLogisticsInfo GetOrdersLogisticsInfo(int oid, int uid, int olid)
        {
            var data = new OrdersLogisticsInfo();

            var order = _ordersRepository.LoadEntitiesNoTracking(x => x.OId == oid && x.UId == uid).FirstOrDefault();
            var region = _regionsRepository.LoadEntitiesNoTracking(x => x.RegionId == order.RegionId).FirstOrDefault();

            if (order == null)
            {
                return data;
            }
            if (region == null)
            {
                return data;
            }

            data.OrderAddressInfo = new OrderAddress
            {
                Address = region.ProvinceName.Trim() + region.CityName.Trim() + region.Name.Trim() + order.Address,
                Consignee = order.Consignee,
                Mobile = order.Mobile,
                ShipSn = order.ShipSn,
                ShiPsyStemName = order.ShiPsyStemName,
                IsCoupons = order.Type,
                Image = ""
            };
            data.Logistics = new KuaiDiModel();

            if (!string.IsNullOrEmpty(order.ShipSn.Trim()))
                data.Logistics = DepponApi.NewTraceQuery(order.ShipSn.Trim());

            if (order.Type == (byte)OrderType.CardOrder)
            {
                var datacoupons = (from c in _couponsRepository.GetDbSetNoTracking()
                                   where c.OId == oid
                                   join ct in _couponTypesRepository.GetDbSetNoTracking() on c.CouponTypeId equals ct.CouponTypeId
                                   select new ImageLogists
                                   {
                                       Image = ct.CouponImg,
                                       StoreName = ct.Name
                                   }).FirstOrDefault();

                data.OrderAddressInfo.Image = FileHelper.GetFileFullUrl(datacoupons?.Image);
                data.OrderAddressInfo.StoreName = datacoupons?.StoreName;
            }

            if (order.Type == (byte)OrderType.RegularOrders)
            {
                var datacommon = (from c in _orderproductRepository.GetDbSetNoTracking()
                                  where c.OId == oid
                                  select new ImageLogists
                                  {
                                      Image = c.ShowImg,
                                      StoreName = c.Name
                                  }).FirstOrDefault();

                data.OrderAddressInfo.Image = FileHelper.GetFileFullUrl(datacommon?.Image);
                data.OrderAddressInfo.StoreName = datacommon?.StoreName;
            }

            if (data.Logistics.data == null) return data;

            foreach (var item in data.Logistics.data)
            {
                var ftime = item.ftime.ToDate();
                var month = ftime.Month.ToString().Length == 1 ? $"0{ftime.Month}" : ftime.Month.ToString();
                var day = ftime.Day.ToString().Length == 1 ? $"0{ftime.Day}" : ftime.Day.ToString();
                item.ftime = $"{month}-{day}";
                var time = item.time.ToDate();
                var hour = time.Hour.ToString().Length == 1 ? $"0{time.Hour}" : time.Hour.ToString();
                var minute = time.Minute.ToString().Length == 1 ? $"0{time.Minute}" : time.Minute.ToString();
                item.time = $"{hour}:{minute}";
            }

            data.State = data.Logistics.state;

            return data;
        }

        /// <summary>
        /// 获取兑换订单物流信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="uid">用户id</param>
        /// <param name="olid">商品订单物流id</param>
        /// <returns></returns>
        public OrdersLogisticsInfo GetOrderLogisticsInfo(int oid)
        {
            var data = new OrdersLogisticsInfo();

            var order = _ordersRepository.LoadEntitiesNoTracking(x => x.OId == oid).FirstOrDefault();

            var region = _regionsRepository.LoadEntitiesNoTracking(x => x.RegionId == order.RegionId)?.FirstOrDefault();

            if (order == null)
            {
                return data;
            }

            data = new OrdersLogisticsInfo
            {
                OrderAddressInfo = new OrderAddress
                {
                    Address = region?.ProvinceName.Trim() + region?.CityName.Trim() + region?.Name.Trim() + order.Address,
                    Consignee = order.Consignee,
                    Mobile = order.Mobile
                },
                Logistics = new KuaiDiModel()
            };

            data.State = data.Logistics.state;
            
            if (!string.IsNullOrEmpty(order.ShipSn.Trim()))
                data.Logistics = DepponApi.NewTraceQuery(order.ShipSn.Trim());

            return data;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void CancelOrder(int oId, int uId)
        {
            UpOrderState(oId, OrderState.Cancelled);
            IncreaseProductStockNumber(oId);//增加商品库存数量
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        public void UpOrderState(int oid, OrderState orderState)
        {
            var model = _ordersRepository.GetById(oid);
            model.OrderState = (byte)orderState;
            _ordersRepository.Update(model);
        }

        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="oId"></param>
        public void IncreaseProductStockNumber(int oId)
        {
            var data = _orderproductRepository.GetDbSetNoTracking().Where(d => d.OId == oId).ToList();

            foreach (var item in data)
            {
                LF_ProductStocks prs = new LF_ProductStocks();
                prs.Number = prs.Number + item.RealCount;
                prs.PId = item.PId;
                _productStocksRepository.Update(prs);
            }
        }

        /// <summary>
        /// 创建订单行为
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderActionType">订单处理类型</param>
        /// <param name="actionDes">行为</param>
        public void CreateOrderAction(int oid, int uid, OrderActionType orderActionType, string actionDes)
        {
            var user = _usersRepository.GetById(uid);
            LF_OrderActions model = new LF_OrderActions();
            model.OId = oid;
            model.UId = uid;
            model.RealName = "";
            model.AdminGId = user.AdminGId;
            model.AdminGTitle = "";
            model.ActionType = (byte)orderActionType;
            model.ActionTime = DateTime.Now;
            model.ActionDes = actionDes;
            _orderActionsRepository.Add(model);
        }

        /// <summary>
        /// 收货
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="receiveTime">收货时间</param>
        /// <param name="ip">ip</param>
        public void ReceiveOrder(LF_Orders orderInfo, DateTime receiveTime, string ip)
        {
            orderInfo.OrderState = (int)OrderState.Evaluate;
            _ordersRepository.Update(orderInfo);//将订单状态设为收货状态
        }

        /// <summary>
        /// 获取各订单状态统计
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public SelOrderStateStatistics GetSelOrderStateStatistics(int uid)
        {
            var order = _ordersRepository.GetDbSetNoTracking();

            var data = new SelOrderStateStatistics
            {
                waitPaying = order.Count(d => d.UId == uid && d.OrderState == (int)OrderState.WaitPaying),

                stayReceive = (from oa in _orderActionsRepository.GetDbSetNoTracking()
                               join or in order on oa.OId equals or.OId
                               where oa.OId == or.OId && oa.ActionType == (int)OrderActionType.AcceptOrder ||
                                     oa.ActionType == (int)OrderActionType.PreProduct ||
                                     oa.ActionType == (int)OrderActionType.Send
                               select oa).Count(),
                stayShipments = (from oa in _orderActionsRepository.GetDbSetNoTracking()
                                 join or in order on oa.OId equals or.OId
                                 where oa.OId == or.OId && oa.ActionType == (int)OrderActionType.PreProduct
                                 select oa).Count()
            };
            return data;
        }

        /// <summary>
        /// 获取订单物流列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public OrderLogistics GetOrderLogistics(int oid, int uid)
        {
            OrderLogistics data = (from o in _ordersRepository.GetDbSetNoTracking()
                                   where o.OId == oid
                                   select new OrderLogistics
                                   {
                                       OId = o.OId,
                                       OSn = o.OSn.Trim(),
                                       ShipSn = o.ShipSn
                                   }).AsNoTracking().First();

            var list = _orderproductRepository.LoadEntitiesNoTracking(x => x.OId == oid)
                .Select(x => new OrderProductList
                {
                    PId = x.PId,
                    Number = x.RealCount,
                    ProdutcsName = x.Name,
                    ShowImg = x.ShowImg,
                })
                .ToList();

            list.ForEach(x => x.ShowImg = FileHelper.GetFileFullUrl(x.ShowImg));

            data.OrderLogisticsProductList = new List<OrderLogisticsProduct>()
            {
                new OrderLogisticsProduct
                {
                    ShipSn = data.ShipSn,
                    OrderProductList = list
                }
            };

            return data;
        }

        /// <summary>
        /// 后台获取所有订单
        /// </summary>
        /// <returns></returns>
        public List<Orderlist> GetOrdersList(AdminOrdertListRequest request, out int total)
        {
            DateTime? t = null;
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (!string.IsNullOrEmpty(request.startTime)) { startTime = DateTime.Parse(request.startTime); }
            if (!string.IsNullOrEmpty(request.endTime)) { endTime = DateTime.Parse(request.endTime); }

            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join u in _usersRepository.GetDbSetNoTracking() on o.UId equals u.UId
                        join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId
                        where o.IsDel == false
                        select new { u, o, r })
                .Where(m => m.o.Type != (byte)OrderType.CardOrder)
                .WhereIf(m => m.o.Type == request.OrderType, request.OrderType > -1)
                .WhereIf(m => m.o.OSn.Contains(request.oSn), !string.IsNullOrEmpty(request.oSn))
                .WhereIf(m => m.o.Mobile.Contains(request.Mobile), !string.IsNullOrEmpty(request.Mobile))
                .WhereIf(m => m.o.Consignee.Contains(request.consigNee), !string.IsNullOrEmpty(request.consigNee))
                .WhereIf(m => m.o.OrderState == request.orderState, request.orderState > 0)
                .WhereIf(m => m.u.UId == request.UId, request.UId > 0)
                .WhereIf(m => m.u.UserName.Contains(request.userName), !string.IsNullOrEmpty(request.userName))
                .WhereIf(m => m.o.AddTime >= startTime, startTime != t && endTime == t)
                .WhereIf(m => m.o.AddTime <= endTime, startTime == t && endTime != t)
                .WhereIf(m => m.o.AddTime >= startTime && m.o.AddTime <= endTime, startTime != t && endTime != t)
                .WhereIf(m => m.r.ProvinceId == request.ProvinceId,
                    request.ProvinceId > 0 && request.CityId == 0 && request.RegionId == 0)
                .WhereIf(m => m.r.ProvinceId == request.ProvinceId && m.r.CityId == request.CityId,
                    request.ProvinceId > 0 && request.CityId > 0 && request.RegionId == 0)
                .WhereIf(
                    m => m.r.RegionId == request.RegionId && m.r.ProvinceId == request.ProvinceId &&
                         m.r.CityId == request.CityId,
                    request.ProvinceId > 0 && request.CityId > 0 && request.RegionId > 0)
                .Select(d => new Orderlist
                {
                    Addtime = d.o.AddTime,
                    OId = d.o.OId,
                    OrderAmount = d.o.OrderAmount,
                    SurplusMoney = d.o.SurplusMoney,
                    OrderState = d.o.OrderState,
                    OSn = d.o.OSn,
                    UserName = d.u.UserName,
                    PayTime = d.o.PayTime,
                    Mobile = d.o.Mobile,
                    OrderType = d.o.Type.ToString()
                })
                .OrderByDescending(d => d.OId)
                .LoadPage(request.page, out total)
                .ToList();
            return data.Select(d => new Orderlist
            {
                Addtimes = d.Addtime.ToChineseDateTimeString(true),
                PayTimes = d.PayTime.ToChineseDateTimeString(true),
                PayTime = d.PayTime,
                Mobile = d.Mobile,
                OId = d.OId,
                OrderAmount = d.OrderAmount,
                OrderState = d.OrderState,
                OrderStateDesc = ((OrderState)d.OrderState).GetDescription(),
                OSn = d.OSn.Trim(),
                OutOSn = d.OutOSn?.Trim() ?? "",
                PayfriendName = d.PayfriendName?.Trim() ?? "",
                UserName = d.UserName.Trim(),
                SellStoreSn = !string.IsNullOrEmpty(d.SellStoreSn) ? d.SellStoreSn.Trim() : "",
                Addtime = d.Addtime,
                SellStoreName = !string.IsNullOrEmpty(d.SellStoreName) ? d.SellStoreName.Trim() : "",
                SurplusMoney = d.SurplusMoney,
                OrderType = ((OrderType)Convert.ToInt32(d.OrderType)).GetDescription()
                //CouponMoney = d.CouponMoney
            }).ToList();
        }

        /// <summary>
        /// 获取待发货订单导出列表
        /// </summary>
        /// <returns></returns>
        public List<OrderGoodsExplist> GetOrderGoodsExplist()
        {
            return _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.Type != (byte)OrderType.CardOrder)
                .Where(m => m.OrderState == (int)OrderState.PreProduct)
                .Select(d => new OrderGoodsExplist
                {
                    OSn = d.OSn,
                    ShipFriendName = "",
                    ShipSn = ""
                })
                .ToList();
        }


        /// <summary>
        /// 获取待发货订单导出列表
        /// </summary>
        /// <returns></returns>
        public List<OrderGoodsExplist> GetOrderCardExplist()
        {
            return _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.Type == (byte)OrderType.CardOrder)
                .Where(m => m.OrderState == (int)OrderState.PreProduct)
                .Select(d => new OrderGoodsExplist
                {
                    OSn = d.OSn,
                    ShipFriendName = "",
                    ShipSn = ""
                })
                .ToList();
        }



        /// <summary>
        /// 后台获取卡片订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CouponsOrderlist> GetAdminCouponsOrderList(AdminCouponsOrdertListReques request, out int total)
        {
            DateTime? t = null;
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (!string.IsNullOrEmpty(request.startTime)) { startTime = DateTime.Parse(request.startTime); }
            if (!string.IsNullOrEmpty(request.endTime)) { endTime = DateTime.Parse(request.endTime); }

            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join u in _usersRepository.GetDbSetNoTracking() on o.UId equals u.UId
                        join c in _couponsRepository.GetDbSetNoTracking() on o.OId equals c.OId
                        join ct in _couponTypesRepository.GetDbSetNoTracking() on c.CouponTypeId equals ct.CouponTypeId
                        select new { u, o, c, ct })
                .WhereIf(m => m.o.OSn.Contains(request.oSn), !string.IsNullOrEmpty(request.oSn))
                .WhereIf(m => m.o.OrderState == request.orderState, request.orderState > 0)
                .WhereIf(m => m.u.UId == request.UId, request.UId > 0)
                .WhereIf(m => m.u.UserName.Contains(request.userName), !string.IsNullOrEmpty(request.userName))
                .WhereIf(m => m.c.UseTime >= startTime, startTime != t && endTime == t)
                .WhereIf(m => m.c.UseTime <= endTime, startTime == t && endTime != t)
                .WhereIf(m => m.c.UseTime >= startTime && m.c.UseTime <= endTime, startTime != t && endTime != t)
                .Select(d => new CouponsOrderlist
                {
                    Addtime = d.o.AddTime,
                    OId = d.o.OId,
                    OrderAmount = d.o.OrderAmount,
                    SurplusMoney = d.o.SurplusMoney,
                    OrderState = d.o.OrderState,
                    OSn = d.o.OSn,
                    UserName = d.u.UserName,
                    PayTime = d.o.PayTime,
                    UId = d.u.UId,
                    CouponTypeId = d.c.CouponTypeId,
                    UseTime = d.c.UseTime,
                    OrderStateDec = "",
                    BuyerRemark = d.o.BuyerRemark,
                    Name = d.ct.Name,
                    CouponImg = d.ct.CouponImg,
                    Type = d.o.Type,
                    ShipSn = d.o.ShipSn,
                    PayfriendName = d.o.PayFriendName,
                    CouponsMoney = d.ct.Money,
                    CouponSn = d.c.CouponSn,
                    PassWord = d.c.PassWord,
                })
                .OrderByDescending(d => d.OId)
                .LoadPage(request.page, out total)
                .ToList();

            return data.Select(d => new CouponsOrderlist
            {
                Addtime = d.Addtime,
                OId = d.OId,
                OrderAmount = d.OrderAmount,
                SurplusMoney = d.SurplusMoney,
                OrderState = d.OrderState,
                OSn = d.OSn,
                UserName = d.UserName,
                PayTime = d.PayTime,
                CouponSn = d.CouponSn,
                PassWord = d.PassWord,
                UId = d.UId,
                CouponTypeId = d.CouponTypeId,
                UseTime = d.UseTime,
                BuyerRemark = d.BuyerRemark,
                Name = d.Name,
                CouponImg = d.CouponImg,
                CouponImgFull = FileHelper.GetFileFullUrl(d.CouponImg),
                Type = d.Type,
                OrderStateDesc = ((OrderState)d.OrderState).GetDescription(),
                UseTimeStr = d.UseTime.ToDateTimeString(),
                PayfriendName = d.PayfriendName,
                ShipSn = d.ShipSn,
                CouponsMoney = d.CouponsMoney
            }).ToList();
        }

        /// <summary>
        /// 获取导出优惠券订单列表
        /// </summary>
        /// <param name="exdata">请求参数</param>
        /// <returns></returns>
        public List<CouponOrderExplist> GetAdminExportCouponOrderList(AdminCouponsOrdertListReques exdata)
        {
            DateTime? t = null;
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (!string.IsNullOrEmpty(exdata.startTime)) { startTime = DateTime.Parse(exdata.startTime); }
            if (!string.IsNullOrEmpty(exdata.endTime)) { endTime = DateTime.Parse(exdata.endTime); }

            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join u in _usersRepository.GetDbSetNoTracking() on o.UId equals u.UId
                        join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId
                        join c in _couponsRepository.GetDbSetNoTracking() on o.OId equals c.OId
                        join ct in _couponTypesRepository.GetDbSetNoTracking() on c.CouponTypeId equals ct.CouponTypeId
                        where o.IsDel == false
                        select new { u, o, r, c, ct })
                .WhereIf(m => m.o.OSn.Contains(exdata.oSn), !string.IsNullOrEmpty(exdata.oSn))
                .WhereIf(m => m.o.OrderState == exdata.orderState, exdata.orderState > 0)
                .WhereIf(m => m.u.UserName.Contains(exdata.userName), !string.IsNullOrEmpty(exdata.userName))
                .WhereIf(m => m.c.UseTime >= startTime, startTime != t && endTime == t)
                .WhereIf(m => m.c.UseTime <= endTime, startTime == t && endTime != t)
                .WhereIf(m => m.c.UseTime >= startTime && m.c.UseTime <= endTime, startTime != t && endTime != t)
                .GroupJoin(_orderActionsRepository.GetDbSetNoTracking(), d => d.o.OId, y => y.OId, (d, y) => new CouponOrderExplistModel
                {
                    OId = d.o.OId,
                    Address = d.r.ProvinceName.Trim() + d.r.CityName.Trim() + d.r.Name.Trim() + d.o.Address.Trim(),
                    Consignee = d.o.Consignee,
                    OSn = d.o.OSn,
                    Mobile = d.o.Mobile,
                    CouponName = d.ct.Name,
                    CouponSn = d.c.CouponSn,
                    CouponPwd = d.c.PassWord,
                    CouponTypeId = d.ct.CouponTypeId,
                    UserName = d.u.UserName,
                    OrderState = d.o.OrderState.ToString(),
                    SalesPlatform = "小程序",
                    BuyerRemark = d.o.BuyerRemark,
                    OrderActionses = y.Select(x => new OrderActionsModel { ActionTime = x.ActionTime, ActionType = x.ActionType }).ToList()
                })
                .OrderByDescending(d => d.OId)
                .ToList();

            return data.Select(d => new CouponOrderExplist
            {
                OId = d.OId,
                Address = d.Address,
                Consignee = d.Consignee,
                CouponName = d.CouponName,
                CouponSn = d.CouponSn,
                CouponPwd = d.CouponPwd,
                OSn = d.OSn,
                Mobile = d.Mobile,
                UserName = d.UserName,
                SalesPlatform = "小程序",
                BuyerRemark = d.BuyerRemark,
                OrderState = ((OrderState)int.Parse(d.OrderState)).GetDescription(),
                CouponContent = GetCouponContentList(d.CouponTypeId),
                PaymentTime = GetActionDateTime(d.OrderActionses, (int)OrderActionType.Pay),
                SendDeliveryTime = GetActionDateTime(d.OrderActionses, (int)OrderActionType.Send),
                CompleteTime = GetActionDateTime(d.OrderActionses, (int)OrderActionType.Receive)
            }).ToList();
        }

        /// <summary>
        /// 后台获取礼品卡内容
        /// </summary>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        public string GetCouponContentList(int couponTypeId)
        {
            var data = _coupontypeContentRepository.GetDbSetNoTracking()
                .Where(c => c.CouponTypeId == couponTypeId)
                .Select(x => x.CouponContent)
                .ToList();
            var content = "";
            if (data.Any() && data.Count > 0)
            {
                content = data.Aggregate(content,
                    (current, couponContent) => current + (couponContent + ","));
                content = content.TrimEnd(',');
            }
            return content;
        }

        /// <summary>
        /// 获取订单动作时间
        /// </summary>
        /// <param name="list"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public string GetActionDateTime(List<OrderActionsModel> list, byte actionType)
        {
            string actionDateTime = "";

            var action = list.FirstOrDefault(x => x.ActionType == actionType);

            if (action != null)
                actionDateTime = action.ActionTime.ToDateTimeString();

            return actionDateTime;
        }
        /// <summary>
        /// 获取导出订单列表
        /// </summary>
        /// <param name="exdata">请求参数</param>
        /// <returns></returns>
        public List<OrderExplist> GetAdminExportOrderList(AdminExportAdminOrdersListRequest exdata)
        {
            DateTime? t = null;
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (!string.IsNullOrEmpty(exdata.startTime)) { startTime = DateTime.Parse(exdata.startTime); }
            if (!string.IsNullOrEmpty(exdata.endTime)) { endTime = DateTime.Parse(exdata.endTime); }

            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join u in _usersRepository.GetDbSetNoTracking() on o.UId equals u.UId
                        join op in _orderproductRepository.GetDbSetNoTracking() on o.OId equals op.OId
                        join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId
                        where o.IsDel == false
                        select new { u, o, r, op })
                .Where(m => m.o.Type != (byte)OrderType.CardOrder)
                .WhereIf(m => m.o.OSn.Contains(exdata.oSn), !string.IsNullOrEmpty(exdata.oSn))
                .WhereIf(m => m.o.Consignee.Contains(exdata.consigNee), !string.IsNullOrEmpty(exdata.consigNee))
                .WhereIf(m => m.o.OrderState == exdata.orderState, exdata.orderState > 0)
                .WhereIf(m => m.u.UserName.Contains(exdata.userName), !string.IsNullOrEmpty(exdata.userName))
                .WhereIf(m => m.o.AddTime >= startTime, startTime != t && endTime == t)
                .WhereIf(m => m.o.AddTime <= endTime, startTime == t && endTime != t)
                .WhereIf(m => m.o.AddTime >= startTime && m.o.AddTime <= endTime, startTime != t && endTime != t)
                .WhereIf(m => m.r.ProvinceId == exdata.ProvinceId, exdata.ProvinceId > 0 && exdata.CityId == 0 && exdata.RegionId == 0)
                .WhereIf(m => m.r.ProvinceId == exdata.ProvinceId && m.r.CityId == exdata.CityId, exdata.ProvinceId > 0 && exdata.CityId > 0 && exdata.RegionId == 0)
                .WhereIf(m => m.r.RegionId == exdata.RegionId && m.r.ProvinceId == exdata.ProvinceId && m.r.CityId == exdata.CityId, exdata.ProvinceId > 0 && exdata.CityId > 0 && exdata.RegionId > 0)
                .Select(d => new OrderExplist
                {
                    OId = d.o.OId,
                    Address = d.r.ProvinceName.Trim() + d.r.CityName.Trim() + d.r.Name.Trim() + d.o.Address.Trim(),
                    Consignee = d.o.Consignee,
                    RealCount = d.op.RealCount,
                    OSn = d.o.OSn,
                    Mobile = d.o.Mobile,
                    ProductsName = d.op.Name,
                    UserName = d.u.UserName,
                    OrderState = d.o.OrderState.ToString(),
                    ShopPrice = d.op.ShopPrice,
                    ShipFee = d.o.ShipFee,
                    SurplusMoney = d.o.SurplusMoney,
                    PaymentGoods = d.op.BuyCount * d.op.ShopPrice
                })
                .GroupJoin(_orderActionsRepository.GetDbSetNoTracking(), d => d.OId, y => y.OId, (d, y) => new OrderExplistModel
                {
                    OId = d.OId,
                    Address = d.Address,
                    Consignee = d.Consignee,
                    RealCount = d.RealCount,
                    OSn = d.OSn,
                    Mobile = d.Mobile,
                    ProductsName = d.ProductsName,
                    UserName = d.UserName,
                    ShopPrice = d.ShopPrice,
                    SalesPlatform = "小程序",
                    ShipFee = d.ShipFee,
                    OrderState = d.OrderState,
                    SurplusMoney = d.SurplusMoney,
                    PaymentGoods = d.PaymentGoods,
                    OrderActionses = y.Select(x => new OrderActionsModel { ActionTime = x.ActionTime, ActionType = x.ActionType }).ToList()
                })
                .OrderByDescending(d => d.OId)
                .ToList();

            return data.Select(d => new OrderExplist
            {
                OId = d.OId,
                Address = d.Address,
                Consignee = d.Consignee,
                RealCount = d.RealCount,
                OSn = d.OSn,
                Mobile = d.Mobile,
                ProductsName = d.ProductsName,
                UserName = d.UserName,
                ShopPrice = d.ShopPrice,
                SalesPlatform = "小程序",
                ShipFee = d.ShipFee,
                OrderState = ((OrderState)int.Parse(d.OrderState)).GetDescription(),
                SurplusMoney = d.SurplusMoney,
                PaymentGoods = d.PaymentGoods,
                PaymentTime = GetActionDateTime(d.OrderActionses, (int)OrderActionType.Pay),
                SendDeliveryTime = GetActionDateTime(d.OrderActionses, (int)OrderActionType.Send),
                CompleteTime = GetActionDateTime(d.OrderActionses, (int)OrderActionType.Receive)
            }).ToList();
        }

        /// <summary>
        /// 根据订单id获取订单动作数据
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public List<OrderActionInfoList> GetOrderActionList(int oid)
        {
            var data = _orderActionsRepository.GetDbSetNoTracking().Where(d => d.OId == oid)
                .Select(d => new OrderActionInfoList
                {
                    ActionTime = d.ActionTime.ToString(),
                    ActionType = d.ActionType,
                }).ToList();
            return data.Select(d => new OrderActionInfoList
            {
                ActionTime = d.ActionTime.ToString(),
                ActionType = d.ActionType,
                ActionTypes = ((OrderActionType)d.ActionType).GetDescription()
            }).ToList();
        }

        /// <summary>
        /// 订单基本信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public List<TakeOutOrderInfo> GetTakeOutOrderInfo(int oid)
        {
            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join u in _usersRepository.GetDbSetNoTracking() on o.UId equals u.UId
                        join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId
                        where o.OId == oid && o.IsDel == false
                        select new RegionsInfo
                        {
                            ShipFeeAmount = o.ShipFee,
                            UseName = u.UserName,
                            Address = o.Address,
                            BuyerRemark = o.BuyerRemark,
                            Mobile = u.Mobile,
                            OSn = o.OSn,
                            //PayFriendName = o.PayFriendName,
                            NickName = u.NickName,
                            OrderState = o.OrderState,
                            OrderAmount = o.OrderAmount,
                            ProductAmount = o.ProductAmount,
                            Uid = u.UId,
                            RName = r.Name,
                            CityName = r.CityName,
                            ProvinceName = r.ProvinceName,
                            SurplusMoney = o.SurplusMoney,
                            // CouponMoney = o.CouponMoney,
                        }).ToList();
            return data.Select(d => new TakeOutOrderInfo
            {
                ShipFeeAmount = d.ShipFeeAmount,
                UseName = d.UseName,
                Address = d.ProvinceName.Trim() + d.CityName.Trim() + d.RName.Trim() + d.Address,
                BuyerRemark = d.BuyerRemark,
                //Email = d.Email.Trim(),
                Mobile = d.Mobile.Trim(),
                OSn = d.OSn.Trim(),
                //PayFriendName = d.PayFriendName.Trim(),
                NickName = d.NickName.Trim(),
                OrderState = d.OrderState,
                OrderAmount = d.OrderAmount,
                ProductAmount = d.ProductAmount,
                SurplusMoney = d.SurplusMoney,
                //CouponMoney = d.CouponMoney,
                Uid = d.Uid,
                OrderStateChinese = ((OrderState)d.OrderState).GetDescription(),
            }).ToList();
        }

        /// <summary>
        /// 获取订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public List<AdminOrderProductsList> GetAdminOrderProductsList(int oid)
        {
            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join op in _orderproductRepository.GetDbSetNoTracking() on o.OId equals op.OId
                        where o.OId == oid
                        select new AdminOrderProductsList
                        {
                            BrandName = "",
                            PId = op.PId,
                            ProductsName = op.Name,
                            //ProductsType = p.Type,
                            SendCount = op.SendCount,
                            ShopPrice = op.ShopPrice,
                            Summary = "",
                            RealCount = op.RealCount,
                            ProductsTogether = op.BuyCount * op.ShopPrice,
                            ShowImg = op.ShowImg,
                            ShipFee = op.ShipFee
                        }).ToList();
            return data.Select(d => new AdminOrderProductsList
            {
                BrandName = d.BrandName,
                PId = d.PId,
                ProductsName = d.ProductsName,
                ProductsType = d.ProductsType,
                SendCount = d.SendCount,
                ShopPrice = d.ShopPrice,
                MName = d.MName,
                Summary = d.Summary,
                RealCount = d.RealCount,
                ProductsTogether = d.ProductsTogether,
                ShowImg = FileHelper.GetFileFullUrl(d.ShowImg),
                ShipFee = d.ShipFee
            }).ToList();
        }

        /// <summary>
        /// 创建订单行为
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderActionType">订单处理类型</param>
        /// <param name="actionDes">行为</param>
        public void CreateOrderAction(int oid, OrderActionType orderActionType, string actionDes)
        {
            LF_OrderActions model = new LF_OrderActions();
            model.OId = oid;
            model.UId = 11;
            model.RealName = "";
            model.ActionType = (byte)orderActionType;
            model.ActionTime = DateTime.Now;
            model.AdminGTitle = "";
            model.ActionDes = actionDes;
            model.AdminGId = 0;
            _orderActionsRepository.Add(model);
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="shipSN">配送单号</param>
        /// <param name="shipTime">配送时间</param>
        public void SendOrder(int oid, OrderState orderState, string shipSN, string shipCom, DateTime shipTime)
        {
            var od = _ordersRepository.GetById(oid);
            od.OId = oid;
            od.OrderState = (byte)orderState;
            od.ShipSn = shipSN;
            od.ShiPsyStemName = shipCom;
            od.ShipTime = shipTime;
            _ordersRepository.Update(od);
        }

        /// <summary>
        /// 获取发货基本信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public List<DeliveryList> GetDeliveryList(int oid)
        {
            var opd = _orderproductRepository.GetDbSetNoTracking();
            var pd = _productsRepository.GetDbSetNoTracking();
            var cd = _couponsRepository.GetDbSetNoTracking();
            var cdt = _couponTypesRepository.GetDbSetNoTracking();
            var order = _ordersRepository.GetDbSetNoTracking().FirstOrDefault(u => u.OId.Equals(oid));

            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId into rd
                        from region in rd.DefaultIfEmpty()
                        where o.OId == oid
                        select new DeliveryList
                        {
                            oid = o.OId,
                            address = region.ProvinceName.Trim() + region.CityName.Trim() + region.Name.Trim() + o.Address.Trim(),
                            buyerRemark = o.BuyerRemark,
                            consignee = o.Consignee,
                            phone = o.Mobile,
                            productList = (
                                from op in opd
                                join p in pd on op.PId equals p.PId
                                where op.OId == o.OId
                                select new ProductList
                                {
                                    Name = p.Name,
                                    ShopPrice = op.ShopPrice,
                                    ShowImg = op.ShowImg,
                                    SendCount = op.SendCount,
                                    RealCount = op.RealCount,
                                    Subtotal = op.ShopPrice * op.RealCount
                                }
                            ).ToList()
                        }).ToList();
            data.ForEach(x => x.productList.ForEach(y => y.ShowImg = FileHelper.GetFileFullUrl(y.ShowImg)));

            if (order.Type == (byte)OrderType.CardOrder)
            {
                var datas = (from o in _ordersRepository.GetDbSetNoTracking()
                             join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId into rd
                             from region in rd.DefaultIfEmpty()
                             where o.OId == oid
                             select new DeliveryList
                             {
                                 oid = o.OId,
                                 address = region.ProvinceName.Trim() + region.CityName.Trim() + region.Name.Trim() + o.Address.Trim(),
                                 buyerRemark = o.BuyerRemark,
                                 consignee = o.Consignee,
                                 phone = o.Mobile,
                                 productList = (
                                     from op in cd
                                     join p in cdt on op.CouponTypeId equals p.CouponTypeId
                                     where op.OId == o.OId
                                     select new ProductList
                                     {
                                         Name = p.Name,
                                         ShopPrice = p.Money,
                                         ShowImg = p.CouponImg,
                                         SendCount = 1,
                                         RealCount = 1,
                                         Subtotal = p.Money
                                     }
                                 ).ToList()
                             }).ToList();
                datas.ForEach(x => x.productList.ForEach(y => y.ShowImg = FileHelper.GetFileFullUrl(y.ShowImg)));
                return datas;
            }
            return data;
        }

        /// <summary>
        /// 获取卡片发货基本信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public List<DeliveryList> GetCouponsDeliveryList(int oid)
        {
            var opd = _couponsRepository.GetDbSetNoTracking();
            var pd = _couponTypesRepository.GetDbSetNoTracking();
            var data = (from o in _ordersRepository.GetDbSetNoTracking()
                        join r in _regionsRepository.GetDbSetNoTracking() on o.RegionId equals r.RegionId into rd
                        from region in rd.DefaultIfEmpty()
                        where o.OId == oid
                        select new DeliveryList
                        {
                            oid = o.OId,
                            address = region.ProvinceName.Trim() + region.CityName.Trim() + region.Name.Trim() + o.Address.Trim(),
                            buyerRemark = o.BuyerRemark,
                            consignee = o.Consignee,
                            phone = o.Mobile,
                            productList = (
                                from op in opd
                                join p in pd on op.CouponTypeId equals p.CouponTypeId
                                where op.OId == o.OId
                                select new ProductList
                                {
                                    Name = p.Name,
                                    ShopPrice = p.Money,
                                    ShowImg = p.CouponImg,
                                    SendCount = 1,
                                    RealCount = 1,
                                    Subtotal = p.Money
                                }
                            ).ToList()
                        }).ToList();
            data.ForEach(x => x.productList.ForEach(y => y.ShowImg = FileHelper.GetFileFullUrl(y.ShowImg)));
            return data;
        }

        /// <summary>
        /// 后台获取订单物流信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="olid">商品订单物流id</param>
        /// <returns></returns>
        public AdminOrdersLogisticsInfo GetAdminOrdersLogisticsInfo(int oid)
        {
            var data = new AdminOrdersLogisticsInfo();

            var order = _ordersRepository.LoadEntitiesNoTracking(x => x.OId == oid).FirstOrDefault();

            var region = _regionsRepository.LoadEntitiesNoTracking(x => x.RegionId == order.RegionId).FirstOrDefault();

            if (order == null)
            {
                return data;
            }

            if (region == null)
            {
                return data;
            }

            data.OrderAddressInfo = new AdminOrderAddress()
            {
                Address = region.ProvinceName + region.CityName + region.Name + order.Address,
                Consignee = order.Consignee,
                Mobile = order.Mobile
            };

            data.Logistics = DepponApi.NewTraceQuery(order.ShipSn.Trim());
            data.State = data.Logistics.state;

            return data;
        }

        /// <summary>
        /// 获取用户消费订单总额
        /// </summary>
        /// <param name="uId">用户id</param>
        /// <returns></returns>
        public decimal GetUserOrderAmount(int uId)
        {
            return _ordersRepository.GetDbSetNoTracking().Where(d => d.UId == uId).Select(m => m.OrderAmount).Sum();
        }

        /// <summary>
        /// 获取用户消费订单次数
        /// </summary>
        /// <param name="uId">用户id</param>
        /// <returns></returns>
        public int GetUserOrderSum(int uId)
        {
            return _ordersRepository.GetDbSetNoTracking().Where(d => d.UId == uId).Count(m => m.UId == uId);
        }

        /// <summary>
        /// 获取当前订单总额
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderAmountByTime(DateTime startTime, DateTime endTime)
        {
            var data = _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.OrderState >= (byte)OrderState.Confirmed && x.AddTime >= startTime &&
                            x.AddTime <= endTime);
            return
                data.Any() ? data.Sum(x => x.OrderAmount) : 0;
        }

        /// <summary>
        /// 获取当前订单总数
        /// </summary>
        /// <returns></returns>
        public int GetOrderCountByTime(DateTime startTime, DateTime endTime)
        {
            return _ordersRepository.GetDbSetNoTracking()
                .Count(x => x.OrderState >= (byte)OrderState.Confirmed && x.AddTime >= startTime && x.AddTime <= endTime);
        }

        /// <summary>
        /// 获取订单总额
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderAmountTotal()
        {
            var data = _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.OrderState >= (byte)OrderState.Confirmed);
            return
                data.Any() ? data.Sum(x => x.OrderAmount) : 0;
        }

        /// <summary>
        /// 获取订单总额每天的数据
        /// </summary>
        /// <returns></returns>
        public List<DataDetail> GetOrderAmountGroupByDate(DateTime startTime, DateTime endTime)
        {
            return _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.OrderState >= (byte)OrderState.Confirmed && x.AddTime >= startTime && x.AddTime <= endTime)
                .GroupBy(x => SqlFunctions.DateName("yy", x.AddTime) + "-" +
                              SqlFunctions.DateName("mm", x.AddTime) + "-" +
                              SqlFunctions.DateName("dd", x.AddTime))
                .Select(x => new DataDetail
                {
                    Date = x.Key,
                    Value = x.Sum(y => y.OrderAmount),
                })
                .ToList();
        }

        /// <summary>
        /// 获取订单数量每天的数据
        /// </summary>
        /// <returns></returns>
        public List<DataDetail> GetOrderCountGroupByDate(DateTime startTime, DateTime endTime)
        {
            return _ordersRepository.GetDbSetNoTracking()
                //.WhereIf(x=>x.Type==type,type==(byte)OrderType.CardOrder||type==(byte)OrderType.RegularOrders||type==(byte)OrderType.BuyCardOrder)
                .Where(x => x.OrderState >= (byte)OrderState.Confirmed && x.AddTime >= startTime && x.AddTime <= endTime)
                .GroupBy(x => SqlFunctions.DateName("yy", x.AddTime) + "-" +
                              SqlFunctions.DateName("mm", x.AddTime) + "-" +
                              SqlFunctions.DateName("dd", x.AddTime))
                .Select(x => new DataDetail
                {
                    Date = x.Key,
                    Value = x.Count(),
                })
                .ToList();
        }

        /// <summary>
        /// 获取商品购买统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>商品列表</returns>
        public List<Data> AdminGetProductByCount(DateTime startTime, DateTime endTime)
        {
            var productDb = _productsRepository.GetDbSetNoTracking();
            var orderDb = _ordersRepository.GetDbSetNoTracking();
            var orderProductList = _orderproductRepository.GetDbSetNoTracking()
                .Join(orderDb, x => x.OId, y => y.OId, (x, y) => new
                {
                    x.PId,
                    x.BuyCount,
                    y.AddTime,
                    y.OrderState
                })
                .Where(x => x.OrderState >= (byte)OrderState.Confirmed && x.AddTime >= startTime && x.AddTime <= endTime)
                .GroupBy(x => x.PId)
                .Select(x => new
                {
                    PId = x.Key,
                    Count = x.Sum(y => y.BuyCount)
                })
                .Join(productDb, x => x.PId, y => y.PId, (x, y) => new Data
                {
                    DataName = y.Name,
                    DataValue = x.Count
                })
                .OrderByDescending(x => x.DataValue)
                .Take(10)
                .ToList();
            return orderProductList;
        }

        /// <summary>
        /// 获取订单交易分析详情
        /// </summary>
        /// <returns></returns>
        public List<OrderAnalysisDetal> GetOrderTransactionAnalysisDetail(DateTime startTime, DateTime endTime, int sId, bool payed)
        {
            return _ordersRepository.GetDbSetNoTracking()
                .Where(x => x.AddTime > startTime && x.AddTime < endTime)
                //.WhereIf(x => x.SId == sId, sId > 0)
                .WhereIf(x => x.OrderState > (byte)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled, payed)
                .GroupJoin(_orderproductRepository.GetDbSetNoTracking(), x => x.OId, y => y.OId,
                    (x, y) => new OrderAnalysisDetal
                    {
                        OId = x.OId,
                        UId = x.UId,
                        OrderState = x.OrderState,
                        SurplusMoney = x.SurplusMoney,
                        ProductCount = y.Any() ? y.Sum(z => z.BuyCount) : 0,
                        AddTime = x.AddTime
                    })
                .ToList();
        }

        /// <summary>
        /// 获取商品分析列表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<AdminProductAnalysisInfo> GetAdminGetProductAnalysisList(DateTime startTime, DateTime endTime,
            string name, PageModel page, out int total)
        {
            var data = _productsRepository.GetDbSetNoTracking()
                .GroupJoin(_orderproductRepository.GetDbSetNoTracking(), x => x.PId, y => y.PId, (x, y) => new
                {
                    x.AddTime,
                    x.Name,
                    x.PId,
                    SalesCount = y.Count(),
                    OId = y.Any() ? y.FirstOrDefault().OId : 0,
                    SalesAmount = y.Any() ? y.Sum(z => z.ShopPrice * z.BuyCount) : 0
                }).GroupJoin(_ordersRepository.GetDbSetNoTracking(), x => x.OId, y => y.OId, (x, y) => new AdminProductAnalysisInfo
                {
                    Name = x.Name,
                    PId = x.PId,
                    SalesCount = x.SalesCount,
                    OId = x.OId,
                    SalesAmount = x.SalesAmount,
                    BuyCount = y.Count(),
                    CreateTime = x.AddTime
                }).WhereIf(x => x.Name.Contains(name), !string.IsNullOrEmpty(name))
                .OrderByDescending(u => u.CreateTime)
                .LoadPage(page, out total)
                .ToList();

            return data;
        }

    }
}

