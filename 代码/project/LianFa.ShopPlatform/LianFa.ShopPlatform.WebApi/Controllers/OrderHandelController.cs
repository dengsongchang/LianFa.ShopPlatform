using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Client.Coupon;
using LianFa.ShopPlatform.Model.Request.Client.Order;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.Cart;
using LianFa.ShopPlatform.Model.Response.Client.Coupon;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.Service.Payment.Wxpay;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using FileHelper = HuCheng.Util.Core.Helper.FileHelper;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Signature]
    [ControllerGroup("订单接口", "用于确认订单,提交订单")]
    public class OrderHandelController : ApiController
    {
        private static object _locker = new object(); //锁对象

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 用户上下文
        /// </summary>
        private readonly IWorkContext _workContext;
        /// <summary>
        /// 购物车服务
        /// </summary>
        private readonly IOrderProductsService _orderProducts;

        /// <summary>
        /// 配送服务
        /// </summary>
        private readonly IShipAddressesService _shipAddressesService;

        /// <summary>
        /// 商品服务
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 订单动作服务
        /// </summary>
        private readonly IOrderActionsService _orderActionsService;

        /// <summary>
        /// 卡片服务
        /// </summary>
        private readonly ICouponsService _coupons;

        /// <summary>
        /// 订单服务
        /// </summary>
        private readonly IOrdersService _orderService;

        /// <summary>
        /// 收货地址服务
        /// </summary>
        private readonly IShipAddressesService _shipAddresses;

        /// <summary>
        /// 区域服务
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 运费服务
        /// </summary>
        private readonly IShippingTemplatesService _shippingTemplatesService;

        /// <summary>
        /// 支付服务
        /// </summary>
        private readonly IWxpayService _wxpayService;

        /// <summary>
        /// 礼品卡类型管理
        /// </summary>
        private readonly ICouponTypesService _couponTypes;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 订单相关接口构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="orderProducts"></param>
        /// <param name="products"></param>
        /// <param name="workContext"></param>
        /// <param name="shipAddressesService"></param>
        /// <param name="orderService"></param>
        /// <param name="orderActionsService"></param>
        /// <param name="users"></param>
        /// <param name="wxpayService"></param>
        /// <param name="shippingTemplatesService"></param>
        /// <param name="alipayService"></param>
        /// <param name="coupons"></param>
        /// <param name="shipAddresses"></param>
        /// <param name="regions"></param>
        public OrderHandelController(IUnitOfWork unitOfWork, IOrderProductsService orderProducts, IProductsService products, IWorkContext workContext,
            IShipAddressesService shipAddressesService, IOrdersService orderService, IOrderActionsService orderActionsService, IUsersService users,
            IWxpayService wxpayService, IShippingTemplatesService shippingTemplatesService, ICouponsService coupons,
            IShipAddressesService shipAddresses, IRegionsService regions, ICouponTypesService couponTypes)
        {
            _unitOfWork = unitOfWork;
            _orderProducts = orderProducts;
            _products = products;
            _workContext = workContext;
            _shipAddressesService = shipAddressesService;
            _orderService = orderService;
            _orderActionsService = orderActionsService;
            _users = users;
            _shippingTemplatesService = shippingTemplatesService;
            _wxpayService = wxpayService;
            _coupons = coupons;
            _shipAddresses = shipAddresses;
            _regions = regions;
            _couponTypes = couponTypes;
        }

        #region 确认订单

        /// <summary>
        /// 确认订单
        /// </summary>
        public BaseResponse<ConfirmOrderResponse> ConfirmOrder(ConfirmOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    //购物车中已经存在的商品列表
                    var orderProductList = _orderProducts.LoadEntitiesNoTracking(x => x.UId == user.UId && x.OId == 0).ToList();

                    for (var i = 0; i < orderProductList.Count; i++)
                    {
                        orderProductList[i].ShowImg = FileHelper.GetFileFullUrl(orderProductList[i].ShowImg);
                    }

                    //购物车信息
                    CartInfo cartInfo = _orderProducts.TidyOrderProductList(request.SelectedCartItemKeyList, orderProductList);
                    if (cartInfo.SelectedOrderProductList.Count < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("请先选择购物车商品");   

                    //返回响应数据
                    var response = new ConfirmOrderResponse
                    {
                        SelectedCartItemKeyList = request.SelectedCartItemKeyList,
                        CartInfo = cartInfo
                    };

                    //获取收货地址
                    if (request.SaId > 0)
                        response.DefaultFullShipAddressInfo =
                            _shipAddressesService.GetFullShipAddressBySAId(user.UId,request.SaId);
                    if (response.DefaultFullShipAddressInfo == null)
                        response.DefaultFullShipAddressInfo =
                            _shipAddressesService.GetDefaultFullShipAddress(user.UId);

                    //计算商品合计金额
                    response.ProductAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList,user.UId);
                    //response.ProductAmount = cartInfo.SelectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);

                    //获取运费
                    response.ShipFee = _shippingTemplatesService.CalculateFreight(cartInfo.SelectedOrderProductList, response.DefaultFullShipAddressInfo);
                    response.TotalCount += cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    response.OrderAmount = response.ProductAmount + response.ShipFee;

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ConfirmOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ConfirmOrder", requestString, ex);
                return BuildResponse.FailResponse<ConfirmOrderResponse>("确认订单错误");
            }
        }

        #endregion

        #region 提交订单  

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SubmitOrderResponse> SubmitOrder(SubmitOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    lock (_locker)
                    {
                        //获得当前用户
                        var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                        //当商城不允许游客使用购物车时
                        if (user == null || user.UId < 1)
                            return BuildResponse.FailResponse<SubmitOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        //配送地址
                        var fullShipAddressInfo =
                            _shipAddressesService.GetFullShipAddressBySAId(user.UId,
                                request.SaId);
                        if (fullShipAddressInfo == null)
                            return BuildResponse.FailResponse<SubmitOrderResponse>("请选择配送地址");

                        //订单商品列表
                        var orderProductList = new List<LF_OrderProducts>();

                        //是否直接购买
                        var isDirectBuy = request.IsDirectBuy == 1;
                        if (isDirectBuy)
                        {
                            var partOrderSkuInfo = _products.LoadEntitieNoTracking(x => x.PId == request.PId);
                            if (partOrderSkuInfo == null)
                                return BuildResponse.FailResponse<SubmitOrderResponse>("产品不存在");

                            //判断商品是否存在
                            var partOrderProductInfo = _products.LoadEntitieNoTracking(x => x.PId == partOrderSkuInfo.PId);
                            if (partOrderProductInfo == null)
                                return BuildResponse.FailResponse<SubmitOrderResponse>("产品不存在");

                            //购买数量不能小于1
                            if (request.Count < 1)
                                return BuildResponse.FailResponse<SubmitOrderResponse>("购买数量不能小于1");

                            //商品库存
                            var stock = _orderProducts.GetProductStockNumberByPid(request.PId);
                            if (stock < request.Count)
                                return BuildResponse.FailResponse<SubmitOrderResponse>("库存不足");

                            //添加新商品到购物车,不进行持久化
                            var shopPrice = partOrderProductInfo.IsCostPrice == (int)WhetherType.Yes
                                ? Math.Round(partOrderSkuInfo.CostPrice, 2, MidpointRounding.AwayFromZero)
                                : Math.Round(partOrderSkuInfo.ShopPrice, 2, MidpointRounding.AwayFromZero);
                            var model = new LF_OrderProducts()
                            {
                                PId = partOrderProductInfo.PId,
                                CateId = partOrderProductInfo.CateId,
                                BrandId = partOrderProductInfo.BrandId,
                                Name = partOrderProductInfo.Name,
                                ShopPrice =
                                    shopPrice,
                                CostPrice =
                                    Math.Round(partOrderSkuInfo.CostPrice, 2, MidpointRounding.AwayFromZero),
                                Weight = partOrderProductInfo.Weight,
                                ShowImg = partOrderProductInfo.ShowImg,
                                UId = user.UId,
                                RealCount = request.Count,
                                BuyCount = request.Count,
                                AddTime = DateTime.Now,
                                SendCount = 0,
                                PSn = partOrderProductInfo.PSn,
                                ShipFee = 0,
                            };
                            model.ShipFee = _shippingTemplatesService.ProductCalculateFreight(model, fullShipAddressInfo);

                            orderProductList.Add(model);
                        }
                        else
                        {
                            //购物车中已经存在的商品列表
                            orderProductList = _orderProducts.GetList(x => x.UId == user.UId && x.OId == 0).ToList();
                            if (orderProductList.Count < 1)
                                return BuildResponse.FailResponse<SubmitOrderResponse>("购物车中没有商品");
                        }

                        //购物车信息
                        CartInfo cartInfo =
                            _orderProducts.TidyOrderProductList(request.SelectedCartItemKeyList,
                                orderProductList);

                        if (cartInfo.SelectedOrderProductList.Count < 1)
                            return BuildResponse.FailResponse<SubmitOrderResponse>("购物车中没有选中的商品");

                        //验证商品
                        foreach (CartProductInfo cartProductInfo in cartInfo.CartProductList)
                        {
                            #region 验证

                            if (!cartProductInfo.IsSelected) continue;

                            var orderProductInfo = cartProductInfo.OrderProductInfo;

                            //验证商品信息
                            var partOrderSkuInfo = _products.LoadEntitieNoTracking(x => x.PId == orderProductInfo.PId);
                            if (partOrderSkuInfo == null)
                                return BuildResponse.FailResponse<SubmitOrderResponse>("产品不存在");

                            var partProductInfo = _products.GetProductsById(partOrderSkuInfo.PId);
                            if (partProductInfo == null || partProductInfo.State == (byte)ProductsStatus.OutSale)
                            {
                                return BuildResponse.FailResponse<SubmitOrderResponse>("商品" + orderProductInfo.Name + "已经下架，请删除此商品");
                            }
                            if (partOrderSkuInfo.IsCostPrice == (int) WhetherType.No)
                            {
                                if (orderProductInfo.ShopPrice !=
                                    Math.Round(partOrderSkuInfo.ShopPrice, 2, MidpointRounding.AwayFromZero) ||
                                    orderProductInfo.CostPrice != Math.Round(partOrderSkuInfo.CostPrice, 2,
                                        MidpointRounding.AwayFromZero))
                                {
                                    return BuildResponse.FailResponse<SubmitOrderResponse>(
                                        "商品" + orderProductInfo.Name + "信息有变化，请删除后重新添加");
                                }
                            }
                            else
                            {
                                if (orderProductInfo.ShopPrice !=
                                    Math.Round(partOrderSkuInfo.CostPrice, 2, MidpointRounding.AwayFromZero) ||
                                    orderProductInfo.CostPrice != Math.Round(partOrderSkuInfo.CostPrice, 2,
                                        MidpointRounding.AwayFromZero))
                                {
                                    return BuildResponse.FailResponse<SubmitOrderResponse>(
                                        "商品" + orderProductInfo.Name + "信息有变化，请删除后重新添加");
                                }
                            }

                            //商品库存
                            var productStock = _orderProducts.GetProductStockNumberByPid(orderProductInfo.PId);
                            if (productStock < request.Count)
                            {
                                return BuildResponse.FailResponse<SubmitOrderResponse>("商品" + orderProductInfo.Name + "库存不足");
                            }

                            #endregion
                        }
                        //计算商品合计金额
                        var productAmount =
                            _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);

                        //开启事务
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                foreach (var item in cartInfo.SelectedOrderProductList)
                                {
                                    item.ShipFee =
                                        _shippingTemplatesService.ProductCalculateFreight(item, fullShipAddressInfo);
                                }

                                //验证已经通过,进行订单保存
                                var orderInfo = _orderService.CreateOrder(user.UId,
                                    cartInfo.SelectedOrderProductList, fullShipAddressInfo, request.PayCreditCount, request.BuyerRemark,
                                    _workContext.Ip, request.PayType, (byte)OrderType.RegularOrders, productAmount);

                                //提交事务
                                var succ = _unitOfWork.Commit();
                                if (succ > 0)
                                {
                                    //减少商品库存数量 
                                    _orderProducts.DecreaseProductStockNumber(cartInfo.SelectedOrderProductList);

                                    //处理OrderProduct
                                    if (isDirectBuy)
                                    {

                                        _orderProducts.BatchAddOrderProducts(cartInfo.SelectedOrderProductList.Select(x => new LF_OrderProducts()
                                        {
                                            PId = x.PId,
                                            CateId = x.CateId,
                                            BrandId = x.BrandId,
                                            Name = x.Name,
                                            ShopPrice = x.ShopPrice,
                                            CostPrice = x.CostPrice,
                                            Weight = x.Weight,
                                            ShowImg = x.ShowImg,
                                            UId = user.UId,
                                            RealCount = request.Count,
                                            BuyCount = request.Count,
                                            SendCount = 0,
                                            AddTime = DateTime.Now,
                                            OId = orderInfo.OId,
                                            ShipFee = x.ShipFee,
                                            PSn = x.PSn,
                                            SId = "0",
                                        }).ToList());
                                    }
                                    else
                                    {
                                        var selectIds = cartInfo.SelectedOrderProductList.Select(x => new
                                        {
                                            x.RecordId,
                                            x.ShipFee
                                        }).ToList();
                                        foreach (var item in selectIds)
                                        {
                                            _orderProducts.BatchUpdate(x => item.RecordId == x.RecordId, x => new LF_OrderProducts()
                                            {
                                                OId = orderInfo.OId,
                                                ShipFee = item.ShipFee
                                            });
                                        }
                                    }

                                    //创建订单处理
                                    _orderActionsService.AddOrderActions(new LF_OrderActions()
                                    {
                                        OId = orderInfo.OId,
                                        UId = user.UId,
                                        RealName = "本人",
                                        AdminGId = 0,
                                        AdminGTitle = "非管理员",
                                        ActionType = (int)OrderActionType.Submit,
                                        ActionTime = DateTime.Now,
                                        ActionDes = orderInfo.OrderState == (int)OrderState.WaitPaying
                                            ? "您提交了订单，等待您付款"
                                            : "您提交了订单，请等待系统确认"
                                    });

                                    _unitOfWork.Commit();
                                    scope.Complete();
                                    var response = new SubmitOrderResponse();
                                    //获取回调地址
                                    var notifyUrl = FileHelper.GetFileFullUrl(ConfigMap.WeXinPayNotifyUrl);

                                    if (!string.IsNullOrEmpty(request.Code))
                                    {
                                        //获取JsApi参数列表
                                        var jsonResult = OAuthApi.GetAccessToken(ConfigMap.AppId, ConfigMap.AppSecret, request.Code);
                                        response.JsApiParameter = _wxpayService.GetJsApiParams(jsonResult.openid, "订单提交", orderInfo.SurplusMoney, orderInfo.Ip.Trim(), notifyUrl, orderInfo.OSn.Trim());
                                    }
                                    else
                                    {
                                        response.WechatParameter = _wxpayService.GetWebAppParams("订单提交", orderInfo.SurplusMoney, orderInfo.Ip.Trim(), notifyUrl, orderInfo.OSn.Trim());
                                    }

                                    return BuildResponse.SuccessResponse(response);
                                }
                            }
                            catch (Exception ex)
                            {
                                ApiLogger.Error("SubmitOrder", requestString, "提交订单错误", ex);
                                return BuildResponse.FailResponse<SubmitOrderResponse>("提交订单失败");
                            }
                            finally
                            {
                                scope.Dispose();
                            }
                        }
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<SubmitOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("SubmitOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<SubmitOrderResponse>("提交订单错误");
            }
        }

        #endregion

        #region 重新提交订单

        /// <summary>
        /// 重新提交订单
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SubmitOrderResponse> ReSubmitOrder(ReSubmitOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    lock (_locker)
                    {
                        //获得当前用户
                        var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                        //当商城不允许游客使用购物车时
                        if (user == null || user.UId < 1)
                            return BuildResponse.FailResponse<SubmitOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        var order = _orderService.GetOrdersById(request.OId);

                        if (order == null)
                            return BuildResponse.FailResponse<SubmitOrderResponse>("订单不存在");

                        if (order.OrderState != (int)OrderState.WaitPaying)
                            return BuildResponse.FailResponse<SubmitOrderResponse>("订单状态不正确");

                        var response = new SubmitOrderResponse();
                        //获取回调地址
                        var notifyUrl = FileHelper.GetFileFullUrl(ConfigMap.WeXinPayNotifyUrl);

                        if (!string.IsNullOrEmpty(request.Code))
                        {
                            //获取JsApi参数列表
                            var jsonResult = OAuthApi.GetAccessToken(ConfigMap.AppId, ConfigMap.AppSecret, request.Code);
                            response.JsApiParameter = _wxpayService.GetJsApiParams(jsonResult.openid, "订单提交", order.SurplusMoney, order.Ip.Trim(), notifyUrl, order.OSn.Trim());
                        }
                        else
                        {
                            response.WechatParameter = _wxpayService.GetWebAppParams("订单提交", order.SurplusMoney, order.Ip.Trim(), notifyUrl, order.OSn.Trim());
                        }
                        return BuildResponse.SuccessResponse(response);
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<SubmitOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ReSubmitOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<SubmitOrderResponse>("提交订单错误");
            }
        }

        #endregion

        #region 再次购买

        /// <summary>
        /// 再次购买
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ReComposeOrderResponse> ReComposeOrder(ReComposeOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    lock (_locker)
                    {
                        //获得当前用户
                        var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                        //当商城不允许游客使用购物车时
                        if (user == null || user.UId < 1)
                            return BuildResponse.FailResponse<ReComposeOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        var orderProduct = _orderProducts.LoadEntitiesNoTracking(x => x.OId == request.OId && x.UId == _workContext.CurrentUser.UId);
                        if (!orderProduct.Any())
                            return BuildResponse.FailResponse<ReComposeOrderResponse>("订单商品不存在");

                        var newProduct = new List<LF_OrderProducts>();
                        foreach (var item in orderProduct)
                        {
                            var userCar = _orderProducts.Get(x => x.PId == item.PId && x.UId == _workContext.CurrentUser.UId && x.OId == 0);
                            if (userCar != null)
                            {
                                userCar.BuyCount = item.BuyCount;
                                userCar.RealCount = item.RealCount;
                                newProduct.Add(userCar);
                                _orderProducts.UpdateOrderProducts(userCar);
                                continue;
                            }
                            var product = _products.LoadEntitieNoTracking(x => x.PId == item.PId);
                            if (product == null || product.State != (byte)ProductsStatus.OnSale)
                                continue;

                            //判断库存
                            var stock = _orderProducts.GetProductStockNumberByPid(item.PId);
                            if (stock < item.BuyCount)
                                continue;

                            var shopPrice = product.IsCostPrice == (int)WhetherType.Yes
                                ? Math.Round(product.CostPrice, 2, MidpointRounding.AwayFromZero)
                                : Math.Round(product.ShopPrice, 2, MidpointRounding.AwayFromZero);
                            var model = new LF_OrderProducts()
                            {
                                OId = 0,
                                SId = "",
                                CostPrice = product.CostPrice,
                                ShopPrice = shopPrice,
                                ShowImg = product.ShowImg,
                                BrandId = product.BrandId,
                                CateId = product.CateId,
                                Weight = product.Weight,
                                Name = product.Name,
                                PId = product.PId,
                                UId = user.UId,
                                RealCount = item.RealCount,
                                SendCount = 0,
                                BuyCount = item.BuyCount,
                                AddTime = DateTime.Now,
                                ShipFee = 0,
                                PSn = product.PSn,
                            };
                            newProduct.Add(model);
                            _orderProducts.AddOrderProducts(model);
                        }
                        _unitOfWork.Commit();
                        var response = new ReComposeOrderResponse
                        {
                            PIds = newProduct.Select(x => x.PId).ToList()
                        };
                        return BuildResponse.SuccessResponse(response);
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ReComposeOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ReComposeOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<ReComposeOrderResponse>("提交订单错误");
            }
        }

        #endregion

        #region 直接确认订单

        /// <summary>
        /// 直接确认订单
        /// </summary>
        public BaseResponse<ConfirmOrderResponse> DirectConfirmOrder(DirectConfirmOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    var partOrderSkuInfo = _products.LoadEntitieNoTracking(x => x.PId == request.PId);
                    if (partOrderSkuInfo == null)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("商品不存在");

                    //判断商品是否存在
                    var partProductInfo = _products.GetProductsById(partOrderSkuInfo.PId);
                    if (partProductInfo == null)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("商品不存在");

                    //购买数量不能小于1
                    if (request.Count < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("购买数量不能小于1");

                    //商品库存
                    var stock = _orderProducts.GetProductStockNumberByPid(request.PId);
                    if (stock < request.Count)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("库存不足");

                    var shopPrice = partOrderSkuInfo.IsCostPrice == (int)WhetherType.Yes
                        ? Math.Round(partOrderSkuInfo.CostPrice, 2, MidpointRounding.AwayFromZero)
                        : Math.Round(partOrderSkuInfo.ShopPrice, 2, MidpointRounding.AwayFromZero);
                    //订单商品列表,四舍五入价格
                    var orderProductList = new List<LF_OrderProducts>
                    {
                        new LF_OrderProducts()
                        {
                            PId = partProductInfo.PId,
                            CateId = partProductInfo.CateId,
                            BrandId = partProductInfo.BrandId,
                            Name = partProductInfo.Name,
                            ShopPrice = shopPrice,
                            CostPrice = Math.Round(partOrderSkuInfo.CostPrice,2, MidpointRounding.AwayFromZero),
                            Weight = partProductInfo.Weight,
                            ShowImg = FileHelper.GetFileFullUrl(partProductInfo.ShowImg),
                            UId = user.UId,
                            RealCount = request.Count,
                            BuyCount = request.Count,
                            AddTime = DateTime.Now,
                        }
                    };

                    //添加新商品到购物车,不进行持久化
                    if (orderProductList.Count < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("购物车中没有商品，请先添加商品");

                    //购物车信息
                    CartInfo cartInfo = _orderProducts.TidyOrderProductList(new List<int>
                    {
                        partOrderSkuInfo.PId
                    }, orderProductList);
                    if (cartInfo.SelectedOrderProductList.Count < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("请先选择购物车商品");

                    var response = new ConfirmOrderResponse
                    {
                        CartInfo = cartInfo,
                        SelectedCartItemKeyList = request.SelectedCartItemKeyList
                    };

                    //获取收货地址
                    if (request.SaId > 0)
                        response.DefaultFullShipAddressInfo =
                            _shipAddressesService.GetFullShipAddressBySAId(user.UId,request.SaId);
                    if (response.DefaultFullShipAddressInfo == null)
                        response.DefaultFullShipAddressInfo =
                            _shipAddressesService.GetDefaultFullShipAddress(user.UId);

                    //计算商品合计金额
                    response.ProductAmount = partProductInfo.ShopPrice * request.Count;
                    if (partProductInfo.IsCostPrice == (int)WhetherType.Yes)
                    {
                        response.ProductAmount = partProductInfo.CostPrice * request.Count;
                    }
                    //response.ProductAmount = _orderProducts.GetProductAmount(cartInfo.SelectedOrderProductList, user.UId);
                    //response.ProductAmount = cartInfo.SelectedOrderProductList.Sum(x => x.BuyCount * x.ShopPrice);
                    response.ProductAmount = Math.Round(response.ProductAmount, 2, MidpointRounding.AwayFromZero);
                    //response.FullCut = cartInfo.CartFullCutList.Sum(x => x.CutMoney);

                    //获取运费
                    response.ShipFee = _shippingTemplatesService.CalculateFreight(cartInfo.SelectedOrderProductList, response.DefaultFullShipAddressInfo);

                    response.TotalCount += cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    response.OrderAmount = Math.Round(response.ProductAmount + response.ShipFee, 2, MidpointRounding.AwayFromZero);

                    return BuildResponse.SuccessResponse(response);
                }


                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ConfirmOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("DirectConfirmOrder", requestString, ex);
                return BuildResponse.FailResponse<ConfirmOrderResponse>("确认订单错误");
            }
        }

        #endregion

        #region 提交卡片订单

        /// <summary>
        /// 提交卡片订单
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SubmitCouponOrderResponse> SubmitCouponOrder(SubmitCouponOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    lock (_locker)
                    {
                        var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                        //返回未登录信息
                        if (user == null || user.UId < 1)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        //礼品卡
                        var couponInfo = _coupons.LoadEntitieNoTracking(y => y.CouponId == request.CouponId);
                        if (couponInfo == null)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>("礼品卡不存在");
                        if (couponInfo.OId > 0 || couponInfo.State == (int)CouponState.Used)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>("该礼品卡已兑换");
                        if (couponInfo.UseEndTime < DateTime.Now || couponInfo.State == (int)CouponState.Due)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>("该礼品卡已过期");
                        if (couponInfo.State == (int)CouponState.Cancel)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>("该礼品卡已作废");

                        //配送地址
                        var fullShipAddressInfo = _shipAddresses.GetFullShipAddressBySAId(user.UId, request.SaId);
                        if (fullShipAddressInfo == null)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>("收货地址/礼品卡信息不存在");

                        var verifyShipAddress = _regions.VerifyShipAddress(fullShipAddressInfo, couponInfo.CouponTypeId);
                        if (!verifyShipAddress)
                            return BuildResponse.FailResponse<SubmitCouponOrderResponse>("该地址无法配送，请更换地址");

                        //开启事务
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                //验证已经通过,进行订单保存
                                var orderInfo = _orderService.CreateCouponOrder(user.UId, null, fullShipAddressInfo, 0, request.BuyerRemark,
                                    _workContext.Ip, (byte)PayModeType.Online, (byte)OrderType.CardOrder);
                                //更改订单状态
                                orderInfo.OrderState = (byte)OrderState.PreProduct;

                                //提交事务
                                var success = _unitOfWork.Commit();
                                if (success > 0)
                                {
                                    //创建订单处理
                                    _orderActionsService.AddOrderActions(new LF_OrderActions()
                                    {
                                        OId = orderInfo.OId,
                                        UId = user.UId,
                                        RealName = "本人",
                                        AdminGId = 0,
                                        AdminGTitle = "非管理员",
                                        ActionType = (int)OrderActionType.Submit,
                                        ActionTime = DateTime.Now,
                                        ActionDes = "您提交了订单，请等待系统确认"
                                    });

                                    //修改礼品卡信息
                                    couponInfo.OId = orderInfo.OId;
                                    couponInfo.UseTime = DateTime.Now;
                                    couponInfo.UseIp = _workContext.Ip;
                                    couponInfo.State = (byte)CouponState.Used;
                                    couponInfo.UseUId = user.UId;
                                    _coupons.UpdateCoupons(couponInfo);

                                    _unitOfWork.Commit();
                                    scope.Complete();

                                    var response = new SubmitCouponOrderResponse
                                    {
                                        OId = orderInfo.OId
                                    };
                                    return BuildResponse.SuccessResponse(response);
                                }
                            }
                            catch (Exception ex)
                            {
                                ApiLogger.Error("SubmitCouponOrder", requestString, "兑换礼品卡错误", ex);
                                return BuildResponse.FailResponse<SubmitCouponOrderResponse>("兑换礼品卡失败");
                            }
                            finally
                            {
                                scope.Dispose();
                            }
                        }
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<SubmitCouponOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("SubmitCouponOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<SubmitCouponOrderResponse>("兑换礼品卡错误");
            }
        }

        #endregion

        #region 提交购买礼品卡订单

        /// <summary>
        /// 提交购买礼品卡订单
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SubmitOrderResponse> SubmitBuyCardOrder(SubmitBuyCardOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    lock (_locker)
                    {
                        var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                        //返回未登录信息
                        if (user == null || user.UId < 1)
                            return BuildResponse.FailResponse<SubmitOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        //配送地址
                        var fullShipAddressInfo =
                            _shipAddressesService.GetFullShipAddressBySAId(user.UId,
                                request.SaId);
                        if (fullShipAddressInfo == null)
                            return BuildResponse.FailResponse<SubmitOrderResponse>("请选择配送地址");

                        var couponInfo = _couponTypes.LoadEntitieNoTracking(p => p.CouponTypeId == request.CouponTypeId && p.State == (byte)WhetherType.Yes);
                        if (couponInfo == null)
                        {
                            return BuildResponse.FailResponse<SubmitOrderResponse>("不存在该礼品卡");
                        }

                        var orderProductList = new List<LF_OrderProducts>();

                        var shopPrice = couponInfo.IsCostPrice == (int)WhetherType.Yes
                            ? Math.Round(couponInfo.CostPrice, 2, MidpointRounding.AwayFromZero)
                            : Math.Round(couponInfo.Money, 2, MidpointRounding.AwayFromZero);
                        //添加新商品到购物车,不进行持久化
                        var model = new LF_OrderProducts()
                        {
                            PId = couponInfo.CouponTypeId,
                            CateId = 0,
                            BrandId = 0,
                            Name = couponInfo.Name,
                            ShopPrice =
                                shopPrice,
                            CostPrice =
                                Math.Round(couponInfo.CostPrice, 2, MidpointRounding.AwayFromZero),
                            Weight = 0,
                            ShowImg = couponInfo.CouponImg,
                            UId = user.UId,
                            RealCount = request.Count,
                            BuyCount = request.Count,
                            AddTime = DateTime.Now,
                            SendCount = 0,
                            PSn = "",
                            ShipFee = 0
                        };

                        //获取运费
                        model.ShipFee = _shippingTemplatesService.CouponCalculateFreight(model, fullShipAddressInfo); 
                        
                        orderProductList.Add(model);

                        //计算商品合计金额
                        var productAmount = couponInfo.Money * request.Count;
                        if (couponInfo.IsCostPrice == (int) WhetherType.Yes)
                        {
                            productAmount = couponInfo.CostPrice * request.Count;
                        }

                        //开启事务
                        using (var scope = new TransactionScope())
                        {
                            try
                            {

                                //验证已经通过,进行订单保存
                                var orderInfo = _orderService.CreateOrder(user.UId,
                                    orderProductList, fullShipAddressInfo, 0, request.BuyerRemark,
                                    _workContext.Ip, (byte)WithdrawAuditType.Wechat, (byte)OrderType.BuyCardOrder, productAmount);

                                //提交事务
                                var succ = _unitOfWork.Commit();
                                if (succ > 0)
                                {
                                    _orderProducts.BatchAddOrderProducts(orderProductList.Select(
                                            x => new LF_OrderProducts()
                                            {
                                                PId = x.PId,
                                                CateId = x.CateId,
                                                BrandId = x.BrandId,
                                                Name = x.Name,
                                                ShopPrice = x.ShopPrice,
                                                CostPrice = x.CostPrice,
                                                Weight = x.Weight,
                                                ShowImg = x.ShowImg,
                                                UId = user.UId,
                                                RealCount = x.RealCount,
                                                BuyCount = x.BuyCount,
                                                SendCount = 0,
                                                AddTime = DateTime.Now,
                                                OId = orderInfo.OId,
                                                ShipFee = x.ShipFee,
                                                PSn = x.PSn,
                                                SId = "0",
                                            })
                                        .ToList());

                                    //创建订单处理
                                    _orderActionsService.AddOrderActions(new LF_OrderActions()
                                    {
                                        OId = orderInfo.OId,
                                        UId = user.UId,
                                        RealName = "本人",
                                        AdminGId = 0,
                                        AdminGTitle = "非管理员",
                                        ActionType = (int)OrderActionType.Submit,
                                        ActionTime = DateTime.Now,
                                        ActionDes = orderInfo.OrderState == (int)OrderState.WaitPaying
                                            ? "您提交了订单，等待您付款"
                                            : "您提交了订单，请等待系统确认"
                                    });

                                    _unitOfWork.Commit();
                                    scope.Complete();
                                    var response = new SubmitOrderResponse();
                                    //获取回调地址
                                    var notifyUrl = FileHelper.GetFileFullUrl(ConfigMap.WeXinPayNotifyUrl);

                                    if (!string.IsNullOrEmpty(request.Code))
                                    {
                                        //获取JsApi参数列表
                                        var jsonResult = OAuthApi.GetAccessToken(ConfigMap.AppId, ConfigMap.AppSecret, request.Code);
                                        response.JsApiParameter = _wxpayService.GetJsApiParams(jsonResult.openid, "订单提交", orderInfo.SurplusMoney, orderInfo.Ip.Trim(), notifyUrl, orderInfo.OSn.Trim());
                                    }
                                    else
                                    {
                                        response.WechatParameter = _wxpayService.GetWebAppParams("订单提交", orderInfo.SurplusMoney, orderInfo.Ip.Trim(), notifyUrl, orderInfo.OSn.Trim());
                                    }

                                    return BuildResponse.SuccessResponse(response);

                                }
                            }
                            catch (Exception ex)
                            {
                                ApiLogger.Error("SubmitBuyCardOrder", requestString, "购买礼品卡错误", ex);
                                return BuildResponse.FailResponse<SubmitOrderResponse>("购买礼品卡失败");
                            }
                            finally
                            {
                                scope.Dispose();
                            }
                        }
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<SubmitOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("SubmitBuyCardOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<SubmitOrderResponse>("购买礼品卡错误");
            }
        }

        #endregion

        #region 确认购买礼品卡订单

        /// <summary>
        /// 确认购买礼品卡订单
        /// </summary>
        public BaseResponse<ConfirmOrderResponse> ConfirmBuyCardOrder(SubmitBuyCardOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                    var couponInfo = _couponTypes.LoadEntitieNoTracking(p => p.CouponTypeId == request.CouponTypeId && p.State == (byte)WhetherType.Yes);
                    if (couponInfo == null)
                    {
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("不存在该礼品卡");
                    }

                    var orderProductList = new List<LF_OrderProducts>();

                    var shopPrice = couponInfo.IsCostPrice == (int)WhetherType.Yes
                        ? Math.Round(couponInfo.CostPrice, 2, MidpointRounding.AwayFromZero)
                        : Math.Round(couponInfo.Money, 2, MidpointRounding.AwayFromZero);

                    //添加新商品到购物车,不进行持久化
                    var model = new LF_OrderProducts()
                    {
                        PId = couponInfo.CouponTypeId,
                        CateId = 0,
                        BrandId = 0,
                        Name = couponInfo.Name,
                        ShopPrice =
                            shopPrice,
                        CostPrice =
                            Math.Round(couponInfo.CostPrice, 2, MidpointRounding.AwayFromZero),
                        Weight = 0,
                        ShowImg = FileHelper.GetFileFullUrl(couponInfo.CouponImg),
                        UId = user.UId,
                        RealCount = request.Count,
                        BuyCount = request.Count,
                        AddTime = DateTime.Now,
                        SendCount = 0,
                        PSn = "",
                        ShipFee = 0
                    };

                    orderProductList.Add(model);
                    //购物车信息
                    CartInfo cartInfo = _orderProducts.TidyOrderProductList(orderProductList.Select(x => x.PId).ToList(), orderProductList);
                    if (cartInfo.SelectedOrderProductList.Count < 1)
                        return BuildResponse.FailResponse<ConfirmOrderResponse>("请先选择购物车商品");

                    //返回响应数据
                    var response = new ConfirmOrderResponse
                    {
                        CartInfo = cartInfo
                    };

                    //获取收货地址
                    if (request.SaId > 0)
                        response.DefaultFullShipAddressInfo =
                            _shipAddressesService.GetFullShipAddressBySAId(user.UId,request.SaId);
                    if (response.DefaultFullShipAddressInfo == null)
                        response.DefaultFullShipAddressInfo =
                            _shipAddressesService.GetDefaultFullShipAddress(user.UId);

                    //计算商品合计金额
                    response.ProductAmount = request.Count * shopPrice;

                    //获取运费
                    response.ShipFee =
                        _shippingTemplatesService.CouponCalculateFreight(model, response.DefaultFullShipAddressInfo);
                    response.TotalCount += cartInfo.SelectedOrderProductList.Sum(x => x.RealCount);
                    response.OrderAmount = response.ProductAmount + response.ShipFee;

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ConfirmOrderResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ConfirmBuyCardOrder", requestString, ex);
                return BuildResponse.FailResponse<ConfirmOrderResponse>("确认购买礼品卡订单错误");
            }
        }

        #endregion

        /// <summary>
        /// 获取模型验证错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        [System.Web.Http.NonAction]
        private string GetModelErrorMsg()
        {
            //错误信息
            var errorMessage = string.Empty;

            //获取所有错误的Key
            var key = ModelState.Keys.FirstOrDefault();
            if (key != null)
            {
                //获取第一个key对应的ModelStateDictionary的第一条错误信息
                var error = ModelState[key].Errors.FirstOrDefault();

                //将错误描述添加到sb中
                errorMessage = error?.ErrorMessage;
            }

            //返回错误信息
            return errorMessage;
        }
    }
}
