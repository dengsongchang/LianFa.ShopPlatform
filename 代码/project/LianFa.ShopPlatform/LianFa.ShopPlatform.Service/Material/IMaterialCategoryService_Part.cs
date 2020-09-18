using System.Linq;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IMaterialCategoryService
    {
        /// <summary>
        /// 获取最大排序值
        /// </summary>
        /// <returns></returns>
        byte GetMaxSort();

        /// <summary>
        /// 获取查询源
        /// </summary>
        /// <returns></returns>
        IQueryable<LF_MaterialCategory> GetCategoryDbSet();
    }
}
