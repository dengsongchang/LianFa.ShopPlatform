using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Model.Response.Admin.Category;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial interface ICategoriesService
    {
        /// <summary>
        /// 移动端获取分类列表
        /// </summary>
        /// <returns></returns>
        List<BrandAndCategoryListInfo> CategoryList();

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns>分类列表</returns>
        List<AdminCategoryInfo> GetCategoryList();
    }
}
