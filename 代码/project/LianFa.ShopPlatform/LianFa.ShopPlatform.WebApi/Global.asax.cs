using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using log4net;
using log4net.Config;

namespace LianFa.ShopPlatform.WebApi
{
    /// <summary>
    /// WebApiApplication
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// 应用程序启动
        /// </summary>
        protected void Application_Start()
        {
            //设置log4net文件
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // 启动依赖注入
            Bootstrapper.Run();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
