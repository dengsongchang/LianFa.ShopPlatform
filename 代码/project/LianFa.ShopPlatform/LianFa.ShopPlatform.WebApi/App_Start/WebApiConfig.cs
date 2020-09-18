using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using HuCheng.Util.Logs.Log4Net;
using HuCheng.Util.Web.Api.WebApi.ActionHandling;
using HuCheng.Util.Web.Api.WebApi.ExceptionHandling;

namespace LianFa.ShopPlatform.WebApi
{
    /// <summary>
    /// WebApiConfig
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //增加全局监控过滤器
            //config.Filters.Add(new MonitorActionFilter(new ApiLogger()));

            //增加全局异常过滤器
            config.Filters.Add(new ExceptionFilter(new ApiLogger()));

            //跨域配置
            var cors = new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true };
            GlobalConfiguration.Configuration.EnableCors(cors);

            // Web API 配置和服务
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new DefaultContractResolver { IgnoreSerializableAttribute = true };

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //默认返回 json  
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));
        }
    }
}
