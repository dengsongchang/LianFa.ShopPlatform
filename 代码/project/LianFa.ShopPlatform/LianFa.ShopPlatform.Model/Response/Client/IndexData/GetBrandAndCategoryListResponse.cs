using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.IndexData
{
    /// <summary>
    /// 首页获取品牌、分类列表响应类
    /// </summary>
    public class GetBrandAndCategoryListResponse
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<BrandAndCategoryListInfo> BrandList { get; set; }

        /// <summary>
        /// 分类列表
        /// </summary>
        public List<BrandAndCategoryListInfo> CategoryList { get; set; }
    }

    /// <summary>
    /// 品牌/分类信息
    /// </summary>
    public class BrandAndCategoryListInfo
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
