using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HuCheng.Util.Configs.Base;
using HuCheng.Util.Core.Config;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using LianFa.ShopPlatform.WebApi.WorkContext;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Dependency;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Datas.EntityFramework;
using HuCheng.Util.Logs.Log4Net;
using HuCheng.Util.Sms.Aliyun;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.WebApi
{
    /// <summary>
    /// 依赖注入启动器
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// 容器
        /// </summary>
        private static IContainer _container;

        /// <summary>
        /// 启动依赖注入
        /// </summary>
        public static void Run()
        {
            SetAutofacContainer();
        }

        /// <summary>
        /// 设置Autofac依赖注入容器
        /// </summary>
        public static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //jwt
            builder.RegisterType<JwtEncoder>().As<IJwtEncoder>().InstancePerLifetimeScope();
            builder.RegisterType<JwtDecoder>().As<IJwtDecoder>().InstancePerLifetimeScope();
            builder.RegisterType<HMACSHA256Algorithm>().As<IJwtAlgorithm>().InstancePerLifetimeScope();
            builder.RegisterType<JsonNetSerializer>().As<IJsonSerializer>().InstancePerLifetimeScope();
            builder.RegisterType<JwtValidator>().As<IJwtValidator>().InstancePerLifetimeScope();
            builder.RegisterType<JwtBase64UrlEncoder>().As<IBase64UrlEncoder>().InstancePerLifetimeScope();
            builder.RegisterType<UtcDateTimeProvider>().As<IDateTimeProvider>().InstancePerLifetimeScope();

            //workcontext
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().SingleInstance();
            builder.RegisterType<AdminWorkContext>().As<IAdminWorkContext>().SingleInstance();
            builder.RegisterType<ConfigBaseManager>().As<IConfigManager>().SingleInstance();

            //log
            builder.RegisterType<ApiLogger>().As<ILog>().SingleInstance();

            //data layer
            builder.RegisterType<DataBaseContextFactory<LF_ShopPlatformEntities>>().As<IDataBaseContextFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //sms
            builder.RegisterType<AliyunSmsService>().As<IAliyunSmsService>().InstancePerLifetimeScope();

            //将仓储层及服务层使用自动注入
            // Resolve all the dependencies for the classes decorated with DependecyRegister
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.GetCustomAttribute<DependencyRegisterAttribute>() != null)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            _container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(_container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
        }
    }
}