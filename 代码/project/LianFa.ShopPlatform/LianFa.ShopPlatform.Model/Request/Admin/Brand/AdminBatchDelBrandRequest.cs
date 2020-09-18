using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Brand
{
    /// <summary>
    /// 后台批量删除品牌请求类
    /// </summary>
    public class AdminBatchDelBrandRequest
    {
        /// <summary>
        /// 品牌id
        /// </summary>
        public List<int> BrandIdList { get; set; }
    }
}
