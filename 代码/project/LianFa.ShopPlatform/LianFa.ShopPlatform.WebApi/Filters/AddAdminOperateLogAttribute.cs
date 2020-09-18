using System;
using System.Web.Http.Filters;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.WorkContext;

namespace LianFa.ShopPlatform.WebApi.Filters
{
    /// <summary>
    /// 添加管理员操作日志
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AddOperateLogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public IAdminWorkContext WorkContext { get; set; }

        /// <summary>
        /// 操作日志服务
        /// </summary>
        public IAdminOperateLogsService AdminOperateLogs { get; set; }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 提交事务
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///  添加管理员操作日志
        /// </summary>
        /// <param name="content">操作内容</param>
        public AddOperateLogAttribute(string content)
        {
            Content = content;
        }

        /// <summary>在调用操作方法之后发生。</summary>
        /// <param name="actionExecutedContext">操作执行的上下文。</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //将Api的请求转换成传统context，获取请求参数 
            string responseDataStr = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
            var responseData = responseDataStr.ToObject<BaseResponse<object>>();

            if (responseData.Code == (int)ResponseCode.Success)
            {
                //获取IP地址
                var ip = WebHelper.GetIp();
                AdminOperateLogs.AddAdminOperateLogs(WorkContext.CurrentAdmin.AdminId, this.Content, ip);
            }
        }
    }
}
