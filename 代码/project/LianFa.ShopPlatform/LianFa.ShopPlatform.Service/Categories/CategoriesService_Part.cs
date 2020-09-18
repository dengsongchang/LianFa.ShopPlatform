using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Response.Admin.Category;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial class CategoriesService
    {
        /// <summary>
        /// 移动端获取分类列表
        /// </summary>
        /// <returns></returns>
        public List<BrandAndCategoryListInfo> CategoryList()
        {
            var list = _categoriesRepository.GetDbSetNoTracking()
                .OrderBy(d => d.DisplayOrder)
                .ThenByDescending(d => d.CateId)
                .Select(d => new BrandAndCategoryListInfo
                {
                    Id = d.CateId,
                    Name = d.Name
                })
                .ToList();

            return list;
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns>分类列表</returns>
        public List<AdminCategoryInfo> GetCategoryList()
        {
            return (from c in _categoriesRepository.GetDbSetNoTracking()
                    select new AdminCategoryInfo
                    {
                        CateId = c.CateId,
                        DisplayOrder = c.DisplayOrder,
                        Name = c.Name
                    })
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(d => d.CateId)
                .ToList();
        }

    }
}
