using System.Linq;
using HuCheng.Util.Core.Datas;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
    public partial class MaterialCategoryService
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 获取最大的排序值
        /// </summary>
        /// <returns></returns>
        public byte GetMaxSort()
        {
            return (from g in _materialCategoryRepository.GetDbSetNoTracking()
                    select g.Sort).Any() ? (from g in _materialCategoryRepository.GetDbSetNoTracking()
                                            select g.Sort).Max() : (byte)0;
        }

        /// <summary>
        /// 获取查询源
        /// </summary>
        /// <returns></returns>
        public IQueryable<LF_MaterialCategory> GetCategoryDbSet()
        {
            return _materialCategoryRepository.GetDbSetNoTracking();
        }
    }
}
