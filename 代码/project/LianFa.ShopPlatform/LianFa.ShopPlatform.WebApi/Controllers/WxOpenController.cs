using System;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Client.WxOpen;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.WxOpen;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;
using LianFa.ShopPlatform.WebApi.WorkContext;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Security.Tokens;
using Senparc.Weixin;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Helpers;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 微信小程序控制器
    /// </summary>
    [Signature]
    [ControllerGroup("微信小程序相关接口", "用于获取微信用户数据等")]
    public class WxOpenController : ApiController
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
        /// token服务类
        /// </summary>
        private readonly ITokenService _tokens;

        /// <summary>
        /// 用户服务类
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 开放授权服务类
        /// </summary>
        private readonly IOauthService _oauths;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 微信小程序控制器构造函数(构造注入)
        /// </summary>
        /// <param name="workContext">工作上下文</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="tokens">Token服务</param>
        /// <param name="oauths">开放授权服务</param>
        /// <param name="users">用户服务类</param>
        public WxOpenController(IWorkContext workContext, IUnitOfWork unitOfWork, ITokenService tokens, IOauthService oauths, IUsersService users)
        {
            _workContext = workContext;
            _unitOfWork = unitOfWork;
            _tokens = tokens;
            _oauths = oauths;
            _users = users;
        }

        /// <summary>
        /// wx.login登陆成功之后发送的请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<OnLoginResponse> OnLogin(OnLoginRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //code 换取 session_key
                    ApiLogger.Info("OnLogin", $"请求参数：code:{request.Code}", "wx.login登陆成功之后发送的请求");

                    var jsonResult = SnsApi.JsCode2Json(ConfigMap.WxMiniAppId, ConfigMap.WxMiniAppSecret, request.Code);
                    if (jsonResult.errcode == ReturnCode.请求成功)
                    {
                        //使用SessionContainer管理登录信息（推荐）
                        var unionId = "";
                        var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

                        //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
                        var response = new OnLoginResponse
                        {
                            SessionId = sessionBag.Key
                        };
                        return BuildResponse.SuccessResponse(response);
                    }

                    ApiLogger.Error("OnLogin", requestString, $"微信小程序授权登录出错,错误原因:{jsonResult.errmsg}");
                    return BuildResponse.FailResponse<OnLoginResponse>("微信小程序授权登录出错");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<OnLoginResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("OnLogin", requestString, "微信小程序授权登录出错", ex);
                return BuildResponse.FailResponse<OnLoginResponse>("微信小程序授权登录出错");
            }
        }

        /// <summary>
        /// wx.getUserInfo登陆成功之后发送的请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<GetUserInfoResponse> GetUserInfo(GetUserInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    ApiLogger.Info("GetUserInfo", $"请求参数：encryptedData:{request.EncryptedData},iv:{request.Iv}", "wx.getUserInfo登陆成功之后发送的请求");

                    //使用SessionContainer管理登录信息（推荐）
                    var sessionBag = SessionContainer.GetSession(request.SessionId);
                    if (sessionBag == null)
                    {
                        return BuildResponse.FailResponse<GetUserInfoResponse>("SessionId过期需重新获取", ResponseCode.SessionTimeOut);
                    }

                    ApiLogger.Info("GetUserInfo", $"返回参数：sessionId:{sessionBag.Key}", "wx.getUserInfo登陆成功之后发送的请求");

                    //用户数据响应数据
                    var response = new GetUserInfoResponse();

                    //获取用户信息,创建用户
                    //第三方固定为微信
                    const string server = "WeChat";

                    //判断此用户是否已经存在
                    int uid = _oauths.GetUidByOpenIdAndServer(sessionBag.OpenId, server);
                    if (uid > 0) //存在时
                    {
                        ApiLogger.Debug("GetUserInfo", $"参数：uid:{uid}", "用户存在");

                        //获取用户信息
                        var partUserInfo = _users.GetUsersById(uid);
                        if (partUserInfo == null)
                        {
                            ApiLogger.Error("GetUserInfo", $"参数：uid:{uid}", "用户不存在");
                            return BuildResponse.FailResponse<GetUserInfoResponse>("用户不存在");
                        }

                        //解密用户信息
                        //解密用户信息
                        var decodedEntity = DecodeUserInfo(request);
                        if (decodedEntity == null)
                        {
                            return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息失败,水印验证不通过");
                        }

                        //当前用户信息
                        var curUserInfo = new UserInfo()
                        {
                            UId = partUserInfo.UId,
                        };

                        //同步上下文
                        _workContext.CurrentUser = curUserInfo;

                        //登录成功，创建Token
                        var token = _tokens.CreateToken(ConfigMap.SignKey, curUserInfo);
                        if (string.IsNullOrEmpty(token))
                        {
                            ApiLogger.Error("GetUserInfo", requestString, "token为空");
                            return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息错误");
                        }

                        //提交事务
                        _unitOfWork.Commit();

                        //返回Token
                        response.Token = token;
                        response.UId = curUserInfo.UId;
                        return BuildResponse.SuccessResponse(response);
                    }

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            //解密用户信息
                            var decodedEntity = DecodeUserInfo(request);
                            if (decodedEntity == null)
                            {
                                return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息失败,水印验证不通过");
                            }

                            ApiLogger.Debug("GetUserInfo", $"参数：nickName:{decodedEntity.nickName}", "用户不存在，插入新用户");

                            //初始化微信用户信息
                            var userInfo = _oauths.InitWeiXinUser(decodedEntity.nickName, decodedEntity.avatarUrl);

                            //用户信息为空
                            if (userInfo == null) return BuildResponse.FailResponse<GetUserInfoResponse>("用户信息为空");

                            //添加用户
                            _users.AddUsers(userInfo);


                            //提交事务以获取用户Id
                            var result = _unitOfWork.Commit();
                            if (result <= 0) return null;


                            //用户Id正确新增用户详情及Oauth记录
                            if (userInfo.UId > 0)
                            {
                                //添加授权用户
                                var oauthInfo = new LF_Oauth
                                {
                                    UId = userInfo.UId,
                                    OpenId = decodedEntity.openId,
                                    Server = server
                                };
                                _oauths.AddOauth(oauthInfo);

                                //提交事务
                                var addResult = _unitOfWork.Commit();
                                if (addResult <= 1)
                                {
                                    ApiLogger.Error("GetUserInfo", requestString, "微信小程序授权获取用户信息出错");
                                    return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息错误");
                                }

                                //当前用户信息
                                var curUserInfo = new UserInfo
                                {
                                    UId = userInfo.UId,
                                };

                                //登录成功，创建Token
                                var token = _tokens.CreateToken(ConfigMap.SignKey, curUserInfo);
                                if (string.IsNullOrEmpty(token))
                                {
                                    ApiLogger.Error("GetUserInfo", requestString, "token为空");
                                    return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息错误");
                                }

                                _unitOfWork.Commit();

                                //成功提交所有事务
                                scope.Complete();

                                //同步上下文
                                _workContext.CurrentUser = curUserInfo;

                                //返回Token
                                response.Token = token;
                                response.UId = curUserInfo.UId;
                                return BuildResponse.SuccessResponse(response);
                            }

                            ApiLogger.Error("GetUserInfo", $"参数：nickName:{decodedEntity.nickName}", "用户不存在，插入新用户失败");
                            return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息错误");
                        }
                        catch (Exception ex)
                        {
                            ApiLogger.Error("GetUserInfo", requestString, "微信小程序授权获取用户信息出错", ex);
                            return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息错误");
                        }
                        finally
                        {
                            scope.Dispose();
                        }
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<GetUserInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetUserInfo", requestString, "微信小程序授权获取用户信息出错", ex);
                return BuildResponse.FailResponse<GetUserInfoResponse>("获取用户信息错误");
            }
        }

        /// <summary>
        /// 解密用户信息
        /// </summary>
        /// <param name="request">获取用户信息请求</param>
        /// <returns>解密后的用户信息</returns>
        private DecodedUserInfo DecodeUserInfo(GetUserInfoRequest request)
        {
            //解密用户信息
            var decodedEntity = EncryptHelper.DecodeUserInfoBySessionId(
                request.SessionId,
                request.EncryptedData, request.Iv);

            //检验水印
            var checkWartmark = false;
            if (decodedEntity != null)
            {
                checkWartmark = decodedEntity.CheckWatermark(ConfigMap.WxMiniAppId);
            }

            ApiLogger.Info("GetUserInfo",
                $"请求参数：sessionId:{request.SessionId},encryptedData:{request.EncryptedData},iv:{request.Iv}",
                $"水印验证：{(checkWartmark ? "通过" : "不通过")}");

            if (checkWartmark) return decodedEntity;

            ApiLogger.Error("GetUserInfo", $"请求参数：sessionId:{request.SessionId}", "水印验证不通过");
            return null;
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