using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Client.IndexData;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers
{
    /// <summary>
    /// 首页数据控制器
    /// </summary>
    [Signature]
    [ControllerGroup("首页数据控制器接口", "首页数据相关")]
    public class IndexDataController : ApiController
    {

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 轮播图管理
        /// </summary>
        private readonly IBannersService _banners;

        /// <summary>
        /// 礼品卡类型管理
        /// </summary>
        private readonly ICouponTypesService _couponTypes;

        /// <summary>
        /// 品牌管理
        /// </summary>
        private readonly IBrandsService _brands;

        /// <summary>
        /// 分类管理
        /// </summary>
        private readonly ICategoriesService _categories;

        /// <summary>
        /// 商品管理
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 商品图片管理
        /// </summary>
        private readonly IProductImagesService _productImages;

        /// <summary>
        /// 商品库存管理
        /// </summary>
        private readonly IProductStocksService _productStocks;

        /// <summary>
        /// 构造函数(构造注入)
        /// </summary>
        /// <param name="banners">轮播图管理</param>
        /// <param name="couponTypes">礼品卡类型管理</param>
        /// <param name="brands">品牌管理</param>
        /// <param name="categories">分类管理</param>
        /// <param name="products">商品管理</param>
        /// <param name="productImages">商品图片管理</param>
        /// <param name="productStocks">商品库存管理</param>
        public IndexDataController(IBannersService banners, ICouponTypesService couponTypes, IBrandsService brands,
            ICategoriesService categories, IProductsService products, IProductImagesService productImages, IProductStocksService productStocks)
        {
            _banners = banners;
            _couponTypes = couponTypes;
            _brands = brands;
            _categories = categories;
            _products = products;
            _productImages = productImages;
            _productStocks = productStocks;
        }

        #region 弹屏信息

        /// <summary>
        /// 弹屏信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<ScreenInfoResponse> GetScreenInfo()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //获取配置文件数据
                    var response = new ScreenInfoResponse
                    {
                        ScreenImg = ConfigMap.ScreenImg,
                        ScreenImgFull = FileHelper.GetFileFullUrl(ConfigMap.ScreenImg),
                        ScreenLink = ConfigMap.ScreenLink
                    };
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ScreenInfoResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetScreenInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<ScreenInfoResponse>("获取弹屏信息错误");
            }
        }
        
        #endregion
        
        #region 轮播图列表

        /// <summary>
        /// 轮播图列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<GetBannerListResponse> GetBannerList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new GetBannerListResponse
                    {
                        BannerList = _banners.BannerList()
                    };
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<GetBannerListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetBannerList", "", ex.Message, ex);
                return BuildResponse.FailResponse<GetBannerListResponse>("获取轮播图列表错误");
            }
        }

        #endregion
        
        #region 礼品卡列表

        /// <summary>
        /// 礼品卡列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<GetCouponListResponse> GetCouponList(GetCouponListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    var couponList = _couponTypes.CouponTypeList(request.Page, out total);
                    var response = new GetCouponListResponse
                    {
                        CouponList = couponList,
                        Total = total
                    };
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<GetCouponListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetCouponList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<GetCouponListResponse>("获取礼品卡列表错误");
            }
        }

        #endregion
        
        #region 品牌、分类列表

        /// <summary>
        /// 品牌、分类列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [NoAuth]
        public BaseResponse<GetBrandAndCategoryListResponse> GetBrandAndCategoryList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new GetBrandAndCategoryListResponse
                    {
                        BrandList = _brands.BrandList(),
                        CategoryList = _categories.CategoryList()
                    };
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<GetBrandAndCategoryListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("GetBrandAndCategoryList", "", ex.Message, ex);
                return BuildResponse.FailResponse<GetBrandAndCategoryListResponse>("获取品牌分类列表错误");
            }
        }

        #endregion
        
        #region 商品列表

        /// <summary>
        /// 商品列表
        /// </summary>
        [NoAuth]
        public BaseResponse<ProductListResponse> ProductList(ProductListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    //获取商家列表
                    var productList =
                        _products.GetProductList(request.BrandId, request.CateId, request.Page, out total);

                    var response = new ProductListResponse
                    {
                        ProductList = productList,
                        Total = total
                    };

                    return BuildResponse.SuccessResponse(response);
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<ProductListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ProductList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<ProductListResponse>("获取商品列表错误");
            }
        }

        #endregion
        
        #region 商品详情

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <returns></returns>
        [NoAuth]    
        public BaseResponse<ProductDetailResponse> ProductDetail(ProductDetailRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var productInfo = _products.LoadEntitieNoTracking(p => p.PId == request.PId && p.State == (byte)WhetherType.Yes);
                    if (productInfo == null)
                    {
                        return BuildResponse.FailResponse<ProductDetailResponse>("不存在该商品");
                    }
                    var showImgList = _productImages.GetProductImagesList(request.PId);
                    var num = 0;//库存
                    var stock = _productStocks.LoadEntitieNoTracking(p => p.PId == productInfo.PId);
                    if (stock != null)
                        num = stock.Number;

                    var model = new ProductDetailResponse
                    {
                        PId = productInfo.PId,
                        ShowImg = productInfo.ShowImg,
                        ShowImgFull = FileHelper.GetFileFullUrl(productInfo.ShowImg),
                        ShowImgList = showImgList,
                        Name = productInfo.Name,
                        ShopPrice = productInfo.ShopPrice,
                        Description = productInfo.Description,
                        Summary = productInfo.Summary,
                        Stock = num,
                        IsCostPrice = productInfo.IsCostPrice,
                        CostPrice = productInfo.CostPrice
                    };

                    return BuildResponse.SuccessResponse(model);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<ProductDetailResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("ProductDetail", "", ex.Message, ex);
                return BuildResponse.FailResponse<ProductDetailResponse>("获取商品详情错误");
            }
        }

        #endregion

        #region 获取模型验证出错信息

        /// <summary>
        /// 获取模型验证出错信息
        /// </summary>
        /// <returns>出错信息</returns>
        [NonAction]
        private string GetModelErrorMsg()
        {
            //出错信息
            var errorMessage = string.Empty;

            //获取所有出错的Key
            var key = ModelState.Keys.FirstOrDefault();
            if (key != null)
            {
                //获取第一个key对应的ModelStateDictionary的第一条出错信息
                var error = ModelState[key].Errors.FirstOrDefault();

                if (string.IsNullOrEmpty(error?.ErrorMessage))
                {
                    ApiLogger.Info("GetModelErrorMsg", key, $"数据验证出错:{error?.Exception.Message}");

                    //将出错描述添加到sb中
                    errorMessage = $"数据验证出错，请求字段：{key}数据类型不正确，无法解析";
                }
                else
                {
                    //将出错描述添加到sb中
                    errorMessage = error.ErrorMessage;
                }
            }

            //返回出错信息
            return errorMessage;
        }

        #endregion

    }
}