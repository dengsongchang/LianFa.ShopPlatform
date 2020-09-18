using System;
using System.Web.Http;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 公共接口
    /// </summary>
    [ControllerGroup("公共接口", "公共帮助接口")]
    public class CommonController : ApiController
    {
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns>服务器时间</returns>
        [HttpPost]
        public string GetServerTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}