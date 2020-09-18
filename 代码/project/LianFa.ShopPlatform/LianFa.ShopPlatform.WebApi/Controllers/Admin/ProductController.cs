using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Product;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Product;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台商品控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台商品相关接口", "用于后台商品操作")]
    [RoutePrefix("api/admin/product")]
    public class ProductController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 商品服务
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 商品库存服务
        /// </summary>
        private readonly IProductStocksService _productsStocks;

        /// <summary>
        /// 商品图片服务
        /// </summary>
        private readonly IProductImagesService _productsImages;

        /// <summary>
        /// 运费模板服务
        /// </summary>
        private readonly IShippingTemplatesService _shippingTemplates;

        /// <summary>
        /// 分类服务
        /// </summary>
        private readonly ICategoriesService _categories;

        /// <summary>
        /// 品牌服务
        /// </summary>
        private readonly IBrandsService _brands;

        /// <summary>
        /// 订单商品服务
        /// </summary>
        private readonly IOrderProductsService _orderProducts;

        /// <summary>
        /// 订单服务
        /// </summary>
        private readonly IOrdersService _orders;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="products"></param>
        /// <param name="productsStocks"></param>
        /// <param name="productsImages"></param>
        /// <param name="shippingTemplates"></param>
        /// <param name="categories"></param>
        /// <param name="orderProducts"></param>
        /// <param name="orders"></param>
        /// <param name="brands"></param>
        public ProductController(IUnitOfWork unitOfWork, IProductsService products, IProductStocksService productsStocks,
            IProductImagesService productsImages,IShippingTemplatesService shippingTemplates,
            ICategoriesService categories,IOrderProductsService orderProducts, IOrdersService orders, IBrandsService brands)
        {
            _unitOfWork = unitOfWork;
            _products = products;
            _productsStocks = productsStocks;
            _productsImages = productsImages;
            _shippingTemplates = shippingTemplates;
            _categories = categories;
            _orderProducts = orderProducts;
            _orders = orders;
            _brands = brands;
        }

        /// <summary>
        /// 后台商品列表
        /// </summary>
        [Route("AdminProductList")]
        public BaseResponse<AdminProductListResponse> AdminProductList(AdminProductListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //定义总数
                    int total;

                    var productList = _products.AdminGetProductList(request.Name, request.BrandId, request.CateId,
                        request.Page, out total);

                    var response = new AdminProductListResponse
                    {
                        ProductList = productList,
                        Total = total
                    };


                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminProductListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminProductList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminProductListResponse>("获取后台商品列表错误");
            }
        }

        /// <summary>
        /// 后台商品信息
        /// </summary>
        [Route("AdminProductInfo")]
        public BaseResponse<AdminProductInfoResponse> AdminProductInfo(AdminProductInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取商品相关信息
                    var productInfo = _products.GetAdminProductInfo(request.PId);
                    
                    productInfo.ShowImgFull = FileHelper.GetFileFullUrl(productInfo.ShowImg);
                    productInfo.Number = productInfo.Number < 0 ? 0 : productInfo.Number;

                    var response = new AdminProductInfoResponse
                    {
                        ProductInfo = productInfo
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminProductInfoResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminProductInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminProductInfoResponse>("获取后台商品信息错误");
            }
        }

        /// <summary>
        /// 后台添加商品
        /// </summary>
        [Route("AdminAddProduct")]
        [AddOperateLog("后台添加商品")]
        public BaseResponse<object> AdminAddProduct(AdminAddProductRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        if (!_categories.Exist(c => c.CateId == request.CateId))
                        {
                            return BuildResponse.FailResponse<object>("请选择分类");
                        }

                        if (!_brands.Exist(b => b.BrandId == request.BrandId))
                        {
                            return BuildResponse.FailResponse<object>("请选择品牌");
                        }

                        if (string.IsNullOrEmpty(request.ShowImg) && request.ShowImg == null)
                        {
                            return BuildResponse.FailResponse<object>("请上传封面图片");
                        }

                        if (request.Weight == 0 && request.TemplateId > 0)
                        {
                            if (_shippingTemplates.Exist(m => m.TemplateId == request.TemplateId))
                            {
                                var tempType = _shippingTemplates
                                    .LoadEntitieNoTracking(m => m.TemplateId == request.TemplateId)
                                    .ValuationMethod;

                                if (tempType == (int) ValuationMethod.Weight)
                                {
                                    return BuildResponse.FailResponse<object>("以重量计算运费的商品请填写正确重量");
                                }
                            }
                        }

                        var product = new LF_Products()
                        {
                            PSn = request.PSn,
                            CateId = request.CateId,
                            BrandId = request.BrandId,
                            Name = request.Name,
                            CostPrice = request.CostPrice,
                            IsCostPrice = request.IsCostPrice,
                            State = request.State,
                            DisplayOrder = request.DisplayOrder,
                            ShowImg = request.ShowImg,
                            Weight = request.Weight,
                            AddTime = DateTime.Now,
                            Description = request.Description,
                            Summary = request.Summary,
                            ShopPrice = request.ShopPrice,
                            TemplateId = request.TemplateId
                        };

                        //添加商品
                        _products.AddProducts(product);

                        //提交事务
                        _unitOfWork.Commit();

                        var productStocks = new LF_ProductStocks()
                        {
                            PId = product.PId,
                            Number = request.Number,
                            Limit = 1
                        };

                        //添加商品库存
                        _productsStocks.AddProductStocks(productStocks);

                        //商品图片列表
                        if (request.Img != null)
                        {
                            var imageList = request.Img.Select((t, i) => new LF_ProductImages()
                                {
                                    PId = product.PId,
                                    ShowImg = t,
                                    DisplayOrder = 0,
                                    IsMain = (byte) (i == 0 ? 1 : 0)
                                })
                                .ToList();

                            //批量添加商品图片
                            _productsImages.BatchAddProductImages(imageList);
                        }

                        //提交事务
                        var result = _unitOfWork.Commit();
                        if (result > 0)
                        {
                            scope.Complete();
                            return BuildResponse.SuccessResponse<object>("添加商品成功");
                        }
                        return BuildResponse.FailResponse<object>("添加商品错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台添加商品错误");
            }
        }

        /// <summary>
        /// 后台编辑商品
        /// </summary>
        [Route("AdminEditProduct")]
        [AddOperateLog("后台编辑商品")]
        public BaseResponse<object> AdminEditProduct(AdminEditProductRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        //获得商品信息
                        var products = _products.GetProductsById(request.PId);
                        if(products == null)
                             return BuildResponse.FailResponse<object>("商品不存在");

                        if (!_categories.Exist(c => c.CateId == request.CateId))
                        {
                            return BuildResponse.FailResponse<object>("请选择分类");
                        }

                        if (!_brands.Exist(b => b.BrandId == request.BrandId))
                        {
                            return BuildResponse.FailResponse<object>("请选择品牌");
                        }

                        if (string.IsNullOrEmpty(request.ShowImg) && request.ShowImg == null)
                        {
                            return BuildResponse.FailResponse<object>("请上传封面图片");
                        }

                        if (request.Weight == 0 && request.TemplateId > 0)
                        {
                            if (_shippingTemplates.Exist(m => m.TemplateId == request.TemplateId))
                            {
                                var tempType = _shippingTemplates
                                    .LoadEntitieNoTracking(m => m.TemplateId == request.TemplateId)
                                    .ValuationMethod;

                                if (tempType == (int)ValuationMethod.Weight)
                                {
                                    return BuildResponse.FailResponse<object>("以重量计算运费的商品请填写正确重量");
                                }
                            }
                        }

                        products.CateId = request.CateId;
                        products.Name = string.IsNullOrEmpty(request.Name) ? products.Name : request.Name;
                        products.State = request.State;
                        products.CostPrice = request.CostPrice;
                        products.IsCostPrice = request.IsCostPrice;
                        products.DisplayOrder = request.DisplayOrder;
                        products.Weight = request.Weight;
                        products.ShowImg = request.ShowImg;
                        products.Description = string.IsNullOrEmpty(request.Description) ? products.Description : request.Description;
                        products.Summary = string.IsNullOrEmpty(request.Summary) ? products.Summary : request.Summary;
                        products.ShopPrice = request.ShopPrice;
                        products.TemplateId = request.TemplateId;

                        //编辑商品
                        _products.UpdateProducts(products);

                        var productStock = _productsStocks.Get(u => u.PId == request.PId);
                        if (productStock.Number != request.Number)
                        {
                            productStock.Number = request.Number;
                        }
                        _productsStocks.UpdateProductStocks(productStock);

                        if (request.Img.Count != 0)
                        {
                            //先删除该商品下的所有图片
                            _productsImages.DeleteProductImages(m => m.PId == request.PId);

                            //商品图片列表
                            var imageList = request.Img.Select((t, i) => new LF_ProductImages()
                            {
                                PId = request.PId,
                                ShowImg = t,
                                DisplayOrder = 0,
                                IsMain = (byte)(i == 0 ? 1 : 0)
                            }).ToList();

                            //批量添加商品图片
                            _productsImages.BatchAddProductImages(imageList);
                        }

                        //提交事务
                        var result = _unitOfWork.Commit();
                        if (result > 0)
                        {
                            scope.Complete();
                            return BuildResponse.SuccessResponse<object>("编辑商品成功");
                        }
                        return BuildResponse.FailResponse<object>("编辑商品错误");
                    }

                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑商品错误");
            }
        }

        /// <summary>
        /// 后台编辑商品排序
        /// </summary>
        [Route("AdminEditProductDisplayOrder")]
        [AddOperateLog("后台编辑商品排序")]
        public BaseResponse<object> AdminEditProductDisplayOrder(AdminEditProductDisplayOrderRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得商品信息
                    var product = _products.GetProductsById(request.PId);
                    if (product == null)
                    {
                        return BuildResponse.FailResponse<object>("无此商品！请确认");
                    }
                    if (product.DisplayOrder == request.DisplayOrder && product.PId == request.PId)
                    {
                        return BuildResponse.SuccessResponse<object>("后台编辑商品排序成功");
                    }

                    //更新排序
                    product.DisplayOrder = request.DisplayOrder;

                    _products.UpdateProducts(product);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("后台编辑商品排序成功") : BuildResponse.FailResponse<object>("后台编辑商品排序失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditProductDisplayOrder", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑商品排序错误");
            }
        }

        /// <summary>
        /// 后台批量上架商品
        /// </summary>
        [Route("AdminOnSaleProduct")]
        public BaseResponse<object> AdminOnSaleProduct(AdminProductIdArrayRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    foreach (var pId in request.PId)
                    {
                        _products.BatchUpdate(m => m.PId == pId, t => new LF_Products { State = (int)ProductsStatus.OnSale });
                    }

                    return BuildResponse.SuccessResponse<object>("后台批量上架商品成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminOnSaleProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台批量上架商品错误");
            }
        }

        /// <summary>
        /// 后台批量下架商品
        /// </summary>
        [Route("AdminOutSaleProduct")]
        public BaseResponse<object> AdminOutSaleProduct(AdminProductIdArrayRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    foreach (var pId in request.PId)
                    {
                        _products.BatchUpdate(m => m.PId == pId, t => new LF_Products { State = (int)ProductsStatus.OutSale });
                    }

                    return BuildResponse.SuccessResponse<object>("后台批量下架商品成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminOutSaleProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台批量下架商品错误");
            }
        }

        /// <summary>
        /// 后台批量删除商品
        /// </summary>
        [Route("AdminDelProduct")]
        [AddOperateLog("后台批量删除商品")]
        public BaseResponse<object> AdminDelProduct(AdminDelProductIdArrayRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //开启事务
                    using (var scope = new TransactionScope())
                    {
                        foreach (var pId in request.PId)
                        {
                            if (!_orderProducts.Exist(m => m.PId == pId)) continue;
                            {
                                var oIdList = _orderProducts.LoadEntitiesNoTracking(m => m.PId == pId)
                                    .Select(m => m.OId)
                                    .ToList();

                                if (!oIdList.Any()) continue;
                                {
                                    if (oIdList.Any(oId => _orders.Exist(m => m.OId == oId && m.OrderState != (int)OrderState.Cancelled)))
                                    {
                                        return BuildResponse.FailResponse<object>("存在未关闭的订单！无法删除该商品。商品序号为：" + pId);
                                    }
                                }
                            }
                        }

                        _products.BatchDelete(m => request.PId.Contains(m.PId));

                        _productsStocks.BatchDelete(m => request.PId.Contains(m.PId));

                        _productsImages.BatchDelete(m => request.PId.Contains(m.PId));

                        //成功提交事务
                        scope.Complete();

                        return BuildResponse.SuccessResponse<object>("后台删除商品成功");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台删除商品错误");
            }
        }

        /// <summary>
        /// 后台上传商品图片
        /// </summary>
        [AllowAnonymous]
        [Route("AdminUploadProductImg")]
        public async Task<BaseResponse<string>> AdminUploadProductImg()
        {

            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/admin/ProductImg/";

                //异步上传图片
                var result = await new UploadFile().ImgUpload(Request.Content, saveTempPath);

                //获取返回结果
                var responseCode = Convert.ToInt32(result["Code"].ToString());
                if (responseCode == (int)ResponseCode.UploadSuccess)
                {
                    //获取上传的文件名
                    var data = result["Data"].ToString();

                    //返回成功结果
                    return BuildResponse.SuccessResponse(data, ResponseCode.UploadSuccess.GetDescription(), ResponseCode.UploadSuccess);
                }

                //返回失败结果
                return BuildResponse.FailResponse<string>(result["Message"].ToString(), (ResponseCode)responseCode);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUploadProductImg", "", ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台上传商品图片错误");
            }
        }

        /// <summary>
        /// 获取模型验证错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        [NonAction]
        private string GetModelErrorMsg()
        {
            //错误信息
            var errorMessage = string.Empty;

            //获取所有错误的Key
            var key = ModelState.Keys.FirstOrDefault();
            if (key != null)
            {
                //获取第一个key对应的ModelStateDictionary的第一条错误信息
                var error = ModelState[key].Errors.FirstOrDefault();

                //将错误描述添加到sb中
                errorMessage = error?.ErrorMessage;
            }

            //返回错误信息
            return errorMessage;
        }
    }
}
