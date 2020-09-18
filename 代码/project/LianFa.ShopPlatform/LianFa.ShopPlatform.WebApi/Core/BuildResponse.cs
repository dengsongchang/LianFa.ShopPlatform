using System;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Response;
using HuCheng.Util.Core.Extension;

namespace LianFa.ShopPlatform.WebApi.Core
{
    /// <summary>
    /// 构建响应
    /// </summary>
    public class BuildResponse
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="response">响应结果</param>
        /// <returns>返回消息Json</returns>
        public static string ReturnResponse<T>(BaseResponse<T> response)
        {
            try
            {
                return response.ToJson();
            }
            catch (Exception ex)
            {
                throw new Exception("构建响应返回消息JSON序列化出错", ex);
            }
        }

        /// <summary>
        /// 成功返回消息
        /// </summary>
        /// <param name="data">泛型对象</param>
        /// <param name="message">提示信息，为空则返回成功</param>
        /// <param name="responseCode">响应编码</param>
        public static BaseResponse<T> SuccessResponse<T>(T data, string message = "", ResponseCode responseCode = ResponseCode.Success)
        {
            try
            {
                //如果提示信息为空则返回成功
                if (string.IsNullOrEmpty(message))
                {
                    message = ResponseCode.Success.GetDescription();
                }

                var successResponse = new BaseResponse<T>
                {
                    Code = (int)ResponseCode.Success,
                    Message = message,
                    Data = data
                };
                return successResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("构建响应返回消息JSON序列化出错", ex);
            }
        }

        /// <summary>
        /// 失败返回消息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="responseCode">错误编码</param>
        /// <returns></returns>
        public static BaseResponse<T> FailResponse<T>(string message = "", ResponseCode responseCode = ResponseCode.Fail)
        {
            try
            {
                //如果提示信息为空则返回失败
                if (string.IsNullOrEmpty(message))
                {
                    message = ResponseCode.Fail.GetDescription();
                }

                var failResponse = new BaseResponse<T>
                {
                    Code = (int)responseCode,
                    Message = message,
                    Data = default(T)
                };
                return failResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("构建响应返回消息JSON序列化出错", ex);
            }
        }
    }
}
