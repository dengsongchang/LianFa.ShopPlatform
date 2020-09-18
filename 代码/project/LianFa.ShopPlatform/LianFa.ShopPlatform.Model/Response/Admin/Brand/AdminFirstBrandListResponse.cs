using System.Collections.Generic;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Admin.Brand
{
    /// <summary>
    /// 后台品牌列表响应类
    /// </summary>
    public class AdminFirstBrandListResponse
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<LF_Brands> FirstBrandList { get; set; }
    }
}
