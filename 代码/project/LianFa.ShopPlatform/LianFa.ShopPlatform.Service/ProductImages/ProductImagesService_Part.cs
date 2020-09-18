using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LianFa.ShopPlatform.Code.Helper;

namespace LianFa.ShopPlatform.Service
{
    public partial class ProductImagesService
    {
        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<string> GetProductImagesList(int pId)
        {
            var data = _productImagesRepository.GetDbSetNoTracking()
                .Where(p => p.PId == pId)
                .OrderByDescending(d => d.IsMain)
                .ThenBy(d => d.DisplayOrder)
                .Select(d => d.ShowImg)
                .ToList();
            if (!data.Any())
                return null;

            for (int i = 0; i < data.Count; i++)
            {
                data[i] = CommonHelper.GetFileFullUrl(data[i]);
            }
            return data;
        }
    }
}
