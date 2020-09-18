using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// 获取用户列表 请求类
    /// </summary>
    public class UserListRequest
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel Page { get; set; }
    }
}
