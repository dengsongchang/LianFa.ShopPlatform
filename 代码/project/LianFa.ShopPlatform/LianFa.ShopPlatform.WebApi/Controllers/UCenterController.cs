using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Security.Tokens;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Client.Order;
using LianFa.ShopPlatform.Model.Request.Client.UCenter;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Model.Response.Client.User;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 用户中心控制器
    /// </summary>
    [Signature]
    [ControllerGroup("用户中心接口", "用于获取用户数据,用户订单等")]
    public class UCenterController : ApiController
    {
        /// <summary>
        /// 上下文
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 会员管理
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 订单服务
        /// </summary>
        private readonly IOrdersService _orders;

        /// <summary>
        /// 卡片服务
        /// </summary>
        private readonly ICouponsService _coupons;

        /// <summary>
        /// 订单动作服务
        /// </summary>
        private readonly IOrderActionsService _orderActions;

        /// <summary>
        /// 订单商品服务
        /// </summary>
        private readonly IOrderProductsService _orderProductsService;

        /// <summary>
        /// 收货地址服务
        /// </summary>
        private readonly IShipAddressesService _shipAddresses;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 区域服务
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 会员管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="workContext"></param>
        /// <param name="users"></param>
        /// <param name="orders"></param>
        /// <param name="orderActions"></param>
        /// <param name="orderProductsService"></param>
        /// <param name="shipCompanies"></param>
        /// <param name="regions"></param>
        /// <param name="coupons"></param>
        /// <param name="shipAddresses"></param>
        public UCenterController(IUnitOfWork unitOfWork, IWorkContext workContext, IUsersService users,
             IOrdersService orders, ICouponsService coupons, IShipAddressesService shipAddresses,
            IOrderActionsService orderActions, IOrderProductsService orderProductsService, IShipCompaniesService shipCompanies, IRegionsService regions
           )
        {
            _unitOfWork = unitOfWork;
            _workContext = workContext;
            _users = users;
            _orders = orders;
            _orderActions = orderActions;
            _orderProductsService = orderProductsService;
            _regions = regions;
            _coupons = coupons;
            _shipAddresses = shipAddresses;
        }

        #region 用户中心数据

        /// <summary>
        /// 用户中心数据
        /// </summary>
        /// <returns></returns>
        public BaseResponse<UCenterDataResponse> UCenterData()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取用户
                    var user = _users.GetUserInfo(_workContext.CurrentUser.UId);
                    if (user == null)
                    {
                        return BuildResponse.FailResponse<UCenterDataResponse>("用户不存在");
                    }
                    var address = _shipAddresses.UCenterGetShipAddressList(_workContext.CurrentUser.UId);
                    var model = new UCenterDataResponse()
                    {
                        Info = user,
                        AddressList = address
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<UCenterDataResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("UCenterData", "", ex.Message, ex);
                return BuildResponse.FailResponse<UCenterDataResponse>("获取用户中心数据错误");
            }
        }

        #endregion

        #region  编辑用户

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<object> EditUser(EditUserRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得用户信息
                    var user = _users.GetUsersById(_workContext.CurrentUser.UId);
                    if (user == null)
                    {
                        return BuildResponse.FailResponse<object>("该用户不存在");
                    }
                    if (user.NickName == request.NickName && user.Avatar == request.Avatar)
                        return BuildResponse.SuccessResponse<object>("编辑用户成功");
                    //手机号判重
                    if (_users.Exist(u => u.UId != _workContext.CurrentUser.UId && u.Mobile == request.Mobile))
                        return BuildResponse.FailResponse<object>("该手机号已存在");

                    //编辑用户
                    user.NickName = request.NickName;
                    user.Avatar = request.Avatar;
                    user.Mobile = request.Mobile;
                    _users.UpdateUsers(user);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    if (result > 0)
                    {
                        return BuildResponse.SuccessResponse<object>("编辑用户成功");
                    }
                    return BuildResponse.FailResponse<object>("编辑用户失败");

                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("EditUser", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("编辑用户错误");
            }
        }

        #endregion

        #region 上传用户中心数据图片
        /// <summary>
        /// 上传用户中心数据图片
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse<string>> UploadUsersImg()
        {
            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/Users/UsersImg/";

                //异步上传图片
                var result = await new UploadFile().ImgUpload(Request.Content, saveTempPath);

                //获取返回结果
                var responseCode = Convert.ToInt32(result["Code"].ToString());
                if (responseCode == (int)ResponseCode.UploadSuccess)
                {
                    //获取上传的文件名
                    var data = result["Data"].ToString();
                    //返回成功结果
                    return BuildResponse.SuccessResponse(data, ResponseCode.UploadSuccess.GetDescription(), ResponseCode.UploadSuccess);
                }

                //返回失败结果
                return BuildResponse.FailResponse<string>(result["Message"].ToString(), (ResponseCode)responseCode);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("UploadUsersImg", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("上传个人中心图片错误");
            }
        }
        #endregion

        #region 订单列表

        /// <summary>
        /// 订单列表
        /// </summary>
        public BaseResponse<OrderListResponse> OrderList(OrderListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    OrderListResponse model = new OrderListResponse()
                    {
                        OrderList = _orders.GetOrderList(request.Page, request.Type, request.orderState, _workContext.CurrentUser.UId, out total),
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<OrderListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("OrderList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<OrderListResponse>("订单列表错误");
            }
        }

        #endregion

        #region 卡片订单列表

        /// <summary>
        /// 卡片订单列表
        /// </summary>
        public BaseResponse<CouponOrderListResponse> CouponOrderList(CouponOrderListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    CouponOrderListResponse model = new CouponOrderListResponse()
                    {
                        CouponOrderList = _orders.GetCouponOrderList(request.Page, request.OrderState, _workContext.CurrentUser.UId, out total),
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CouponOrderListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CouponOrderList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<CouponOrderListResponse>("卡片订单列表错误");
            }
        }

        #endregion

        #region 订单信息
        /// <summary>
        /// 订单信息
        /// </summary>
        public BaseResponse<OrderInfoResponse> OrderInfo(OrderInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    var ordersd = _orders.GetOrderInfo(request.oid);
                    if (ordersd == null)
                        return BuildResponse.FailResponse<OrderInfoResponse>("订单不存在");

                    OrderInfoResponse model = new OrderInfoResponse()
                    {
                        OrderInfo = ordersd,
                        ProductsList = _orders.GetProductsList(request.oid),
                        OrderAddressInfo = _orders.GetOrderAddressInfo(request.oid),
                    };

                    model.OrderInfo.OrderStateDesc = ((OrderState)model.OrderInfo.OrderState).GetDescription();
                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<OrderInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("OrderInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<OrderInfoResponse>("订单信息错误");
            }
        }
        #endregion

        #region 兑换订单信息
        /// <summary>
        /// 兑换订单信息
        /// </summary>
        public BaseResponse<CouponsOrderInfoResponse> CouponsOrderInfo(CouponsOrderInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    var orders = _orders.GetOrderInfo(request.oid);
                    if (orders == null)
                        return BuildResponse.FailResponse<CouponsOrderInfoResponse>("订单不存在");

                    CouponsOrderInfoResponse model = new CouponsOrderInfoResponse()
                    {
                        OrderInfo = orders,
                        ProductsList = _orders.GetProductsList(request.oid),
                        OrderAddressInfo = _orders.GetOrderAddressInfo(request.oid),
                        CouponInfo = _orders.GetCouponsInfo(request.oid),
                        OrdersLogisticsInfo = _orders.GetOrderLogisticsInfo(request.oid)
                    };
                    model.OrderInfo.OrderStateDesc = ((OrderState)model.OrderInfo.OrderState).GetDescription();
                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CouponsOrderInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("OrderInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<CouponsOrderInfoResponse>("兑换订单信息错误");
            }
        }
        #endregion

        #region 订单物流
        /// <summary>
        ///订单物流
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public BaseResponse<OrderLogisticsResponse> OrderLogistics(OrderLogisticsRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.GetOrdersById(request.Oid);
                    if (orderInfo == null)
                        return BuildResponse.FailResponse<OrderLogisticsResponse>("查询订单物流失败没有找到此订单信息");

                    OrderLogisticsResponse model = new OrderLogisticsResponse()
                    {
                        OrdersLogisticsInfo = _orders.GetOrdersLogisticsInfo(request.Oid, _workContext.CurrentUser.UId, request.OLId)
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<OrderLogisticsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("OrderLogistics", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<OrderLogisticsResponse>("订单物流信息错误");
            }
        }
        #endregion

        #region 取消订单
        /// <summary>
        /// 取消订单
        /// </summary>
        public BaseResponse<object> CancelOrder(CancelOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.LoadEntitieNoTracking(x => x.OId == request.oid);
                    if (orderInfo == null || orderInfo.UId != _workContext.CurrentUser.UId)
                        return BuildResponse.FailResponse<object>("订单不存在");

                    if (orderInfo.OrderState != (int)OrderState.WaitPaying)
                        return BuildResponse.FailResponse<object>("订单当前不能取消");

                    //取消订单
                    _orders.CancelOrder(orderInfo.OId, orderInfo.UId);

                    //创建订单处理
                    _orders.CreateOrderAction(orderInfo.OId, orderInfo.UId, OrderActionType.Cancel, "您取消了订单");
                    int row = _unitOfWork.Commit();
                    if (row > 0)
                        return BuildResponse.SuccessResponse<object>("取消订单成功");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CancelOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("取消订单错误");
            }
        }
        #endregion 

        #region 删除订单
        /// <summary>
        /// 删除订单
        /// </summary>
        public BaseResponse<object> DelOrder(CancelOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.LoadEntitieNoTracking(x => x.OId == request.oid);
                    if (orderInfo == null || orderInfo.UId != _workContext.CurrentUser.UId)
                        return BuildResponse.FailResponse<object>("订单不存在");

                    var data = _orders.Get(d => d.OId == request.oid);
                    data.IsDel = true;
                    _orders.UpdateOrders(data);

                    int row = _unitOfWork.Commit();
                    return row > 0
                        ? BuildResponse.SuccessResponse<object>("删除订单成功")
                        : BuildResponse.FailResponse<object>("删除订单失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("DelOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除订单错误");
            }
        }
        #endregion

        #region 收货

        /// <summary>
        /// 收货
        /// </summary>
        /// <returns></returns>
        public BaseResponse<object> ReceiveOrder(ReceiveOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.GetOrdersById(request.oid);
                    if (orderInfo == null || orderInfo.UId != _workContext.CurrentUser.UId)
                        return BuildResponse.FailResponse<object>("订单不存在");

                    string orderstatedec = ((OrderState)orderInfo.OrderState).GetDescription();
                    if (orderInfo.OrderState != (int)OrderState.Send)
                        return BuildResponse.FailResponse<object>("订单" + orderstatedec + ",不能收货");

                    //收货
                    _orders.ReceiveOrder(orderInfo, DateTime.Now, _workContext.Ip);

                    //创建订单处理
                    _orders.CreateOrderAction(orderInfo.OId, orderInfo.UId, OrderActionType.Receive, "您已经收货");
                    int row = _unitOfWork.Commit();
                    if (row > 0)
                        return BuildResponse.SuccessResponse<object>("确认收货成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ReceiveOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("收货错误");
            }
        }
        #endregion

        #region 各订单状态统计

        /// <summary>
        /// 各订单状态统计
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SelOrderStateStatisticsResponse> SelOrderStateStatistics(SelOrderStateStatisticsRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    SelOrderStateStatisticsResponse model = new SelOrderStateStatisticsResponse()
                    {
                        SelOrderStateStatistics = _orders.GetSelOrderStateStatistics(_workContext.CurrentUser.UId)
                    };
                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<SelOrderStateStatisticsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("SelOrderStateStatistics", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<SelOrderStateStatisticsResponse>("查询各订单状态统计错误");
            }
        }

        #endregion

        #region 订单物流列表

        /// <summary>
        /// 订单物流列表
        /// </summary>
        public BaseResponse<OrderLogisticsListResponse> OrderLogisticsList(OrderLogisticsListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    bool exist = _orders.Exist(d => d.OId == request.OId && d.UId == _workContext.CurrentUser.UId);
                    if (exist == false)
                        return BuildResponse.FailResponse<OrderLogisticsListResponse>("订单不存在");

                    OrderLogisticsListResponse model = new OrderLogisticsListResponse();
                    model.OrderLogisticsList = _orders.GetOrderLogistics(request.OId, _workContext.CurrentUser.UId);

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<OrderLogisticsListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("OrderLogisticsList", requestString, ex);
                return BuildResponse.FailResponse<OrderLogisticsListResponse>("查询订单物流列表错误");
            }
        }

        #endregion

        /// <summary>
        /// 获取模型验证错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        [NonAction]
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
