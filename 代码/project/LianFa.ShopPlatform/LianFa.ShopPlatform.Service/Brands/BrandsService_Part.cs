using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Model.Response.Admin.Brand;
using LianFa.ShopPlatform.Model.Response.Admin.Category;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{ 
    public partial class BrandsService
    {
        /// <summary>
        /// 移动端获取品牌列表
        /// </summary>
        /// <returns></returns>
        public List<BrandAndCategoryListInfo> BrandList()
        {
            var list = _brandsRepository.GetDbSetNoTracking()
                .OrderBy(d => d.DisplayOrder)
                .ThenBy(d => d.BrandId)
                .Select(d => new BrandAndCategoryListInfo
                {
                    Id = d.BrandId,
                    Name = d.Name
                })
                .ToList();

            return list;
        }

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns>品牌列表</returns>
        public List<AdminBrandInfo> GetBrandList(string name, PageModel page,out int total)
        {
            return (from c in _brandsRepository.GetDbSetNoTracking()
                    select new AdminBrandInfo
                    {
                        BrandId = c.BrandId,
                        DisplayOrder = c.DisplayOrder,
                        Name = c.Name
                    })
                .WhereIf(u=>u.Name.Contains(name),!string.IsNullOrEmpty(name))
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(d => d.BrandId)
                .LoadPage(page,out total)
                .ToList();
        }
    }
}
