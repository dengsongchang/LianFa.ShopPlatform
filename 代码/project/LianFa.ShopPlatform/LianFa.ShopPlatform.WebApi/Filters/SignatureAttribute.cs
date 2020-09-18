using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using JWT;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request;
using LianFa.ShopPlatform.WebApi.WorkContext;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Exceptions;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.Filters
{
    /// <summary>
    /// 签名验证过滤器
    /// </summary>
    public class SignatureAttribute : AuthorizeAttribute
    {
        #region Fields

        /// <summary>
        /// 上下文
        /// </summary>
        public IWorkContext WorkContext = Bootstrapper.GetFromFac<IWorkContext>();

        /// <summary>
        /// Jwt解密服务
        /// </summary>
        public IJwtDecoder JwtDecoder = Bootstrapper.GetFromFac<IJwtDecoder>();

        /// <summary>
        /// 接口日志记录器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        #endregion

        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="actionContext"></param>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var response = new BaseResponse<object>();

            //请求数据
            var requestData = "";

            //请求头
            var requestHead = new RequestHead();

            //是否允许不进行授权
            var isNoAuth = actionContext.ActionDescriptor.GetCustomAttributes<NoAuthAttribute>().Any();

            //记录访问日志
            var actionName = "/" + actionContext.ControllerContext.RouteData.Values["controller"] + "/" + actionContext.ControllerContext.RouteData.Values["action"];
            try
            {
                //签名http头部验证
                if (!HeadersVerification(actionContext.Request.Headers, requestHead, actionName, response, isNoAuth))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                    return false;
                }

//#if !DEBUG

////签名验证
//                if (!SignVerification(requestHead, actionName, requestData, response, isNoAuth))
//                {
//                    //授权不成功，返回401未授权码
//                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
//                    return false;
//                }
//#endif

                //需要授权或token不为空时，授权成功，初始化上下文
                if (!string.IsNullOrEmpty(requestHead.Token) && requestHead.Token != "null")
                {
                    InitWorkContext(requestHead.Token);
                }
                else
                {
                    if (!isNoAuth)
                    {
                        response.Code = (int)ResponseCode.TokenError;
                        response.Message = ResponseCode.TokenError.GetDescription();
                        ApiLogger.Debug(actionName, requestData, $"请求头数据:{requestHead.ToJson()}\r\n\r\n响应数据:{response.ToJson()}");

                        //token验证不成功，返回401未授权码
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                        return false;
                    }

                    WorkContext.CurrentUser = null;
                }

                return true;
            }
            catch (TokenException ex)
            {
                //允许不授权时出现Token失效返回正常响应
                if (isNoAuth)
                {
                    return true;
                }

                response.Code = (int)ResponseCode.TokenError;
                response.Message = ex.Message;
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()}\r\n请求数据:{requestData}\r\n响应数据:{response.Message}", ex);
                //token验证不成功，返回401未授权码
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                return false;
            }
            catch (TokenExpiredException ex)
            {
                //允许不授权时出现Token失效返回正常响应
                if (isNoAuth)
                {
                    return true;
                }

                response.Code = (int)ResponseCode.TokenError;
                response.Message = ex.Message;
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()}\r\n请求数据:{requestData}\r\n响应数据:{response.Message}", ex);
                //token验证不成功，返回401未授权码
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                return false;
            }
            catch (SignatureVerificationException ex)
            {
                //允许不授权时出现Token失效返回正常响应
                if (isNoAuth)
                {
                    return true;
                }

                response.Code = (int)ResponseCode.ServerError;
                response.Message = ex.Message;
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()}\r\n请求数据:{requestData}\r\n响应数据:{response.Message}", ex);
                //token验证不成功，返回401未授权码
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                return false;
            }
            catch (Exception ex)
            {
                response.Code = (int)ResponseCode.ServerError;
                response.Message = ResponseCode.ServerError.GetDescription();
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()}\r\n请求数据:{requestData}\r\n响应数据:{response.Message}", ex);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, response);
                return false;
            }
        }

        /// <inheritdoc />
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            if (filterContext.Response.Content == null && filterContext.Response.StatusCode == HttpStatusCode.Unauthorized)
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
        /// <param name="isNoAuth">无需授权验证</param>
        /// <param name="response">响应类</param>
        /// <returns></returns>
        private bool HeadersVerification(HttpHeaders requestHeaders, RequestHead requestHead, string actionName, BaseResponse<object> response, bool isNoAuth = false)
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
                response.Code = (int)ResponseCode.TimestampUnknown;
                response.Message = "时间戳不能为空";
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()},响应数据:{response.Message}");
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
                response.Code = (int)ResponseCode.SignError;
                response.Message = "签名串不能为空";
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()},响应数据:{response.Message}");
                return false;
            }

            if (requestHeaders.TryGetValues("MethodName", out values))
            {
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
                ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()},响应数据:{response.Message}");
                return false;
            }

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

            //如果不进行授权则不验证Token
            if (isNoAuth) return true;

            response.Code = (int)ResponseCode.DataError;
            response.Message = "Token不能为空";
            ApiLogger.Error(actionName, $"请求头数据:{requestHead.ToJson()},响应数据:{response.Message}");
            return false;
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="requestHead">请求头</param>
        /// <param name="actionName">动作名</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="response">响应类</param>
        /// <param name="isNoAuth"></param>
        /// <returns></returns>
        private bool SignVerification(RequestHead requestHead, string actionName, string requestData, BaseResponse<object> response, bool isNoAuth = false)
        {
            ApiLogger.Debug(actionName, $"Header参数:{requestHead.ToJson()},请求参数:{requestData}");

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

            if (!isNoAuth && string.IsNullOrEmpty(requestHead.Token))
            {
                response.Code = (int)ResponseCode.DataError;
                response.Message = "Token不能为空";
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

            var timeStamp = DateTimeHelper.GetDateTimeByTimestamp(requestHead.TimeStamp);
            if (timeStamp != DateTime.MinValue)
            {
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
                    $"SIGNKEY={ConfigMap.SignKey};TIMESTAMP={requestHead.TimeStamp};METHODNAME={requestHead.MethodName};";

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
                return false;
            }

            //时间戳错误，返回错误信息
            response.Code = (int)ResponseCode.TimestampUnknown;
            response.Message = ResponseCode.TimestampUnknown.GetDescription();
            ApiLogger.Error(actionName, $"请求数据:{requestData},响应数据:{response.Message}");
            return false;
        }

        /// <summary>
        /// 初始化上下文
        /// </summary>
        public void InitWorkContext(string token)
        {
            try
            {
                //对token进行解密,获取用户信息
                var secureKey = ConfigMap.SignKey;
                if (string.IsNullOrEmpty(token) || token == "null")
                {
                    throw new TokenException(ResponseCode.TokenError.GetDescription());
                }

                var payload = JwtDecoder.DecodeToObject<Payload>(token, secureKey, true);
                if (payload?.UserInfo == null)
                {
                    throw new TokenException(ResponseCode.TokenError.GetDescription());
                }

                //存在更新用户信息
                WorkContext.CurrentUser = payload.UserInfo;
            }
            catch (ArgumentException)
            {
                throw new TokenException(ResponseCode.TokenError.GetDescription());
            }
        }
    }
}
