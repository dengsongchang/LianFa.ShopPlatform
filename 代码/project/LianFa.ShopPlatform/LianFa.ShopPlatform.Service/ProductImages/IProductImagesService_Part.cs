using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IProductImagesService
    {
        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        List<string> GetProductImagesList(int pId);
    }
}
