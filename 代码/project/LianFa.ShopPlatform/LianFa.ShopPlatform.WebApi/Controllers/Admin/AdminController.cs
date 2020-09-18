using System;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Randoms;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Admin;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;
using LianFa.ShopPlatform.Model.Response.Admin.Admin;
using LianFa.ShopPlatform.Model.Response.Admin.DepartMent;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台管理员管理控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台管理员管理相关接口", "后台管理员管理操作")]
    [RoutePrefix("api/admin/admin")]
    public class AdminController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 会员管理服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 工作上下文
        /// </summary>
        private readonly IAdminWorkContext _adminWorkContext;

        /// <summary>
        /// 商城管理组服务
        /// </summary>
        private readonly IAdminGroupsService _admingroups;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台管理员管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="users">管理员管理服务</param>
        /// <param name="adminWorkContext">工作上下文</param>
        /// <param name="admingroups">商城管理组服务</param>
        public AdminController(IUnitOfWork unitOfWork, IUsersService users, IAdminWorkContext adminWorkContext, IAdminGroupsService admingroups)
        {
            _unitOfWork = unitOfWork;
            _users = users;
            _adminWorkContext = adminWorkContext;
            _admingroups = admingroups;
        }

        /// <summary>
        /// 后台获取管理员列表
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [NoAuth]
        [Route("AdminAdminList")]
        public BaseResponse<AdminListsResponse> AdminAdminList(AdminAdminListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //后台获取会员列表 
                    var adminList = _users.GetAdminList(request.UserName, request.AdminGId);
                    adminList.ForEach(x => x.RegisterTimeStr = x.RegisterTime.ToDateTimeString(true));

                    var response = new AdminListsResponse
                    {
                        AdminList = adminList
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<AdminListsResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAdminList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminListsResponse>("后台获取管理员列表错误");
            }


        }

        /// <summary>
        /// 后台添加管理员
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [AddOperateLog("添加管理员")]
        [Route("AdminAddAdmin")]
        public BaseResponse<object> AdminAddAdmin(AdminAddAdminRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    #region 授权验证 暂未开启

                    ////获得系统管理员信息
                    //var admin = _adminWorkContext.CurrentAdmin;

                    ////身份验证
                    //if (admin == null)
                    //{
                    //    return BuildResponse.FailResponse<object>("请登录");
                    //}

                    //if (!admin.IsAdmin)
                    //{
                    //    return BuildResponse.FailResponse<object>("该用户不为后台总管理员，无法添加");
                    //}

                    #endregion

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        //后台获取管理员信息
                        var adminInfo = _users.GetUserInfoByName(request.UserName);

                        if (adminInfo != null && adminInfo.AdminGId != (int)UserType.User)
                            return BuildResponse.FailResponse<object>("管理员信息已经存在！");

                        //获取盐值
                        var salt = RandomHelper.CreateRandomValue(6, true);

                        //获取加密后的密码
                        var passWord = _users.CreateUserPassword(request.PassWord, salt);

                        var adminPartInfo = new LF_Users
                        {
                            UserName = request.UserName,
                            PassWord = passWord,
                            Salt = salt,
                            AdminGId = request.AdminGId,
                            Avatar = "",
                            Mobile = "",
                            RegisterTime = DateTime.Now,
                            Gender = (byte)WhetherType.No,
                            NickName = "",
                        };

                        //添加管理员信息
                        _users.AddUsers(adminPartInfo);

                        //提交事务
                        _unitOfWork.Commit();

                        //成功提交事务
                        scope.Complete();

                        return BuildResponse.SuccessResponse<object>("后台添加管理员成功");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddAdmin", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("添加管理错误");
            }


        }

        /// <summary>
        ///后台删除管理员
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [AddOperateLog("删除管理员")]
        [Route("AdminDelAdmin")]
        public BaseResponse<object> AdminDelAdmin(AdminDelAdminRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    #region 授权验证 暂未开启

                    ////获得系统管理员信息
                    //var admin = _adminWorkContext.CurrentAdmin;

                    ////身份验证
                    //if (admin == null)
                    //{
                    //    return BuildResponse.FailResponse<object>("请登录");
                    //}

                    //if (!admin.IsAdmin)
                    //{
                    //    return BuildResponse.FailResponse<object>("该用户不为后台总管理员，无法添加");
                    //}

                    #endregion

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        _users.BatchDelete(m => request.UId == m.UId);

                        //成功提交事务
                        scope.Complete();

                        return BuildResponse.SuccessResponse<object>("后台删除管理员成功");
                    }
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelAdmin", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台删除管理员错误");
            }
        }

        /// <summary>
        /// 后台获取管理员信息
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [Route("AdminAdminInfo")]
        public BaseResponse<AdminInfosResponse> AdminAdminInfo(AdminAdminInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    #region 授权验证 暂未开启

                    ////获得系统管理员信息
                    //var admin = _adminWorkContext.CurrentAdmin;

                    ////身份验证
                    //if (admin == null)
                    //{
                    //    return BuildResponse.FailResponse<AdminInfoResponse>("请登录");
                    //}

                    //if (!admin.IsAdmin)
                    //{
                    //    return BuildResponse.FailResponse<AdminInfoResponse>("该用户不为后台总管理员，无法添加");
                    //}

                    #endregion

                    //后台获取管理员信息
                    var adminInfo = _users.GetAdminInfo(request.UId);

                    if (adminInfo == null)
                    {
                        return BuildResponse.FailResponse<AdminInfosResponse>("管理员不存在！请确定");
                    }

                    var response = new AdminInfosResponse
                    {
                        AdminInfo = adminInfo
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<AdminInfosResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAdminInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminInfosResponse>("后台获取管理员信息错误");
            }


        }


        /// <summary>
        /// 后台编辑管理员基本信息
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [AddOperateLog("编辑管理员基本信息")]
        [Route("AdminEditAdminInfo")]
        public BaseResponse<object> AdminEditAdminInfo(AdminEditAdminInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    #region 授权验证 暂未开启

                    ////获得系统管理员信息
                    //var admin = _adminWorkContext.CurrentAdmin;

                    ////身份验证
                    //if (admin == null)
                    //{
                    //    return BuildResponse.FailResponse<object>("请登录");
                    //}

                    //if (!admin.IsAdmin)
                    //{
                    //    return BuildResponse.FailResponse<object>("该用户不为后台总管理员，无法添加");
                    //}

                    #endregion

                    //获取用户信息
                    var userInfo = _users.GetUsersById(request.UId);

                    if (userInfo == null)
                    {
                        return BuildResponse.FailResponse<object>("管理员信息不存在！");
                    }

                    //赋值
                    userInfo.AdminGId = request.AdmingId;

                    //更新
                    _users.UpdateUsers(userInfo);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("后台编辑管理员成功") : BuildResponse.FailResponse<object>("后台编辑管理员错误");

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditAdminInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑管理员错误");
            }
        }


        /// <summary>
        /// 后台编辑管理员密码
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [AddOperateLog("编辑管理员密码")]
        [Route("AdminEditAdminPwd")]
        public BaseResponse<object> AdminEditAdminPwd(AdminEditAdminPwdRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    #region 授权验证 暂未开启

                    ////获得系统管理员信息
                    //var admin = _adminWorkContext.CurrentAdmin;

                    ////身份验证
                    //if (admin == null)
                    //{
                    //    return BuildResponse.FailResponse<object>("请登录");
                    //}

                    //if (!admin.IsAdmin)
                    //{
                    //    return BuildResponse.FailResponse<object>("该用户不为后台总管理员，无法添加");
                    //}

                    #endregion

                    //获取用户信息
                    var userInfo = _users.GetUsersById(request.UId);

                    if (userInfo == null)
                    {
                        return BuildResponse.FailResponse<object>("管理员信息不存在！");
                    }

                    //获取盐值
                    var salt = RandomHelper.CreateRandomValue(6, true);

                    //获取加密后的密码
                    var passWord = _users.CreateUserPassword(request.PassWord, salt);

                    //赋值
                    userInfo.PassWord = passWord;
                    userInfo.Salt = salt;

                    //更新
                    _users.UpdateUsers(userInfo);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("后台编辑管理员成功") : BuildResponse.FailResponse<object>("后台编辑管理员错误");

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditAdminPwd", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑管理员错误");
            }
        }

        /// <summary>
        /// 获取管理员权限菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAdminPermissionsList")]
        public BaseResponse<AdminPermissionsListResponse> GetAdminPermissionsList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //如果为系统管理员则获取所有权限列表,否则取对应权限
                    var permissionsList = _adminWorkContext.CurrentAdmin.IsSystemAdmin ? _admingroups.GetAllAdminActionMenuList() :
                        _admingroups.GetAdminActionPermissionsList(_adminWorkContext.CurrentAdmin.AdminGroupId);

                    var model = new AdminPermissionsListResponse { PermissionsList = permissionsList };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminPermissionsListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetAdminPermissionsList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminPermissionsListResponse>("获取管理员权限列表出错");
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