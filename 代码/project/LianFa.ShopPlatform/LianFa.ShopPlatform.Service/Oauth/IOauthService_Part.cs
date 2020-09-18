using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IOauthService
    {
        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <returns>用户id</returns>
        int GetUidByOpenIdAndServer(string openId, string server);

        /// <summary>
        /// 获得开放授权用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>开放授权用户</returns>
        LF_Oauth GetOAuthUserByUid(int uid);

        /// <summary>
        /// 初始化微信用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="avatar">用户头像地址</param>
        /// <returns>用户信息</returns>
        LF_Users InitWeiXinUser(string userName, string avatar);
    }
}
