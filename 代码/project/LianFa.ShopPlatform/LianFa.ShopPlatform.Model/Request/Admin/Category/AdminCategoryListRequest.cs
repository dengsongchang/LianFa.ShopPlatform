using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Category
{
    /// <summary>
    /// 后台分类列表请求类
    /// </summary>
    public class AdminCategoryListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
}