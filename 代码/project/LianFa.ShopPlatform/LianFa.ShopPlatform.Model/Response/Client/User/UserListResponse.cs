using System.Collections.Generic;
using LianFa.ShopPlatform.Model.Response.Client.User;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 用户列表 响应类
    /// </summary>
    public class UserListResponse
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserInfoResponse> UserList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
