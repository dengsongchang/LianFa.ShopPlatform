using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.ShipCompanies;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.ShipCompanies;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台配送公司控制器
    /// </summary>
    [ControllerGroup("后台配送公司相关接口", "用于后台配送公司操作")]
    [RoutePrefix("api/admin/shipcompanie")]
    public class ShipCompaniesController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 配送公司服务
        /// </summary>
        private readonly IShipCompaniesService _shipCompanies;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台配送公司控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="shipCompanies">配送公司服务</param>
        public ShipCompaniesController(IUnitOfWork unitOfWork, IShipCompaniesService shipCompanies)
        {
            _unitOfWork = unitOfWork;
            _shipCompanies = shipCompanies;
        }

        /// <summary>
        /// 后台配送公司列表
        /// </summary>
        [Route("AdminShipCompanieList")]
        public BaseResponse<AdminShipCompanieListResponse> AdminShipCompanieList(AdminShipCompanieListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var shipCompanieList = _shipCompanies.GetShipCompaniesList(request.Name);
                    var response = new AdminShipCompanieListResponse()
                    {
                        //获取配送公司列表
                        ShipCompanieList = shipCompanieList
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminShipCompanieListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminShipCompanieList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminShipCompanieListResponse>("获取后台配送公司列表错误");
            }
        }

        /// <summary>
        /// 后台配送公司信息
        /// </summary>
        [Route("AdminShipCompanieInfo")]
        public BaseResponse<AdminShipCompanieInfoResponse> AdminShipCompanieInfo(AdminShipCompanieInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    if (ModelState.IsValid)
                    {
                        var shipCompanieInfo = _shipCompanies.LoadEntitieNoTracking(m => m.ShipCoId == request.ShipCoId);

                        if (shipCompanieInfo == null)
                        {
                            return BuildResponse.FailResponse<AdminShipCompanieInfoResponse>("不存在此配送公司!");
                        }

                        shipCompanieInfo.Name = shipCompanieInfo.Name.Trim();

                        var response = new AdminShipCompanieInfoResponse
                        {
                            //获取配送公司
                            ShipCompanieInfo = shipCompanieInfo
                        };

                        return BuildResponse.SuccessResponse(response);

                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminShipCompanieInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminShipCompanieInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminShipCompanieInfoResponse>("获取后台配送公司信息错误");
            }
        }

        /// <summary>
        /// 后台添加配送公司
        /// </summary>
        [Route("AdminAddShipCompanie")]
        public BaseResponse<object> AdminAddShipCompanie(AdminAddShipCompanieRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    if (_shipCompanies.Exist(m => m.Name == request.Name))
                        return BuildResponse.FailResponse<object>("配送公司名称已经存在");

                    if (ModelState.IsValid)
                    {
                        var shipCompanieInfo = new LF_ShipCompanies
                        {
                            Name = request.Name,
                            DisplayOrder = 0,
                            State = (int)ShipCompaniesStatusa.Close
                        };

                        //添加配送公司
                        _shipCompanies.AddShipCompanies(shipCompanieInfo);

                        //提交事务
                        var result = _unitOfWork.Commit();

                        return result > 0 ? BuildResponse.SuccessResponse<object>("添加配送公司成功") : BuildResponse.FailResponse<object>("添加配送公司错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddShipCompanie", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("添加配送公司错误");
            }
        }

        /// <summary>
        /// 后台编辑配送公司
        /// </summary>
        [Route("AdminEditShipCompanie")]
        public BaseResponse<object> AdminEditShipCompanie(AdminEditShipCompanieRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //获取配送公司
                    var shipCompanieInfo = _shipCompanies.GetShipCompaniesById(request.ShipCoId);
                    if (shipCompanieInfo == null)
                        return BuildResponse.FailResponse<object>("此配送公司不存在");
                    if (!string.Equals(shipCompanieInfo.Name.Trim(), request.Name))
                    {
                        if (_shipCompanies.Exist(m => m.Name == request.Name))
                            return BuildResponse.FailResponse<object>("配送公司名称已经存在");
                    }
                    if (string.Equals(shipCompanieInfo.Name.Trim(), request.Name) && shipCompanieInfo.ShipCoId == request.ShipCoId)
                    {
                        return BuildResponse.SuccessResponse<object>("编辑配送公司成功");
                    }

                    if (request.ShipCoId > 0)
                    {
                        //编辑配送公司信息
                        shipCompanieInfo.Name = request.Name;

                        //编辑配送公司
                        _shipCompanies.UpdateShipCompanies(shipCompanieInfo);

                        //提交事务
                        var result = _unitOfWork.Commit();
                        return result > 0 ? BuildResponse.SuccessResponse<object>("编辑配送公司成功") : BuildResponse.FailResponse<object>("编辑配送公司错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditShipCompanie", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("编辑配送公司错误");
            }
        }

        /// <summary>
        /// 后台删除配送公司
        /// </summary>
        [Route("AdminDelShipCompanie")]
        public BaseResponse<object> AdminDelShipCompanie(AdminShipCompanieIdRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取配送公司
                    var shipCompanieInfo = _shipCompanies.GetShipCompaniesById(request.ShipCoId);
                    if (shipCompanieInfo == null)
                        ModelState.AddModelError("BrandName", "请选择配送公司");

                    if (ModelState.IsValid)
                    {
                        //删除配送公司
                        _shipCompanies.DeleteShipCompanies(shipCompanieInfo);

                        //提交事务
                        var result = _unitOfWork.Commit();
                        return result > 0 ? BuildResponse.SuccessResponse<object>("删除配送公司成功") : BuildResponse.FailResponse<object>("删除配送公司错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelShipCompanie", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除配送公司错误");
            }
        }

        /// <summary>
        /// 后台更改配送公司状态
        /// </summary>
        [Route("AdminChangeShipCompanieState")]
        public BaseResponse<object> AdminChangeShipCompanieState(AdminShipCompanieIdRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取配送公司
                    var shipCompanieInfo = _shipCompanies.GetShipCompaniesById(request.ShipCoId);
                    if (shipCompanieInfo == null)
                        return BuildResponse.FailResponse<object>("请选择配送公司");


                    if (shipCompanieInfo.State == (int)ShipCompaniesStatusa.Close)
                    {
                        shipCompanieInfo.State = (int)ShipCompaniesStatusa.Open;
                    }
                    else
                    {
                        shipCompanieInfo.State = (int)ShipCompaniesStatusa.Close;
                    }

                    //更新配送公司状态
                    _shipCompanies.UpdateShipCompanies(shipCompanieInfo);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("更改配送公司状态成功") : BuildResponse.FailResponse<object>("更改配送公司状态错误");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminChangeShipCompanieState", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("更改配送公司状态错误");
            }
        }

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
