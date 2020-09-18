using System;
using System.Linq;
using System.Web.Http;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.DepartMent;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.DepartMent;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台商城部门管理控制器类
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台商城部门管理控制器类", "用于后台商城部门管理操作")]
    [RoutePrefix("api/admin/Department")]
    public class AdminDepartMentController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 商城管理组服务
        /// </summary>
        private readonly IAdminGroupsService _adminGroups;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台商城部门管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="admingroups">商城管理组服务</param>
        public AdminDepartMentController(IUnitOfWork unitOfWork, IAdminGroupsService admingroups)
        {
            _unitOfWork = unitOfWork;
            _adminGroups = admingroups;
        }

        /// <summary>
        /// 查询部门管理列表
        /// </summary>
        /// <returns></returns>
        [NoAuth]
        [Route("AdminDepartmentList")]
        public BaseResponse<AdminDepartmentListResponse> AdminDepartmentList(AdminDepartmentListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;
                    var data = _adminGroups.GetDepartmentList(request.Page, out total);

                    //转换为Dto
                    var departmentListDto = data.MapToList<LF_AdminGroups, Department>();

                    var model = new AdminDepartmentListResponse { DepartmentList = departmentListDto, Total = total };
                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminDepartmentListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDepartmentList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminDepartmentListResponse>("查询部门列表出错");
            }
        }

        /// <summary>
        /// 查询所有部门列表
        /// </summary>
        /// <returns></returns>
        [NoAuth]
        [Route("AdminAllDepartmentList")]
        public BaseResponse<AdminDepartmentListResponse> AdminAllDepartmentList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _adminGroups.GetAllDepartmentList();

                    //转换为Dto
                    var departmentListDto = data.MapToList<LF_AdminGroups, Department>();

                    var model = new AdminDepartmentListResponse { DepartmentList = departmentListDto };
                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminDepartmentListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAllDepartmentList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminDepartmentListResponse>("查询部门列表出错");
            }
        }

        /// <summary>
        /// 新增部门管理
        /// </summary>
        /// <returns></returns>
        [AddOperateLog("添加部门管理")]
        [Route("AdminAddDepartment")]
        public BaseResponse<object> AdminAddDepartment(AdminAddDepartmentRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var model = new LF_AdminGroups
                    {
                        Title = request.DepartmentName,
                        Description = request.Description,
                        ActionIds = string.Empty
                    };

                    _adminGroups.AddAdminGroups(model);

                    var row = _unitOfWork.Commit();

                    return row > 0 ? BuildResponse.SuccessResponse<object>("新增成功") : BuildResponse.SuccessResponse<object>("新增失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddDepartment", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("新增部门出错");
            }
        }

        /// <summary>
        /// 删除部门管理
        /// </summary>
        /// <returns></returns>
        [AddOperateLog("删除部门管理")]
        [Route("AdminDeleteDepartment")]
        public BaseResponse<object> AdminDeleteDepartment(AdminDeleteDepartmentRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    _adminGroups.DeleteAdminGroups(d => d.AdminGId == request.AdminGId);//删除部门

                    var row = _unitOfWork.Commit();
                    return row > 0 ? BuildResponse.SuccessResponse<object>("删除成功") : BuildResponse.SuccessResponse<object>("删除失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDeleteDepartment", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除部门出错");
            }
        }

        /// <summary>
        /// 修改部门管理
        /// </summary>
        /// <returns></returns>
        [AddOperateLog("编辑部门管理")]
        [Route("AdminUpdateDepartment")]
        public BaseResponse<object> AdminUpdateDepartment(AdminEditDepartmentRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //修改部门
                    var data = _adminGroups.GetAdminGroupsById(request.AdminGId);

                    data.Title = request.DepartmentName;
                    data.Description = request.Description;
                    data.AdminGId = (short)request.AdminGId;

                    _adminGroups.UpdateAdminGroups(data);

                    var row = _unitOfWork.Commit();

                    return row > 0 ? BuildResponse.SuccessResponse<object>("修改部门成功") : BuildResponse.SuccessResponse<object>("修改部门失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUpdateDepartment", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("修改部门管理出错");
            }
        }

        /// <summary>
        /// 查询权限列表
        /// </summary>
        /// <returns></returns>
        [NoAuth]
        [Route("AdminPermissionsList")]
        public BaseResponse<AdminPermissionsListResponse> AdminPermissionsList(AdminPermissionsListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var data = _adminGroups.GetAdminActionMenuPermissions(request.DepartmentId);

                    var model = new AdminPermissionsListResponse { PermissionsList = data };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminPermissionsListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminPermissionsList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminPermissionsListResponse>("查询权限列表出错");
            }
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <returns></returns>
        [AddOperateLog("设置部门权限")]
        [Route("AdminSetPermissions")]
        public BaseResponse<object> AdminSetPermissions(AdminSetPermissionsRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var adminGroup = _adminGroups.GetList(d => d.AdminGId == request.DepartmentId).FirstOrDefault();
                    if (adminGroup != null)
                    {
                        adminGroup.ActionIds = request.ActionIds;
                        _adminGroups.UpdateAdminGroups(adminGroup);
                    }

                    //成功提交事务,清除对应部门权限缓存
                    var result = _unitOfWork.Commit();
                    if (result > 0)
                    {
                        _adminGroups.ClearPermissionsCache(request.DepartmentId);
                    }

                    return BuildResponse.SuccessResponse<object>("设置成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminSetPermissions", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("设置权限出错");
            }
        }

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
    }
}
