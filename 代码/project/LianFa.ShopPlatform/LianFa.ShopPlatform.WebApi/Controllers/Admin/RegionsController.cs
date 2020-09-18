using System;
using System.Linq;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Regions;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Regions;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台区域管理控制器
    /// </summary>
    [ControllerGroup("后台区域管理相关接口", "用于后台区域管理操作")]
    [RoutePrefix("api/admin/regions")]
    public class RegionsController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 区域服务
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台区域管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="regions">区域服务</param>
        public RegionsController(IUnitOfWork unitOfWork, IRegionsService regions)
        {
            _unitOfWork = unitOfWork;
            _regions = regions;
        }

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
                        .Select(d => new RegionsList { Name = d.Name, RegionId = d.RegionId }).ToList();

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
                        .Select(d => new RegionsList { Name = d.Name, RegionId = d.RegionId }).ToList();

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
                        .Select(d => new RegionsList { Name = d.Name, RegionId = d.RegionId }).ToList();

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
                        .Select(d => new RegionsList { Name = d.Name, RegionId = d.RegionId }).ToList();

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

        #region 增加区域
        /// <summary>
        /// 增加区域
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminAddRegion")]
        public BaseResponse<AdminAddReguinsResponse> AdminAddRegion(AdminAddRegionRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    LF_Regions hr = new LF_Regions();
                    hr.CityId = request.CityId;
                    hr.CityName = _regions.GetRegionsById(request.CityId).Name;
                    hr.DisplayOrder = 0;
                    hr.Layer = request.Layer;
                    hr.Name = request.Name;
                    hr.ParentId = request.ParentId;
                    hr.ProvinceId = request.ProvinceId;
                    hr.ProvinceName = _regions.GetRegionsById(request.ProvinceId).Name;
                    hr.ShortSpell = "";
                    hr.Spell = "";

                    _regions.AddRegions(hr);
                    int row = _unitOfWork.Commit();
                    if (row > 0)
                    {
                        AdminAddReguinsResponse model = new AdminAddReguinsResponse() { RegionId = hr.RegionId };
                        return BuildResponse.SuccessResponse(model);
                    }
                    else
                    {
                        return BuildResponse.FailResponse<AdminAddReguinsResponse>("新增区域失败");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminAddReguinsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddReguins", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminAddReguinsResponse>("增加区域时出错");
            }
        }
        #endregion

        #region 编辑区域
        /// <summary>
        /// 编辑区域
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminEditRegion")]
        public BaseResponse<object> AdminEditRegion(AdminUpRegionRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _regions.GetRegionsById(request.RegionId);
                    data.Name = request.Name;

                    _regions.UpdateRegions(data);
                    int row = _unitOfWork.Commit();
                    if (row > 0)
                    {
                        return BuildResponse.SuccessResponse<object>("编辑域区域成功");
                    }
                    else
                    {
                        return BuildResponse.FailResponse<object>("编辑区域失败");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditRegion", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("编辑区域时出错");
            }
        }
        #endregion

        #region 删除区域
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminDeleteRegion")]
        public BaseResponse<object> AdminDeleteRegion(AdminUpRegionRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    _regions.DeleteRegions(d => d.RegionId == request.RegionId);

                    int row = _unitOfWork.Commit();
                    if (row > 0)
                    {
                        return BuildResponse.SuccessResponse<object>("删除区域成功");
                    }
                    else
                    {
                        return BuildResponse.FailResponse<object>("删除区域失败");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDeleteRegion", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除区域时出错");
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
