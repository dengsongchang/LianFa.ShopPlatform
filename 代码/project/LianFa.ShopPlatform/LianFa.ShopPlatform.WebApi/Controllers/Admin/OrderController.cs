using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Offices;
using HuCheng.Util.Office.Npoi.Excel;
using LianFa.ShopPlatform.Code.Deppon;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Code.KuaiDi100;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.AfterSalesService;
using LianFa.ShopPlatform.Model.Request.Admin.Orders;
using LianFa.ShopPlatform.Model.Request.Admin.Regions;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.AfterSalesService;
using LianFa.ShopPlatform.Model.Response.Admin.Orders;
using LianFa.ShopPlatform.Model.Response.Admin.Regions;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台订单相控制器
    /// </summary>
    [ControllerGroup("后台订单相关接口", "用于后台订单管理操作")]
    [RoutePrefix("api/admin/order")]
    public class OrderController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 订单服务
        /// </summary>
        private readonly IOrdersService _orders;

        /// <summary>
        /// 订单动作服务
        /// </summary>
        private readonly IOrderActionsService _orderActions;

        /// <summary>
        /// 区域服务
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 配送公司服务
        /// </summary>
        private readonly IShipCompaniesService _shipcompanies;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台订单控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="orders"></param>
        /// <param name="orderActions"></param>
        /// <param name="regions"></param>
        /// <param name="shipcompanies"></param>
        public OrderController(IUnitOfWork unitOfWork, IOrdersService orders, IOrderActionsService orderActions,
            IRegionsService regions, IShipCompaniesService shipcompanies)
        {
            _orders = orders;
            _unitOfWork = unitOfWork;
            _orderActions = orderActions;
            _regions = regions;
            _shipcompanies = shipcompanies;
        }

        #region 后台获取普通类型订单列表

        /// <summary>
        /// 后台获取全部类型订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminOrderList")]
        public BaseResponse<AdminOrderListModelResponse> AdminOrderList(AdminOrdertListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;

                    //后台获取全部类型订单列表
                    var orderTlb = _orders.GetOrdersList(request, out total);

                    AdminOrderListModelResponse model = new AdminOrderListModelResponse()
                    {
                        OrderList = orderTlb,
                        Total = total,
                        TotalAmount = orderTlb.Sum(d => d.OrderAmount)
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminOrderListModelResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrderList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminOrderListModelResponse>("订单列表");
            }
        }

        #endregion

        #region 后台获取卡片订单列表

        /// <summary>
        /// 后台获取卡片订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminCouponsOrderList")]
        public BaseResponse<AdminCouponsOrderListModelResponse> AdminCouponsOrderList(AdminCouponsOrdertListReques request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;

                    //后台获取卡片订单列表
                    var orderCoupons = _orders.GetAdminCouponsOrderList(request, out total);

                    AdminCouponsOrderListModelResponse model = new AdminCouponsOrderListModelResponse()
                    {
                        OrderList = orderCoupons,
                        Total = total,
                        TotalAmount = orderCoupons.Sum(d => d.OrderAmount)
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminCouponsOrderListModelResponse>(errorMessage,
                    ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrderList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminCouponsOrderListModelResponse>("卡片订单列表");
            }
        }

        #endregion

        #region 后台导出订单列表

        /// <summary>
        /// 后台导出订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("ExportAdminOrdesList")]
        public BaseResponse<string> ExportAdminOrdesList(AdminExportAdminOrdersListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    //获取订单列表
                    var ordersList = _orders.GetAdminExportOrderList(request);
                    //Excel头部
                    var headList = new List<CellHead>
                    {
                        new CellHead
                        {
                            Head = "订单ID"
                        },
                        new CellHead
                        {
                            Head = "订单编号",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "订单状态",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "收款日期",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "发货日期",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "完成日期",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "销售平台",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "商品名称",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "数量"
                        },
                        new CellHead
                        {
                            Head = "单价",
                            ColumnWidth = 10
                        },
                        new CellHead
                        {
                            Head = "货款",
                            ColumnWidth = 10
                        },
                        new CellHead
                        {
                            Head = "运费",
                            ColumnWidth = 10
                        },
                        new CellHead
                        {
                            Head = "实收金额",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "买家帐号",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "收件人姓名",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "联系电话",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "收货地址",
                            ColumnWidth = 50
                        }
                    };

                    var saveTempPath = "/upload/excel/temp/";
                    //获取导出Excel的地址
                    var excelUrl = ExcelExport.ExportCustomCellHeadExcel(headList, ordersList, saveTempPath);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(FileHelper.GetFileFullUrl(excelUrl));
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ExportAdminOrdesList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导出订单列表错误");
            }
        }

        #endregion

        #region 后台导出卡片订单列表

        /// <summary>
        /// 后台导出卡片订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("ExportAdminCouponsOrdesList")]
        public BaseResponse<string> ExportAdminCouponsOrdesList(AdminCouponsOrdertListReques request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取订单列表
                    var orderCouponsList = _orders.GetAdminExportCouponOrderList(request);
                    //Excel头部
                    var headList = new List<CellHead>
                    {
                        new CellHead
                        {
                            Head = "订单ID"
                        },
                        new CellHead
                        {
                            Head = "订单编号",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "订单状态",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "收款日期",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "发货日期",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "完成日期",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "销售平台",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "优惠卡名称",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "序列号",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "密码",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "买家帐号",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "收件人姓名",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "联系电话",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "收货地址",
                            ColumnWidth = 50
                        },
                        new CellHead
                        {
                            Head = "买家备注",
                            ColumnWidth = 50
                        },
                        new CellHead
                        {
                            Head = "礼品内容",
                            ColumnWidth = 50
                        }
                    };

                    var saveTempPath = "/upload/excel/temp/";
                    //获取导出Excel的地址
                    var excelUrl = ExcelExport.ExportCustomCellHeadExcel(headList, orderCouponsList, saveTempPath);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(FileHelper.GetFileFullUrl(excelUrl));
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ExportAdminCouponsOrdesList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导出订单列表错误");
            }
        }

        #endregion

        #region 订单信息详情

        /// <summary>
        /// 订单信息详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminOrderInfo")]
        public BaseResponse<AdminOrderInfoModelResaponse> AdminOrderInfo(AdminOrderInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.Get(d => d.OId == request.oid);
                    AdminOrderInfoModelResaponse model = new AdminOrderInfoModelResaponse()
                    {
                        TakeOutOrderInfo = _orders.GetTakeOutOrderInfo(request.oid), //订单基本信息
                        OrderProductList = _orders.GetAdminOrderProductsList(orderInfo.OId), //订单商品信息
                        OrderActionInfo = _orders.GetOrderActionList(request.oid), //订单动作列表
                        // OrderLogisticsList = _orders.GetAdminOrderLogistics(request.oid),//订单物流列表
                        Logistics = DepponApi.NewTraceQuery(orderInfo.ShipSn.Trim())
                    };
                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminOrderInfoModelResaponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrderInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminOrderInfoModelResaponse>("订单详细信息错误");
            }
        }

        #endregion

        #region 卡片订单信息详情

        /// <summary>
        /// 卡片订单信息详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminCouponsOrderInfo")]
        public BaseResponse<AdminCouponsOrderInfoModelResponse> AdminCouponsOrderInfo(AdminOrderInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var ordersd = _orders.GetOrderInfo(request.oid);
                    if (ordersd == null)
                        return BuildResponse.FailResponse<AdminCouponsOrderInfoModelResponse>("订单不存在");
                    AdminCouponsOrderInfoModelResponse model = new AdminCouponsOrderInfoModelResponse()
                    {
                        OrderInfo = ordersd,
                        ProductsList = _orders.GetProductsList(request.oid),
                        OrderAddressInfo = _orders.GetOrderAddressInfo(request.oid),
                        CouponInfo = _orders.GetAdminCouponsInfo(request.oid),
                        OrdersLogisticsInfo = _orders.GetOrderLogisticsInfo(request.oid),
                    };
                    model.OrderInfo.OrderStateDesc = ((OrderState)model.OrderInfo.OrderState).GetDescription();

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminCouponsOrderInfoModelResponse>(errorMessage,
                    ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrderInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminCouponsOrderInfoModelResponse>("卡片订单详细信息错误");
            }
        }

        #endregion

        #region 操作订单

        /// <summary>
        /// 操作订单
        /// </summary>
        [Route("AdminOperateOrder")]
        public BaseResponse<AdminOperateOrderModelResponse> AdminOperateOrder(AdminOperateOrderActionRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.GetOrdersById(request.oid);
                    if (orderInfo == null)
                        return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("订单不存在", ResponseCode.Success);

                    if (request.actionDes.Length > 125)
                    {
                        var model = new AdminOperateOrderModelResponse
                        {
                            OrderInfo = orderInfo,
                            OrderActionType = (OrderActionType)request.actionType,
                            ActionDes = request.actionDes
                        };


                        ModelState.AddModelError("actionDes", "最多只能输入125个字");
                        return BuildResponse.SuccessResponse(model);
                    }

                    OrderActionType orderActionType = (OrderActionType)request.actionType;
                    OrderState orderState = (OrderState)orderInfo.OrderState;

                    if (orderActionType == OrderActionType.Confirm)//确认订单
                    {
                        if (orderState != OrderState.PreProduct)
                            return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("状态，不能确认订单");

                        _orders.UpOrderState(orderInfo.OId, OrderState.Received);
                        _orders.CreateOrderAction(request.oid, orderActionType, request.actionDes.Length == 0 ? "您的订单已经确认" : request.actionDes);
                    }
                    else if (orderActionType == OrderActionType.PreProduct)//备货
                    {
                        if (orderState != OrderState.Confirmed)
                            return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("订单还未确认，不能备货");

                        _orders.UpOrderState(request.oid, OrderState.PreProducting);
                        _orders.CreateOrderAction(request.oid, orderActionType, request.actionDes.Length == 0 ? "您的订单正在备货" : request.actionDes);
                    }
                    else if (orderActionType == OrderActionType.Send)//发货
                    {
                        if (orderState != OrderState.PreProduct)
                            return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("订单还未备货，不能发货");

                        var shipSn = request.ShipSn;
                        var shipCom = request.ShipCom;
                        if (shipSn.Length < 1)
                        {
                            AdminOperateOrderModelResponse model =
                                new AdminOperateOrderModelResponse
                                {
                                    OrderInfo = orderInfo,
                                    OrderActionType = orderActionType,
                                    ActionDes = request.actionDes
                                };

                            ModelState.AddModelError("shipSN", "请填写配送单号");
                            return BuildResponse.SuccessResponse(model);
                        }
                        _orders.SendOrder(request.oid, OrderState.Send, shipSn, shipCom, DateTime.Now);//发货                       
                        _orders.CreateOrderAction(request.oid, orderActionType, request.actionDes.Length == 0 ? "您的订单已经发货,发货方式为:" + orderInfo.ShipFriendName + "，单号为：" + shipSn : request.actionDes);

                        ////发送订单发货模板消息    
                        ////获取模板id 如果不为空说明设置了发送模板消息
                        //var orderShippingId = ConfigHelper.ReadConfig("Template", "configuration/OrderShippingId");
                        //if (orderShippingId != "")
                        //{
                        //    //获取订单商品名称
                        //    var pNameList = _orderProducts.GetList(m => m.OId == orderInfo.OId).Select(m => m.Name).ToArray();

                        //    //发送模板消息给用户
                        //    var data = new
                        //    {
                        //        //快递公司
                        //        keyword1 = new TemplateDataItem(orderInfo.ShipFriendName),
                        //        //快递单号
                        //        keyword2 = new TemplateDataItem(shipSN),
                        //        //购买时间
                        //        keyword3 = new TemplateDataItem(orderInfo.PayTime.ToShortDateString()),
                        //        //物品名称
                        //        keyword4 = new TemplateDataItem(CommonHelper.StringArrayToString(pNameList)),
                        //        //发货时间
                        //        keyword5 = new TemplateDataItem(DateTime.Now.ToShortDateString()),
                        //        //订单号
                        //        keyword6 = new TemplateDataItem(orderInfo.OSn),
                        //    };

                        //    var accessToken = AccessTokenContainer.TryGetAccessToken(_appId, _secret);

                        //    //获取该订单的用户openId
                        //    var openId = _oauth.GetOauthById(orderInfo.UId).OpenId;
                        //    //var openId = "ofMEd0d_noDVRzq-xFOL7f9FRH2M";
                        //    //获取formId
                        //    var formId = CacheHelper.GetCache("OrderShipping_FormId" + orderInfo.UId) as string;
                        //    if (formId != "")
                        //    {
                        //        //获取微信授权码
                        //        Senparc.Weixin.WxOpen.AdvancedAPIs.Template.TemplateApi.SendTemplateMessage(accessToken, openId, orderShippingId, data, formId);
                        //    }
                        //}
                    }
                    else if (orderActionType == OrderActionType.Lock)//锁定订单
                    {
                        if (!(orderState == OrderState.WaitPaying || (orderState == OrderState.Confirming && orderInfo.PayMode == 0)))
                            return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("订单当前不能锁定");

                        _orders.CreateOrderAction(request.oid, orderActionType, "订单已锁定：" + request.actionDes);
                    }
                    else if (orderActionType == OrderActionType.Cancel)//取消订单
                    {
                        if (!(orderState == OrderState.WaitPaying || (orderState == OrderState.Confirming && orderInfo.PayMode == 0)))
                            return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("订单当前不能取消");

                        _orders.CancelOrder(orderInfo.OId, orderInfo.UId);
                        _orders.CreateOrderAction(request.oid, orderActionType, request.actionDes.Length == 0 ? "订单已取消" : request.actionDes);
                    }
                    else
                    {
                        return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("当前操作不存在");
                    }

                    _unitOfWork.Commit();//提交事务

                    return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("操作已完成", ResponseCode.Success);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminOperateOrderModelResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOperateOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminOperateOrderModelResponse>("操作订单");
            }
        }

        #endregion

        #region 获取发货订单基本信息

        /// <summary>
        /// 获取发货订单基本信息
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetDeliveryList")]
        public BaseResponse<AdminGetDeliveryListResponse> AdminGetDeliveryList(AdminGetDeliveryListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    AdminGetDeliveryListResponse model = new AdminGetDeliveryListResponse
                    {
                        DeliveryList = _orders.GetDeliveryList(request.oid),
                        ShipCompaniesList = _shipcompanies.GetAll().Select(d => new ShipCompaniesList
                        { Name = d.Name.Trim(), ShipCoId = d.ShipCoId }).ToList()
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetDeliveryListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetDeliveryList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetDeliveryListResponse>("获取发货订单基本信息错误");
            }
        }

        #endregion

        #region 获取卡片订单发货订单基本信息

        /// <summary>
        /// 获取卡片订单发货订单基本信息
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetCouponsDeliveryList")]
        public BaseResponse<AdminGetDeliveryListResponse> AdminGetCouponsDeliveryList(AdminGetDeliveryListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    AdminGetDeliveryListResponse model = new AdminGetDeliveryListResponse
                    {
                        DeliveryList = _orders.GetCouponsDeliveryList(request.oid),
                        ShipCompaniesList = _shipcompanies.GetAll().Select(d => new ShipCompaniesList
                        { Name = d.Name.Trim(), ShipCoId = d.ShipCoId }).ToList()
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetDeliveryListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetCouponsDeliveryList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetDeliveryListResponse>("获取发货订单基本信息错误");
            }
        }

        #endregion

        #region 删除订单

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <returns></returns>
        [Route("AdminDelOrders")]
        [AddOperateLog("删除订单")]
        public BaseResponse<object> AdminDelOrders(AdminDelOrdesRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < request.oid.Count(); i++)
                    {
                        int oid = request.oid[i];
                        var data = _orders.Get(d => d.OId == oid);
                        data.IsDel = true;
                        _orders.UpdateOrders(data);
                    }

                    int row = _unitOfWork.Commit();
                    if (row > 0)
                    {
                        return BuildResponse.SuccessResponse<object>("删除成功!");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelOrders", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除订单出错");
            }
        }

        #endregion

        #region 后台获取省/自治区/直辖市列表

        /// <summary>
        /// 后台获取省/自治区/直辖市列表
        /// </summary>
        /// <returns></returns>
        [Route("AdminProvinceList")]
        public BaseResponse<AdminRegionsResponse> AdminProvinceList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _regions.GetList(d => d.Layer == 1)
                        .Select(d => new RegionsList { Name = d.Name.Trim(), RegionId = d.RegionId }).ToList();

                    var model = new AdminRegionsResponse { RegionsList = data };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminProvinceList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询省/自治区/直辖市列表出错");
            }
        }

        #endregion

        #region 后台获取地级市列表

        /// <summary>
        /// 后台获取地级市列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminCityList")]
        public BaseResponse<AdminRegionsResponse> AdminCityList(AdminCityRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _regions.GetList(d => d.Layer == 2 && d.ParentId == request.ParentId)
                        .Select(d => new RegionsList { Name = d.Name.Trim(), RegionId = d.RegionId }).ToList();

                    AdminRegionsResponse model = new AdminRegionsResponse();
                    model.RegionsList = data;

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminCityList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询地级市列表出错");
            }
        }

        #endregion

        #region 后台获取市辖区/县(县市级)列表

        /// <summary>
        /// 后台获取市辖区/县(县市级)列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminMunicipalDistrictList")]
        public BaseResponse<AdminRegionsResponse> AdminMunicipalDistrictList(AdminCityRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _regions.GetList(d => d.Layer == 3 && d.ParentId == request.ParentId)
                        .Select(d => new RegionsList { Name = d.Name.Trim(), RegionId = d.RegionId }).ToList();

                    AdminRegionsResponse model = new AdminRegionsResponse();
                    model.RegionsList = data;

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminMunicipalDistrictList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询市辖区/县(县市级)列表出错");
            }
        }

        #endregion

        #region 后台全部乡/镇/街道列表

        /// <summary>
        /// 后台获取乡/镇/街道列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminVillagesTownsList")]
        public BaseResponse<AdminRegionsResponse> AdminVillagesTownsList(AdminCityRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _regions.GetList(d => d.Layer == 4 && d.ParentId == request.ParentId)
                        .Select(d => new RegionsList { Name = d.Name.Trim(), RegionId = d.RegionId }).ToList();

                    AdminRegionsResponse model = new AdminRegionsResponse();
                    model.RegionsList = data;

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminVillagesTownsList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询后台乡/镇/街道列表出错");
            }
        }

        #endregion

        #region 订单发货

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <returns></returns>
        [Route("AdminOrdersGoods")]
        [AddOperateLog("订单发货")]
        public BaseResponse<object> AdminOrdersGoods(AdminOrdersGoodsRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var order = _orders.GetOrdersById(request.OId);
                    if (order.OrderState != (byte)OrderState.PreProduct)
                    {
                        return BuildResponse.FailResponse<object>("订单状态不正确！");
                    }

                    #region 处理订单

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            //更新订单状态为配送中
                            order.OrderState = (int)OrderState.Send;
                            order.ShiPsyStemName = request.ShipFriendName;
                            order.ShipFriendName = request.ShipFriendName;
                            order.ShipSn = request.ShipCode;

                            //更新
                            _orders.UpdateOrders(order);

                            //创建订单处理
                            var orderAction = new LF_OrderActions
                            {
                                OId = order.OId,
                                UId = order.UId,
                                RealName = "",
                                AdminGId = 0,
                                AdminGTitle = "非管理员",
                                ActionType = (int)OrderActionType.Send,
                                ActionTime = DateTime.Now,
                                ActionDes = "订单已发货"
                            };
                            _orderActions.AddOrderActions(orderAction);

                            _unitOfWork.Commit();

                            //成功提交所有事务
                            scope.Complete();
                            ApiLogger.Info("AdminOrdersGoods", $"请求[{order.OId}]订单数据成功，更新物流成功");
                        }
                        catch (Exception ex)
                        {
                            ApiLogger.Error("AdminOrdersGoods", $"请求[{request.OId}]订单数据成功，更新物流失敗，已回滚", ex);
                        }
                        finally
                        {
                            scope.Dispose();
                        }
                    }

                    #endregion

                    return BuildResponse.SuccessResponse<object>("更新成功!");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrdersGoods", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("更新订单出错");
            }
        }


        /// <summary>
        /// 后台导出待发货订单列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("ExportAdminOrdesGoodsList")]
        public BaseResponse<string> ExportAdminOrdesGoodsList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    //获取订单列表
                    var ordersList = _orders.GetOrderGoodsExplist();

                    //Excel头部
                    var headList = new List<CellHead>
                    {
                        new CellHead
                        {
                            Head = "订单编号",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "物流公司",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "快递单号",
                            ColumnWidth = 20
                        }
                    };

                    var saveTempPath = "/upload/excel/temp/";
                    //获取导出Excel的地址
                    var excelUrl = ExcelExport.ExportCustomCellHeadExcel(headList, ordersList, saveTempPath);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(FileHelper.GetFileFullUrl(excelUrl));
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ExportAdminOrdesGoodsList", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导出待发货订单列表错误");
            }
        }

        /// <summary>
        /// 后台导出待发货订单列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("ExportAdminOrdesCardList")]
        public BaseResponse<string> ExportAdminOrdesCardList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    //获取订单列表
                    var ordersList = _orders.GetOrderCardExplist();

                    //Excel头部
                    var headList = new List<CellHead>
                    {
                        new CellHead
                        {
                            Head = "订单编号",
                            ColumnWidth = 20
                        },
                        new CellHead
                        {
                            Head = "物流公司",
                            ColumnWidth = 15
                        },
                        new CellHead
                        {
                            Head = "快递单号",
                            ColumnWidth = 20
                        }
                    };

                    var saveTempPath = "/upload/excel/temp/";
                    //获取导出Excel的地址
                    var excelUrl = ExcelExport.ExportCustomCellHeadExcel(headList, ordersList, saveTempPath);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(FileHelper.GetFileFullUrl(excelUrl));
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ExportAdminOrdesCardList", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导出待发货订单列表错误");
            }
        }

        /// <summary>
        /// 批量导入发货订单Excel数据
        /// </summary>
        [AllowAnonymous]
        [Route("BatchUploadAdminAdminOrdesGoods")]
        public async Task<BaseResponse<string>> BatchUploadAdminAdminOrdesGoods()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //上传文件路径
                    const string saveTempPath = "/upload/admin/Excel/";

                    //异步上传文件
                    var result = await new UploadFile().ExcelUploasd(Request.Content, saveTempPath);

                    //获取返回结果
                    var responseCode = Convert.ToInt32(result["Code"].ToString());
                    if (responseCode != (int)ResponseCode.UploadSuccess)
                    {
                        //返回失败结果
                        return BuildResponse.FailResponse<string>(result["Message"].ToString(), (ResponseCode)responseCode);
                    }

                    var excelUrl = result["Data"].ToString();

                    var filePath = AppDomain.CurrentDomain.BaseDirectory + excelUrl;

                    var excelObj = new ExcelHelper(filePath);

                    var table = excelObj.ExcelToDataTable(excelUrl, true);

                    var list = DataTableHelper.DataTableToList<BatchUploadOrderGoods>(table);

                    foreach (var info in list)
                    {
                        var order = _orders.Get(m => m.OSn.Contains(info.订单编号.Trim()));

                        if (order?.OrderState != (byte)OrderState.PreProduct || string.IsNullOrEmpty(info.快递单号) ||
                            string.IsNullOrEmpty(info.物流公司)) continue;

                        #region 处理订单

                        //开启事务
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                //更新订单状态为配送中
                                order.OrderState = (int)OrderState.Send;
                                order.ShiPsyStemName = info.物流公司;
                                order.ShipFriendName = info.物流公司;
                                order.ShipSn = info.快递单号;


                                //更新
                                _orders.UpdateOrders(order);

                                //创建订单处理
                                var orderAction = new LF_OrderActions
                                {
                                    OId = order.OId,
                                    UId = order.UId,
                                    RealName = "",
                                    AdminGId = 0,
                                    AdminGTitle = "非管理员",
                                    ActionType = (int)OrderActionType.Send,
                                    ActionTime = DateTime.Now,
                                    ActionDes = "订单已发货"
                                };
                                _orderActions.AddOrderActions(orderAction);
                                _unitOfWork.Commit();

                                //成功提交所有事务
                                scope.Complete();
                                ApiLogger.Info("AdminMerchantsOrdersGoods", $"请求[{order.OId}]订单数据成功，更新物流成功");
                            }
                            catch (Exception ex)
                            {
                                ApiLogger.Error("AdminMerchantsOrdersGoods", $"请求[{order.OId}]订单数据成功，更新物流失敗，已回滚", ex);
                            }
                            finally
                            {
                                scope.Dispose();
                            }
                        }

                        #endregion

                        //提交事务
                        _unitOfWork.Commit();
                    }
                    //返回成功结果
                    return BuildResponse.SuccessResponse("批量发货成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("BatchUploadAdminAdminOrdesGoods", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("批量发货错误");
            }
        }

        #endregion

        #region 订单物流

        /// <summary>
        ///订单物流
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [Route("AdminOrderLogistics")]
        public BaseResponse<AdminOrderLogisticsResponse> AdminOrderLogistics(AdminOrderLogisticsRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    var orderInfo = _orders.GetOrdersById(request.Oid);
                    if (orderInfo == null)
                        return BuildResponse.FailResponse<AdminOrderLogisticsResponse>("查询订单物流失败没有找到此订单信息");

                    AdminOrderLogisticsResponse model = new AdminOrderLogisticsResponse
                    {
                        OrdersLogisticsInfo = _orders.GetAdminOrdersLogisticsInfo(request.Oid)
                    };
                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息  
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminOrderLogisticsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrderLogistics", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminOrderLogisticsResponse>("订单物流信息错误");
            }
        }

        #endregion

        #region 批量发货

        /// <summary>
        /// 批量发货
        /// </summary>
        [Route("AdminUploadShipSnList")]
        [AddOperateLog("批量发货")]
        public async Task<BaseResponse<string>> AdminUploadShipSnList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //上传文件路径
                    const string saveTempPath = "/upload/admin/Excel/";

                    //异步上传文件
                    var excelResult = await new UploadFile().ExcelUploasd(Request.Content, saveTempPath);

                    //获取返回结果
                    var responseCode = Convert.ToInt32(excelResult["Code"].ToString());
                    if (responseCode != (int)ResponseCode.UploadSuccess)
                    {
                        return BuildResponse.FailResponse<string>("批量导入礼品卡Excel数据错误");
                    }

                    var excelUrl = excelResult["Data"].ToString();
                    var filePath = AppDomain.CurrentDomain.BaseDirectory + excelUrl;
                    var excelObj = new ExcelHelper(filePath);
                    var table = excelObj.ExcelToDataTable(excelUrl, true);
                    var shipSnList = DataTableHelper.DataTableToList<AdminUploadShipSnInfo>(table);

                    foreach (var shipInfo in shipSnList)
                    {
                        if (string.IsNullOrWhiteSpace(shipInfo.订单编号) || string.IsNullOrWhiteSpace(shipInfo.快递公司) ||
                            string.IsNullOrWhiteSpace(shipInfo.快递单号))
                        {
                            return BuildResponse.FailResponse<string>("请参考模版填写完整字段信息！");
                        }

                        var order = _orders.LoadEntitieNoTracking(u => u.OSn == shipInfo.订单编号);
                        if (order == null)
                            return BuildResponse.FailResponse<string>("订单不存在");

                        #region 处理订单

                        //开启事务
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                //更新订单状态为配送中
                                order.OrderState = (int)OrderState.Send;
                                order.ShiPsyStemName = shipInfo.快递公司;
                                order.ShipFriendName = shipInfo.快递公司;
                                order.ShipSn = shipInfo.快递单号;

                                //更新
                                _orders.UpdateOrders(order);

                                //创建订单处理
                                var orderAction = new LF_OrderActions
                                {
                                    OId = order.OId,
                                    UId = order.UId,
                                    RealName = "",
                                    AdminGId = 0,
                                    AdminGTitle = "非管理员",
                                    ActionType = (int)OrderActionType.Send,
                                    ActionTime = DateTime.Now,
                                    ActionDes = "订单已发货"
                                };
                                _orderActions.AddOrderActions(orderAction);

                                var result = _unitOfWork.Commit();

                                //成功提交所有事务
                                scope.Complete();
                                ApiLogger.Info("AdminOrdersGoods", $"请求[{order.OId}]订单数据成功，更新物流成功");
                                return result > 0
                                    ? BuildResponse.SuccessResponse("批量发货成功")
                                    : BuildResponse.FailResponse<string>("批量发货失败");
                            }
                            catch (Exception ex)
                            {
                                ApiLogger.Error("AdminUploadShipSnList", $"请求[{order.OId}]订单数据成功，更新物流失敗，已回滚", ex);
                                return BuildResponse.FailResponse<string>("批量发货错误");
                            }
                            finally
                            {
                                scope.Dispose();
                            }
                        }

                        #endregion
                    }
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUploadShipSnList", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("批量发货错误");
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
