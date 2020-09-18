using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.UI;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Randoms;
using HuCheng.Util.Core.TypeConvert;
using HuCheng.Util.Office.Npoi.Excel;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Coupon;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;
using LianFa.ShopPlatform.Model.Response.Admin.User;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Repository;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{

    /// <summary>
    /// 后台礼品卡控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台礼品卡相关接口", "用于后台礼品卡管理")]
    [RoutePrefix("api/admin/Coupon")]
    public class AdminCouponController : ApiController
    {
        private static object _locker = new object(); //锁对象

        /// <summary>
        /// 管理员上下文
        /// </summary>
        private readonly IAdminWorkContext _workContext;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 礼品卡类型服务
        /// </summary>
        public readonly ICouponTypesService _couponTypes;

        /// <summary>
        /// 礼品卡类型内容服务
        /// </summary>
        public readonly ICouponTypeContentsService _couponTypeContents;

        /// <summary>
        /// 礼品卡服务
        /// </summary>
        public readonly ICouponsService _coupons;

        /// <summary>
        /// 礼品卡配送区域服务
        /// </summary>
        public readonly ICouponDeliveryAreasService _couponDeliveryAreas;

        /// <summary>
        /// 用户服务
        /// </summary>
        public readonly IUsersService _users;

        /// <summary>
        /// 收货地址服务
        /// </summary>
        public readonly IShipAddressesService _shipAddresses;

        /// <summary>
        /// 区域服务
        /// </summary>
        public readonly IRegionsService _regions;

        /// <summary>
        /// 订单服务
        /// </summary>
        public readonly IOrdersService _orders;

        /// <summary>
        /// 订单动作服务
        /// </summary>
        public readonly IOrderActionsService _orderActions;

        /// <summary>
        /// 运费模板服务
        /// </summary>
        private readonly IShippingTemplatesService _shippingTemplates;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 礼品卡控制器构造函数（构造注入）
        /// </summary>
        /// <param name="workContext"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="couponTypes"></param>
        /// <param name="couponTypeContents"></param>
        /// <param name="coupons"></param>
        /// <param name="couponDeliveryAreas"></param>
        /// <param name="users"></param>
        /// <param name="shipAddresses"></param>
        /// <param name="regions"></param>
        /// <param name="orders"></param>
        /// <param name="orderActions"></param>
        /// <param name="shippingTemplates"></param>
        public AdminCouponController(IAdminWorkContext workContext, IUnitOfWork unitOfWork, ICouponTypesService couponTypes,
            ICouponTypeContentsService couponTypeContents, ICouponsService coupons, ICouponDeliveryAreasService couponDeliveryAreas,
            IUsersService users, IShipAddressesService shipAddresses, IRegionsService regions, IOrdersService orders,
            IOrderActionsService orderActions, IShippingTemplatesService shippingTemplates)
        {
            _workContext = workContext;
            _unitOfWork = unitOfWork;
            _couponTypes = couponTypes;
            _couponTypeContents = couponTypeContents;
            _coupons = coupons;
            _couponDeliveryAreas = couponDeliveryAreas;
            _users = users;
            _shipAddresses = shipAddresses;
            _regions = regions;
            _orders = orders;
            _orderActions = orderActions;
            _shippingTemplates = shippingTemplates;
        }

        #region 配送区域列表

        /// <summary>
        /// 配送区域列表
        /// </summary>
        [Route("DeliveryAreaList")]
        public BaseResponse<DeliveryAreaListResponse> DeliveryAreaList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new DeliveryAreaListResponse
                    {
                        DeliveryAreaList = _couponDeliveryAreas.GetDeliveryAreaList()
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<DeliveryAreaListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("DeliveryAreaList", "", ex.Message, ex);
                return BuildResponse.FailResponse<DeliveryAreaListResponse>("配送区域列表错误");
            }
        }

        #endregion

        #region 礼品卡类型列表

        /// <summary>
        /// 礼品卡类型列表
        /// </summary>
        [Route("CouponTypeList")]
        public BaseResponse<CouponTypeListResponse> CouponTypeList(CouponTypeListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    var model = new CouponTypeListResponse
                    {
                        CouponTypeList = _couponTypes.GetCouponTypeList(request.Page, request.CouponName, request.DeliveryAreaId, out total),
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CouponTypeListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CouponTypeList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<CouponTypeListResponse>("礼品卡类型列表错误");
            }
        }

        #endregion

        #region 礼品卡类型详情

        /// <summary>
        /// 礼品卡类型详情
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetCouponTypeDetail")]
        public BaseResponse<AdminGetCouponTypeDetailResponse> AdminGetCouponTypeDetail(AdminGetCouponTypeDetailRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = _couponTypes.AdminGetCouponTypeDetail(request.CouponTypeId);
                    if (info == null)
                    {
                        return BuildResponse.FailResponse<AdminGetCouponTypeDetailResponse>("不存在该礼品卡");
                    }
                    var content = _couponTypeContents.GetCouponContentList(request.CouponTypeId);

                    var model = new AdminGetCouponTypeDetailResponse
                    {
                        CouponTypeDetail = info,
                        Content = content
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<AdminGetCouponTypeDetailResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetCouponTypeDetail", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetCouponTypeDetailResponse>("获取礼品卡类型详情错误");
            }
        }

        #endregion

        #region 添加礼品卡类型

        /// <summary>
        /// 添加礼品卡类型
        /// </summary>
        [Route("AdminAddCouponType")]
        [AddOperateLog("添加礼品卡类型")]
        public BaseResponse<object> AdminAddCouponType(AdminAddCouponTypeRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        var area =
                            _couponDeliveryAreas.LoadEntitieNoTracking(x => x.DeliveryAreaId == request.DeliveryAreaId);
                        if (area == null)
                        {
                            return BuildResponse.FailResponse<object>("请选择配送区域");
                        }
                        if (_couponTypes.Exist(x => x.CouponTypeSn == request.CouponTypeSn && x.DeliveryAreaId == request.DeliveryAreaId))
                        {
                            return BuildResponse.FailResponse<object>("相同配送区域下已存在该序列号");
                        }
                        if (string.IsNullOrEmpty(request.CouponImg) && request.CouponImg == null)
                        {
                            return BuildResponse.FailResponse<object>("请上传礼品卡图片");
                        }
                        if (string.IsNullOrEmpty(request.ProductImg) && request.ProductImg == null)
                        {
                            return BuildResponse.FailResponse<object>("请上传商品图片");
                        }
                        if (string.IsNullOrEmpty(request.Content) && request.Content == null)
                        {
                            return BuildResponse.FailResponse<object>("请填写礼品卡内容");
                        }

                        if (_shippingTemplates.Exist(m => m.TemplateId == request.TemplateId))
                        {
                            var tempType = _shippingTemplates
                                .LoadEntitieNoTracking(m => m.TemplateId == request.TemplateId)
                                .ValuationMethod;

                            if (tempType == (int) ValuationMethod.Weight)
                            {
                                return BuildResponse.FailResponse<object>("礼品卡无法以重量计算运费，请重新选择模板");
                            }
                        }

                        var coupon = new LF_CouponTypes()
                        {
                            Name = request.Name,
                            State = (int)CouponTypeState.OnSale,
                            AddTime = DateTime.Now,
                            CouponImg = request.CouponImg,
                            ProductImg = request.ProductImg,
                            RegionIds = "",
                            Money = request.Money,
                            DeliveryAreaId = request.DeliveryAreaId,
                            UseStartTime = DateTime.Now,
                            UseEndTime = DateTime.Now,
                            CouponTypeSn = request.CouponTypeSn,
                            DeliveryAreaSn = area.DeliveryAreaSn,
                            IsCostPrice = request.IsCostPrice,
                            CostPrice = request.CostPrice,
                            TemplateId = request.TemplateId
                        };

                        //添加礼品卡类型
                        _couponTypes.AddCouponTypes(coupon);

                        //提交事务
                        _unitOfWork.Commit();

                        var content = request.Content.Replace('，', ',');
                        List<string> contentList = content.Split(',').ToList();

                        //循环添加礼品卡内容
                        foreach (var con in contentList)
                        {
                            var conInfo = new LF_CouponTypeContents()
                            {
                                CouponTypeId = coupon.CouponTypeId,
                                CouponContent = con

                            };
                            //添加礼品卡内容
                            _couponTypeContents.AddCouponTypeContents(conInfo);
                        }

                        //提交事务
                        var result = _unitOfWork.Commit();
                        if (result > 0)
                        {
                            scope.Complete();
                            return BuildResponse.SuccessResponse<object>("添加礼品卡成功");
                        }
                        return BuildResponse.FailResponse<object>("添加礼品卡错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddCouponType", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台添加礼品卡错误");
            }
        }

        #endregion

        #region 编辑礼品卡类型

        /// <summary>
        /// 编辑礼品卡类型
        /// </summary>
        [Route("AdminEditCouponType")]
        [AddOperateLog("编辑礼品卡类型")]
        public BaseResponse<object> AdminEditCouponType(AdminEditCouponTypeRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var coupon = _couponTypes.GetCouponTypesById(request.CouponTypeId);
                    if (coupon == null)
                        return BuildResponse.FailResponse<object>("礼品卡不存在");

                    if (_coupons.Exist(x => x.CouponTypeId == request.CouponTypeId && (coupon.CouponTypeSn != request.CouponTypeSn || coupon.DeliveryAreaId != request.DeliveryAreaId)))
                    {
                        return BuildResponse.FailResponse<object>("该礼品卡类型已存在礼品卡，无法编辑序列号和区域");
                    }

                    var area = _couponDeliveryAreas.LoadEntitieNoTracking(x => x.DeliveryAreaId == request.DeliveryAreaId);
                    if (area == null)
                    {
                        return BuildResponse.FailResponse<object>("请选择配送区域");
                    }
                    if (_couponTypes.Exist(x => x.CouponTypeSn == request.CouponTypeSn && x.DeliveryAreaId == request.DeliveryAreaId && x.CouponTypeId != request.CouponTypeId))
                    {
                        return BuildResponse.FailResponse<object>("相同配送区域下已存在该序列号");
                    }
                    if (string.IsNullOrEmpty(request.CouponImg) && request.CouponImg == null)
                    {
                        return BuildResponse.FailResponse<object>("请上传礼品卡图片");
                    }
                    if (string.IsNullOrEmpty(request.ProductImg) && request.ProductImg == null)
                    {
                        return BuildResponse.FailResponse<object>("请上传商品图片");
                    }
                    if (string.IsNullOrEmpty(request.Content) && request.Content == null)
                    {
                        return BuildResponse.FailResponse<object>("请填写礼品卡内容");
                    }

                    if (_shippingTemplates.Exist(m => m.TemplateId == request.TemplateId))
                    {
                        var tempType = _shippingTemplates
                            .LoadEntitieNoTracking(m => m.TemplateId == request.TemplateId)
                            .ValuationMethod;

                        if (tempType == (int)ValuationMethod.Weight)
                        {
                            return BuildResponse.FailResponse<object>("礼品卡无法以重量计算运费，请重新选择模板");
                        }
                    }

                    //编辑礼品卡
                    coupon.Name = request.Name;
                    coupon.CouponImg = request.CouponImg;
                    coupon.ProductImg = request.ProductImg;
                    coupon.RegionIds = "";
                    coupon.Money = request.Money;
                    coupon.DeliveryAreaId = request.DeliveryAreaId;
                    coupon.UseStartTime = DateTime.Now;
                    coupon.UseEndTime = DateTime.Now;
                    coupon.CouponTypeSn = request.CouponTypeSn;
                    coupon.DeliveryAreaSn = area.DeliveryAreaSn;
                    coupon.CostPrice = request.CostPrice;
                    coupon.IsCostPrice = request.IsCostPrice;
                    coupon.TemplateId = request.TemplateId;

                    //更新礼品卡类型
                    _couponTypes.UpdateCouponTypes(coupon);

                    //删除原来的内容并添加新的内容
                    _couponTypeContents.DeleteCouponTypeContents(x => x.CouponTypeId == request.CouponTypeId);

                    var content = request.Content.Replace('，', ',');
                    List<string> newContentList = content.Split(',').ToList();
                    var contentList = newContentList.Select(con => new LF_CouponTypeContents
                    {
                        CouponTypeId = request.CouponTypeId,
                        CouponContent = con
                    })
                        .ToList();
                    _couponTypeContents.BatchAddCouponTypeContents(contentList);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0
                        ? BuildResponse.SuccessResponse<object>("编辑成功")
                        : BuildResponse.FailResponse<object>("编辑失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditCouponType", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑礼品卡错误");
            }
        }

        #endregion

        #region 后台批量删除礼品卡类型

        /// <summary>
        /// 后台批量删除礼品卡类型
        /// </summary>
        [Route("AdminDelCouponType")]
        [AddOperateLog("后台批量删除礼品卡类型")]
        public BaseResponse<object> AdminDelCouponType(AdminDelCouponTypeRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        foreach (var couponTypeId in request.CouponTypeId)
                        {
                            if (!_couponTypes.Exist(x => x.CouponTypeId == couponTypeId))
                                return BuildResponse.FailResponse<object>("礼品卡类型不存在");

                            if (_coupons.Exist(x => x.CouponTypeId == couponTypeId && x.OId > 0))
                                return BuildResponse.FailResponse<object>("礼品卡类型下存在已兑换的礼品卡，无法删除该礼品卡类型。");
                        }

                        //批量删除礼品卡、礼品卡类型、礼品内容
                        _coupons.BatchDelete(m => request.CouponTypeId.Contains(m.CouponTypeId));
                        _couponTypes.BatchDelete(m => request.CouponTypeId.Contains(m.CouponTypeId));
                        _couponTypeContents.BatchDelete(m => request.CouponTypeId.Contains(m.CouponTypeId));

                        //成功提交事务
                        scope.Complete();

                        return BuildResponse.SuccessResponse<object>("后台批量删除礼品卡类型成功");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelCouponType", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台批量删除礼品卡类型错误");
            }
        }

        #endregion

        #region 上/下架礼品卡类型

        /// <summary>
        /// 上/下架礼品卡类型
        /// </summary>
        /// <returns></returns>
        [Route("AdminEditCouponTypeState")]
        [AddOperateLog("上/下架礼品卡类型")]
        public BaseResponse<object> AdminEditCouponTypeState(AdminEditCouponTypeStateRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = _couponTypes.GetCouponTypesById(request.CouponTypeId);
                    if (info == null)
                    {
                        return BuildResponse.FailResponse<object>("礼品卡不存在");
                    }

                    if (info.State == request.Type)
                    {
                        return BuildResponse.SuccessResponse<object>("编辑成功");
                    }

                    info.State = (byte)request.Type;
                    _couponTypes.UpdateCouponTypes(info);
                    var result = _unitOfWork.Commit();

                    return result > 0
                        ? BuildResponse.SuccessResponse<object>("编辑成功")
                        : BuildResponse.FailResponse<object>("编辑失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditCouponTypeState", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑礼品卡状态状态错误");
            }
        }

        #endregion

        #region 礼品卡列表

        /// <summary>
        /// 礼品卡列表
        /// </summary>
        [Route("CouponList")]
        public BaseResponse<CouponListResponse> CouponList(CouponListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    var model = new CouponListResponse
                    {
                        CouponList = _coupons.GetCouponList(request.Page, request.CouponSn, request.State, request.Name, out total),
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<CouponListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CouponList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<CouponListResponse>("礼品卡列表错误");
            }
        }

        #endregion

        #region 后台批量延期礼品卡

        /// <summary>
        /// 后台批量延期礼品卡
        /// </summary>
        [Route("AdminEditCouponTime")]
        [AddOperateLog("后台批量延期礼品卡")]
        public BaseResponse<object> AdminEditCouponTime(AdminEditCouponTimeRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                        foreach (var couponId in request.CouponId)
                        {
                            //获取礼品卡
                            var coupon = _coupons.GetCouponsById(couponId);
                            if (coupon == null)
                                return BuildResponse.FailResponse<object>("礼品卡不存在");

                            if (coupon.UseEndTime == request.UseEndTime) continue;

                            //礼品卡已兑换
                            if (coupon.OId > 0 || coupon.State == (byte)CouponState.Used)
                            {
                                coupon.UseEndTime = request.UseEndTime;
                            }
                            else
                            {
                                coupon.UseEndTime = request.UseEndTime;
                                coupon.State = (byte) CouponState.UnUse;
                                if (request.UseEndTime <= DateTime.Now)
                                {
                                    coupon.State = (byte) CouponState.Due;
                                }
                            }
                            _coupons.UpdateCoupons(coupon);
                        }

                        //成功提交事务
                        _unitOfWork.Commit();

                        return BuildResponse.SuccessResponse<object>("后台批量延期礼品卡成功");                    
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditCouponTime", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台批量延期礼品卡错误");
            }
        }

        #endregion

        #region 礼品卡详情

        /// <summary>
        /// 礼品卡详情
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetCouponDetail")]
        public BaseResponse<AdminGetCouponDetailResponse> AdminGetCouponDetail(AdminGetCouponDetailRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = _coupons.LoadEntitieNoTracking(x => x.CouponId == request.CouponId);
                    if (info == null)
                    {
                        return BuildResponse.FailResponse<AdminGetCouponDetailResponse>("不存在该礼品卡");
                    }
                    var couponType = _couponTypes.LoadEntitieNoTracking(x => x.CouponTypeId == info.CouponTypeId);
                    if (couponType == null)
                    {
                        return BuildResponse.FailResponse<AdminGetCouponDetailResponse>("不存在该礼品卡");
                    }
                    var userName = "";
                    if (info.UId > 0)
                    {
                        userName = _users.LoadEntitieNoTracking(u => u.UId == info.UId)?.UserName;
                    }
                    var oSn = "";
                    byte? state = 0;
                    if (info.OId > 0)
                    {
                        oSn = _orders.LoadEntitieNoTracking(o => o.OId == info.OId)?.OSn;
                        state = _orders.LoadEntitieNoTracking(o => o.OId == info.OId)?.OrderState;
                    }
                    var model = new AdminGetCouponDetailResponse
                    {
                        CouponId = info.CouponId,
                        CouponSn = info.CouponSn,
                        Password = info.PassWord,
                        Name = couponType.Name,
                        UserName = userName,
                        AddTime = info.AddTime,
                        AddTimes = info.AddTime.ToDateString(),
                        UseTime = info.UseTime,
                        UseTimes = info.UseTime.Year == 1900 ? "" : info.UseTime.ToDateString(),
                        State = info.State,
                        StateDec = ((CouponState)info.State).GetDescription(),
                        OSn = oSn,
                        OId = info.OId,
                        OrderState = state,
                        OrderStateDec = state == 0 || state == null ? null : ((OrderState)state).GetDescription()
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<AdminGetCouponDetailResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetCouponDetail", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetCouponDetailResponse>("获取礼品卡详情错误");
            }
        }

        #endregion

        #region 后台批量删除礼品卡

        /// <summary>
        /// 后台批量删除礼品卡
        /// </summary>
        [Route("AdminDelCoupon")]
        [AddOperateLog("后台批量删除礼品卡")]
        public BaseResponse<object> AdminDelCoupon(AdminDelCouponRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        foreach (var couponId in request.CouponId)
                        {
                            if(!_coupons.Exist(x => x.CouponId == couponId))
                                return BuildResponse.FailResponse<object>("礼品卡不存在");

                            if(_coupons.Exist(x => x.CouponId == couponId && x.OId > 0))
                            return BuildResponse.FailResponse<object>("礼品卡已兑换！无法删除该礼品卡。");

                        }

                        //批量删除礼品卡
                        _coupons.BatchDelete(m => request.CouponId.Contains(m.CouponId));

                        //成功提交事务
                        scope.Complete();

                        return BuildResponse.SuccessResponse<object>("后台批量删除礼品卡成功");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelCoupon", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台批量删除礼品卡错误");
            }
        }

        #endregion

        #region 批量作废礼品卡

        /// <summary>
        /// 批量作废礼品卡
        /// </summary>
        [Route("AdminCancelCoupon")]
        [AddOperateLog("批量作废礼品卡")]
        public BaseResponse<object> AdminCancelCoupon(AdminDelCouponRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //验证礼品卡
                    foreach (var couponId in request.CouponId)
                    {
                        var coupon = _coupons.GetCouponsById(couponId);

                        if (coupon == null)
                            return BuildResponse.FailResponse<object>("礼品卡不存在");

                        if (coupon.OId > 0 || coupon.State == (int) CouponState.Used)
                            return BuildResponse.FailResponse<object>("礼品卡" + coupon.CouponSn.Trim() + "已兑换！无法作废该礼品卡。");
                    }

                    //批量更新礼品卡
                    var result = _coupons.BatchUpdate(x => request.CouponId.Contains(x.CouponId),
                        c => new LF_Coupons {State = (byte) CouponState.Cancel});

                    return result > 0
                        ? BuildResponse.SuccessResponse<object>("操作成功")
                        : BuildResponse.FailResponse<object>("操作失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminCancelCoupon", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台批量作废礼品卡错误");
            }
        }

        #endregion

        #region 礼品卡类型下拉列表

        /// <summary>
        /// 礼品卡类型下拉列表
        /// </summary>
        [Route("MiniCouponTypeList")]
        public BaseResponse<MiniCouponTypeListResponse> MiniCouponTypeList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new MiniCouponTypeListResponse
                    {
                        CouponTypeList = _couponTypes.GetMiniCouponTypeList()
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<MiniCouponTypeListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CouponTypeList", "", ex.Message, ex);
                return BuildResponse.FailResponse<MiniCouponTypeListResponse>("礼品卡类型列表错误");
            }
        }

        #endregion

        #region 添加礼品卡
        
        /// <summary>
        /// 添加礼品卡
        /// </summary>
        [Route("AdminAddCoupon")]
        [AddOperateLog("添加礼品卡")]
        public BaseResponse<object> AdminAddCoupon(AdminAddCouponRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    if (request.CouponSn.Trim().Length < 6)
                    {
                        return BuildResponse.FailResponse<object>("序列号为：" + request.CouponSn.Trim() + "，序列号有误");
                    }

                    //获取已有的区域编号
                    var areaSnList = _couponDeliveryAreas.GetDeliveryAreaSnList();

                    //验证配送区域编号
                    var areaSn = areaSnList.Find(x => request.CouponSn.StartsWith(x));
                    if (string.IsNullOrWhiteSpace(areaSn))
                        return BuildResponse.FailResponse<object>("序列号有误，不存在该区域编码");

                    var area = _couponDeliveryAreas.LoadEntitieNoTracking(x => x.DeliveryAreaSn == areaSn);
                    if (area == null)
                        return BuildResponse.FailResponse<object>("序列号有误，不存在该区域编码");

                    //验证礼品卡种类编号
                    var couponTypeSn = request.CouponSn.Substring(areaSn.Length, 3);
                    var couponType = _couponTypes.LoadEntitieNoTracking(x => x.CouponTypeSn == couponTypeSn && x.DeliveryAreaId == area.DeliveryAreaId);
                    if (couponType == null)
                    {
                        return BuildResponse.FailResponse<object>("序列号有误，不存在该礼品卡类型");
                    }
                    
                    //判断序列号不能相同
                    if (_coupons.Exist(m => m.CouponSn == request.CouponSn))
                    {
                        return BuildResponse.FailResponse<object>("序列号已存在");
                    }
                    
                    var coupon = new LF_Coupons()
                    {
                        UId = 0,
                        CouponTypeId = couponType.CouponTypeId,
                        OId = 0,
                        UseTime = new DateTime(1900, 1, 1),
                        UseIp = "",
                        AddTime = DateTime.Now,
                        PassWord = request.PassWord,
                        State = (int)CouponState.UnUse,
                        UseUId = 0,
                        UseStartTime = DateTime.Now,
                        UseEndTime = request.EndTime,
                        CouponSn = request.CouponSn
                    };

                    //添加礼品卡
                    _coupons.AddCoupons(coupon);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0
                        ? BuildResponse.SuccessResponse<object>("添加成功")
                        : BuildResponse.FailResponse<object>("添加失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddCoupon", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台添加礼品卡错误");
            }
        }
        
        #endregion

        #region 下载导入模板

        /// <summary>
        /// 下载导入模板
        /// </summary>
        [Route("DownloadUploadTemplate")]
        [AddOperateLog("下载导入模板")]
        public BaseResponse<string> DownloadUploadTemplate()

        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取上传模板地址
                    var excelUrl = FileHelper.GetFileFullUrl("/upload/admin/上传模板/礼品卡导入模板.xlsx");

                    //返回成功结果
                    return BuildResponse.SuccessResponse(excelUrl);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("DownloadUploadTemplate", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("下载上传模板错误");
            }
        }

        #endregion

        #region 批量导入礼品卡

        /// <summary>
        /// 批量导入礼品卡
        /// </summary>
        [Route("AdminUploadCouponList")]
        [AllowAnonymous]
        public async Task<BaseResponse<string>> AdminUploadCouponList()
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
                    var userList = DataTableHelper.DataTableToList<AdminUploadCouponInfo>(table);
                    //获取已有的区域编号
                    var areaSnList = _couponDeliveryAreas.GetDeliveryAreaSnList();

                    foreach (var couponInfo in userList)
                    {
                        if (string.IsNullOrWhiteSpace(couponInfo.序列号) || string.IsNullOrWhiteSpace(couponInfo.密码) ||
                            string.IsNullOrWhiteSpace(couponInfo.兑换截止日期))
                        {
                            return BuildResponse.FailResponse<string>("请参考模版填写完整字段信息！");
                        }
                        if (couponInfo.序列号.Trim().Length < 6)
                        {
                            return BuildResponse.FailResponse<string>("序列号为：" + couponInfo.序列号.Trim() + "，序列号有误");
                        }
                        if (couponInfo.兑换截止日期.Contains('/'))
                        {
                            couponInfo.兑换截止日期 = couponInfo.兑换截止日期.Replace('/', '-');
                        }
                        var endDate = Conv.ToDateOrNull(couponInfo.兑换截止日期);
                        if (!endDate.HasValue)
                        {
                            return BuildResponse.FailResponse<string>("请填写正确的兑换截止日期");
                        }

                        //验证配送区域编号
                        var areaSn = areaSnList.Find(x => couponInfo.序列号.StartsWith(x));
                        if(string.IsNullOrWhiteSpace(areaSn))
                            return BuildResponse.FailResponse<string>($"序列号{couponInfo.序列号}找不到区域编号");
                        var area = _couponDeliveryAreas.LoadEntitieNoTracking(x => x.DeliveryAreaSn == areaSn);
                        if (area == null)
                            return BuildResponse.FailResponse<string>($"序列号{couponInfo.序列号}找不到区域编号");

                        //验证礼品卡种类编号
                        var couponTypeSn = couponInfo.序列号.Substring(areaSn.Length, 3);
                        var couponType = _couponTypes.LoadEntitieNoTracking(x => x.CouponTypeSn == couponTypeSn && x.DeliveryAreaId == area.DeliveryAreaId);
                        if (couponType == null)
                        {
                            return BuildResponse.FailResponse<string>("序列号为：" + couponInfo.序列号.Trim() + "，礼品卡类型编号有误");
                        }

                        //判断序列号不能相同
                        if (_coupons.Exist(m => m.CouponSn == couponInfo.序列号.Trim()))
                        {
                            return BuildResponse.FailResponse<string>("序列号为：" + couponInfo.序列号.Trim() + "已存在");
                        }
                        var coupon = new LF_Coupons()
                        {
                            UId = 0,
                            CouponTypeId = couponType.CouponTypeId,
                            OId = 0,
                            UseTime = new DateTime(1900, 1, 1),
                            UseIp = "",
                            AddTime = DateTime.Now,
                            PassWord = couponInfo.密码,
                            State = (int)CouponState.UnUse,
                            UseUId = 0,
                            UseStartTime = DateTime.Now,
                            UseEndTime = endDate.Value,
                            CouponSn = couponInfo.序列号
                        };

                        //添加礼品卡
                        _coupons.AddCoupons(coupon);
                    }

                    //成功提交所有事务
                    var result = _unitOfWork.Commit();
                    return result > 0
                        ? BuildResponse.SuccessResponse("导入礼品卡成功")
                        : BuildResponse.FailResponse<string>("导入礼品卡失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUploadCouponList", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导入礼品卡Excel数据错误");
            }
        }

        #endregion

        #region 管理员兑换列表

        /// <summary>
        /// 管理员兑换列表
        /// </summary>
        [Route("AdminCouponList")]
        public BaseResponse<AdminCouponListResponse> AdminCouponList(AdminCouponListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    var model = new AdminCouponListResponse
                    {
                        CouponList = _coupons.GetAdminCouponList(request.Page, request.CouponSn, out total),
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(model);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminCouponListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminCouponList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminCouponListResponse>("管理员兑换列表错误");
            }
        }

        #endregion

        #region 兑换礼品卡

        /// <summary>
        /// 兑换礼品卡
        /// </summary>
        /// <returns></returns>
        [Route("AdminSubmitCouponOrder")]
        [AddOperateLog("兑换礼品卡")]
        public BaseResponse<string> AdminSubmitCouponOrder(AdminSubmitCouponOrderRequest request)
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
                        var adminUser = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentAdmin.AdminId);

                        //返回未登录信息
                        if (adminUser == null || adminUser.UId < 1)
                            return BuildResponse.FailResponse<string>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);

                        //验证联系方式
                        if (!Code.Helper.ValidateHelper.IsValidMobile(request.ShipMobile.Trim()))
                        {
                            if (!Code.Helper.ValidateHelper.IsPhone(request.ShipMobile.Trim()))
                            {
                                return BuildResponse.FailResponse<string>("手机号格式错误");
                            }
                        }

                        //礼品卡
                        var couponInfo = _coupons.LoadEntitieNoTracking(y => y.CouponSn == request.CouponSn);
                        if (couponInfo == null)
                            return BuildResponse.FailResponse<string>("礼品卡不存在");
                        if (couponInfo.OId > 0 || couponInfo.State == (int)CouponState.Used)
                            return BuildResponse.FailResponse<string>("该礼品卡已兑换");
                        if (couponInfo.UseEndTime < DateTime.Now || couponInfo.State == (int)CouponState.Due)
                            return BuildResponse.FailResponse<string>("该礼品卡已过期");
                        if (couponInfo.State == (int)CouponState.Cancel)
                            return BuildResponse.FailResponse<string>("该礼品卡已作废");
                        if (couponInfo.UseStartTime > DateTime.Now)
                            return BuildResponse.FailResponse<string>("该礼品卡未到使用日期");
                        if (couponInfo.PassWord != request.Password)
                            return BuildResponse.FailResponse<string>("请输入正确的序列号/密码");

                        var verifyShipAddress = _regions.AdminVerifyShipAddress(request.RegionId, couponInfo.CouponTypeId);
                        if (!verifyShipAddress)
                            return BuildResponse.FailResponse<string>("该地址无法配送，请更换地址");

                        //开启事务
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                //验证已经通过,进行订单保存
                                var orderInfo = new LF_Orders
                                {
                                    OSn = _orders.GenerateOsn(couponInfo.UId),
                                    UId = adminUser.UId,
                                    Weight = 0,
                                    ProductAmount = 0,
                                    ShipFee = 0,
                                    OrderAmount = 0,
                                    SurplusMoney = 0,
                                    OrderState = (byte)OrderState.PreProduct,
                                    AddTime = DateTime.Now,
                                    Type = (byte)OrderType.CardOrder,
                                    PaySystemName = "",
                                    PaySn = "",
                                    ShiPsyStemName = "",
                                    ShipFriendName = "",
                                    ShipSn = "",
                                    PayMode = (byte)PayModeType.Online,
                                    RegionId = request.RegionId,
                                    Consignee = request.Consignee,
                                    Mobile = request.ShipMobile,
                                    Address = request.Address,
                                    BuyerRemark = request.BuyerRemark,
                                    Ip = _workContext.Ip,
                                    ShipTime = new DateTime(1900, 1, 1),
                                    IsDel = false,
                                    OrderSource = (int)OrderSource.WxMini
                                };
                                _orders.AddOrders(orderInfo);
                                //提交事务
                                var success = _unitOfWork.Commit();
                                if (success > 0)
                                {
                                    //创建订单处理
                                    _orderActions.AddOrderActions(new LF_OrderActions()
                                    {
                                        OId = orderInfo.OId,
                                        UId = adminUser.UId,
                                        RealName = adminUser.UserName,
                                        AdminGId = 0,
                                        AdminGTitle = "管理员",
                                        ActionType = (int)OrderActionType.Submit,
                                        ActionTime = DateTime.Now,
                                        ActionDes = "您提交了订单，请等待系统确认"
                                    });

                                    //修改礼品卡信息
                                    couponInfo.OId = orderInfo.OId;
                                    couponInfo.UseTime = DateTime.Now;
                                    couponInfo.UseIp = _workContext.Ip;
                                    couponInfo.State = (byte)CouponState.Used;
                                    couponInfo.UseUId = adminUser.UId;
                                    _coupons.UpdateCoupons(couponInfo);

                                    _unitOfWork.Commit();
                                    scope.Complete();

                                    return BuildResponse.SuccessResponse("兑换成功");
                                }
                            }
                            catch (Exception ex)
                            {
                                ApiLogger.Error("SubmitCouponOrder", requestString, "兑换礼品卡错误", ex);
                                return BuildResponse.FailResponse<string>("兑换礼品卡失败");
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
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminSubmitCouponOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("兑换礼品卡错误");
            }
        }

        #endregion

        #region 下载兑换模板

        /// <summary>
        /// 下载兑换模板
        /// </summary>
        [Route("DownloadCouponOrderTemplate")]
        [AddOperateLog("下载兑换模板")]
        public BaseResponse<string> DownloadCouponOrderTemplate()

        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取上传模板地址
                    var excelUrl = FileHelper.GetFileFullUrl("/upload/admin/上传模板/礼品卡兑换模板.xlsx");

                    //返回成功结果
                    return BuildResponse.SuccessResponse(excelUrl);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("DownloadCouponOrderTemplate", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("下载兑换模板错误");
            }
        }

        #endregion

        #region 批量兑换礼品卡

        /// <summary>
        /// 批量兑换礼品卡
        /// </summary>
        [Route("AdminUploadCouponOrderList")]
        [AllowAnonymous]
        public async Task<BaseResponse<string>> AdminUploadCouponOrderList()
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
                        return BuildResponse.FailResponse<string>("批量兑换礼品卡Excel数据错误");
                    }
                    var excelUrl = excelResult["Data"].ToString();
                    var filePath = AppDomain.CurrentDomain.BaseDirectory + excelUrl;
                    var excelObj = new ExcelHelper(filePath);
                    var table = excelObj.ExcelToDataTable(excelUrl, true);
                    var couponList = DataTableHelper.DataTableToList<AdminUploadCouponOrderListInfo>(table);

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            foreach (var userInfo in couponList)
                            {
                                if (string.IsNullOrWhiteSpace(userInfo.序列号) || string.IsNullOrWhiteSpace(userInfo.密码) ||
                                    string.IsNullOrWhiteSpace(userInfo.省) ||
                                    string.IsNullOrWhiteSpace(userInfo.市) || string.IsNullOrWhiteSpace(userInfo.区) ||
                                    string.IsNullOrWhiteSpace(userInfo.收货人)
                                    || string.IsNullOrWhiteSpace(userInfo.收货地址) ||
                                    string.IsNullOrWhiteSpace(userInfo.手机号码))
                                {
                                    return BuildResponse.FailResponse<string>("请参考模版填写完整字段信息！");
                                }

                                //验证联系方式
                                if (!Code.Helper.ValidateHelper.IsValidMobile(userInfo.手机号码.Trim()))
                                {
                                    if (!Code.Helper.ValidateHelper.IsPhone(userInfo.手机号码.Trim()))
                                    {
                                        return BuildResponse.FailResponse<string>("请填写正确的手机号码");
                                    }
                                }

                                var adminUser =
                                    _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentAdmin.AdminId);
                                //礼品卡
                                var couponInfo = _coupons.LoadEntitieNoTracking(y => y.CouponSn == userInfo.序列号);
                                if (couponInfo == null)
                                    return BuildResponse.FailResponse<string>("序列号" + userInfo.序列号.Trim() + "不存在");
                                if (couponInfo.OId > 0 || couponInfo.State == (int)CouponState.Used)
                                    return BuildResponse.FailResponse<string>("序列号" + userInfo.序列号.Trim() + "已兑换");
                                if (couponInfo.UseEndTime < DateTime.Now || couponInfo.State == (int)CouponState.Due)
                                    return BuildResponse.FailResponse<string>("序列号" + userInfo.序列号.Trim() +  "已过期");
                                if (couponInfo.State == (int)CouponState.Cancel)
                                    return BuildResponse.FailResponse<string>("序列号" + userInfo.序列号.Trim() + "已作废");
                                if (couponInfo.UseStartTime > DateTime.Now)
                                    return BuildResponse.FailResponse<string>("序列号" + userInfo.序列号.Trim() + "未到使用日期");
                                if (couponInfo.PassWord != userInfo.密码)
                                    return BuildResponse.FailResponse<string>("序列号" + userInfo.序列号.Trim() + "密码错误");

                                //根据省市区获取regionId
                                var regionId = _regions.AdminGetRegionId(userInfo.省, userInfo.市, userInfo.区);
                                if (regionId == 0)
                                    return BuildResponse.FailResponse<string>("请输入正确省市区");
                                var verifyShipAddress =
                                    _regions.AdminVerifyShipAddress(regionId, couponInfo.CouponTypeId);
                                if (!verifyShipAddress)
                                    return BuildResponse.FailResponse<string>("该地址无法配送，请更换地址");

                                //验证已经通过,进行订单保存
                                var orderInfo = new LF_Orders
                                {
                                    OSn = _orders.GenerateOsn(couponInfo.UId),
                                    UId = _workContext.CurrentAdmin.AdminId,
                                    Weight = 0,
                                    ProductAmount = 0,
                                    ShipFee = 0,
                                    OrderAmount = 0,
                                    SurplusMoney = 0,
                                    OrderState = (byte)OrderState.PreProduct,
                                    AddTime = DateTime.Now,
                                    Type = (byte)OrderType.CardOrder,
                                    PaySystemName = "",
                                    PaySn = "",
                                    ShiPsyStemName = "",
                                    ShipFriendName = "",
                                    ShipSn = "",
                                    PayMode = (byte)PayModeType.Online,
                                    RegionId = (short)regionId,
                                    Consignee = userInfo.收货人,
                                    Mobile = userInfo.手机号码,
                                    Address = userInfo.收货地址,
                                    BuyerRemark = userInfo.买家备注,
                                    Ip = _workContext.Ip,
                                    ShipTime = new DateTime(1900, 1, 1),
                                    IsDel = false,
                                    OrderSource = (int)OrderSource.WxMini
                                };
                                _orders.AddOrders(orderInfo);
                                _unitOfWork.Commit();

                                //创建订单处理
                                _orderActions.AddOrderActions(new LF_OrderActions()
                                {
                                    OId = orderInfo.OId,
                                    UId = adminUser.UId,
                                    RealName = adminUser.UserName,
                                    AdminGId = 0,
                                    AdminGTitle = "管理员",
                                    ActionType = (int)OrderActionType.Submit,
                                    ActionTime = DateTime.Now,
                                    ActionDes = "您提交了订单，请等待系统确认"
                                });

                                //修改礼品卡信息
                                couponInfo.OId = orderInfo.OId;
                                couponInfo.UseTime = DateTime.Now;
                                couponInfo.UseIp = _workContext.Ip;
                                couponInfo.State = (byte)CouponState.Used;
                                couponInfo.UseUId = adminUser.UId;
                                _coupons.UpdateCoupons(couponInfo);

                            }
                            //成功提交所有事务
                            var result = _unitOfWork.Commit();
                            scope.Complete();
                            return result > 0
                                ? BuildResponse.SuccessResponse("批量兑换礼品卡成功")
                                : BuildResponse.FailResponse<string>("批量兑换礼品卡失败");
                        }
                        catch (Exception ex)
                        {
                            ApiLogger.Error("AdminUploadCouponOrderList", "", "批量兑换礼品卡出错", ex);
                            return BuildResponse.FailResponse<string>("批量兑换礼品卡错误");
                        }
                        finally
                        {
                            scope.Dispose();
                        }
                    }
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminUploadCouponOrderList", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("批量兑换礼品卡错误");
            }
        }

        #endregion

        #region 后台上传礼品卡图片

        /// <summary>
        /// 后台上传礼品卡图片
        /// </summary>
        [AllowAnonymous]
        [Route("AdminUploadCouponImg")]
        public async Task<BaseResponse<string>> AdminUploadCouponImg()
        {

            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/admin/CouponImg/";

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
                ApiLogger.Error("AdminUploadCouponImg", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台上传礼品卡图片错误");
            }
        }

        #endregion

        #region 获取模型验证出错信息

        /// <summary>
        /// 获取模型验证出错信息
        /// </summary>
        /// <returns>出错信息</returns>
        [NonAction]
        private string GetModelErrorMsg()
        {
            //出错信息
            var errorMessage = string.Empty;

            //获取所有出错的Key
            var key = ModelState.Keys.FirstOrDefault();
            if (key != null)
            {
                //获取第一个key对应的ModelStateDictionary的第一条出错信息
                var error = ModelState[key].Errors.FirstOrDefault();

                if (string.IsNullOrEmpty(error?.ErrorMessage))
                {
                    ApiLogger.Info("GetModelErrorMsg", key, $"数据验证出错:{error?.Exception.Message}");

                    //将出错描述添加到sb中
                    errorMessage = $"数据验证出错，请求字段：{key}数据类型不正确，无法解析";
                }
                else
                {
                    //将出错描述添加到sb中
                    errorMessage = error.ErrorMessage;
                }
            }

            //返回出错信息
            return errorMessage;
        }

        #endregion
    }
}
