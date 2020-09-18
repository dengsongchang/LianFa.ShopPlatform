using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Model.Response.Admin.Brand;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IBrandsService
    {
        /// <summary>
        /// 移动端获取品牌列表
        /// </summary>
        /// <returns></returns>
        List<BrandAndCategoryListInfo> BrandList();

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns>品牌列表</returns>
        List<AdminBrandInfo> GetBrandList(string name,PageModel page,out int total);
    }
}
