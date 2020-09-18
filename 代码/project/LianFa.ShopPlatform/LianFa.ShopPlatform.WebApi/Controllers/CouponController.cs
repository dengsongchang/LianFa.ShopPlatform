using System;
using System.Linq;
using System.Web.Http;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Client.Coupon;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.Coupon;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Repository;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 礼品券控制器
    /// </summary>
    [Signature]
    [ControllerGroup("礼品券控制器接口", "礼品券相关")]
    public class CouponController : ApiController
    {
        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 礼品卡类型管理
        /// </summary>
        private readonly ICouponTypesService _couponTypes;

        /// <summary>
        /// 礼品卡管理
        /// </summary>
        private readonly ICouponsService _coupons;

        /// <summary>
        /// 收货地址服务
        /// </summary>
        private readonly IShipAddressesService _shipAddresses;

        /// <summary>
        /// 区域服务
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 上下文
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// 礼品卡详情服务
        /// </summary>
        private readonly ICouponTypeContentsRepository _couponTypeContentsService;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 构造函数(构造注入)
        /// </summary>
        /// <param name="couponTypes">礼品卡类型管理</param>
        /// <param name="coupons">礼品卡管理</param>
        /// <param name="shipAddresses">收货地址管理</param>
        /// <param name="regions">区域管理</param>
        /// <param name="workContext"></param>
        /// <param name="couponTypeContentsService"></param>
        public CouponController(ICouponTypesService couponTypes, ICouponsService coupons,
            IShipAddressesService shipAddresses, IRegionsService regions, IWorkContext workContext,
            ICouponTypeContentsRepository couponTypeContentsService, IUsersService users)
        {
            _couponTypes = couponTypes;
            _coupons = coupons;
            _shipAddresses = shipAddresses;
            _regions = regions;
            _workContext = workContext;
            _couponTypeContentsService = couponTypeContentsService;
            _users = users;
        }

        #region 礼品卡banner

        /// <summary>
        /// 礼品卡banner
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<GetCouponImgInfoResponse> GetCouponImgInfo()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //获取配置文件数据
                    var response = new GetCouponImgInfoResponse
                    {
                        CouponImg = ConfigMap.CouponImg,
                        CouponFullImg = FileHelper.GetFileFullUrl(ConfigMap.CouponImg)
                    };
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<GetCouponImgInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetCouponImgInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<GetCouponImgInfoResponse>("获取礼品卡banner错误");
            }
        }

        #endregion

        #region 判断是否登录

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> IsLogin()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //获得当前用户
                    var user = _users.LoadEntitieNoTracking(u => u.UId == _workContext.CurrentUser.UId);

                    //当商城不允许游客使用购物车时
                    if (user == null || user.UId < 1)
                    {
                        //返回未登录响应
                        return BuildResponse.FailResponse<bool>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    return BuildResponse.SuccessResponse(true);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<bool>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetCouponImgInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<bool>("获取登录状态错误");
            }
        }

        #endregion

        #region 礼品卡详情

        /// <summary>
        /// 礼品卡详情
        /// </summary>
        /// <returns></returns>
        [NoAuth]
        public BaseResponse<CouponDetailResponse> CouponDetail(CouponDetailRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var couponInfo = _couponTypes.LoadEntitieNoTracking(p => p.CouponTypeId == request.CouponTypeId && p.State == (byte)WhetherType.Yes);
                    if (couponInfo == null)
                    {
                        return BuildResponse.FailResponse<CouponDetailResponse>("不存在该礼品卡");
                    }
                    var couponContent = _couponTypeContentsService
                        .LoadEntitiesNoTracking(p => p.CouponTypeId == couponInfo.CouponTypeId)
                        .Select(p => p.CouponContent)
                        .ToList();

                    var model = new CouponDetailResponse
                    {
                        CouponTypeId = couponInfo.CouponTypeId,
                        Name = couponInfo.Name,
                        CouponImg = FileHelper.GetFileFullUrl(couponInfo.CouponImg),
                        Content = couponContent,
                        ProductImg = FileHelper.GetFileFullUrl(couponInfo.ProductImg),
                        Money = couponInfo.Money,
                        CostPrice = couponInfo.CostPrice,
                        IsCostPrice = couponInfo.IsCostPrice
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<CouponDetailResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CouponDetail", "", ex.Message, ex);
                return BuildResponse.FailResponse<CouponDetailResponse>("获取礼品卡详情错误");
            }
        }

        #endregion

        #region 礼品卡有效期查询

        /// <summary>
        /// 礼品卡有效期查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [NoAuth]
        [HttpPost]
        public BaseResponse<GetCouponValidDateResponse> GetCouponVaildDate(GetCouponValidDateRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var couponInfo = _coupons.LoadEntitieNoTracking(p => p.CouponSn == request.CouponSn);
                    if (couponInfo == null)
                        return BuildResponse.FailResponse<GetCouponValidDateResponse>("不存在该礼品卡");
                    var couponType = _couponTypes.LoadEntitieNoTracking(p => p.CouponTypeId == couponInfo.CouponTypeId);
                    if (couponType == null)
                        return BuildResponse.FailResponse<GetCouponValidDateResponse>("不存在该礼品卡");

                    var model = new GetCouponValidDateResponse
                    {
                        CouponName = couponType.Name,
                        ValidDate = couponInfo.UseEndTime,
                        ValidDates = couponInfo.UseEndTime.ToString("yyyy.MM.dd"),
                        State = couponInfo.State,
                        StateDec = ((CouponState)couponInfo.State).GetDescription()
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<GetCouponValidDateResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetCouponVaildDate", "", ex.Message, ex);
                return BuildResponse.FailResponse<GetCouponValidDateResponse>("礼品卡有效期查询错误");
            }
        }

        #endregion

        #region 验证礼品卡

        /// <summary>
        /// 验证礼品卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<VerifyCouponInfoResponse> VerifyCouponInfo(VerifyCouponInfoRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //返回未登录信息 
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    var couponInfo = _coupons.LoadEntitieNoTracking(p => p.CouponSn == request.CouponSn && p.PassWord == request.Password);
                    if (couponInfo == null)
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>("请输入正确的序列号或验证码/密码");
                    var couponType = _couponTypes.LoadEntitieNoTracking(p => p.CouponTypeId == couponInfo.CouponTypeId);
                    if (couponType == null)
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>("请输入正确的序列号或验证码/密码");
                    if (couponInfo.OId > 0 || couponInfo.State == (int)CouponState.Used)
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>("该礼品卡已兑换");
                    if (couponInfo.UseEndTime < DateTime.Now || couponInfo.State == (int)CouponState.Due)
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>("该礼品卡已过期");
                    if (couponInfo.State == (int)CouponState.Cancel)
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>("该礼品卡已作废");
                    if (couponInfo.UseStartTime > DateTime.Now)
                        return BuildResponse.FailResponse<VerifyCouponInfoResponse>("该礼品卡未到使用日期");

                    var model = new VerifyCouponInfoResponse
                    {
                        CouponId = couponInfo.CouponId
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<VerifyCouponInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("VerifyCouponInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<VerifyCouponInfoResponse>("验证礼品卡错误");
            }
        }

        #endregion

        #region 兑换页面礼品卡详情

        /// <summary>
        /// 兑换页面礼品卡详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<GetCouponInfoResponse> GetCouponInfo(GetCouponInfoRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //返回未登录信息 
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<GetCouponInfoResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    var couponInfo = _coupons.LoadEntitieNoTracking(p => p.CouponId == request.CouponId);
                    if (couponInfo == null)
                        return BuildResponse.FailResponse<GetCouponInfoResponse>("礼品卡不存在");
                    var couponType = _couponTypes.LoadEntitieNoTracking(p => p.CouponTypeId == couponInfo.CouponTypeId);
                    if (couponType == null)
                        return BuildResponse.FailResponse<GetCouponInfoResponse>("礼品卡不存在");
                    if (couponInfo.OId > 0)
                        return BuildResponse.FailResponse<GetCouponInfoResponse>("该礼品卡已兑换");
                    if (couponInfo.UseEndTime < DateTime.Now)
                        return BuildResponse.FailResponse<GetCouponInfoResponse>("该礼品卡已过期");
                    if (couponInfo.UseStartTime > DateTime.Now)
                        return BuildResponse.FailResponse<GetCouponInfoResponse>("该礼品卡未到使用日期");

                    var couponContent = _couponTypeContentsService
                        .LoadEntitiesNoTracking(p => p.CouponTypeId == couponInfo.CouponTypeId)
                        .Select(p => p.CouponContent)
                        .ToList();

                    //获取收货地址
                    FullShipAddressInfo address = new FullShipAddressInfo();
                    address = _shipAddresses.GetFullShipAddressBySAId(_workContext.CurrentUser.UId, request.SaId);
                    if (address == null)
                        address = _shipAddresses.GetDefaultFullShipAddress(_workContext.CurrentUser.UId);
                    var isDeliveryArea = false;
                    if (address != null)
                    {
                        isDeliveryArea = _regions.VerifyShipAddress(address, couponInfo.CouponTypeId);
                    }

                    var model = new GetCouponInfoResponse
                    {
                        CouponTypeId = couponInfo.CouponTypeId,
                        CouponId = couponInfo.CouponId,
                        Name = couponType.Name,
                        CouponImg = FileHelper.GetFileFullUrl(couponType.CouponImg),
                        Content = couponContent,
                        ProductImg = FileHelper.GetFileFullUrl(couponType.ProductImg),
                        Money = couponType.Money,
                        Address = address,
                        IsDeliveryArea = isDeliveryArea,
                        CostPrice = couponType.CostPrice,
                        IsCostPrice = couponType.IsCostPrice
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<GetCouponInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetCouponInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<GetCouponInfoResponse>("获取兑换页面礼品卡详情错误");
            }
        }

        #endregion

        #region 验证收货地址

        /// <summary>
        /// 验证收货地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<string> VerifyShipAddress(VerifyShipAddressRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //返回未登录信息 
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<string>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    //配送地址
                    var fullShipAddressInfo = _shipAddresses.GetFullShipAddressBySAId(_workContext.CurrentUser.UId, request.SaId);
                    if (fullShipAddressInfo == null)
                        return BuildResponse.FailResponse<string>("收货地址/礼品卡信息不存在");

                    var verifyShipAddress = _regions.VerifyShipAddress(fullShipAddressInfo, request.CouponTypeId);
                    if (!verifyShipAddress)
                        return BuildResponse.FailResponse<string>("该地址无法配送，请更换地址");
                    return BuildResponse.SuccessResponse("该地址可配送");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetCouponInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("验证收货地址错误");
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
