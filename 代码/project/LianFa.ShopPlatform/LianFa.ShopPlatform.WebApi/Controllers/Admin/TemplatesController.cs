using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin.TimeProductActivity;
using LianFa.ShopPlatform.Model.Request.Admin.Templates;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Templates;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台运费模板控制器
    /// </summary>
    [AdminAuth]
    [ControllerGroup("后台运费模板相关接口", "用于后台运费模板操作")]
    [RoutePrefix("api/admin/templates")]
    public class TemplatesController : ApiController
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 运费模板服务
        /// </summary>
        private readonly IShippingTemplatesService _shippingTemplates;

        /// <summary>
        /// 配送地区价格服务
        /// </summary>
        private readonly IShippingRegionsGroupsService _shippingRegionsGroups;

        /// <summary>
        /// 配送地区服务
        /// </summary>
        private readonly IShippingRegionsService _shippingRegions;

        /// <summary>
        /// 区域服务
        /// </summary>
        private readonly IRegionsService _regions;

        /// <summary>
        /// 运费模板计价服务
        /// </summary>
        private readonly IShippingPriceService _shippingPrice;

        /// <summary>
        /// 商品服务
        /// </summary>
        private readonly IProductsService _products;

        /// <summary>
        /// 指定包邮地区服务
        /// </summary>
        private readonly IShippingAppointRegionsService _shippingAppointRegions;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台品牌控制器构造函数(构造注入)
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="shippingTemplates">运费模板服务</param>
        /// <param name="shippingRegions">配送地区服务</param>
        /// <param name="shippingRegionsGroups">配送地区价格服务</param>
        /// <param name="regions">区域服务</param>
        /// <param name="products">商品服务</param>
        /// <param name="shippingPrice">运费模板计价服务</param>
        /// <param name="shippingAppointRegions">指定包邮地区服务</param>
        public TemplatesController(IUnitOfWork unitOfWork, IShippingTemplatesService shippingTemplates,
                                   IShippingRegionsService shippingRegions, IShippingRegionsGroupsService shippingRegionsGroups,
                                   IRegionsService regions, IProductsService products, IShippingPriceService shippingPrice,
                                   IShippingAppointRegionsService shippingAppointRegions)
        {
            _unitOfWork = unitOfWork;
            _shippingTemplates = shippingTemplates;
            _shippingRegions = shippingRegions;
            _shippingRegionsGroups = shippingRegionsGroups;
            _regions = regions;
            _products = products;
            _shippingPrice = shippingPrice;
            _shippingAppointRegions = shippingAppointRegions;
        }

        /// <summary>
        /// 后台运费模板列表
        /// </summary>
        [Route("AdminTemplatesList")]
        public BaseResponse<AdminTemplatesListResponse> AdminTemplatesList(AdminTemplatesListRequest request)
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

                    //获得运费模板列表
                    var templatesList = _shippingTemplates.GetTemplatesList(request.Page, out total)
                        .MapToList<TemplatesListInfo, TemplatesListInfo, AdminTemplatesListProfile>();

                    //获得返回值
                    var response = new AdminTemplatesListResponse
                    {
                        TemplatesList = templatesList,
                        Total = total
                    };


                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminTemplatesListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminTemplatesList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminTemplatesListResponse>("获取后台运费模板列表错误");
            }
        }

        /// <summary>
        /// 后台运费模板信息
        /// </summary>
        [Route("AdminTemplatesInfo")]
        public BaseResponse<AdminTemplatesInfoResponse> AdminTemplatesInfo(AdminTemplatesIdRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //定义模板信息对象
                    var adminTemplatesInfo = new AdminTemplatesInfo();

                    //判断是否存在该运费模板
                    if (_shippingTemplates.Exist(m => m.TemplateId == request.TemplateId))
                    {
                        //获得运费模板通用信息
                        adminTemplatesInfo.ShippingTemplates =
                            _shippingTemplates.LoadEntitieNoTracking(m => m.TemplateId == request.TemplateId).MapTo<ShippingTemplatesInfo>();

                        //判断此运费模板信息为哪种计价方式
                        var type = adminTemplatesInfo.ShippingTemplates.ValuationMethod;

                        if (type > 3)
                        {
                            #region 获得该运费模板下的金额区间列表信息
                            //当运费模板类型为金额区间计价时，指定区域的模板信息列表为null
                            adminTemplatesInfo.ShippingRegionsGroupsList = new List<ShippingRegionsGroups>();
                            adminTemplatesInfo.ShippingAppointRegionsList = new List<ShippingAppointRegions>();

                            //获得该运费模板的金额区间列表

                            adminTemplatesInfo.ShippingPriceInfo = _shippingPrice.LoadEntitieNoTracking(m => m.TemplateId == request.TemplateId).MapTo<ShippingPriceInfo>();
                            #endregion
                        }
                        else
                        {
                            #region 获得该运费模板下的指定区域模板

                            //判断该运费模板下是否存在指定区域模板
                            if (_shippingRegionsGroups.Exist(m => m.TemplateId == request.TemplateId))
                            {
                                //当运费模板类型不为金额区间计价时，金额区间计价列表为null
                                adminTemplatesInfo.ShippingPriceInfo = new ShippingPriceInfo();

                                //获得指定区域的模板信息列表
                                adminTemplatesInfo.ShippingRegionsGroupsList =
                                    _shippingTemplates.GetRegionsGroupsList(request.TemplateId);

                                //循环获得指定模板信息列表的每一条信息（用于获得区域列表）
                                foreach (var shippingRegionsGroupsInfo in adminTemplatesInfo.ShippingRegionsGroupsList)
                                {
                                    //根据运费模板id以及地区组id获得区域列表
                                    var shippingRegionsList = _shippingRegions.LoadEntitiesNoTracking(m => m.TemplateId == request.TemplateId && m.GroupId == shippingRegionsGroupsInfo.GroupId).OrderBy(m => m.RegionId).ToList();


                                    //定义区域列表对象
                                    var regionList = new List<Regions>();

                                    //如果列表不为空则查询区域信息
                                    if (shippingRegionsList.Count != 0)
                                    {
                                        //循环获得指定模板信息的区域列表
                                        foreach (var shippingRegionsInfo in shippingRegionsList)
                                        {
                                            //判断区域id是否存在(如果存在则赋值到新的列表对象)
                                            if (shippingRegionsInfo.RegionId > 0 && _regions.Exist(m => m.RegionId == shippingRegionsInfo.RegionId))
                                            {
                                                //查询区域信息
                                                var regionInfo = _regions
                                                    .LoadEntitieNoTracking(m => m.RegionId == shippingRegionsInfo.RegionId);

                                                var name = "";
                                                if (regionInfo.Layer == 1)
                                                {
                                                    name = regionInfo.Name.TrimEnd() + ",所有市,所有县";
                                                }
                                                else if (regionInfo.Layer == 2)
                                                {
                                                    name = regionInfo.ProvinceName.TrimEnd() + "," +
                                                           regionInfo.Name.TrimEnd() + ",所有县";
                                                }
                                                else if (regionInfo.Layer == 3)
                                                {
                                                    name = regionInfo.ProvinceName.TrimEnd() + "," +
                                                           regionInfo.CityName.TrimEnd() + "," +
                                                           regionInfo.Name.TrimEnd();
                                                }

                                                //赋值区域信息
                                                var regions = new Regions
                                                {
                                                    RegionId = shippingRegionsInfo.RegionId,
                                                    Name = name,
                                                    PId = regionInfo.ParentId,
                                                    Sub = new List<Regions>(),
                                                    CityId = regionInfo.CityId,
                                                    ProvinceId = regionInfo.ProvinceId
                                                };

                                                //将区域信息添加到区域列表
                                                regionList.Add(regions);

                                                shippingRegionsGroupsInfo.ShippingRegionsList = regionList;
                                            }
                                            else
                                            {
                                                //赋值区域信息
                                                var regions = new Regions
                                                {
                                                    RegionId = 0,
                                                    Name = "所有省,所有市,所有县",
                                                    PId = 0,
                                                    Sub = new List<Regions>(),
                                                    CityId = 0,
                                                    ProvinceId = 0
                                                };
                                                //将区域信息添加到区域列表
                                                regionList.Add(regions);

                                                shippingRegionsGroupsInfo.ShippingRegionsList = regionList;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //赋值区域信息
                                        var regions = new Regions
                                        {
                                            RegionId = 0,
                                            Name = "所有省,所有市,所有县",
                                            PId = 0,
                                            Sub = new List<Regions>(),
                                            CityId = 0,
                                            ProvinceId = 0
                                        };
                                        //将区域信息添加到区域列表
                                        regionList.Add(regions);
                                        shippingRegionsGroupsInfo.ShippingRegionsList = regionList;
                                    }

                                }
                            }
                            #endregion

                            #region 获得该运费模板下的指定包邮地区信息

                            //获得指定包邮地区的模板信息列表
                            adminTemplatesInfo.ShippingAppointRegionsList =
                                _shippingAppointRegions.LoadEntitiesNoTracking(m => m.TemplateId == request.TemplateId).MapToList<LF_ShippingAppointRegions, ShippingAppointRegions>();

                            if (adminTemplatesInfo.ShippingAppointRegionsList.Count != 0)
                            {
                                //循环获得指定模板信息列表的每一条信息（用于获得区域列表）
                                foreach (var shippingAppointRegionsInfo in adminTemplatesInfo.ShippingAppointRegionsList)
                                {
                                    //根据运费模板id以及指定包邮地区id获得区域列表
                                    var shippingRegionsList =
                                        _shippingRegions
                                            .LoadEntitiesNoTracking(
                                                m => m.TemplateId == request.TemplateId)
                                            .OrderBy(m => m.RegionId)
                                            .ToList();


                                    //定义区域列表对象
                                    var regionList = new List<Regions>();

                                    //如果列表不为空则查询区域信息
                                    if (shippingRegionsList.Count != 0)
                                    {
                                        //循环获得指定模板信息的区域列表
                                        foreach (var shippingRegionsInfo in shippingRegionsList)
                                        {
                                            //判断区域id是否存在(如果存在则赋值到新的列表对象)
                                            if (shippingRegionsInfo.RegionId > 0 &&
                                                _regions.Exist(m => m.RegionId == shippingRegionsInfo.RegionId))
                                            {
                                                //查询区域信息
                                                var regionInfo = _regions
                                                    .LoadEntitieNoTracking(
                                                        m => m.RegionId == shippingRegionsInfo.RegionId);

                                                //赋值区域信息
                                                var regions = new Regions
                                                {
                                                    RegionId = shippingRegionsInfo.RegionId,
                                                    Name = regionInfo.Name,
                                                    PId = regionInfo.ParentId,
                                                    Sub = null
                                                };

                                                //将区域信息添加到区域列表
                                                regionList.Add(regions);

                                                shippingAppointRegionsInfo.AppointRegionsList = regionList;
                                            }
                                            else
                                            {
                                                shippingAppointRegionsInfo.AppointRegionsList = regionList;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        shippingAppointRegionsInfo.AppointRegionsList = regionList;
                                    }
                                }
                            }

                            #endregion
                        }

                    }
                    else
                    {
                        return BuildResponse.FailResponse<AdminTemplatesInfoResponse>("无此运费信息！请确定");
                    }

                    var response = new AdminTemplatesInfoResponse
                    {
                        AdminTemplatesInfo = adminTemplatesInfo
                    };


                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminTemplatesInfoResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminTemplatesInfo", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminTemplatesInfoResponse>("获取后台运费模板信息错误");
            }
        }

        /// <summary>
        /// 后台添加运费模板
        /// </summary>
        [Route("AdminAddTemplates")]
        public BaseResponse<object> AdminAddTemplates(AdminAddTemplatesRequest request)
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
                        if (_shippingTemplates.Exist(m => m.TemplateName == request.TemplateName))
                        {
                            return BuildResponse.FailResponse<object>("已存在该模板名称！请重新输入");
                        }

                        var templatesInfo = new LF_ShippingTemplates
                        {
                            TemplateName = request.TemplateName,
                            DefaultNumber = request.DefaultNumber,
                            AddNumber = request.AddNumber,
                            Price = request.Price,
                            AddPrice = request.AddPrice,
                            IsfreeShipping = request.IsfreeShipping,
                            ValuationMethod = request.ValuationMethod,
                            IsAppoint = request.IsAppoint
                        };
                        _shippingTemplates.AddShippingTemplates(templatesInfo);
                        _unitOfWork.Commit();

                        if (request.ValuationMethod > 3)
                        {
                            #region 添加运费模板金额区间

                            //循环添加运费模板计价
                            var shippingPrice = new LF_ShippingPrice
                            {
                                TemplateId = templatesInfo.TemplateId,
                                StartPrice = request.StartPrice,
                                EndPrice = request.EndPrice,
                                Freight = request.Freight,
                                WithFree = request.WithFree,
                                SPrice = request.SPrice,
                                SFreight = request.SFreight,
                                Type = request.ValuationMethod == (int)ValuationMethod.Percentage ? (int)PriceType.Percentage : (int)PriceType.Fixed
                            };
                            _shippingPrice.AddShippingPrice(shippingPrice);

                            #endregion
                        }
                        else
                        {
                            #region 添加运费模板指定配送信息

                            if (request.IsAppoint)
                            {
                                //如果指定配送信息列表不为空
                                if (request.ShippingRegionsGroupsList != null)
                                {
                                    //循环获得指定配送信息
                                    foreach (var shippingRegionsGroupsInfo in request.ShippingRegionsGroupsList)
                                    {
                                        var shippingRegionsGroups = new LF_ShippingRegionsGroups
                                        {
                                            TemplateId = templatesInfo.TemplateId,
                                            DefaultNumber = shippingRegionsGroupsInfo.DefaultNumber,
                                            AddNumber = shippingRegionsGroupsInfo.AddNumber,
                                            Price = shippingRegionsGroupsInfo.Price,
                                            AddPrice = shippingRegionsGroupsInfo.AddPrice
                                        };

                                        //添加指定的配送信息
                                        _shippingRegionsGroups.AddShippingRegionsGroups(shippingRegionsGroups);
                                        _unitOfWork.Commit();

                                        //判断有无选择区域
                                        if (shippingRegionsGroupsInfo.RegionIdList.Count == 0) continue;

                                        //如果有则循环记录区域id
                                        foreach (var regionId in shippingRegionsGroupsInfo.RegionIdList)
                                        {
                                            var region = new LF_ShippingRegions
                                            {
                                                TemplateId = templatesInfo.TemplateId,
                                                GroupId = shippingRegionsGroups.GroupId,
                                                RegionId = regionId,
                                                //AppId = 0
                                            };
                                            _shippingRegions.AddShippingRegions(region);
                                        }
                                    }
                                }

                                //如果指定包邮地区列表不为空
                                if (request.ShippingAppointRegionsList != null)
                                {
                                    //循环获得指定包邮地区信息
                                    foreach (var shippingAppointRegionsInfo in request.ShippingAppointRegionsList)
                                    {
                                        var shippingAppointRegions = new LF_ShippingAppointRegions()
                                        {
                                            TemplateId = templatesInfo.TemplateId,
                                        };

                                        //添加指定包邮地区信息
                                        _shippingAppointRegions.AddShippingAppointRegions(shippingAppointRegions);
                                        _unitOfWork.Commit();

                                        //判断有无选择区域
                                        if (shippingAppointRegionsInfo.AppointRegionsList.Count == 0) continue;

                                        //如果有则循环记录区域id
                                        foreach (var regionId in shippingAppointRegionsInfo.AppointRegionsList)
                                        {
                                            var region = new LF_ShippingRegions
                                            {
                                                TemplateId = templatesInfo.TemplateId,
                                                GroupId = 0,
                                                RegionId = regionId,
                                                //AppId = shippingAppointRegions.AppId
                                            };
                                            _shippingRegions.AddShippingRegions(region);
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        //提交事务
                        var result = _unitOfWork.Commit();
                        if (result > 0)
                        {
                            scope.Complete();
                            return BuildResponse.SuccessResponse<object>("添加运费模板成功");
                        }
                        return BuildResponse.FailResponse<object>("添加运费模板错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminAddTemplates", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台添加运费模板错误");
            }
        }

        /// <summary>
        /// 后台编辑运费模板
        /// </summary>
        [Route("AdminEditTemplates")]
        public BaseResponse<object> AdminEditTemplates(AdminEditTemplatesRequest request)
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
                        if (_shippingTemplates.Exist(m => m.TemplateName == request.TemplateName && m.TemplateId != request.TemplateId))
                        {
                            return BuildResponse.FailResponse<object>("已存在该模板名称！请重新输入");
                        }

                        var templatesInfo = _shippingTemplates.GetShippingTemplatesById(request.TemplateId);

                        templatesInfo.TemplateId = request.TemplateId;
                        templatesInfo.TemplateName = request.TemplateName;
                        templatesInfo.DefaultNumber = request.DefaultNumber;
                        templatesInfo.AddNumber = request.AddNumber;
                        templatesInfo.Price = request.Price;
                        templatesInfo.AddPrice = request.AddPrice;
                        templatesInfo.IsfreeShipping = request.IsfreeShipping;
                        templatesInfo.ValuationMethod = request.ValuationMethod;
                        templatesInfo.IsAppoint = request.IsAppoint;

                        _shippingTemplates.UpdateShippingTemplates(templatesInfo);
                        _unitOfWork.Commit();

                        if (request.ValuationMethod > 3)
                        {
                            #region 编辑运费模板金额区间
                            var shippingPriceList = _shippingPrice.LoadEntitiesNoTracking(m => m.TemplateId == request.TemplateId)
                                .ToList();
                            if (shippingPriceList.Count > 0)
                            {
                                _shippingPrice.BatchDelete(m => m.TemplateId == request.TemplateId);
                            }

                            //循环添加运费模板计价
                            var shippingPrice = new LF_ShippingPrice
                            {
                                TemplateId = templatesInfo.TemplateId,
                                StartPrice = request.StartPrice,
                                EndPrice = request.EndPrice,
                                Freight = request.Freight,
                                WithFree = request.WithFree,
                                SPrice = request.SPrice,
                                SFreight = request.SFreight,
                                Type = request.ValuationMethod == (int)ValuationMethod.Percentage ? (int)PriceType.Percentage : (int)PriceType.Fixed
                            };
                            _shippingPrice.AddShippingPrice(shippingPrice);
                            #endregion
                        }
                        else
                        {
                            #region 编辑运费模板指定配送信息
                            if (request.IsAppoint)
                            {
                                var shippingPriceList = _shippingPrice.LoadEntitiesNoTracking(m => m.TemplateId == request.TemplateId)
                                    .ToList();
                                if (shippingPriceList.Count > 0)
                                {
                                    _shippingPrice.BatchDelete(m => m.TemplateId == request.TemplateId);
                                }
                                var groupIdList = _shippingRegionsGroups.LoadEntitiesNoTracking(m => m.TemplateId == request.TemplateId)
                                    .Select(m => m.GroupId).ToList();

                                //批量删除运费模板下所有的指定配送地区及区域信息
                                _shippingRegions.BatchDelete(m => m.TemplateId == request.TemplateId &&
                                                                  groupIdList.Contains(m.GroupId));

                                _shippingRegionsGroups.BatchDelete(m => m.TemplateId == request.TemplateId);

                                _shippingRegions.BatchDelete(m => m.TemplateId == request.TemplateId);

                                _shippingAppointRegions.BatchDelete(m => m.TemplateId == request.TemplateId);

                                if (request.ShippingRegionsGroupsList != null)
                                {
                                    //循环获得指定配送信息
                                    foreach (var shippingRegionsGroupsInfo in request.ShippingRegionsGroupsList)
                                    {
                                        var shippingRegionsGroups = new LF_ShippingRegionsGroups
                                        {
                                            TemplateId = templatesInfo.TemplateId,
                                            DefaultNumber = shippingRegionsGroupsInfo.DefaultNumber,
                                            AddNumber = shippingRegionsGroupsInfo.AddNumber,
                                            Price = shippingRegionsGroupsInfo.Price,
                                            AddPrice = shippingRegionsGroupsInfo.AddPrice
                                        };

                                        //添加指定的配送信息
                                        _shippingRegionsGroups.AddShippingRegionsGroups(shippingRegionsGroups);
                                        _unitOfWork.Commit();

                                        //判断有无选择区域
                                        if (shippingRegionsGroupsInfo.RegionIdList.Count == 0) continue;

                                        //如果有则循环记录区域id
                                        foreach (var regionId in shippingRegionsGroupsInfo.RegionIdList)
                                        {
                                            var region = new LF_ShippingRegions
                                            {
                                                TemplateId = templatesInfo.TemplateId,
                                                GroupId = shippingRegionsGroups.GroupId,
                                                RegionId = regionId
                                            };
                                            _shippingRegions.AddShippingRegions(region);
                                        }
                                    }

                                    //如果指定包邮地区列表不为空
                                    if (request.ShippingAppointRegionsList != null)
                                    {
                                        //循环获得指定包邮地区信息
                                        foreach (var shippingAppointRegionsInfo in request.ShippingAppointRegionsList)
                                        {
                                            var shippingAppointRegions = new LF_ShippingAppointRegions()
                                            {
                                                TemplateId = templatesInfo.TemplateId,
                                            };

                                            //添加指定包邮地区信息
                                            _shippingAppointRegions.AddShippingAppointRegions(shippingAppointRegions);
                                            _unitOfWork.Commit();

                                            //判断有无选择区域
                                            if (shippingAppointRegionsInfo.AppointRegionsList.Count == 0) continue;

                                            //如果有则循环记录区域id
                                            foreach (var regionId in shippingAppointRegionsInfo.AppointRegionsList)
                                            {
                                                var region = new LF_ShippingRegions
                                                {
                                                    TemplateId = templatesInfo.TemplateId,
                                                    GroupId = 0,
                                                    RegionId = regionId,
                                                    //AppId = shippingAppointRegions.AppId
                                                };
                                                _shippingRegions.AddShippingRegions(region);
                                            }
                                        }
                                    }
                                }

                            }
                            #endregion
                        }

                        //提交事务
                        var result = _unitOfWork.Commit();
                        if (result > 0)
                        {
                            scope.Complete();
                            return BuildResponse.SuccessResponse<object>("编辑运费模板成功");
                        }
                        return BuildResponse.FailResponse<object>("编辑运费模板错误");
                    }
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditTemplates", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑运费模板错误");
            }
        }

        /// <summary>
        /// 后台删除运费模板
        /// </summary>
        [Route("AdminDelTemplates")]
        public BaseResponse<object> AdminDelTemplates(AdminDelTemplatesRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    foreach (var templatesId in request.TemplatesIdList)
                    {
                        var template = _shippingTemplates.LoadEntitieNoTracking(m => m.TemplateId == templatesId);

                        var name = template == null ? "" : template.TemplateName;

                        if (_products.Exist(m => m.TemplateId == templatesId) && template != null)
                        {
                            return BuildResponse.FailResponse<object>("已有商品选择该运费模板，请先从商品移除 运费模板名称为：" + name);
                        }
                    }

                    //var groupIdList = _shippingRegionsGroups.LoadEntitiesNoTracking(m => request.TemplatesIdList.Contains(m.TemplateId))
                    //    .Select(m => m.GroupId).ToList();

                    //if (groupIdList.Count != 0)
                    //{
                    //    _shippingRegions.BatchDelete(m => request.TemplatesIdList.Contains(m.TemplateId) &&
                    //                                      groupIdList.Contains(m.GroupId));
                    //}

                    _shippingPrice.BatchDelete(m => request.TemplatesIdList.Contains(m.TemplateId));

                    //_shippingRegionsGroups.BatchDelete(m => request.TemplatesIdList.Contains(m.TemplateId));

                    //_shippingAppointRegions.BatchDelete(m => request.TemplatesIdList.Contains(m.TemplateId));

                    _shippingTemplates.BatchDelete(m => request.TemplatesIdList.Contains(m.TemplateId));

                    return BuildResponse.SuccessResponse<object>("后台删除运费模板成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminDelTemplates", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台删除运费模板错误");
            }
        }

        #region 区域列表相关接口

        /// <summary>
        /// 后台获取筛选层级区域列表
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetRegionDataList")]
        public BaseResponse<RegionDataListResponse> AdminGetRegionDataList(RegionIdListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var response = new RegionDataListResponse
                    {
                        RegionList = _regions.GetRegionDataListByIdList(request.RegionIdList),
                        RIdList = request.RegionIdList
                    };
                    //返回结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<RegionDataListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetRegionDataList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<RegionDataListResponse>("后台获取筛选层级区域列表出错");
            }
        }

        /// <summary>
        /// 后台获取筛选外层级区域列表
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetNotRegionDataList")]
        public BaseResponse<RegionDataListResponse> AdminGetNotRegionDataList(RegionIdListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var response = new RegionDataListResponse
                    {
                        RegionList = _regions.GetNotRegionDataListByIdList(request.RegionIdList)
                    };
                    //返回结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<RegionDataListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetNotRegionDataList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<RegionDataListResponse>("后台获取筛选外层级区域列表出错");
            }
        }

        /// <summary>
        /// 后台获取展示层级区域列表
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetShowRegionDataList")]
        public BaseResponse<RegionDataListResponse> AdminGetShowRegionDataList(RegionIdListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var response = new RegionDataListResponse
                    {
                        RegionList = _regions.GetShowRegionDataListByIdList(request.RegionIdList),
                        RIdList = request.RegionIdList
                    };
                    //返回结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<RegionDataListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetShowRegionDataList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<RegionDataListResponse>("后台获取展示层级区域列表出错");
            }
        }

        #endregion

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

