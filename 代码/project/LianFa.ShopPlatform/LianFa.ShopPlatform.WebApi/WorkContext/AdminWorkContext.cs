using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.WorkContext
{
    /// <summary>
    /// 后台工作上下文类
    /// </summary>
    public class AdminWorkContext : IAdminWorkContext
    {
        /// <summary>
        /// 获取/设置当前登录的管理员
        /// </summary>
        public AdminInfo CurrentAdmin { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        public string Ip => WebHelper.GetIp();
    }
}