using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Product
{
    /// <summary>
    /// 后台商品信息响应类
    /// </summary>
    public class AdminProductInfoResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public AdminProductInfo ProductInfo { get; set; }
    }

    /// <summary>
    /// 后台商品信息响应类
    /// </summary>
    public class AdminProductInfo
    {
        /// <summary>  
        /// 商品id  
        /// </summary>
        public int PId { get; set; }

        /// <summary>  
        /// 分类id  
        /// </summary>
        public short CateId { get; set; }
        /// <summary>  
        /// 分类名称 
        /// </summary>
        public string CateName { get; set; }
        /// <summary>  
        /// 品牌id  
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>  
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>  
        /// 状态  0-下架1-上架
        /// </summary>
        public byte State { get; set; }
        /// <summary>  
        /// 运费模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 商城价  
        /// </summary>
        public decimal ShopPrice { get; set; }
        /// <summary>  
        /// 成本价  
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 是否开启特价
        /// </summary>
        public int IsCostPrice { get; set; }
        /// <summary>  
        /// 库存数量  
        /// </summary>
        public int Number { get; set; }
        /// <summary>  
        /// 封面图
        /// </summary>
        public string ShowImg { get; set; }
        /// <summary>  
        /// 封面图完整路径
        /// </summary>
        public string ShowImgFull { get; set; }
        /// <summary>  
        /// 商品图片列表
        /// </summary>
        public List<string> Img { get; set; }

        /// <summary>  
        /// 商品图片完整路径列表
        /// </summary>
        public List<string> ImgFull { get; set; }
        
        /// <summary>  
        /// 参数
        /// </summary>
        public string Summary { get; set; }
        
        /// <summary>  
        /// 商品描述  
        /// </summary>
        public string Description { get; set; }

        /// <summary>  
        /// 重量 (单位为g) 
        /// </summary>
        public int Weight { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ImgModel
    {
        /// <summary>  
        /// 图片路径  
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 图片Id
        /// </summary>
        public int ImgId { get; set; }
    }
}
