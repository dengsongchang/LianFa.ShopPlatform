using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.IndexData
{
    /// <summary>
    /// 首页获取商品列表响应类
    /// </summary>
    public class ProductListResponse
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<ProductInfo> ProductList { get; set; }

        /// <summary>
        /// 分页数据总条数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 商家列表信息类
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品主图
        /// </summary>
        public string ShowImg { get; set; }

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
    }
}