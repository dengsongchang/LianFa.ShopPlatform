using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using JWT;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.WebApi.WorkContext;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Security.Tokens;
using HuCheng.Util.Core.Exceptions;

namespace LianFa.ShopPlatform.WebApi.Filters
{
    /// <summary>
    /// 后台权限验证过滤器
    /// </summary>
    public class AdminAuthAttribute : AuthorizeAttribute
    {
        #region Fields

        /// <summary>
        /// 上下文
        /// </summary>
        public IAdminWorkContext WorkContext = Bootstrapper.GetFromFac<IAdminWorkContext>();

        /// <summary>
        /// Jwt解密服务
        /// </summary>
        public IJwtDecoder JwtDecoder = Bootstrapper.GetFromFac<IJwtDecoder>();

        /// <summary>
        /// 接口日志记录器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        #endregion

        /// <inheritdoc />
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var response = new BaseResponse<object>();
            var requestData = "";

            //api路由
            var actionName = actionContext.ControllerContext.RouteData.Route.RouteTemplate;
            try
            {
                //获取请求头
                var requestHead = new RequestHead();

                //是否允许不进行授权
                var isNoAuth = actionContext.ActionDescriptor.GetCustomAttributes<NoAuthAttribute>().Any();

                //签名http头部验证
                if (!HeadersVerification(actionContext.Request.Headers, requestHead, actionName, requestData, response, isNoAuth))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                    return false;
                }


                //需要授权或token不为空时，授权成功，初始化上下文
                if (!isNoAuth)
                {
                    if (!string.IsNullOrEmpty(requestHead.Token))
                    {
                        InitWorkContext(requestHead.Token);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(requestHead.Token))
                    {
                        InitWorkContext(requestHead.Token);
                    }
                    else
                    {
                        WorkContext.CurrentAdmin = null;
                        return true;
                    }
                }

                //需要授权，授权成功，初始化上下文
                InitWorkContext(requestHead.Token);

                //如果当前用户不是商城管理员或者店长
                if (WorkContext.CurrentAdmin != null && WorkContext.CurrentAdmin.AdminGroupId == (int)UserType.User)
                {
                    response.Message = "权限不足";
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                    return false;
                }

