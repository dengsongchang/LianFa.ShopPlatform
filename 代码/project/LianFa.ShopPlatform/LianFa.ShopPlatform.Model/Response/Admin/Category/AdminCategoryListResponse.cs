using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Category
{
    /// <summary>
    /// 后台分类列表响应类
    /// </summary>
    public class AdminCategoryListResponse
    {
        /// <summary>
        /// 分类列表
        /// </summary>
        public List<AdminCategoryInfo> CategoryList { get; set; }

    }

    /// <summary>
    /// 分类信息
    /// </summary>
    public class AdminCategoryInfo
    {
        /// <summary>  
        /// 分类id  
        /// </summary>
        public short CateId { get; set; }
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
    /// 商品分类信息类  
    /// </summary>
    public class CategoryProductInfo
    {
        /// <summary>  
        /// 分类名称  
        /// </summary>
        public string CateName { get; set; }

        /// <summary>  
        /// 分类商品 
        /// </summary>
        public PartProductInfo ProductInfo { get; set; }
    }

    /// <summary>  
    /// 商品部分信息类  
    /// </summary>
    public class PartProductInfo
    {
        /// <summary>  
        /// 分类名称  
        /// </summary>
        public string CateName { get; set; }
        /// <summary>  
        /// 商品id  
        /// </summary>
        public int PId { get; set; }
        /// <summary>  
        /// 商品编号  
        /// </summary>
        public string PSn { get; set; }
        /// <summary>  
        /// 分类id  
        /// </summary>
        public short CateId { get; set; }
        /// <summary>  
        /// 品牌id  
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>  
        /// 商城价  
        /// </summary>
        public decimal ShopPrice { get; set; }
        /// <summary>  
        /// 成本价  
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>  
        /// 状态  
        /// </summary>
        public byte State { get; set; }
        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>  
        /// 重量(单位为克)  
        /// </summary>
        public int Weight { get; set; }
        /// <summary>  
        /// 主图  
        /// </summary>
        public string ShowImg { get; set; }
       /// <summary>  
        /// 添加时间  
        /// </summary>
        public System.DateTime AddTime { get; set; }
        /// <summary>  
        /// 描述  
        /// </summary>
        public string Description { get; set; }
    }
}