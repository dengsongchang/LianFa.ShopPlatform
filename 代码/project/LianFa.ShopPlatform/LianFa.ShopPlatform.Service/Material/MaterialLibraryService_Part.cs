using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Material;

namespace LianFa.ShopPlatform.Service
{
    public partial class MaterialLibraryService
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
        public List<MaterialModel> GetMaterialList(PageModel page, string fileName, int categoryId, int displayOrder, bool isAsc, out int total)
        {
            var data = (from g in _materialLibraryRepository.GetDbSetNoTracking()
                        select new MaterialModel
                        {
                            CategoryId = g.CategoryId,
                            CreateTime = g.CreateTime,
                            FileSize = g.FileSize,
                            MaterialId = g.MaterialId,
                            ShowName = g.ShowName,
                            Type = g.Type,
                            UpdateTime = g.UpdateTime,
                            FileUrl = g.FileUrl
                        })
                //查询条件
                .Where(d => d.CategoryId == categoryId)
                .WhereIf(d => d.ShowName.Equals(fileName), !string.IsNullOrEmpty(fileName))
                //上传时间排序
                .OrderByIf(x => x.CreateTime, displayOrder == (int)MaterialListOrder.CreateTime && isAsc)
                .OrderByDescendingIf(x => x.CreateTime, displayOrder == (int)MaterialListOrder.CreateTime && !isAsc)
                //文件名排序
                .OrderByIf(x => x.ShowName, displayOrder == (int)MaterialListOrder.FileName && isAsc)
                .OrderByDescendingIf(x => x.ShowName, displayOrder == (int)MaterialListOrder.FileName && !isAsc)
                //更新时间排序
                .OrderByIf(x => x.UpdateTime, displayOrder == (int)MaterialListOrder.UpdateTime && isAsc)
                .OrderByDescendingIf(x => x.UpdateTime, displayOrder == (int)MaterialListOrder.UpdateTime && !isAsc)
                //图片大小排序
                .OrderByIf(x => x.FileSize, displayOrder == (int)MaterialListOrder.ImageSize && isAsc)
                .OrderByDescendingIf(x => x.FileSize, displayOrder == (int)MaterialListOrder.ImageSize && !isAsc)
                //分页
                .LoadPage(page, out total)
                .ToList();
            return data;
        }

        /// <summary>
        /// 获取查询源
        /// </summary>
        /// <returns></returns>
        public IQueryable<LF_MaterialLibrary> GetMaterialDbSet()
        {
            return _materialLibraryRepository.GetDbSetNoTracking();
        }
    }
}