                return true;
            }
            catch (TokenException ex)
            {
                response.Code = (int)ResponseCode.ServerError;
                response.Message = ex.Message;
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}", ex);
                //token验证不成功，返回401未授权码
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                return false;
            }
            catch (TokenExpiredException ex)
            {
                response.Code = (int)ResponseCode.ServerError;
                response.Message = ex.Message;
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}", ex);
                //token验证不成功，返回401未授权码
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                return false;
            }
            catch (SignatureVerificationException ex)
            {
                response.Code = (int)ResponseCode.ServerError;
                response.Message = ex.Message;
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}", ex);
                //token验证不成功，返回401未授权码
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                return false;
            }
            catch (Exception ex)
            {
                response.Code = (int)ResponseCode.ServerError;
                response.Message = ResponseCode.ServerError.GetDescription();
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}", ex);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
                return false;
            }
        }

        /// <inheritdoc />
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            if (filterContext.Response != null && filterContext.Response.StatusCode == HttpStatusCode.Unauthorized)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }

            var response = filterContext.Response = filterContext.Response ?? new HttpResponseMessage();
            response.StatusCode = filterContext.Response.StatusCode;
            response.Content = filterContext.Response.Content;
        }

        /// <summary>
        /// 签名http头部验证
        /// </summary>
        /// <param name="requestHeaders">http请求头</param>
        /// <param name="requestHead">请求头</param>
        /// <param name="actionName">动作名</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="response">响应类</param>
        /// <param name="isNoAuth">是否不进行授权</param>
        /// <returns></returns>
        private bool HeadersVerification(HttpHeaders requestHeaders, RequestHead requestHead, string actionName, string requestData, BaseResponse<object> response, bool isNoAuth = false)
        {
            //检验数组
            IEnumerable<string> values;

            if (requestHeaders.TryGetValues("TimeStamp", out values))
            {
                //获取时间戳
                var timeStamp = values.FirstOrDefault();
                if (timeStamp != null)
                {
                    requestHead.TimeStamp = timeStamp;
                }
            }
            else
            {
                response.Code = (int)ResponseCode.DataError;
                response.Message = "时间戳不能为空";
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            if (requestHeaders.TryGetValues("Sign", out values))
            {
                //获取签名串
                var sign = values.FirstOrDefault();
                if (sign != null)
                {
                    requestHead.Sign = sign;
                }
            }
            else
            {
                response.Code = (int)ResponseCode.DataError;
                response.Message = "签名串不能为空";
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            if (requestHeaders.TryGetValues("MethodName", out values))
            {
                //ApiLogger.Error(actionName, requestHead.MethodName, "进这里没？");
                //获取方法名
                var methodName = values.FirstOrDefault();
                if (methodName != null)
                {
                    requestHead.MethodName = methodName;
                }
            }
            else
            {
                response.Code = (int)ResponseCode.MethodError;
                response.Message = ResponseCode.MethodError.GetDescription();
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            //如果不进行授权则不验证Token
            if (isNoAuth) return true;

            if (requestHeaders.TryGetValues("Token", out values))
            {
                //获取token
                var token = values.FirstOrDefault();
                if (token != null)
                {
                    requestHead.Token = token;
                }
                return true;
            }

            response.Code = (int)ResponseCode.DataError;
            response.Message = "Token不能为空";
            ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
            return false;
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="requestHead">请求头</param>
        /// <param name="actionName">动作名</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="response">响应类</param>
        /// <param name="isNoAuth">是否不进行授权</param>
        /// <returns></returns>
        private bool SignVerification(RequestHead requestHead, string actionName, string requestData, BaseResponse<object> response, bool isNoAuth = false)
        {
            //验证数据
            if (string.IsNullOrEmpty(requestHead.TimeStamp))
            {
                response.Code = (int)ResponseCode.DataError;
                response.Message = "时间戳不能为空";
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            if (string.IsNullOrEmpty(requestHead.Sign))
            {
                response.Code = (int)ResponseCode.DataError;
                response.Message = "签名串不能为空";
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            //验证请求的方法名是否一致
            if (string.IsNullOrEmpty(requestHead.MethodName) || !requestHead.MethodName.ToLower().Equals(actionName.ToLower()))
            {
                response.Code = (int)ResponseCode.MethodError;
                response.Message = ResponseCode.MethodError.GetDescription();
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            //获取mac地址
            var macs = WebHelper.GetMacByNetworkInterface();
            if (macs.Count == 0)
            {
                response.Code = (int)ResponseCode.UnknownRequest;
                response.Message = ResponseCode.UnknownRequest.GetDescription();
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            //如果不为匿名访问时则验证Token
            if (!isNoAuth && string.IsNullOrEmpty(requestHead.Token))
            {
                response.Code = (int)ResponseCode.DataError;
                response.Message = "Token不能为空";
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            //获取时间戳
            var timeStamp = DateTimeHelper.GetDateTimeByTimestamp(requestHead.TimeStamp);

            //时间戳5分钟过期
            if (timeStamp.AddMinutes(ConfigMap.CheckTime) < DateTime.Now)
            {
                response.Code = (int)ResponseCode.TimestampUnknown;
                response.Message = ResponseCode.TimestampUnknown.GetDescription();
                ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
                return false;
            }

            //签名验证
            var signStr =
                $"SIGNKEY={ConfigMap.AdminSignKey};TIMESTAMP={requestHead.TimeStamp};METHODNAME={requestHead.MethodName};";

            //如果进行授权或Token不为空时对Token进行加密
            if (!isNoAuth || !string.IsNullOrEmpty(requestHead.Token) && requestHead.Token != "null")
            {
                signStr += $"TOKEN={requestHead.Token};";
            }

            //Server端加密的md5
            var newsign = Md5Encrypt.Md5By32(signStr);

            ApiLogger.Debug(actionName, $"\r\n加密前参数：{signStr}\r\n客户端加密的md5：{requestHead.Sign}\r\nServer端加密的md5：{newsign}");

            //验证签名是否成功
            if (requestHead.Sign.ToUpper().Equals(newsign)) return true;

            //签名错误，返回错误信息
            response.Code = (int)ResponseCode.SignError;
            response.Message = ResponseCode.SignError.GetDescription();
            ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
            return true;
        }

        /// <summary>
        /// 初始化上下文
        /// </summary>
        public void InitWorkContext(string token)
        {
            //对token进行解密,获取管理员信息
            var secureKey = ConfigMap.AdminSignKey;
            AdminPayload payload;
            try
            {
                payload = JwtDecoder.DecodeToObject<AdminPayload>(token, secureKey, true);
            }
            catch (Exception ex)
            {
                throw new TokenException(ex.Message, ex);
            }

            if (payload?.AdminInfo == null)
            {
                throw new TokenException(ResponseCode.TokenError.GetDescription());
            }

            WorkContext.CurrentAdmin = payload.AdminInfo;
        }
    }
}