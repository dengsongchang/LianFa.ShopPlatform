using System.Security.Principal;
using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.WorkContext
{
    /// <summary>
    /// 用户身份认证信息
    /// </summary>
    public class UserIdentity : IIdentity
    {
        /// <summary>
        /// 初始化用户身份认证信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public UserIdentity(UserInfo userInfo)
        {
            Name = userInfo.UserName;
            UserInfo = userInfo;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 认证类型
        /// </summary>
        public string AuthenticationType { get; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuthenticated { get; }
    }
}