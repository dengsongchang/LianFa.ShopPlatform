using System;
using System.Security.Principal;
using HuCheng.Util.Core.Security.Tokens;

namespace LianFa.ShopPlatform.WebApi.WorkContext
{
    /// <summary>
    /// 用户安全信息
    /// </summary>
    public class UserPrincipal : IPrincipal
    {
        /// <summary>
        /// 用户身份信息
        /// </summary>
        public IIdentity Identity { get; }

        /// <summary>
        /// 初始化用户安全信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public UserPrincipal(UserInfo userInfo)
        {
            Identity = new UserIdentity(userInfo);
        }

        /// <summary>
        /// IsInRole
        /// </summary>
        /// <param name="role">role</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}