using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Product
{
    /// <summary>
    /// 后台添加商品请求类
    /// </summary>
    public class AdminAddProductRequest
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string PSn { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择分类id")]
        public short CateId { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择品牌id")]
        public int BrandId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 商品状态1-上架 0-下架
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择正确的状态")]
        public byte State { get; set; }

        /// <summary>
        /// 商品排序
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 商品主图
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 商城价
        /// </summary>
        [Range(0.01, int.MaxValue, ErrorMessage = "价格最低为0.01元")]
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 是否开启特价
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择正确的状态")]
        public int IsCostPrice { get; set; }

        /// <summary>
        /// 特价
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "价格最低为0元")]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 运费模板id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择运费模板")]
        public int TemplateId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>  
        /// 商品图片组
        /// </summary>
        public List<string> Img { get; set; }
    }

}
