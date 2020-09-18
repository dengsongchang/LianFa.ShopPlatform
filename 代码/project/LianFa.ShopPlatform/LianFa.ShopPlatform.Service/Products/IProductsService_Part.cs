using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Model.Response.Admin.Product;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IProductsService
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="cateId"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ProductInfo> GetProductList(int brandId, int cateId, PageModel page, out int total);

        /// <summary>
        /// 后台获取商品列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="brandId"></param>
        /// <param name="cateId"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ProductListInfo> AdminGetProductList(string name, int brandId, int cateId, PageModel page, out int total);

        /// <summary>
        /// 商品数
        /// </summary>
        /// <returns></returns>
        int GetProductCountTotal();


        /// <summary>
        /// 获取商品类型统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>商品列表</returns>
        List<Data> AdminGetProductCategoryCount(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取产品上架每天的数据
        /// </summary>
        /// <returns></returns>
        List<DataDetail> GetProductCountGroupByDate(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取商品相关信息
        /// </summary>
        AdminProductInfo GetAdminProductInfo(int pId);
    }
}
