using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using LianFa.ShopPlatform.Model.Response.Client.Account;
using LianFa.ShopPlatform.Model.Request.Client.Account;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Randoms;
using HuCheng.Util.Core.Security.Tokens;
using HuCheng.Util.Sms.Aliyun;
using LianFa.ShopPlatform.Code.Cache;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [Signature]
    [ControllerGroup("账号接口", "用于登录注册账号")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// 上下文
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 用户Token服务
        /// </summary>
        private readonly ITokenService _tokens;

        /// <summary>
        /// 阿里云短信服务
        /// </summary>
        private readonly IAliyunSmsService _aliyunSms;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 构造函数(构造注入)
        /// </summary>
        /// <param name="workContext">上下文</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="users">用户服务</param>
        /// <param name="tokens">用户Token服务</param>
        public AccountController(IWorkContext workContext, IUnitOfWork unitOfWork, ITokenService tokens, IUsersService users, IAliyunSmsService aliyunSms)
        {
            _workContext = workContext;
            _unitOfWork = unitOfWork;
            _users = users;
            _aliyunSms = aliyunSms;
            _tokens = tokens;
        }

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request">请求</param>
        [NoAuth]
        public BaseResponse<LoginResponse> Login(LoginRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    if (!_users.Exist(m => m.Mobile == request.Mobile))
                    {
                        return BuildResponse.FailResponse<LoginResponse>("手机号未注册，前往注册");
                    }

                    //1.获取用户信息
                    var userInfo = _users.LoadEntitieNoTracking(u => u.Mobile == request.Mobile);

                    if (userInfo == null)
                        return BuildResponse.FailResponse<LoginResponse>("手机号未注册，前往注册");

                    //2.校验验证码
                    //获取缓存中的手机验证码
                    var caheCode = CacheHelper.GetCache("login" + request.Mobile);
                    if (caheCode == null || caheCode.ToString() != request.Code)
                        return BuildResponse.FailResponse<LoginResponse>("验证码错误");

                    //当前用户信息
                    var curUserInfo = new UserInfo
                    {
                        UId = userInfo.UId
                    };

                    //同步上下文
                    _workContext.CurrentUser = curUserInfo;

                    //更新Http上下文用户安全信息
                    var userPrincipal = new UserPrincipal(curUserInfo);
                    HttpContext.Current.User = userPrincipal;

                    //登录成功，创建Token
                    var token = _tokens.CreateToken(ConfigMap.SignKey, curUserInfo, CacheKeys.YearCacheTime);
                    if (string.IsNullOrEmpty(token))
                    {
                        ApiLogger.Error("Login", requestString, "token为空");
                        return BuildResponse.FailResponse<LoginResponse>("获取用户信息错误");
                    }

                    var response = new LoginResponse
                    {
                        Token = token
                    };
                    return BuildResponse.SuccessResponse(response);

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<LoginResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("Login", requestString, "账号登录出错", ex);
                return BuildResponse.FailResponse<LoginResponse>("账号登录出错");
            }
        }

        #endregion

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [NoAuth]
        public BaseResponse<LoginResponse> Register(RegisterRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    if (_users.Exist(m => m.Mobile == request.Mobile))
                    {
                        return BuildResponse.FailResponse<LoginResponse>("该手机号码已注册,前往登录");
                    }

                    //2.校验验证码
                    //获取缓存中的手机验证码
                    var caheCode = CacheHelper.GetCache("register" + request.Mobile);
                    if (caheCode == null || caheCode.ToString() != request.Code)
                        return BuildResponse.FailResponse<LoginResponse>("验证码错误");

                    //创建并添加用户实体
                    var userInfo = new LF_Users
                    {
                        UserName = request.Mobile,
                        Mobile = request.Mobile,
                        PassWord = "",
                        AdminGId = (short)UserType.User,
                        NickName = request.Mobile,
                        Avatar = ConfigMap.InitialAvatar,
                        Salt = "",
                        RegisterTime = DateTime.Now,
                        Gender = (byte)UserGender.Nothing
                    };
                    _users.AddUsers(userInfo);

                    var result = _unitOfWork.Commit();
                    if(result <= 0)
                        return BuildResponse.FailResponse<LoginResponse>("注册失败");

                    //当前用户信息
                    var curUserInfo = new UserInfo
                    {
                        UId = userInfo.UId
                    };

                    //同步上下文
                    _workContext.CurrentUser = curUserInfo;

                    //更新Http上下文用户安全信息
                    var userPrincipal = new UserPrincipal(curUserInfo);
                    HttpContext.Current.User = userPrincipal;

                    //登录成功，创建Token
                    var token = _tokens.CreateToken(ConfigMap.SignKey, curUserInfo, CacheKeys.YearCacheTime);
                    if (string.IsNullOrEmpty(token))
                    {
                        ApiLogger.Error("Login", requestString, "token为空");
                        return BuildResponse.FailResponse<LoginResponse>("获取用户信息错误");
                    }

                    var response = new LoginResponse
                    {
                        Token = token
                    };

                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证出错信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<LoginResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("Register", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<LoginResponse>("注册出错");
            }
        }

        #endregion

        #region 发送验证码短信

        /// <summary>
        /// 发送验证手机短信
        /// </summary>
        [NoAuth]
        public BaseResponse<string> SendMobileVerifyCode(SendMobileVerifyRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //手机号码校验
                    var isMobile = Code.Helper.ValidateHelper.IsValidMobile(request.Mobile);
                    if (!isMobile)
                        return BuildResponse.FailResponse<string>("手机号码格式不正确");
                    
                    if (request.Type == 1)
                    {
                        if (!_users.Exist(m => m.Mobile == request.Mobile))
                        {
                            return BuildResponse.FailResponse<string>("手机号未注册，前往注册");
                        }

                        //获取缓存中的手机验证码
                        var caheCode = CacheHelper.GetCache("GetCode_login" + request.Mobile);
                        if (caheCode != null)
                        {
                            return BuildResponse.FailResponse<string>("获取验证码过于频繁，请稍后再试");
                        }
                    }
                    else
                    {
                        if (_users.Exist(m => m.Mobile == request.Mobile))
                        {
                            return BuildResponse.FailResponse<string>("该手机号码已注册,前往登录");
                        }

                        //获取缓存中的手机验证码
                        var caheCode = CacheHelper.GetCache("GetCode_register" + request.Mobile);
                        if (caheCode != null)
                        {
                            return BuildResponse.FailResponse<string>("获取验证码过于频繁，请稍后再试");
                        }
                    }

                    //生成验证码（数字）
                    var verifyCode = RandomHelper.CreateRandomValue(6, true);

                    //验证码获取时间,（秒）
                    const int getCodeTimeOut = 60;

                    //验证码有效时间,（秒）
                    const int codeTimeOut = 60 * CacheKeys.SmsCodeValidTime;

                    //发送验证手机短信
                    _aliyunSms.InitAliYunSms(ConfigMap.AccessKeyId, ConfigMap.AccessKeySecret, ConfigMap.SignName);
                    var response = _aliyunSms.SendVerifyCode(ConfigMap.TemplateCode, verifyCode, request.Mobile);

                    ApiLogger.Info("SendVerifyCode", $"ContactsPhone:{request.Mobile},verifyCode:{verifyCode}", $"发送验证码返回结果:{response.ToJson()}");

                    if (response.Code == "OK")
                    {
                        //将验证值保存在缓存中 
                        if (request.Type == 1)
                        {
                            CacheHelper.SetCache("login" + request.Mobile, verifyCode, codeTimeOut);
                            CacheHelper.SetCache("GetCode_login" + request.Mobile, request.Mobile, getCodeTimeOut);
                        }
                        else
                        {
                            CacheHelper.SetCache("register" + request.Mobile, verifyCode, codeTimeOut);
                            CacheHelper.SetCache("GetCode_register" + request.Mobile, request.Mobile, getCodeTimeOut);
                        }

                        //返回成功消息
                        return BuildResponse.SuccessResponse($"发送验证码成功,验证码为：{verifyCode}");
                    }

                    //返回失败结果
                    return BuildResponse.FailResponse<string>("发送验证码失败");
                }
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("SendMobileVerifyCode", requestString, "发送验证手机短信错误", ex);
                return BuildResponse.FailResponse<string>("发送验证手机短信错误");
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
