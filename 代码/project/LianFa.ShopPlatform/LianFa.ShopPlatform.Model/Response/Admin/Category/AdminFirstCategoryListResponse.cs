using System.Collections.Generic;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Admin.Category
{
    /// <summary>
    /// 后台一级分类列表响应类
    /// </summary>
    public class AdminFirstCategoryListResponse
    {
        /// <summary>
        /// 一级分类列表
        /// </summary>
        public List<LF_Categories> FirstCategoryList { get; set; }
    }
}
