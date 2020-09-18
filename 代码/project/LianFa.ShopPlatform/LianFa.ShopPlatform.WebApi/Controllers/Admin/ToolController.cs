using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Cache;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Admin.Regions;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Regions;
using LianFa.ShopPlatform.Model.Response.Admin.UEditor;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台工具类控制器
    /// </summary>
    [ControllerGroup("后台工具类相关接口", "用于后台上传操作")]
    [RoutePrefix("api/admin/tool")]
    public class ToolController : ApiController
    {
        /// <summary>
        /// 区域管理
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();


        /// <summary>
        /// 后台工具类管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="regions">区域管理</param> 
        public ToolController(IRegionsService regions)
        {
            _regions = regions;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("Upload")]
        public IHttpActionResult Upload(string operation)
        {
            if (operation == "uploadproducteditorimage")//上传商品编辑器中图片
            {
                //上传文件路径
                const string saveTempPath = "/upload/admin/product/editor/";

                //上传图片
                var result = new UploadFile().UploadImage(Request.Content, saveTempPath);

                return Content(HttpStatusCode.OK,
                    string.Format("{2}'url':'upload/product/editor/{0}','state':'{1}','originalName':'','name':'','size':'','type':''{3}", result, GetUEState(result), "{", "}"));
            }

            return Content(HttpStatusCode.NotFound, "找不到对应的上传图片方法");
        }

        /// <summary>
        /// 上传商品编辑器中图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("UploadProductEditorImage")]
        public BaseResponse<string> UploadProductEditorImage()
        {
            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/admin/editor/Product/";

                //上传图片
                var result = new UploadFile().UploadImage(Request.Content, saveTempPath);

                //返回完整路径
                var url = FileHelper.GetFileFullUrl(saveTempPath + result);

                return BuildResponse.SuccessResponse(url);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("UploadProductEditorImage", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("上传商品编辑器中图片错误");
            }
        }

        /// <summary>
        /// 上传编辑器中图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("UploadEditor")]
        public IHttpActionResult UploadEditor(string action)
        {
            try
            {
                switch (action)
                {
                    case "config":
                        return Json(Config.Items);
                    case "uploadimage":
                        //上传图片路径
                        const string saveTempPath = "/upload/admin/editor/images";

                        //上传图片
                        var result = new UploadFile().UploadImage(Request.Content, saveTempPath);

                        //返回完整路径
                        var url = FileHelper.GetFileFullUrl(saveTempPath + result);

                        var response = new UEditorUploadImageResponse
                        {
                            State = "SUCCESS",
                            Url = url,
                            Title = result,
                            Original = result
                        };
                        return Json(response);
                    case "uploadvideo":
                        //上传视频文件路径
                        const string videoSaveTempPath = "/upload/admin/editor/video/";

                        //上传视频
                        var videoResult = new UploadFile().UploadImage(Request.Content, videoSaveTempPath);

                        //返回完整路径
                        var videoUrl = FileHelper.GetFileFullUrl(videoSaveTempPath + videoResult);

                        var videoResponse = new UEditorUploadImageResponse
                        {
                            State = "SUCCESS",
                            Url = videoUrl,
                            Title = videoResult,
                            Original = videoResult
                        };
                        return Json(videoResponse);
                    default:
                        var noResponse = new UEditorNotSupportedResponse
                        {
                            State = "action 参数为空或者 action 不被支持。"
                        };
                        return Json(noResponse);
                }
            }
            catch (Exception ex)
            {
                ApiLogger.Error("UploadProductEditorImage", "", ex.Message, ex);
                return Content(HttpStatusCode.OK, "未知错误");
            }
        }

        /// <summary>
        /// 获得ueditor状态
        /// </summary>
        /// <param name="result">上传结果</param>
        /// <returns></returns>
        private string GetUEState(string result)
        {
            if (result == "-1")
            {
                return "上传图片不能为空";
            }
            else if (result == "-2")
            {
                return "不允许的图片类型";
            }
            else if (result == "-3")
            {
                return "图片大小超出网站限制";
            }
            else
            {
                return "SUCCESS";
            }
        }

        #region 获取省/自治区/直辖市列表
        /// <summary>
        /// 获取省/自治区/直辖市列表
        /// </summary>
        /// <returns></returns>
        public BaseResponse<AdminRegionsResponse> ProvinceList(AdminCityRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = new List<RegionsList> { new RegionsList { Name = "请选择省", Layer = 0, RegionId = 0 } };

                    //获得省列表
                    var regionsList = _regions.GetProvinceList();

                    info.AddRange(regionsList);

                    var model = new AdminRegionsResponse { RegionsList = request.Type == 0 ? info : regionsList };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ProvinceList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询省/自治区/直辖市列表出错");
            }
        }
        #endregion

        #region 获取地级市列表
        /// <summary>
        /// 获取地级市列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<AdminRegionsResponse> CityList(AdminCityRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = new List<RegionsList> { new RegionsList { Name = "请选择市", Layer = 0, RegionId = 0 } };

                    //获得地级市列表
                    var regionsList = _regions.GetCityList(request.ParentId);

                    info.AddRange(regionsList);

                    var model = new AdminRegionsResponse { RegionsList = request.Type == 0 ? info : regionsList };


                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("CityList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询地级市列表出错");
            }
        }
        #endregion

        #region 获取市辖区/县(县市级)列表
        /// <summary>
        /// 获取市辖区/县(县市级)列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<AdminRegionsResponse> MunicipalDistrictList(AdminCityRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = new List<RegionsList> { new RegionsList { Name = "请选择区", Layer = 0, RegionId = 0 } };

                    //获得省列表
                    var regionsList = _regions.GetMunicipalDistrictList(request.ParentId);

                    info.AddRange(regionsList);

                    var model = new AdminRegionsResponse { RegionsList = request.Type == 0 ? info : regionsList };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminRegionsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("MunicipalDistrictList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminRegionsResponse>("查询市辖区/县(县市级)列表出错");
            }
        }
        #endregion

        #region 新区域列表

        /// <summary>
        /// 新区域列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public BaseResponse<NewRegionListResponse> NewRegionList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //查找缓存数据
                    var response = CacheHelper.GetCache(CacheKeys.NEWREGION_LIST) as NewRegionListResponse;
                    if (response == null)
                    {
                        //获得省列表
                        var regionsList = _regions.GetTreeRegionDataList();
                        //定义省市区动态类型
                        dynamic province = new System.Dynamic.ExpandoObject();
                        dynamic city = new System.Dynamic.ExpandoObject();
                        dynamic county = new System.Dynamic.ExpandoObject();

                        //创建省索引 
                        var pIndex = 1;
                        //遍历省节点
                        foreach (var item in regionsList)
                        {
                            //按照索引规则 创建省属性名
                            var key = string.Concat(pIndex.ToString().PadLeft(2, '0'), "0000");
                            //添加省属性值
                            ((IDictionary<string, object>) province).Add(key, new
                            {
                                Name = item.name,
                                RegionId = item.code
                            });

                            //创建市索引 
                            var cIndex = 1;
                            //遍历市节点
                            foreach (var cityItem in item.sub)
                            {
                                //按照索引规则 创建市属性名
                                var cityKey = string.Concat(pIndex.ToString().PadLeft(2, '0'),
                                    cIndex.ToString().PadLeft(2, '0'), "00");
                                //添加市属性值
                                ((IDictionary<string, object>) city).Add(cityKey, new
                                {
                                    Name = cityItem.name,
                                    RegionId = cityItem.code
                                });

                                //创建区索引 
                                var coIndex = 1;
                                //遍历区节点
                                foreach (var countyItem in cityItem.sub)
                                {
                                    //按照索引规则 创建区属性名
                                    var countyKey = string.Concat(pIndex.ToString().PadLeft(2, '0'),
                                        cIndex.ToString().PadLeft(2, '0'), coIndex.ToString().PadLeft(2, '0'));
                                    //添加区属性值
                                    ((IDictionary<string, object>) county).Add(countyKey, new
                                    {
                                        Name = countyItem.name,
                                        RegionId = countyItem.code
                                    });
                                    coIndex++;
                                }
                                cIndex++;
                            }
                            pIndex++;
                        }

                        response = new NewRegionListResponse
                        {
                            ProvinceList = province,
                            CityList = city,
                            CountyList = county,
                        };
                        //设置缓存,永久缓存
                        CacheHelper.SetCache(CacheKeys.NEWREGION_LIST, response, CacheKeys.YearCacheTime);
                    }
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<NewRegionListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("NewRegionList", "", ex.Message, ex);
                return BuildResponse.FailResponse<NewRegionListResponse>("查询区域列表列表出错");
            }
        }

        #endregion


        #region 清除缓存
        /// <summary>
        ///  清除缓存
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public BaseResponse<object> ClearTheCache()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //清除分类缓存键
                    CacheHelper.ClearCahe(CacheKeys.SHOP_CATEGORY_LIST);
                    CacheHelper.ClearCahe(CacheKeys.ADMIN_ADMINMENU_LIST);
                    CacheHelper.ClearCahe(CacheKeys.ADMIN_ADMINACTION_LIST);

                    return BuildResponse.SuccessResponse<object>("清除成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ClearTheCache", "", ex.Message, ex);
                return BuildResponse.FailResponse<object>("清除缓存出错");
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
