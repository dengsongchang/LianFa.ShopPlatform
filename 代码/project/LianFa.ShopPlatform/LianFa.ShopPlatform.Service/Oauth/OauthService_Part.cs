
using System;
using System.Linq;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Randoms;

namespace LianFa.ShopPlatform.Service
{
    public partial class OauthService
    {
        #region Fields
        /// <summary>
        /// 商品Sku仓储类
        /// </summary>
        private readonly IRepository<LF_Users> _usersRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public OauthService(IRepository<LF_Users> usersRepository, IRepository<LF_Oauth> oauthRepository)
        {
            _usersRepository = usersRepository;
            _oauthRepository = oauthRepository;
        }

        #endregion

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <returns></returns>
        public int GetUidByOpenIdAndServer(string openId, string server)
        {
            if (string.IsNullOrWhiteSpace(openId) || string.IsNullOrWhiteSpace(server))
                return -1;

            return _oauthRepository.LoadEntitiesNoTracking(m => m.OpenId == openId && m.Server == server).Select(m => m.UId).FirstOrDefault();
        }

        /// <summary>
        /// 获得开放授权用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public LF_Oauth GetOAuthUserByUid(int uid)
        {
            return _oauthRepository.GetById(uid);
        }

        /// <summary>
        /// 初始化微信用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="avatar">用户头像地址</param>
        /// <returns></returns>
        public LF_Users InitWeiXinUser(string userName, string avatar)
        {
            //用户信息
            var userInfo = new LF_Users { Salt = RandomHelper.CreateRandomValue(6, false) };
            userInfo.PassWord = Md5Encrypt.Md5By32(ConfigMap.DefaultPassword + userInfo.Salt);
            userInfo.AdminGId = (int)UserType.User;
            userInfo.UserName = userName;
            userInfo.Mobile = string.Empty;
            userInfo.Avatar = avatar;

            return userInfo;
        }
    }
}
