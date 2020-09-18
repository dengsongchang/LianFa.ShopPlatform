using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.DepartMent
{
    /// <summary>
    /// 获取部门管理列表请求类
    /// </summary>
    public class AdminDepartmentListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
}
