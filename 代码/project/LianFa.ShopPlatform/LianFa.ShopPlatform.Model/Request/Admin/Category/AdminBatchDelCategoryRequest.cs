using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Category
{
    /// <summary>
    /// 后台批量删除分类请求类
    /// </summary>
    public class AdminBatchDelCategoryRequest
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public List<int> CateIdList { get; set; }
    }
}
