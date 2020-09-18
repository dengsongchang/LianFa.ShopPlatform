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
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Request.Admin.Category;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Category;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台分类控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台分类相关接口", "用于后台分类操作")]
    [RoutePrefix("api/admin/category")]
    public class CategoryController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 分类服务
        /// </summary>
        private readonly ICategoriesService _categories;
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
        /// 后台分类控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="categories">分类服务</param>
        /// <param name="products">商品服务</param>
        public CategoryController(IUnitOfWork unitOfWork, ICategoriesService categories, IProductsService products)
        {
            _unitOfWork = unitOfWork;
            _categories = categories;
            _products = products;
        }

        /// <summary>
        /// 后台分类列表
        /// </summary>
        [Route("AdminCategoryList")]
        public BaseResponse<AdminCategoryListResponse> AdminCategoryList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取分类列表
                    var categoryDto = _categories.GetCategoryList();
                    var response = new AdminCategoryListResponse
                    {
                        CategoryList = categoryDto
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminCategoryListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminCategoryList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminCategoryListResponse>("获取后台分类列表错误");
            }
        }

        /// <summary>
        /// 后台分类信息
        /// </summary>
        [Route("AdminCategoryInfo")]
        public BaseResponse<AdminCategoryInfoResponse> AdminCategoryInfo(AdminCategoryInfoRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获得分类信息
                    var categoryInfoDto = _categories.LoadEntitieNoTracking(m => m.CateId == request.CateId);

                    if (categoryInfoDto == null)
                    {
                        return BuildResponse.FailResponse<AdminCategoryInfoResponse>("无此分类信息！请确认");
                    }

                    //模型映射
                    var categoryInfo = categoryInfoDto.MapTo<AdminCategoryInfo>();

                    var response = new AdminCategoryInfoResponse()
                    {
                        CategoryInfo = categoryInfo
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminCategoryInfoResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminCategoryInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminCategoryInfoResponse>("获取后台分类信息错误");
            }
        }

        /// <summary>
        /// 后台添加分类
        /// </summary>
        [Route("AdminAddCategory")]
        [AddOperateLog("后台添加分类")]
        public BaseResponse<object> AdminAddCategory(AdminAddCategoryRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    if (_categories.Exist(m => m.Name == request.Name))
                    {
                        return BuildResponse.FailResponse<object>("分类名称已经存在！请重新输入");
                    }

                    //定义对象
                    var category = new LF_Categories
                    {
                        Name = request.Name,
                        DisplayOrder = request.DisplayOrder
                    };

                    //添加分类
                    _categories.AddCategories(category);

                    //提交事务
                    _unitOfWork.Commit();


                    //清除分类列表缓存
                    CacheHelper.ClearCahe(CacheKeys.SHOP_CATEGORY_LIST);
                    return BuildResponse.SuccessResponse<object>("添加分类成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddCategory", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("添加分类错误");
            }
        }

        /// <summary>
        /// 后台编辑分类
        /// </summary>
        [Route("AdminEditCategory")]
        [AddOperateLog("后台编辑分类")]
        public BaseResponse<object> AdminEditCategory(AdminEditCategoryRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //获取分类
                    var categoryInfo = _categories.GetCategoriesById(request.CateId);

                    if (categoryInfo == null)
                    {
                        return BuildResponse.FailResponse<object>("无此分类信息！请确认");
                    }

                    if (_categories.Exist(m => m.Name == request.Name && m.CateId != request.CateId))
                    {
                        return BuildResponse.FailResponse<object>("分类名称已经存在！请重新输入");
                    }

                    //编辑分类信息
                    categoryInfo.Name = request.Name;
                    categoryInfo.DisplayOrder = request.DisplayOrder;

                    //编辑分类
                    _categories.UpdateCategories(categoryInfo);

                    //提交事务
                    _unitOfWork.Commit();

                    //清除分类列表缓存
                    CacheHelper.ClearCahe(CacheKeys.SHOP_CATEGORY_LIST);
                    return BuildResponse.SuccessResponse<object>("编辑分类成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditCategory", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("编辑分类错误");
            }
        }

        /// <summary>
        /// 后台批量删除分类
        /// </summary>
        [Route("AdminBatchDelCategory")]
        [AddOperateLog("后台批量删除分类")]
        public BaseResponse<object> AdminBatchDelCategory(AdminBatchDelCategoryRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var categroriesProduct = _products.Exist(x => request.CateIdList.Contains(x.CateId));
                    if (categroriesProduct)
                    {
                        return BuildResponse.FailResponse<object>("该分类下存在商品，删除失败");
                    }
                    //批量删除
                    var result = _categories.BatchDelete(m => request.CateIdList.Contains(m.CateId));


                    return result > 0 ? BuildResponse.SuccessResponse<object>("删除分类成功") : BuildResponse.FailResponse<object>("删除分类失败");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminBatchDelCategory", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除分类错误");
            }
        }

        /// <summary>
        /// 后台转移分类商品
        /// </summary>
        [Route("AdminTransferCategoryProduct")]
        [AddOperateLog("后台转移分类商品")]
        public BaseResponse<object> AdminTransferCategoryProduct(AdminTransferCategoryProductRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //根据分类id查询商品列表
                    var productsLsit = _products.GetList(m => m.CateId == request.OldCateId);

                    if (!productsLsit.Any())
                    {
                        return BuildResponse.FailResponse<object>("该分类下无要转移的商品");
                    }
                    //批量更新分类id
                    _products.BatchUpdate(m => m.CateId == request.OldCateId, t => new LF_Products { CateId = request.NewCateId });

                    return BuildResponse.SuccessResponse<object>("后台转移分类商品成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminTransferCategoryProduct", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台转移分类商品错误");
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
