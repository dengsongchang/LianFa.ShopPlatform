using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Exports;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Randoms;
using HuCheng.Util.Office.Npoi.Excel;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Admin.User;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.User;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台会员管理控制器
    /// </summary>
    //[AdminAuth]
    [ControllerGroup("后台会员管理相关接口", "用于会员管理操作")]
    [RoutePrefix("api/admin/user")]
    public class UserController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 会员管理
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 会员订单
        /// </summary>
        private readonly IOrdersService _order;

        /// <summary>
        /// 区域管理
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="users"></param>
        /// <param name="order"></param>
        /// <param name="creditLogs"></param>
        /// <param name="regions"></param>
        public UserController(IUnitOfWork unitOfWork, IUsersService users,  IOrdersService order,  IRegionsService regions)
        {
            _unitOfWork = unitOfWork;
            _users = users;
            _order = order;
            _regions = regions;
        }

        #region 后台获取会员列表

        /// <summary>
        /// 后台获取会员列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminUserList")]
        public BaseResponse<AdminUserListResponse> AdminUserList(AdminUserListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;
                    //后台获取会员列表
                    var userList = _users.AdminGetUserList(request.Mobile, request.UserName, request.NickName, request.RegisterTimeStart, request.RegisterTimeEnd, request.Page, out total);

                    AdminUserListResponse model = new AdminUserListResponse()
                    {
                        UserList = userList,
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<AdminUserListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUserList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminUserListResponse>("后台获取会员列表错误");
            }
        }

        #endregion

        #region 后台编辑会员

        /// <summary>
        /// 后台编辑会员
        /// </summary>
        [Route("AdminEditUser")]
        [AddOperateLog("后台编辑会员")]
        public BaseResponse<object> AdminEditUser(AdminEditUserRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得用户相关信息
                    var user = _users.GetUsersById(request.UId);
                    if (user == null)
                        return BuildResponse.FailResponse<object>("会员不存在", ResponseCode.DataError);

                    user.NickName = request.NickName;
                    user.UserName = request.UserName;
                    user.Mobile = request.Mobile;
                    _users.UpdateUsers(user);

                    //提交事务
                    var result = _unitOfWork.Commit();

                    return result > 0 ? BuildResponse.SuccessResponse<object>("后台编辑会员成功") : BuildResponse.FailResponse<object>("后台编辑会员错误");

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditUser", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑会员错误");
            }
        }

        #endregion

        #region 后台编辑会员登陆密码

        /// <summary>
        /// 后台编辑会员登陆密码
        /// </summary>
        [Route("AdminEditUserPwd")]
        [AddOperateLog("后台编辑会员登陆密码")]
        public BaseResponse<object> AdminEditUserPwd(AdminEditUserPwdRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得用户相关信息
                    var user = _users.GetUsersById(request.UId);
                    if (user == null)
                        return BuildResponse.FailResponse<object>("会员不存在", ResponseCode.DataError);

                    //密码验证
                    if (string.IsNullOrWhiteSpace(request.Password))
                    {
                        return BuildResponse.FailResponse<object>("密码不能为空", ResponseCode.DataError);
                    }
                    if (request.Password.Length < 6 || request.Password.Length > 32)
                    {
                        return BuildResponse.FailResponse<object>("密码必须大于6且不大于32个字符", ResponseCode.DataError);
                    }
                    if (request.Password != request.ConfirmPwd)
                    {
                        return BuildResponse.FailResponse<object>("两次输入的密码不一样", ResponseCode.DataError);
                    }

                    //获取盐值
                    var salt = RandomHelper.CreateRandomValue(6, true);

                    //获取加密后的密码
                    var passWord = _users.CreateUserPassword(request.ConfirmPwd, salt);

                    user.PassWord = passWord;
                    user.Salt = salt;

                    _users.UpdateUsers(user);

                    //提交事务
                    var result = _unitOfWork.Commit();

                    return result > 0 ? BuildResponse.SuccessResponse<object>("后台编辑会员登录密码成功") : BuildResponse.FailResponse<object>("后台编辑会员登录密码错误");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditUserPwd", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑会员登录密码错误");
            }
        }

        #endregion

        #region 后台删除会员

        /// <summary>
        /// 后台删除会员
        /// </summary>
        [Route("AdminDelUser")]
        [AddOperateLog("后台删除会员")]
        public BaseResponse<object> AdminDelUser(AdminDelUserRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        _users.BatchDelete(m => request.UId.Contains(m.UId));

                        _order.BatchDelete(m => request.UId.Contains(m.UId));

                        //成功提交事务
                        scope.Complete();

                        return BuildResponse.SuccessResponse<object>("后台删除会员成功");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelUser", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台删除用户错误");
            }
        }

        #endregion

        #region 后台导出用户列表

        /// <summary>
        /// 后台导出用户列表
        /// </summary>
        [Route("ExportAdminUserList")]
        public BaseResponse<string> ExportAdminUserList(ExportAdminUserListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;
                    //上传文件路径
                    const string saveTempPath = "/upload/excel/";
                    //后台获取会员列表 
                    var userList = _users.ExportAdminGetUserList();


                    //Excel头部
                    var headList = new List<string>
                    {
                        "会员ID",
                        "姓名/昵称",
                        "登录帐号",
                        "注册时间"
                    };

                    //获取导出Excel的地址
                    var excelUrl = ExcelExport.ExportExcel(headList, userList, saveTempPath, true, ExportFormat.Xls);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(excelUrl);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("ExportAdminUserList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导出用户列表错误");
            }
        }

        #endregion

        #region 后台获取会员详情

        /// <summary>
        /// 后台获取会员详情
        /// </summary>
        [Route("AdminGetUserInfo")]
        public BaseResponse<AdminGetUserInfoResponse> AdminGetUserInfo(AdminGetUserInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得用户相关信息
                    var user = _users.GetUsersById(request.UId);
                    if (user == null)
                        return BuildResponse.FailResponse<AdminGetUserInfoResponse>("会员不存在", ResponseCode.DataError);

                    //用户基本信息类
                    var userInfo = new UserInformation
                    {
                        UserName = user.UserName,
                        Gender = ((UserGender)user.Gender).GetDescription(),
                        NickName = user.NickName,
                        Mobile = user.Mobile,
                        Avatar = user.Avatar,
                        CreateTime = user.RegisterTime.ToDateTimeString(),
                        AvatarFull = FileHelper.GetFileFullUrl(user.Avatar)
                    };

                    //判断用户是否存在订单表
                    var isHaveOrder = _order.Exist(m => m.UId == request.UId);
                    decimal orderAmount = 0;
                    var orderSum = 0;
                    if (isHaveOrder)
                    {
                        orderAmount = _order.GetUserOrderAmount(request.UId);
                        orderSum = _order.GetUserOrderSum(request.UId);
                    }

                    //统计信息 
                    var statisticsInfo = new Statistics
                    {
                        OrderAmount = orderAmount,
                        OrderSum = orderSum,
                    };

                    var model = new AdminGetUserInfoResponse
                    {
                        UserInfo = userInfo,
                        StatisticsInfo = statisticsInfo,
                    };

                    return BuildResponse.SuccessResponse(model);

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetUserInfoResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetUserInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetUserInfoResponse>("后台获取会员详情错误");
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