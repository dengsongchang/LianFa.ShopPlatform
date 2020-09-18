using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Product
{
    /// <summary>
    /// 后台商品列表响应类
    /// </summary>
    public class AdminProductListResponse
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<ProductListInfo> ProductList { get; set; }

        /// <summary>  
        /// 总条数 
        /// </summary>
        public int Total { get; set; }
    }
    /// <summary>
    /// 商品列表信息
    /// </summary>
    public class ProductListInfo
    {
        /// <summary>  
        /// 商品id  
        /// </summary>
        public int PId { get; set; }

        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>  
        /// 零售价  
        /// </summary>
        public decimal ShopPrice { get; set; }
        /// <summary>  
        /// 状态  
        /// </summary>
        public byte State { get; set; }

        /// <summary>  
        /// 状态  
        /// </summary>
        public string StateDec { get; set; }

        /// <summary>  
        /// 销量  
        /// </summary>
        public int SalesVolume  { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}
