using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.WorkContext
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        UserInfo CurrentUser { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        string Ip { get; }
    }
}
