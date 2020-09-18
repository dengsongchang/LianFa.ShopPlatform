using System;
using System.Linq;
using System.Web.Http;
using LianFa.ShopPlatform.Model.Response.Admin.Account;
using LianFa.ShopPlatform.Model.AutoMapper.Profiles.Client;
using LianFa.ShopPlatform.Model.Request.Admin.Account;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Randoms;
using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台账号控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台账号相关接口", "用于后台登录注册及管理账号")]
    [RoutePrefix("api/admin/AdminAccount")]
    public class AdminAccountController : ApiController
    {
        /// <summary>
        /// 管理员上下文
        /// </summary>
        private readonly IAdminWorkContext _workContext;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// token服务类
        /// </summary>
        private readonly ITokenService _tokens;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 账号控制器构造函数(构造注入)
        /// </summary>
        /// <param name="users">用户服务</param>
        /// <param name="tokens">token服务类</param>
        /// <param name="workContext">管理员上下文</param>
        /// <param name="unitOfWork">工作单元</param>
        public AdminAccountController(IUsersService users, ITokenService tokens, IAdminWorkContext workContext, IUnitOfWork unitOfWork)
        {
            _users = users;
            _tokens = tokens;
            _workContext = workContext;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="request">请求</param>
        [NoAuth]
        [Route("AdminLogin")]
        public BaseResponse<AdminLoginResponse> AdminLogin(AdminLoginRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取用户信息
                    var userInfo = _users.Get(m => m.UserName == request.UserName && m.AdminGId != (int)UserType.User);

                    if (userInfo == null)
                    {
                        return BuildResponse.FailResponse<AdminLoginResponse>("用户名不存在!");
                    }

                    //判断密码是否正确
                    if (_users.CreateUserPassword(request.Password, userInfo.Salt) != userInfo.PassWord.ToUpper())
                    {
                        return BuildResponse.FailResponse<AdminLoginResponse>("密码不正确!");
                    }

                    //用户信息
                    var authInfo = new AdminInfo
                    {
                        AdminId = userInfo.UId,
                        IsSystemAdmin = userInfo.AdminGId == (int)UserType.SystemManager,
                        AdminGroupId = userInfo.AdminGId,
                        AdminName = userInfo.UserName
                    };

                    //创建Token
                    var token = _tokens.CreateAdminToken(ConfigMap.AdminSignKey, authInfo);
                    if (string.IsNullOrEmpty(token))
                    {
                        ApiLogger.Error("Login", requestString, "token为空");
                        return BuildResponse.FailResponse<AdminLoginResponse>("登录失败");
                    }

                    //更新上下文
                    _workContext.CurrentAdmin = authInfo;

                    //返回响应数据
                    var response = new AdminLoginResponse
                    {
                        UserName = userInfo.UserName,
                        Token = token
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminLoginResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminLogin", requestString, "账号登录出错", ex);
                return BuildResponse.FailResponse<AdminLoginResponse>("账号登录出错");
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        [Route("Logout")]
        public BaseResponse<string> Logout()
        {
            try
            {
                if (_workContext.CurrentAdmin == null || _workContext.CurrentAdmin.AdminId <= 0)
                    return BuildResponse.SuccessResponse("退出登录成功");

                //清除上下文信息
                _workContext.CurrentAdmin = null;
                return BuildResponse.SuccessResponse("退出登录成功");
            }
            catch (Exception ex)
            {
                ApiLogger.Error("Logout", "", "退出登录错误", ex);
                return BuildResponse.FailResponse<string>("退出登录错误");
            }
        }

        #region 操作管理员

        /// <summary>
        /// 删除管理员
        /// </summary>
        [HttpPost]
        [Route("DeleteAdmin")]
        public BaseResponse<string> DeleteAdmin(DeleteAdminRequest requset)
        {
            //将请求参数转为Json字符串
            var requestString = requset.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取管理员信息
                    var adminInfo = _users.Get(u => u.UId == requset.UId && u.AdminGId == (int)UserType.SystemManager);
                    if (adminInfo == null)
                    {
                        return BuildResponse.FailResponse<string>("管理员不存在！");
                    }

                    _users.DeleteUsers(adminInfo);

                    //提交事务
                    _unitOfWork.Commit();

                    return BuildResponse.SuccessResponse("管理员删除成功！");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("DeleteAdmin", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("删除管理员出错");
            }
        }

        /// <summary>
        /// 编辑管理员
        /// </summary>
        [HttpPost]
        [Route("UpdateAdmin")]
        public BaseResponse<string> UpdateAdmin(UpdateAdminRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取管理员信息
                    var adminInfo = _users.Get(u => u.UId == request.UId && u.AdminGId == (int)UserType.SystemManager);
                    if (adminInfo == null)
                    {
                        return BuildResponse.FailResponse<string>("找不到相关管理员");
                    }

                    //修改管理员信息
                    adminInfo.UserName = request.UserName == string.Empty ? adminInfo.UserName : request.UserName;
                    adminInfo.Mobile = request.Mobile == string.Empty ? adminInfo.Mobile : request.Mobile;
                    adminInfo.AdminGId = request.AdminGId;
                    adminInfo.NickName = request.NickName == string.Empty ? adminInfo.NickName : request.NickName;
                    adminInfo.Avatar = request.Avatar == string.Empty ? adminInfo.Avatar : request.Avatar;

                    //修改密码
                    if (!string.IsNullOrEmpty(request.PassWord))
                    {
                        //获取盐值
                        var salt = RandomHelper.CreateRandomValue(6, true);
                        adminInfo.Salt = salt;
                        adminInfo.PassWord = Md5Encrypt.Md5By32(request.PassWord + salt);
                    }

                    //更新管理员
                    _users.UpdateUsers(adminInfo);

                    //提交事务
                    _unitOfWork.Commit();
                    return BuildResponse.SuccessResponse("管理员修改成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("UpdateAdmin", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("管理员修改出错");
            }
        }

        #endregion

        #region 查询管理员

        /// <summary>
        /// 管理员信息
        /// </summary>
        [HttpPost]
        [Route("GetAdminInfo")]
        public BaseResponse<AdminInfoResponse> GetAdminInfo(AdminInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取管理员信息
                    var adminInfo = _users.Get(u => u.UId == request.UId && u.AdminGId == (int)UserType.SystemManager);
                    if (adminInfo == null)
                    {
                        return BuildResponse.FailResponse<AdminInfoResponse>("找不到相关管理员");
                    }

                    //映射为响应类
                    var adminDto = adminInfo.MapTo<AdminInfoResponse>();

                    //返回数据
                    return BuildResponse.SuccessResponse(adminDto);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetAdminInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminInfoResponse>("获取管理员信息出错");
            }
        }

        /// <summary>
        /// 管理员列表
        /// </summary>
        [HttpPost]
        [Route("GetAdminList")]
        public BaseResponse<AdminListResponse> GetAdminList(GetAdminListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取管理员分页列表
                    var userList = _users.GetUserList(new Pager(request.Page.PageIndex, request.Page.PageSize));

                    //映射响应数据
                    var response = new AdminListResponse
                    {
                        AdminList = userList.MapToList<LF_Users, AdminInfoResponse, UserInfoProfile>(),
                        Total = userList.TotalCount
                    };

                    //返回数据
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证出错信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetAdminList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminListResponse>("获取管理员列表出错");
            }
        }

        #endregion

        /// <summary>
        /// 后台修改密码
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [Route("AdminUserUpPasswd")]
        public BaseResponse<string> AdminUserUpPasswd(AdminChangePassWordRequest request)
        {
            //将参数转换为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取当前登录用户
                    var uid = _workContext.CurrentAdmin.AdminId;

                    //判断用户是否存在
                    if (_users.Exist(s => s.UId == uid) == false)
                    {
                        return BuildResponse.FailResponse<string>("不存在该用户");
                    }

                    //获取用户信息
                    var userInfo = _users.GetUsersById(uid);

                    if (_users.CreateUserPassword(request.EnterPassword, userInfo.Salt) != userInfo.PassWord)
                    {
                        return BuildResponse.FailResponse<string>("旧密码错误");
                    }
                    if (request.NewPassword != request.AffirmPassword)
                        return BuildResponse.FailResponse<string>("两次输入密码不一致");

                    if (request.NewPassword.Length < 6)
                    {
                        return BuildResponse.FailResponse<string>("密码长度须不小于6位");
                    }

                    var newpwd = _users.CreateUserPassword(request.NewPassword, userInfo.Salt);
                    if (request.NewPassword == request.AffirmPassword && newpwd == userInfo.PassWord)
                    {
                        return BuildResponse.SuccessResponse("修改密码成功");
                    }

                    //修改并更新用户
                    userInfo.PassWord = _users.CreateUserPassword(request.NewPassword, userInfo.Salt);
                    _users.UpdateUsers(userInfo);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0
                        ? BuildResponse.SuccessResponse("修改密码成功")
                        : BuildResponse.FailResponse<string>("修改密码失败");
                }

                //获取验证出错信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUserUpPasswd", requestString, "后台修改密码错误", ex);
                return BuildResponse.FailResponse<string>("修改密码错误");
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