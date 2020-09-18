using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.WorkContext
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IAdminWorkContext
    {
        /// <summary>
        /// 获取当前管理员
        /// </summary>
        AdminInfo CurrentAdmin { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        string Ip { get; }
    }
}