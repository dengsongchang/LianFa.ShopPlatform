using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Code.Helper;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin.Material;
using LianFa.ShopPlatform.Model.Request.Admin.Material;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Material;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台素材库控制器
    /// </summary>
    [ControllerGroup("后台素材库相关接口", "用于后台素材库操作")]
    [RoutePrefix("api/admin/Material")]
    public class MaterialController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 素材库服务
        /// </summary>
        private readonly IMaterialLibraryService _materialLibrary;

        /// <summary>
        /// 素材库分类服务
        /// </summary>
        private readonly IMaterialCategoryService _materialCategory;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台素材库控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="materialLibrary">素材库服务</param>
        /// <param name="materialCategory">素材库分类服务</param>
        public MaterialController(IUnitOfWork unitOfWork, IMaterialLibraryService materialLibrary, IMaterialCategoryService materialCategory)
        {
            _unitOfWork = unitOfWork;
            _materialLibrary = materialLibrary;
            _materialCategory = materialCategory;
        }

        #region 上传临时图片

        /// <summary>
        /// 上传临时图片
        /// </summary>
        /// <returns></returns>
        [Route("AdminUploadTempImage")]
        public async Task<BaseResponse<string>> AdminUploadTempImage()
        {
            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/media/temp/";

                //异步上传图片
                var result = await new UploadFile().ImgUpload(Request.Content, saveTempPath);

                //获取返回结果
                var responseCode = Convert.ToInt32(result["Code"].ToString());
                if (responseCode == (int)ResponseCode.UploadSuccess)
                {
                    //获取上传的文件名
                    var data = result["Data"].ToString();

                    //返回成功结果
                    return BuildResponse.SuccessResponse(data,
                        ResponseCode.UploadSuccess.GetDescription(), ResponseCode.UploadSuccess);
                }
                //返回失败结果
                return BuildResponse.FailResponse<string>(result["Message"].ToString(), (ResponseCode)responseCode);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUploadTempImage", "", ex.Message);
                return BuildResponse.FailResponse<string>("上传错误");
            }
        }

        #endregion

        #region 确认上传素材

        /// <summary>
        /// 确认上传素材
        /// </summary>
        [Route("AdminSaveMaterial")]
        public BaseResponse<object> AdminSaveMaterial(AdminSaveMaterialRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    const string saveTempPath = "/upload/media/image/";
                    //异步操作
                    var modelList = new List<LF_MaterialLibrary>();
                    foreach (var item in request.TempImageList)
                    {
                        //剪切文件
                        var copyResult = CustomFileHelper.MoveToFiles(item, saveTempPath);
                        //获取返回结果
                        var responseCode = Convert.ToInt32(copyResult["Code"].ToString());
                        if (responseCode == (int)ResponseCode.UploadSuccess)
                        {
                            //文件名
                            var fileName = copyResult["FileName"].ToString().Split('.')[0];
                            modelList.Add(new LF_MaterialLibrary
                            {
                                CategoryId = request.CategoryId,
                                CreateTime = DateTime.Now,
                                FileName = fileName,
                                ShowName = fileName,
                                FileExt = copyResult["FileExt"].ToString(),
                                FileSize = Convert.ToInt32(copyResult["FileSize"].ToString()) / 1024,
                                FileUrl = saveTempPath + copyResult["FileName"],
                                Type = (byte)MaterialType.Image,
                                UpdateTime = DateTime.Now
                            });
                        }
                    }
                    _materialLibrary.BatchAddMaterialLibrary(modelList);
                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("上传素材成功") : BuildResponse.FailResponse<object>("上传素材失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminSaveMaterial", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("上传素材错误");
            }
        }

        #endregion

        #region 直接上传图片

        /// <summary>
        /// 直接上传图片
        /// </summary>
        /// <returns></returns>
        [Route("AdminUploadImage")]
        public async Task<BaseResponse<string>> AdminUploadImage(int categoryId)
        {
            try
            {
                //上传文件路径
                const string saveTempPath = "/upload/media/temp/";

                //异步上传图片
                var result = await new UploadFile().ImgUpload(Request.Content, saveTempPath);

                //获取返回结果
                var responseCode = Convert.ToInt32(result["Code"].ToString());
                if (responseCode == (int)ResponseCode.UploadSuccess)
                {
                    //获取上传的文件名
                    var data = result["Data"].ToString();
                    _materialLibrary.AddMaterialLibrary(new LF_MaterialLibrary
                    {
                        CategoryId = categoryId,
                        CreateTime = DateTime.Now,
                        FileName = result["FileName"].ToString(),
                        ShowName = result["FileName"].ToString(),
                        FileExt = result["FileExt"].ToString(),
                        FileSize = Convert.ToInt32(result["FileSize"].ToString()) / 1024,
                        FileUrl = saveTempPath + result["FileName"] + result["FileExt"],
                        Type = (byte)MaterialType.Image,
                        UpdateTime = DateTime.Now
                    });
                    var succ = _unitOfWork.Commit();
                    if (succ > 0)
                        //返回成功结果
                        return BuildResponse.SuccessResponse(data,
                            ResponseCode.UploadSuccess.GetDescription(), ResponseCode.UploadSuccess);
                    return BuildResponse.FailResponse<string>("添加失败");
                }
                //返回失败结果
                return BuildResponse.FailResponse<string>(result["Message"].ToString(), (ResponseCode)responseCode);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUploadImage", "", ex.Message);
                return BuildResponse.FailResponse<string>("上传错误");
            }
        }

        #endregion

        #region 获取素材列表

        /// <summary>
        /// 获取素材列表
        /// </summary>
        [Route("AdminGetMaterialList")]
        public BaseResponse<AdminGetMaterialListResponse> AdminGetMaterialList(AdminGetMaterialListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //总数
                    int total;
                    //查询列表
                    var list = _materialLibrary.GetMaterialList(request.Page, request.FileName, request.CategoryId,
                        request.DisplayOrder, request.IsAsc == 1, out total);
                    //返回结果
                    return BuildResponse.SuccessResponse(new AdminGetMaterialListResponse
                    {
                        List = list.MapToList<MaterialModel, MaterialModel, AdminGetMaterialListProfile>(),
                        Total = total
                    });
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetMaterialListResponse>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetMaterialList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetMaterialListResponse>("获取素材列表错误");
            }
        }

        #endregion

        #region 修改素材名称

        /// <summary>
        /// 修改素材名称
        /// </summary>
        [Route("AdminUpdateMaterialName")]
        [AddOperateLog("修改素材名称")]
        public BaseResponse<object> AdminUpdateMaterialName(AdminUpdateMaterialNameRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //查询
                    var model = _materialLibrary.GetMaterialLibraryById(request.Id);
                    if (model == null) return BuildResponse.FailResponse<object>("素材不存在！");

                    //更新
                    model.ShowName = request.Name;
                    model.UpdateTime = DateTimeHelper.GetBeiJingNowDateTime();
                    _materialLibrary.UpdateMaterialLibrary(model);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("修改素材名称成功") : BuildResponse.FailResponse<object>("修改素材名称失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUpdateMaterialName", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("修改素材名称错误");
            }
        }

        #endregion

        #region 移动素材

        /// <summary>
        /// 移动素材
        /// </summary>
        [Route("AdminMoveMaterial")]
        [AddOperateLog("移动素材")]
        public BaseResponse<object> AdminMoveMaterial(AdminMoveMaterialRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //批量更新
                    var result = _materialLibrary.BatchUpdate(x => request.Ids.Any(t => t == x.MaterialId), x => new LF_MaterialLibrary
                    {
                        CategoryId = request.CategoryId
                    });

                    return result > 0 ? BuildResponse.SuccessResponse<object>("移动素材成功") : BuildResponse.FailResponse<object>("移动素材失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminMoveMaterial", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("移动素材错误");
            }
        }

        #endregion

        #region 删除素材

        /// <summary>
        /// 删除素材
        /// </summary>
        [Route("AdminDeleteMaterial")]
        [AddOperateLog("删除素材")]
        public BaseResponse<object> AdminDeleteMaterial(AdminDeleteMaterialRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //删除
                    _materialLibrary.DeleteMaterialLibrary(x => x.MaterialId == request.Id);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("删除素材成功") : BuildResponse.FailResponse<object>("删除素材失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDeleteMaterial", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除素材错误");
            }
        }

        #endregion

        #region 批量删除素材

        /// <summary>
        /// 批量删除素材
        /// </summary>
        [Route("AdminBatchDeleteMaterial")]
        [AddOperateLog("批量删除素材")]
        public BaseResponse<object> AdminBatchDeleteMaterial(AdminBatchDeleteMaterialRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //批量删除
                    _materialLibrary.BatchDelete(x => request.Ids.Any(y => y == x.MaterialId));

                    return BuildResponse.SuccessResponse<object>("批量删除素材成功");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminBatchDeleteMaterial", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("批量删除素材错误");
            }
        }

        #endregion

        #region 添加分类

        /// <summary>
        /// 添加分类
        /// </summary>
        [Route("AdminAddCategory")]
        [AddOperateLog("添加分类")]
        public BaseResponse<object> AdminAddCategory(AdminAddMaterialCategoryRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //排序
                    var sort = _materialCategory.GetMaxSort() + 1;

                    //模型
                    var model = new LF_MaterialCategory
                    {
                        CreateTime = DateTimeHelper.GetBeiJingNowDateTime(),
                        Name = request.Name,
                        Sort = (byte)sort
                    };
                    //添加
                    _materialCategory.AddMaterialCategory(model);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("添加分类成功") : BuildResponse.FailResponse<object>("添加分类失败");
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

        #endregion

        #region 更新分类名称

        /// <summary>
        /// 更新分类名称
        /// </summary>
        [Route("AdminUpdateCategoryName")]
        [AddOperateLog("更新分类名称")]
        public BaseResponse<object> AdminUpdateCategoryName(AdminUpdateCategoryNameRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //查询
                    var model = _materialCategory.GetMaterialCategoryById(request.Id);
                    if (model == null) return BuildResponse.FailResponse<object>("分类不存在！");

                    //更新
                    model.Name = request.Name;
                    _materialCategory.UpdateMaterialCategory(model);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("更新分类名称成功") : BuildResponse.FailResponse<object>("更新分类名称失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUpdateCategoryName", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("更新分类名称错误");
            }
        }

        #endregion

        #region 更新分类排序

        /// <summary>
        /// 更新分类排序
        /// </summary>
        [Route("AdminUpdateCategorySort")]
        [AddOperateLog("更新分类排序")]
        public BaseResponse<object> AdminUpdateCategorySort(AdminUpdateCategorySortRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //最大排序值
                    const int maxsort = 255;
                    if (request.Sort > maxsort)
                        return BuildResponse.FailResponse<object>("排序值必须小于256");

                    //查询
                    var model = _materialCategory.GetMaterialCategoryById(request.Id);
                    if (model == null) return BuildResponse.FailResponse<object>("分类不存在！");

                    //更新
                    model.Sort = (byte)request.Sort;
                    _materialCategory.UpdateMaterialCategory(model);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("更新分类排序成功") : BuildResponse.FailResponse<object>("更新分类排序失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminUpdateCategorySort", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("更新分类排序错误");
            }
        }

        #endregion

        #region 删除分类

        /// <summary>
        /// 删除分类
        /// </summary>
        [Route("AdminDeleteCategory")]
        [AddOperateLog("删除分类")]
        public BaseResponse<object> AdminDeleteCategory(AdminDeleteMaterialCategoryRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    //直接删除
                    _materialCategory.DeleteMaterialCategory(x => x.CategoryId == request.Id);

                    //提交事务
                    var result = _unitOfWork.Commit();
                    return result > 0 ? BuildResponse.SuccessResponse<object>("删除分类成功") : BuildResponse.FailResponse<object>("删除分类失败");
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminDeleteCategory", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("删除分类错误");
            }
        }

        #endregion

        #region 查询分类

        /// <summary>
        /// 查询分类
        /// </summary>
        [Route("AdminGetCategoryList")]
        public BaseResponse<AdminGetCategoryListResponse> AdminGetCategoryList(AdminGetCategoryListRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //分类数据源
                    var categoryDbSet = _materialCategory.GetCategoryDbSet();
                    //图片数据源
                    var materialDbSet = _materialLibrary.GetMaterialDbSet();

                    var groupData = (from md in materialDbSet
                        group md by md.CategoryId
                        into k
                        select new
                        {
                            CategoryId = k.Key,
                            Number = k.Count()
                        }
                    );
                    int total;
                    var data = (from c in categoryDbSet
                            join m in groupData
                            on c.CategoryId equals m.CategoryId into l
                            from lgu in l.DefaultIfEmpty()
                            select new CategoryModel
                            {
                                CreateTime = c.CreateTime,
                                CategoryId = c.CategoryId,
                                ImageCount = l.Any() ? l.FirstOrDefault().Number : 0,
                                Name = c.Name,
                                Sort = c.Sort
                            })
                        .OrderBy(x => x.Sort)
                        .LoadPage(request.Page, out total)
                        .ToList();


                    return BuildResponse.SuccessResponse(new AdminGetCategoryListResponse
                    {
                        List = data,
                        Total = total
                    });
                }
                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetCategoryListResponse>(errorMessage, ResponseCode.DataError);

            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetCategoryList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetCategoryListResponse>("查询分类错误");
            }
        }

        #endregion

        #region 获取模型验证错误信息
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
        #endregion

    }
}
