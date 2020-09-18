using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Account
{
    /// <summary>  
    /// 管理员列表
    /// </summary>
    public class GetAdminListRequest
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel Page { get; set; }
    }
}
