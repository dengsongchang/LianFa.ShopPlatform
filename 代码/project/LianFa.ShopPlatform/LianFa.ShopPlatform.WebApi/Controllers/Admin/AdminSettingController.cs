using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HuCheng.Util.Core.Config;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Admin.Setting;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Setting;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台平台设置控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台平台设置相关接口", "用于后台设置管理操作")]
    [RoutePrefix("api/admin/AdminSetting")]
    public class AdminSettingController : ApiController
    {
        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 配置文件管理器
        /// </summary>
        public IConfigManager ConfigManager = Bootstrapper.GetFromFac<IConfigManager>();

        #region 平台设置数据

        /// <summary>
        /// 平台设置数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminPlatformSetupData")]
        public BaseResponse<AdminBasicSetupDataResponse> AdminPlatformSetupData()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var model = new AdminBasicSetupDataResponse
                    {
                        Address = ConfigMap.Address,
                        CustomerPhone = ConfigMap.CustomerPhone
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminBasicSetupDataResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminPlatformSetupData", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminBasicSetupDataResponse>("查询平台设置出错");
            }
        }

        #endregion

        #region 平台设置

        /// <summary>
        /// 平台设置
        /// </summary>
        /// <returns></returns>
        [Route("AdminPlatformSetup")]
        [AddOperateLog("平台设置")]
        public BaseResponse<object> AdminPlatformSetup(AdminBasicSetupRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    ConfigManager.WriteConfig("BasicSetting", "configuration/Address", request.Address);
                    ConfigManager.WriteConfig("BasicSetting", "configuration/CustomerPhone", request.CustomerPhone);

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
                ApiLogger.Error("AdminPlatformSetup", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("平台设置出错");
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
