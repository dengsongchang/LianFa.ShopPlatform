using System;
using System.Linq;
using System.Web.Http;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Security.Tokens;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin.OperateLog;
using LianFa.ShopPlatform.Model.Request.Admin.AdminOperateLog;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Account;
using LianFa.ShopPlatform.Model.Response.Admin.Admin;
using LianFa.ShopPlatform.Model.Response.Admin.AdminOperateLog;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台日志控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台操作日志相关接口", "用于后台操作日志操作")]
    [RoutePrefix("api/admin/adminOperateLog")]
    public class AdminOperateLogController : ApiController
    {
        /// <summary>
        /// 工作上下文
        /// </summary>
        private readonly IAdminWorkContext _adminWorkContext;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly IAdminOperateLogsService _adminOperateLogs;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台日志控制器构造函数(构造注入)
        /// </summary>
        /// <param name="adminWorkContext">工作上下文</param>
        /// <param name="adminOperateLogs">日志服务</param>
        public AdminOperateLogController(IAdminWorkContext adminWorkContext, IAdminOperateLogsService adminOperateLogs)
        {
            _adminWorkContext = adminWorkContext;
            _adminOperateLogs = adminOperateLogs;

        }

        /// <summary>
        /// 后台管理员列表
        /// </summary>
        [Route("AdminList")]
        public BaseResponse<AdminListsResponse> AdminList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取日志表管理员列表
                    var adminDto = _adminOperateLogs.GetAdminList();

                    //模型映射
                    var adminList = adminDto.MapToList<LF_AdminOperateLogs, AdminsInfo>();

                    var response = new AdminListsResponse
                    {
                        AdminList = adminList
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminListsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminListsResponse>("获取后台管理员列表错误");
            }
        }

        /// <summary>
        /// 后台管理员操作日志列表
        /// </summary>
        [NoAuth]
        [Route("AdminOperateLogList")]
        public BaseResponse<AdminOperateLogListResponse> AdminOperateLogList(AdminOperateLogListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    int total;

                    //获取操作日志表
                    var adminOperateLogsDto = _adminOperateLogs.GetAdminOperateLogsList(request.Page, request.StartTime, request.EndTime, request.UId, out total);

                    //模型映射
                    var adminOperateLogsList = adminOperateLogsDto.MapToList<LF_AdminOperateLogs, AdminOperateLogInfo, AdminAdminOperateLogListProfle>();

                    var response = new AdminOperateLogListResponse
                    {
                        AdminOperateLogList = adminOperateLogsList,
                        Total = total
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<AdminOperateLogListResponse>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminOperateLogList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminOperateLogListResponse>("获取后台管理员操作日志列表错误");
            }
        }

        /// <summary>
        /// 后台删除管理员操作日志列表
        /// </summary>
        [HttpPost]
        [AddOperateLog("后台删除管理员操作日志列表")]
        [Route("DeleteAdminOperateLog")]
        public BaseResponse<object> DeleteAdminOperateLog(AdminDelAdminOperateLogRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_adminWorkContext.CurrentAdmin.IsSystemAdmin)
                    {
                        return BuildResponse.FailResponse<object>("只有系统管理员才可以删除操作日志");
                    }

                    //删除操作日志
                    _adminOperateLogs.BatchDelete(a => request.LogIdList.Contains(a.LogId));

                    return BuildResponse.SuccessResponse<object>("删除操作日志成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelAdminOperateLog", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除操作日志错误");
            }
        }

        /// <summary>
        /// 后台清空管理员操作日志列表
        /// </summary>
        [AddOperateLog("后台清空管理员操作日志列表")]
        [Route("ClearAdminOperateLog")]
        public BaseResponse<object> ClearAdminOperateLog()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_adminWorkContext.CurrentAdmin.IsSystemAdmin)
                    {
                        return BuildResponse.FailResponse<object>("只有系统管理员才可以删除操作日志");
                    }

                    //清空操作日志
                    _adminOperateLogs.BatchDelete(a => true);

                    //提交事务
                    return BuildResponse.SuccessResponse<object>("清空操作日志成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("ClearAdminOperateLog", "", ex.Message, ex);
                return BuildResponse.FailResponse<object>("清空操作日志错误");
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

    }
}