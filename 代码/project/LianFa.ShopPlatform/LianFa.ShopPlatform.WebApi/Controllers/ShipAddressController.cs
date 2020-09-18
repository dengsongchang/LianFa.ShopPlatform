using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Client.ShipAddress;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.ShipAddress;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 用户收货地址管理控制器
    /// </summary>
    [Signature]
    [ControllerGroup("用户收货地址管理相关接口", "收货地址管理操作")]
    [RoutePrefix("api/admin/shipAddress")]
    public class ShipAddressController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

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
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 用户收货地址管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="shipAddresses">收货地址服务</param>
        /// <param name="regions">区域服务</param>
        /// <param name="workContext"></param>
        public ShipAddressController(IUnitOfWork unitOfWork, IShipAddressesService shipAddresses, IRegionsService regions, IWorkContext workContext)
        {
            _unitOfWork = unitOfWork;
            _shipAddresses = shipAddresses;
            _regions = regions;
            _workContext = workContext;
        }

        #region 区域列表

        /// <summary>
        /// 区域列表
        /// </summary>
        /// <returns></returns>
        public BaseResponse<RegionListResponse> RegionList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var response = new RegionListResponse
                    {
                        RegionList = _regions.GetTreeRegionDataList()
                    };
                    //返回结果
                    return BuildResponse.SuccessResponse(response);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<RegionListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("RegionList", "", ex.Message, ex);
                return BuildResponse.FailResponse<RegionListResponse>("区域址列表错误");
            }
        }

        #endregion

        #region 收货地址列表

        /// <summary>
        /// 收货地址列表
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ShipAddressListResponse> ShipAddressList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //返回未登录信息
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<ShipAddressListResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    var response = new ShipAddressListResponse
                    {
                        ShipAddressList = _shipAddresses.GetShipAddressList(_workContext.CurrentUser.UId)
                    };
                    //返回结果
                    return BuildResponse.SuccessResponse(response);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ShipAddressListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ShipAddressList", "", ex.Message, ex);
                return BuildResponse.FailResponse<ShipAddressListResponse>("收货地址列表错误");
            }
        }

        #endregion

        #region 添加收货地址

        /// <summary>
        /// 添加收货地址
        /// </summary>
        public BaseResponse<ShipAddressPartInfo> AddShipAddress(AddShipAddressRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //返回未登录信息
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<ShipAddressPartInfo>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    //验证联系方式
                    if (!Code.Helper.ValidateHelper.IsValidMobile(request.Mobile.Trim()))
                    {
                        if (!Code.Helper.ValidateHelper.IsPhone(request.Mobile.Trim()))
                        {
                            return BuildResponse.FailResponse<ShipAddressPartInfo>("手机号格式错误");
                        }
                    }

                    //区域信息校验
                    bool isLastLayer = _regions.CheckRegionLayer(request.RegionId, (int)RegionLayer.ToAreaOrCounty);

                    //判断级别信息
                    if (!isLastLayer)
                        return BuildResponse.FailResponse<ShipAddressPartInfo>("区域信息没有精确到区或县");

                    string flag = string.Join(",", request.Flag.Select(a => $"{a.Flag1},{a.Flag2},{a.Flag3}"));

                    var uId = _workContext.CurrentUser.UId;

                    if (_shipAddresses.Exist(m => m.UId == uId && m.IsDefault == (byte)Default.IsDefault) && request.IsDefault == (byte)Default.IsDefault)
                    {
                        _shipAddresses.BatchUpdate(m => m.UId == uId && m.IsDefault == (byte)Default.IsDefault,
                            d => new LF_ShipAddresses { IsDefault = (byte)Default.NotDefault });
                    }

                    //添加收货地址
                    var addShipAddress = new LF_ShipAddresses()
                    {
                        Address = request.Address,
                        Consignee = request.Consignee,
                        IsDefault = request.IsDefault,
                        Mobile = request.Mobile,
                        RegionId = request.RegionId,
                        UId = uId,
                        Flag = flag
                    };

                    //查询是否存在收货地址,不存在设置为默认地址
                    if (!_shipAddresses.Exist(m => m.UId == _workContext.CurrentUser.UId))
                    {
                        addShipAddress.IsDefault = (int)Default.IsDefault;
                    }

                    //添加收货地址
                    _shipAddresses.AddShipAddresses(addShipAddress);

                    //提交事务
                    var result = _unitOfWork.Commit();

                    var shipAddressPartInfo = new ShipAddressPartInfo();

                    if (result > 0)
                    {
                        var region = _regions.LoadEntitieNoTracking(m => m.RegionId == request.RegionId);

                        shipAddressPartInfo = new ShipAddressPartInfo
                        {
                            SAId = addShipAddress.SAId,
                            Consignee = addShipAddress.Consignee,
                            Mobile = addShipAddress.Mobile,
                            ProvinceName = region.ProvinceName,
                            CityName = region.CityName,
                            RName = region.Name,
                            Address = addShipAddress.Address,
                            IsDefault = addShipAddress.IsDefault
                        };
                    }

                    //返回成功信息
                    return result > 0 ? BuildResponse.SuccessResponse(shipAddressPartInfo) : BuildResponse.FailResponse<ShipAddressPartInfo>("添加地址失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                return BuildResponse.FailResponse<ShipAddressPartInfo>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AddShipAddressResponse", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<ShipAddressPartInfo>("添加地址错误");
            }
        }

        #endregion

        #region 编辑收货地址

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        public BaseResponse<string> EditShipAddress(EditShipAddressRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //返回未登录信息 
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<string>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    //验证联系方式
                    if (!Code.Helper.ValidateHelper.IsValidMobile(request.Mobile.Trim()))
                    {
                        if (!Code.Helper.ValidateHelper.IsPhone(request.Mobile.Trim()))
                        {
                            return BuildResponse.FailResponse<string>("手机号格式错误");
                        }
                    }

                    //区域信息校验
                    bool isLastLayer = _regions.CheckRegionLayer(request.RegionId, (int)RegionLayer.ToAreaOrCounty);

                    //判断级别信息
                    if (!isLastLayer)
                        return BuildResponse.FailResponse<string>("区域信息没有精确到区或县");

                    //获取收货地址信息
                    var shipAddressInfo = _shipAddresses.GetShipAddressesById(request.SaId);

                    //如果收货地址不存在，则返回
                    if (shipAddressInfo == null || shipAddressInfo.UId != _workContext.CurrentUser.UId)
                        return BuildResponse.FailResponse<string>("收货地址信息不存在");

                    string flag = string.Join(",", request.Flag.Select(a => $"{a.Flag1},{a.Flag2},{a.Flag3}"));

                    var uId = _workContext.CurrentUser.UId;

                    if (_shipAddresses.Exist(m => m.UId == uId && m.IsDefault == (byte)Default.IsDefault && m.SAId != request.SaId) && request.IsDefault == (byte)Default.IsDefault)
                    {
                        _shipAddresses.BatchUpdate(m => m.UId == uId && m.IsDefault == (byte)Default.IsDefault && m.SAId != request.SaId,
                            d => new LF_ShipAddresses { IsDefault = (byte)Default.NotDefault });
                    }

                    //如果没有更改数据直接返回成功
                    if (shipAddressInfo.Consignee == request.Consignee && shipAddressInfo.Address == request.Address && shipAddressInfo.RegionId == request.RegionId &&
                        shipAddressInfo.Mobile == request.Mobile && shipAddressInfo.Flag == flag && shipAddressInfo.IsDefault == request.IsDefault)
                    {
                        return BuildResponse.SuccessResponse("修改配送地址成功");
                    }

                    //修改收货地址信息
                    shipAddressInfo.Consignee = request.Consignee;
                    shipAddressInfo.Address = request.Address;
                    shipAddressInfo.RegionId = request.RegionId;
                    shipAddressInfo.Mobile = request.Mobile;
                    shipAddressInfo.Flag = flag;
                    shipAddressInfo.IsDefault = request.IsDefault;

                    //提交事务
                    var result = _unitOfWork.Commit();

                    //返回成功信息
                    return result > 0 ? BuildResponse.SuccessResponse("修改配送地址成功") : BuildResponse.FailResponse<string>("修改配送地址失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                return BuildResponse.FailResponse<string>(errorMessage);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("EditShipAddress", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("修改配送地址错误");
            }
        }

        #endregion

        #region 获取收货地址信息

        /// <summary> 
        /// 获取收货地址信息
        /// </summary>
        [HttpPost]
        public BaseResponse<GetShipAddressResponse> GetShipAddress(GetShipAddressRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //返回未登录信息
                    if (_workContext.CurrentUser == null || _workContext.CurrentUser.UId < 1)
                    {
                        return BuildResponse.FailResponse<GetShipAddressResponse>(ResponseCode.NoLogin.GetDescription(), ResponseCode.NoLogin);
                    }

                    //获取收货地址信息
                    var shipAddressInfo = _shipAddresses.GetShipAddressInfo(request.SaId, _workContext.CurrentUser.UId);

                    //如果地址信息为空，则返回
                    if (shipAddressInfo == null)
                        return BuildResponse.FailResponse<GetShipAddressResponse>("收货地址信息不存在");

                    //返回收货地址信息
                    GetShipAddressResponse response = new GetShipAddressResponse
                    {
                        ShipAddressInfo = shipAddressInfo
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                return BuildResponse.FailResponse<GetShipAddressResponse>(errorMessage);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetShipAddress", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<GetShipAddressResponse>("获取配送地址错误");
            }
        }

        #endregion

        #region 删除收货地址

        /// <summary>
        /// 删除收货地址
        /// </summary>
        public BaseResponse<string> DelShipAddress(DelShipAddressRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
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

                    //获取收货地址信息
                    var shipAddressInfo = _shipAddresses.GetShipAddressesById(request.SaId);

                    //如果不存在，则返回
                    if (shipAddressInfo == null || shipAddressInfo.UId != _workContext.CurrentUser.UId)
                        return BuildResponse.FailResponse<string>("地址不存在");

                    //删除收货地址
                    _shipAddresses.DeleteShipAddresses(shipAddressInfo);

                    //提交事务
                    var result = _unitOfWork.Commit();

                    //返回成功消息
                    return result > 0 ? BuildResponse.SuccessResponse("删除成功") : BuildResponse.FailResponse<string>("删除失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("DelShipAddress", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("删除配送地址错误");
            }
        }

        #endregion

        #region 设置默认配送地址

        /// <summary>
        /// 设置默认配送地址
        /// </summary>
        public BaseResponse<string> SetDefaultShipAddress(SetDefaultShipAddressRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
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
                    //获取地址信息
                    var shipAddressInfo = _shipAddresses.GetShipAddressesById(request.SaId);

                    //如果信息不存在，则返回
                    if (shipAddressInfo == null || shipAddressInfo.UId != _workContext.CurrentUser.UId)
                        return BuildResponse.FailResponse<string>("地址信息不存在");

                    //如果已经为默认地址则直接返回
                    if (shipAddressInfo.IsDefault == (int)Default.IsDefault)
                    {
                        return BuildResponse.SuccessResponse("设置成功");
                    }

                    //先把所有地址设为不默认
                    _shipAddresses.BatchUpdate(a => a.UId == _workContext.CurrentUser.UId, a => new LF_ShipAddresses() { IsDefault = (int)Default.NotDefault });

                    //设置默认地址
                    shipAddressInfo.IsDefault = (int)Default.IsDefault;

                    //提交事务
                    int result = _unitOfWork.Commit();

                    //返回结果
                    return result > 0 ? BuildResponse.SuccessResponse("设置成功") : BuildResponse.FailResponse<string>("设置失败");

                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("SetDefaultShipAddress", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("设置默认配送地址错误");
            }
        }

        #endregion

        #region 获取模型验证错误信息

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

                if (string.IsNullOrEmpty(error?.ErrorMessage))
                {
                    ApiLogger.Info("GetModelErrorMsg", key, $"数据验证错误:{error?.Exception.Message}");
                }

                //将错误描述添加到sb中
                errorMessage = error?.ErrorMessage;
            }

            //返回错误信息
            return errorMessage;
        }

        #endregion

    }
}
