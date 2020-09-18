using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HuCheng.Util.Core.Config;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Code.File;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Banner;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Banner;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台轮播图控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台轮播相关接口", "用于后台轮播管理相关")]
    [RoutePrefix("api/admin/Banner")]
    public class BannerController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 轮播管理
        /// </summary>
        private readonly IBannersService _banners;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 配置文件管理器
        /// </summary>
        public IConfigManager ConfigManager = Bootstrapper.GetFromFac<IConfigManager>();

        /// <summary>
        /// 轮播控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="banners">轮播管理</param>
        public BannerController(IUnitOfWork unitOfWork, IBannersService banners)
        {
            _unitOfWork = unitOfWork;
            _banners = banners;
        }

        #region 后台轮播列表

        /// <summary>
        /// 后台轮播列表
        /// </summary>
        /// <returns></returns>
        [Route("AdminBannerList")]
        public BaseResponse<AdminBannerListResponse> AdminBannerList(AdminBannerListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;
                    var list = _banners.AdminBannerList(request.Page, out total);

                    var response = new AdminBannerListResponse
                    {
                        AdminBannerList = list,
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminBannerListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminBannerList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminBannerListResponse>("获取后台轮播列表错误");
            }
        }

        #endregion

        #region 后台轮播信息

        /// <summary>
        /// 后台轮播信息
        /// </summary>
        /// <returns></returns>
        [Route("AdminBannerInfo")]
        public BaseResponse<AdminBannerInfoResponse> AdminBannerInfo(AdminBannerInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = _banners.AdminBannerInfo(request.BannerId);
                    if (info == null)
                        return BuildResponse.FailResponse<AdminBannerInfoResponse>("不存在该轮播信息");

                    var response = new AdminBannerInfoResponse
                    {
                        AdminBannerInfo = info
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminBannerInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminBannerInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminBannerInfoResponse>("获取后台轮播信息错误");
            }
        }

        #endregion

        #region 后台添加轮播

        /// <summary>
        /// 后台添加轮播
        /// </summary>
        /// <returns></returns>
        [Route("AdminAddBanner")]
        [AddOperateLog("添加轮播图")]
        public BaseResponse<object> AdminAddBanner(AdminAddBannerRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var bannerInfo = new LF_Banners
                    {
                        Url = string.IsNullOrEmpty(request.Link) ? "" : request.Link,
                        ShowImg = request.ShowImg
                    };
                    _banners.AddBanners(bannerInfo);
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
                ApiLogger.Error("AdminAddBanner", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台添加轮播错误");
            }
        }

        #endregion

        #region 后台编辑轮播

        /// <summary>
        /// 后台编辑轮播
        /// </summary>
        /// <returns></returns>
        [Route("AdminEditBanner")]
        [AddOperateLog("编辑轮播图")]
        public BaseResponse<object> AdminEditBanner(AdminEditBannerRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var info = _banners.GetBannersById(request.BannerId);

                    if (info == null)
                    {
                        return BuildResponse.FailResponse<object>("无此轮播信息");
                    }

                    if (info.ShowImg == request.ShowImg && info.Url == request.Link)
                    {
                        return BuildResponse.SuccessResponse<object>("编辑成功");
                    }

                    info.ShowImg = request.ShowImg;
                    info.Url = string.IsNullOrEmpty(request.Link) ? "" : request.Link;

                    _banners.UpdateBanners(info);
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
                ApiLogger.Error("AdminEditBanner", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑轮播错误");
            }
        }

        #endregion

        #region 后台删除轮播

        /// <summary>
        /// 后台删除轮播
        /// </summary>
        /// <returns></returns>
        [Route("AdminDelBanner")]
        [AddOperateLog("删除轮播图")]
        public BaseResponse<object> AdminDelBanner(AdminDelBannerRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    if (!request.BannerIdList.Any())
                    {
                        return BuildResponse.FailResponse<object>("请选择要删除的轮播");
                    }

                    _banners.BatchDelete(m => request.BannerIdList.Contains(m.BannerId));

                    return BuildResponse.SuccessResponse<object>("删除成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelBanner", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台删除轮播错误");
            }
        }

        #endregion

        #region 后台上传轮播图片

        /// <summary>
        /// 上传轮播图片
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AdminUploadBannerImg")]
        public async Task<BaseResponse<string>> AdminUploadBannerImg()
        {

            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/client/BannerImg/";

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
                ApiLogger.Error("AdminUploadBannerImg", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台上传轮播图片错误");
            }
        }

        #endregion

        #region 广告位设置数据

        /// <summary>
        /// 广告位设置数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminScreenSetupData")]
        public BaseResponse<AdminScreenSetupDataResponse> AdminScreenSetupData()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var model = new AdminScreenSetupDataResponse
                    {
                        ScreenImg = ConfigMap.ScreenImg,
                        ScreenFullImg = FileHelper.GetFileFullUrl(ConfigMap.ScreenImg),
                        ScreenLink = ConfigMap.ScreenLink
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminScreenSetupDataResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminScreenSetupData", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminScreenSetupDataResponse>("查询广告位设置出错");
            }
        }

        #endregion

        #region 广告位设置

        /// <summary>
        /// 广告位设置
        /// </summary>
        /// <returns></returns>
        [Route("AdminScreenSetup")]
        [AddOperateLog("广告位设置")]
        public BaseResponse<object> AdminScreenSetup(AdminScreenSetupRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    ConfigManager.WriteConfig("BasicSetting", "configuration/ScreenImg", request.ScreenImg);
                    ConfigManager.WriteConfig("BasicSetting", "configuration/ScreenLink", request.ScreenLink);

                    //返回成功信息
                    return BuildResponse.SuccessResponse<object>("编辑成功");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminScreenSetup", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("广告位设置出错");
            }
        }

        #endregion

        #region 礼品卡banner设置数据

        /// <summary>
        /// 礼品卡banner设置数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminCouponImgSetupData")]
        public BaseResponse<AdminCouponImgSetupDataResponse> AdminCouponImgSetupData()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var model = new AdminCouponImgSetupDataResponse
                    {
                        CouponImg = ConfigMap.CouponImg,
                        CouponFullImg = FileHelper.GetFileFullUrl(ConfigMap.CouponImg)
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminCouponImgSetupDataResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminCouponImgSetupData", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminCouponImgSetupDataResponse>("查询礼品卡banner设置出错");
            }
        }

        #endregion

        #region 礼品卡banner设置

        /// <summary>
        /// 礼品卡banner设置
        /// </summary>
        /// <returns></returns>
        [Route("AdminCouponImgSetup")]
        [AddOperateLog("礼品卡banner设置")]
        public BaseResponse<object> AdminCouponImgSetup(AdminCouponImgSetupRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    ConfigManager.WriteConfig("BasicSetting", "configuration/CouponImg", request.CouponImg);

                    //返回成功信息
                    return BuildResponse.SuccessResponse<object>("编辑成功");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminCouponImgSetup", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("礼品卡banner设置出错");
            }
        }

        #endregion

        #region 后台上传广告位相关图片

        /// <summary>
        /// 后台上传广告位相关图片
        /// </summary>
        [AllowAnonymous]
        [Route("AdminSetupScreenImg")]
        public async Task<BaseResponse<string>> AdminSetupScreenImg()
        {

            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/admin/SetupScreenImg/";

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
                ApiLogger.Error("AdminSetupScreenImg", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台上传广告位相关图片错误");
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
