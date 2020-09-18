using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.WorkContext
{
    /// <summary>
    /// 前台工作上下文类
    /// </summary>
    public class WebWorkContext : IWorkContext
    {
        /// <summary>
        /// 获取/设置当前登录的用户
        /// </summary>
        public UserInfo CurrentUser { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        public string Ip => WebHelper.GetIp();
    }
}