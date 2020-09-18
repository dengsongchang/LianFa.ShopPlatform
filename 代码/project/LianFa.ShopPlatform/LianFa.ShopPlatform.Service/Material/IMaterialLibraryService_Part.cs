using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Material;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IMaterialLibraryService
    {
        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fileName"></param>
        /// <param name="categoryId"></param>
        /// <param name="displayOrder"></param>
        /// <param name="isAsc"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<MaterialModel> GetMaterialList(PageModel page, string fileName, int categoryId,
           int displayOrder, bool isAsc, out int total);


        /// <summary>
        /// 获取查询源
        /// </summary>
        /// <returns></returns>
        IQueryable<LF_MaterialLibrary> GetMaterialDbSet();
    }
}
