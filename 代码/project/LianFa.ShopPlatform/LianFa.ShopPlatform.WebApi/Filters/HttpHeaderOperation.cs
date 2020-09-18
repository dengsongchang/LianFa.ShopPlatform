using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace LianFa.ShopPlatform.WebApi.Filters
{
    /// <summary>
    /// HttpHeaderOperation
    /// </summary>
    public class HttpHeaderOperation : IOperationFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            //获取action的过滤器
            var actFilters = apiDescription.ActionDescriptor;
            var isAuthorized = actFilters.GetCustomAttributes<SignatureAttribute>().Any() || actFilters.GetCustomAttributes<AdminAuthAttribute>().Any();

            if (isAuthorized == false) //提供action都没有权限特性标记，检查控制器有没有
            {
                var controllerAttrs = apiDescription.ActionDescriptor.ControllerDescriptor;

                isAuthorized = controllerAttrs.GetCustomAttributes<SignatureAttribute>().Any() || controllerAttrs.GetCustomAttributes<AdminAuthAttribute>().Any();
            }

            //是否允许匿名访问
            var isAllowAnonymous = actFilters.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (!isAuthorized || isAllowAnonymous) return;

            //添加TimeStamp头部参数
            operation.parameters.Add(
                new Parameter
                {
                    name = "TimeStamp",
                    @in = "header",
                    schema = new Schema
                    {
                        type = "string"
                    },
                    description = "时间戳",
                    required = true
                });

            //添加Sign头部参数
            operation.parameters.Add(
                new Parameter
                {
                    name = "Sign",
                    @in = "header",
                    schema = new Schema
                    {
                        type = "string"
                    },
                    description = "签名串",
                    required = true
                });

            //添加MethodName头部参数
            operation.parameters.Add(
                new Parameter
                {
                    name = "MethodName",
                    @in = "header",
                    schema = new Schema
                    {
                        type = "string"
                    },
                    description = "方法名",
                    required = true
                });

            //是否不需要登录授权
            var isNoAuth = actFilters.GetCustomAttributes<NoAuthAttribute>().Any();

            //添加Token头部参数
            operation.parameters.Add(
                new Parameter
                {
                    name = "Token",
                    @in = "header",
                    schema = new Schema
                    {
                        type = "string"
                    },
                    description = "Token",
                    required = !isNoAuth
                });
        }
    }
}