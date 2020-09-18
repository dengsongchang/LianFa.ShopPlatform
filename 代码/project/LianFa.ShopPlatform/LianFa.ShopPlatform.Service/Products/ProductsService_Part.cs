using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Product;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;
using CommonHelper = LianFa.ShopPlatform.Code.Helper.CommonHelper;

namespace LianFa.ShopPlatform.Service
{
    public partial class ProductsService
    {
        #region Fields

        private readonly IRepository<LF_Categories> _categoriesRepository;

        private readonly IRepository<LF_Brands> _brandsRepository;

        private readonly IRepository<LF_OrderProducts> _orderProductsRepository;

        private readonly IRepository<LF_ProductImages> _productImagesRepository;

        private readonly IRepository<LF_ProductStocks> _productStocksRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productsRepository">商品仓储类</param>
        public ProductsService(IRepository<LF_Products> productsRepository, IRepository<LF_Categories> categoriesRepository,
            IRepository<LF_OrderProducts> orderProductsRepository, IRepository<LF_ProductImages> productImagesRepository,
            IRepository<LF_ProductStocks> productStocksRepository, IRepository<LF_Brands> brandsRepository)
        {
            this._productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _orderProductsRepository = orderProductsRepository;
            _productImagesRepository = productImagesRepository;
            _productStocksRepository = productStocksRepository;
            _brandsRepository = brandsRepository;
        }

        #endregion

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="cateId"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ProductInfo> GetProductList(int brandId, int cateId, PageModel page, out int total)
        {
            var data = _productsRepository.GetDbSetNoTracking()
                .Where(p => p.State == (byte)WhetherType.Yes)
                .WhereIf(p => p.BrandId == brandId, brandId > 0)
                .WhereIf(p => p.CateId == cateId, cateId > 0)
                .Select(d => new ProductInfo
                {
                    PId = d.PId,
                    Name = d.Name,
                    ShowImg = d.ShowImg,
                    ShopPrice = d.ShopPrice,
                    DisplayOrder = d.DisplayOrder,
                    IsCostPrice = d.IsCostPrice,
                    CostPrice = d.CostPrice
                })
                .OrderBy(p => p.DisplayOrder)
                .ThenByDescending(p => p.PId)
                .LoadPage(page, out total)
                .ToList();

            if (!data.Any())
                return new List<ProductInfo>();

            foreach (var product in data)
            {
                product.ShowImg = CommonHelper.GetFileFullUrl(product.ShowImg);
            }
            return data;
        }

        /// <summary>
        /// 后台获取商品列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="brandId"></param>
        /// <param name="cateId"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ProductListInfo> AdminGetProductList(string name ,int brandId, int cateId, PageModel page, out int total)
        {
            var data = _productsRepository.GetDbSetNoTracking()
                .WhereIf(p => p.Name.Contains(name),!string.IsNullOrEmpty(name))
                .WhereIf(p => p.BrandId == brandId, brandId > 0)
                .WhereIf(p => p.CateId == cateId, cateId > 0)
                .GroupJoin(_orderProductsRepository.GetDbSetNoTracking().Where(p => p.OId>0),x=> x.PId,y => y.PId,(x,y)=> new ProductListInfo
                {
                    PId = x.PId,
                    Name = x.Name,
                    ShopPrice = x.ShopPrice,
                    State = x.State,
                    SalesVolume = y.Count(),
                    DisplayOrder = x.DisplayOrder
                })
                .OrderBy(p => p.DisplayOrder)
                .ThenByDescending(p => p.PId)
                .LoadPage(page, out total)
                .ToList();

            if (!data.Any())
                return null;

            foreach (var product in data)
            {
                product.StateDec = ((ProductsStatus) product.State).GetDescription();
            }
            return data;
        }

        /// <summary>
        /// 用户数
        /// </summary>
        /// <returns></returns>
        public int GetProductCountTotal()
        {
            return _productsRepository.Count();
        }


        /// <summary>
        /// 获取商品类型统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>商品列表</returns>
        public List<Data> AdminGetProductCategoryCount(DateTime startTime, DateTime endTime)
        {
            var cate = _categoriesRepository.GetDbSetNoTracking();
            return _productsRepository.GetDbSetNoTracking()
                .Where(x => x.AddTime >= startTime && x.AddTime <= endTime)
                .GroupBy(x => x.CateId)
                .Select(x => new
                {
                    CateId = x.Key,
                    Count = x.Count()
                })
                .Join(cate, x => x.CateId, y => y.CateId, (x, y) => new Data
                {
                    DataName = y.Name,
                    DataValue = x.Count
                }).ToList();
        }

        /// <summary>
        /// 获取产品上架每天的数据
        /// </summary>
        /// <returns></returns>
        public List<DataDetail> GetProductCountGroupByDate(DateTime startTime, DateTime endTime)
        {
            return _productsRepository.GetDbSetNoTracking()
                .Where(x => x.AddTime >= startTime && x.AddTime <= endTime)
                .GroupBy(x => SqlFunctions.DateName("yy", x.AddTime) + "-" +
                              SqlFunctions.DateName("mm", x.AddTime) + "-" +
                              SqlFunctions.DateName("dd", x.AddTime))
                .Select(x => new DataDetail
                {
                    Date = x.Key,
                    Value = x.Count(),
                })
                .ToList();
        }

        /// <summary>
        /// 获取商品相关信息
        /// </summary>
        public AdminProductInfo GetAdminProductInfo(int pId)
        {
            ////获得该商品下的图片列表
            var productImagesList = _productImagesRepository.LoadEntitiesNoTracking(m => m.PId == pId).ToList();

            //定义string类型数组
            var imgList = productImagesList.Select(productImageInfo => productImageInfo.ShowImg).ToList();

            //定义string类型数组（图片完整路径）
            var imgFullList = productImagesList.Select(productImageInfo => FileHelper.GetFileFullUrl(productImageInfo.ShowImg)).ToList();

            var adminProductInfo = (from p in _productsRepository.GetDbSetNoTracking()
                join ps in _productStocksRepository.GetDbSetNoTracking() on p.PId equals ps.PId
                join c in _categoriesRepository.GetDbSetNoTracking() on p.CateId equals c.CateId
                join b in _brandsRepository.GetDbSetNoTracking() on p.BrandId equals b.BrandId
                select new AdminProductInfo
                {
                    PId = p.PId,
                    CateId = p.CateId,
                    CateName = c.Name,
                    BrandId = p.BrandId,
                    BrandName = b.Name,
                    Name = p.Name,
                    Summary = p.Summary,
                    State = p.State,
                    TemplateId = p.TemplateId,
                    ShopPrice = p.ShopPrice,
                    CostPrice = p.CostPrice,
                    Number = ps.Number,
                    Img = imgList,
                    ImgFull = imgFullList,
                    Description = p.Description,
                    ShowImg = p.ShowImg,
                    Weight = p.Weight,
                    IsCostPrice = p.IsCostPrice
                }).FirstOrDefault(m => m.PId == pId);

            return adminProductInfo;
        }

    }
}
