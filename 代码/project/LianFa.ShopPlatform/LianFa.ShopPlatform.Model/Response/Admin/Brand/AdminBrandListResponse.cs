using System.Collections.Generic;
using LianFa.ShopPlatform.Model.Response.Admin.Category;

namespace LianFa.ShopPlatform.Model.Response.Admin.Brand
{
    /// <summary>
    /// 后台品牌列表响应类
    /// </summary>
    public class AdminBrandListResponse
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<AdminBrandInfo> BrandList { get; set; }

    }

    /// <summary>
    /// 品牌信息
    /// </summary>
    public class AdminBrandInfo
    {
        /// <summary>  
        /// 品牌id  
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }
        
    }


    /// <summary>  
    /// 商品品牌信息类  
    /// </summary>
    public class BrandProductInfo
    {
        /// <summary>  
        /// 品牌名称  
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>  
        /// 品牌商品 
        /// </summary>
        public PartProductInfo ProductInfo { get; set; }
    }
    
}