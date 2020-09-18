using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Config;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Cache;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Brand;
using LianFa.ShopPlatform.Model.Request.Admin.Category;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Brand;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台品牌控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台品牌相关接口", "用于后台品牌操作")]
    [RoutePrefix("api/admin/brand")]
    public class BrandController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 品牌服务
        /// </summary>
        private readonly IBrandsService _brands;
        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();
        /// <summary>
        /// 商品服务
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 配置文件管理器
        /// </summary>
        public IConfigManager ConfigManager = Bootstrapper.GetFromFac<IConfigManager>();

        /// <summary>
        /// 后台品牌控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="brands">品牌服务</param>
        /// <param name="products">商品服务</param>
        public BrandController(IUnitOfWork unitOfWork, IBrandsService brands, IProductsService products)
        {
            _unitOfWork = unitOfWork;
            _brands = brands;
            _products = products;
        }

        /// <summary>
        /// 后台品牌列表
        /// </summary>
        [Route("AdminBrandList")]
        public BaseResponse<AdminBrandListResponse> AdminBrandList(AdminBrandListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    int total;

                    //获取品牌列表
                    var brandDto = _brands.GetBrandList(request.Name,request.Page,out total);
                    var response = new AdminBrandListResponse
                    {
                        BrandList = brandDto
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminBrandListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminBrandList", "requestString", ex.Message, ex);
                return BuildResponse.FailResponse<AdminBrandListResponse>("获取后台品牌列表错误");
            }
        }

        /// <summary>
        /// 后台品牌信息
        /// </summary>
        [Route("AdminBrandInfo")]
        public BaseResponse<AdminBrandInfoResponse> AdminBrandInfo(AdminBrandInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得品牌信息
                    var brandInfoDto = _brands.LoadEntitieNoTracking(m => m.BrandId == request.BrandId);

                    if (brandInfoDto == null)
                    {
                        return BuildResponse.FailResponse<AdminBrandInfoResponse>("无此品牌信息！请确认");
                    }

                    //模型映射
                    var brandInfo = brandInfoDto.MapTo<AdminBrandInfo>();

                    var response = new AdminBrandInfoResponse()
                    {
                        BrandInfo = brandInfo
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminBrandInfoResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminBrandInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminBrandInfoResponse>("获取后台品牌信息错误");
            }
        }

        /// <summary>
        /// 后台添加品牌
        /// </summary>
        [Route("AdminAddBrand")]
        [AddOperateLog("后台添加品牌")]
        public BaseResponse<object> AdminAddBrand(AdminAddBrandRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    if (_brands.Exist(m => m.Name == request.Name))
                    {
                        return BuildResponse.FailResponse<object>("品牌名称已经存在！请重新输入");
                    }

                    //定义对象
                    var brand = new LF_Brands()
                    {
                        Name = request.Name,
                        DisplayOrder = request.DisplayOrder
                    };

                    //添加品牌
                    _brands.AddBrands(brand);

                    //提交事务
                    _unitOfWork.Commit();


                    //清除品牌列表缓存
                    CacheHelper.ClearCahe(CacheKeys.SHOP_CATEGORY_LIST);
                    return BuildResponse.SuccessResponse<object>("添加品牌成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddBrand", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("添加品牌错误");
            }
        }

        /// <summary>
        /// 后台编辑品牌
        /// </summary>
        [Route("AdminEditBrand")]
        [AddOperateLog("后台编辑品牌")]
        public BaseResponse<object> AdminEditBrand(AdminEditBrandRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取品牌
                    var brandInfo = _brands.GetBrandsById(request.BrandId);

                    if (brandInfo == null)
                    {
                        return BuildResponse.FailResponse<object>("无此品牌信息！请确认");
                    }

                    if (_brands.Exist(m => m.Name == request.Name && m.BrandId != request.BrandId))
                    {
                        return BuildResponse.FailResponse<object>("品牌名称已经存在！请重新输入");
                    }

                    //编辑品牌信息
                    brandInfo.Name = request.Name;
                    brandInfo.DisplayOrder = request.DisplayOrder;

                    //编辑品牌
                    _brands.UpdateBrands(brandInfo);

                    //提交事务
                    _unitOfWork.Commit();

                    //清除品牌列表缓存
                    CacheHelper.ClearCahe(CacheKeys.SHOP_CATEGORY_LIST);
                    return BuildResponse.SuccessResponse<object>("编辑品牌成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditBrand", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("编辑品牌错误");
            }
        }

        /// <summary>
        /// 后台批量删除品牌
        /// </summary>
        [Route("AdminBatchDelBrand")]
        [AddOperateLog("后台批量删除品牌")]
        public BaseResponse<object> AdminBatchDelBrand(AdminBatchDelBrandRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var categroriesProduct = _products.Exist(x => request.BrandIdList.Contains(x.BrandId));
                    if (categroriesProduct)
                    {
                        return BuildResponse.FailResponse<object>("该品牌下存在商品，删除失败");
                    }
                    //批量删除
                    var result = _brands.BatchDelete(m => request.BrandIdList.Contains(m.BrandId));


                    return result > 0 ? BuildResponse.SuccessResponse<object>("删除品牌成功") : BuildResponse.FailResponse<object>("删除品牌失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminBatchDelBrand", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除品牌错误");
            }
        }

        /// <summary>
        /// 后台转移品牌商品
        /// </summary>
        [Route("AdminTransferBrandProduct")]
        [AddOperateLog("后台转移品牌商品")]
        public BaseResponse<object> AdminTransferBrandProduct(AdminTransferBrandProductRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //根据品牌id查询商品列表
                    var productsLsit = _products.GetList(m => m.BrandId == request.OldBrandId);

                    if (!productsLsit.Any())
                    {
                        return BuildResponse.FailResponse<object>("该品牌下无要转移的商品");
                    }
                    //批量更新品牌id
                    _products.BatchUpdate(m => m.BrandId == request.OldBrandId, t => new LF_Products { BrandId = request.NewBrandId });

                    return BuildResponse.SuccessResponse<object>("后台转移品牌商品成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminTransferBrandProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台转移品牌商品错误");
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
