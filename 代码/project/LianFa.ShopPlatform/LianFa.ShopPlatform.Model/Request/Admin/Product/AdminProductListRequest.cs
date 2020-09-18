using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Product
{
    /// <summary>
    /// 后台商品列表请求类
    /// </summary>
    public class AdminProductListRequest
    {
        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }

        /// <summary>  
        /// 品牌id (0默认为全部)
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>  
        /// 分类id (0默认为全部)
        /// </summary>
        public int CateId { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
}
