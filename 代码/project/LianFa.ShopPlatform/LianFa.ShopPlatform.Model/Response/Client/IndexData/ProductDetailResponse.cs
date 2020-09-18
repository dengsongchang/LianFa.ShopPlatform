using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.IndexData
{
    /// <summary>
    /// 获取商品详情响应类
    /// </summary>
    public class ProductDetailResponse
    {
        /// <summary>  
        /// 商品id  
        /// </summary>
        public int PId { get; set; }
        /// <summary>  
        /// 主图
        /// </summary>
        public string ShowImg { get; set; }
        /// <summary>  
        /// 主图
        /// </summary>
        public string ShowImgFull { get; set; }
        /// <summary>  
        /// 图片组 
        /// </summary>
        public List<string> ShowImgList { get; set; }
        /// <summary>
        /// 商城价
        /// </summary>
        public decimal ShopPrice { get; set; }
        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 商品库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>  
        /// 成本价  
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 是否开启特价
        /// </summary>
        public int IsCostPrice { get; set; }
    }
}
