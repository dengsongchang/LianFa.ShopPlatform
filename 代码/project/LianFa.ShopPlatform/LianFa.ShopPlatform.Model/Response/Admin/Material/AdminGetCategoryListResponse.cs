using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Material
{
    /// <summary>
    /// 查询分类列表 响应类
    /// </summary>
    public class AdminGetCategoryListResponse
    {
        /// <summary>
        /// 分类列表
        /// </summary>
        public List<CategoryModel> List { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
    /// <summary>
    /// 分类模型
    /// </summary>
    public class CategoryModel
    {
        /// <summary>  
        /// 素材Id  
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>  
        /// 展示名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>  
        /// 创建时间  
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>  
        /// 排序  
        /// </summary>
        public byte Sort { get; set; }

        /// <summary>
        /// 图片数量
        /// </summary>
        public int ImageCount { get; set; }
    }

}
