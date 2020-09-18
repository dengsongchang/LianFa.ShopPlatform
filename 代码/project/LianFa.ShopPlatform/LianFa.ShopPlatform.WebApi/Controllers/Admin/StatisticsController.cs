using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using HuCheng.Util.Core.Offices;
using HuCheng.Util.Core.TypeConvert;
using HuCheng.Util.Office.Npoi.Excel;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Admin.Statistics;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.Statistics;
using LianFa.ShopPlatform.Service;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台数据统计控制器
    /// </summary>
    [ControllerGroup("后台数据统计相关接口", "用于后台数据统计操作")]
    [RoutePrefix("api/admin/statistics")]
    public class StatisticsController : ApiController
    {
        /// <summary>
        /// 商品服务
        /// </summary>
        private readonly IProductsService _product;

        /// <summary>
        /// 订单服务
        /// </summary>
        private readonly IOrdersService _order;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUsersService _users;

        /// <summary>
        /// 卡片服务
        /// </summary>
        private readonly ICouponsService _coupons;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 后台会员管理控制器构造函数(构造注入)
        /// </summary>
        /// <param name="order"></param>
        /// <param name="product"></param>
        /// <param name="users"></param>
        /// <param name="coupons"></param>
        public StatisticsController(IOrdersService order, IProductsService product, IUsersService users, ICouponsService coupons)
        {
            _order = order;
            _product = product;
            _users = users;
            _coupons = coupons;
        }

        #region 获取会员增长情况列表

        /// <summary>
        /// 获取会员增长情况列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AdminGetAddUserAnalyzeList")]
        public BaseResponse<AdminGetAddUserAnalyzeListResponse> AdminGetAddUserAnalyzeList(AdminGetAddUserAnalyzeListRequest request)
        {
                //将请求参数转为Json字符串
                var requestString = request.ToJson();
                try
                {
                    //验证数据格式是否正确
                    if (ModelState.IsValid)
                    {
                        List<TimeAndUserCount> list = new List<TimeAndUserCount>();
                        //判断是按年算还是按月算
                        if (request.Month == 0)
                        {
                            //按年算
                            //判断是闰年还是平年
                            int yearCount = JudgeYear(request.Year);
                            //如果算今年只算到今天
                            int dayOfYear = DateTime.Now.DayOfYear;
                            //从一月一号开始
                            DateTime startDay = Conv.ToDate(request.Year + "-01-01");
                            DateTime endDay = Conv.ToDate(request.Year + "-12-31");
                            if (request.Year == DateTime.Now.Year)
                            {
                                yearCount = dayOfYear;
                                endDay = DateTime.Now.Date;
                            }
                            int i = 0;
                            var timeAndUserCountList = _users.GetUserCountAndTime(startDay, endDay);
                            while (yearCount > 0)
                            {
                                TimeAndUserCount model = new TimeAndUserCount();

                                if (timeAndUserCountList.Any())
                                {
                                    if (timeAndUserCountList[i].Time.ToDate() == startDay)
                                    {
                                        model.Time = timeAndUserCountList[i].Time;
                                        model.UserCount = timeAndUserCountList[i].UserCount;
                                        if (timeAndUserCountList.Count - 1 > i)
                                        {
                                            i++;
                                        }

                                    }
                                    else
                                    {
                                        model.Time = startDay.ToDateString();
                                        model.UserCount = 0;
                                    }

                                }
                                else
                                {
                                    model.Time = startDay.ToDateString();
                                    model.UserCount = 0;
                                }
                                list.Add(model);
                                startDay = startDay.AddDays(1);
                                yearCount--;
                            }
                        }
                        else
                        {
                            //按月算
                            int dayCount = DateTime.DaysInMonth(request.Year, request.Month);
                            //从本月一号开始
                            DateTime startDay = Conv.ToDate(request.Year + "-" + request.Month + "-01");
                            DateTime endDay = DateTime.Now.Date;
                            if (request.Year == DateTime.Now.Year && request.Month == DateTime.Now.Month)
                            {
                                dayCount = DateTime.Now.Day;
                            }
                            var timeAndUserCountList = _users.GetUserCountAndTime(startDay, endDay);
                            int i = 0;
                            while (dayCount > 0)
                            {
                                TimeAndUserCount model = new TimeAndUserCount();

                                if (timeAndUserCountList.Any())
                                {
                                    if (timeAndUserCountList[i].Time.ToDate() == startDay)
                                    {
                                        model.Time = timeAndUserCountList[i].Time;
                                        model.UserCount = timeAndUserCountList[i].UserCount;
                                        if (timeAndUserCountList.Count - 1 > i)
                                        {
                                            i++;
                                        }

                                    }
                                    else
                                    {
                                        model.Time = startDay.ToDateString();
                                        model.UserCount = 0;
                                    }

                                }
                                else
                                {
                                    model.Time = startDay.ToDateString();
                                    model.UserCount = 0;
                                }
                                list.Add(model);
                                startDay = startDay.AddDays(1);
                                dayCount--;
                            }
                        }

                        AdminGetAddUserAnalyzeListResponse response = new AdminGetAddUserAnalyzeListResponse()
                        {
                            List = list
                        };
                        //返回成功结果
                        return BuildResponse.SuccessResponse(response);
                    }

                    //获取验证错误信息
                    var errorMessage = GetModelErrorMsg();

                    //返回失败结果
                    return BuildResponse.FailResponse<AdminGetAddUserAnalyzeListResponse>(errorMessage, ResponseCode.DataError);
                }

                catch (Exception ex)
                {
                    ApiLogger.Error("AdminGetAddUserAnalyzeList", requestString, ex.Message, ex);
                    return BuildResponse.FailResponse<AdminGetAddUserAnalyzeListResponse>("获取会员增长情况列表错误");
                }
            }

        #endregion

        #region 后台导出会员增长情况列表

        /// <summary>
        /// 后台导出会员增长情况列表
        /// </summary>
        [Route("ExportAdminUserAnalyzeList")]
        public BaseResponse<string> ExportAdminUserAnalyzeList(AdminGetAddUserAnalyzeListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();

            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {

                    List<TimeAndUserCount> list = new List<TimeAndUserCount>();
                    //判断是按年算还是按月算
                    if (request.Month == 0)
                    {
                        //按年算
                        //判断是闰年还是平年
                        int yearCount = JudgeYear(request.Year);
                        //如果算今年只算到今天
                        int dayOfYear = DateTime.Now.DayOfYear;
                        //从一月一号开始
                        DateTime startDay = Conv.ToDate(request.Year + "-01-01");
                        DateTime endDay = Conv.ToDate(request.Year + "-12-31");
                        if (request.Year == DateTime.Now.Year)
                        {
                            yearCount = dayOfYear;
                            endDay = DateTime.Now.Date;
                        }
                        int i = 0;
                        var timeAndUserCountList = _users.GetUserCountAndTime(startDay, endDay);
                        while (yearCount > 0)
                        {
                            TimeAndUserCount model = new TimeAndUserCount();

                            if (timeAndUserCountList.Any())
                            {
                                if (timeAndUserCountList[i].Time.ToDate() == startDay)
                                {
                                    model.Time = timeAndUserCountList[i].Time;
                                    model.UserCount = timeAndUserCountList[i].UserCount;
                                    if (timeAndUserCountList.Count - 1 > i)
                                    {
                                        i++;
                                    }

                                }
                                else
                                {
                                    model.Time = startDay.ToDateString();
                                    model.UserCount = 0;
                                }

                            }
                            else
                            {
                                model.Time = startDay.ToDateString();
                                model.UserCount = 0;
                            }
                            list.Add(model);
                            startDay = startDay.AddDays(1);
                            yearCount--;
                        }
                    }
                    else
                    {
                        //按月算
                        int dayCount = DateTime.DaysInMonth(request.Year, request.Month);
                        //从本月一号开始
                        DateTime startDay = Conv.ToDate(request.Year + "-" + request.Month + "-01");
                        DateTime endDay = DateTime.Now.Date;
                        if (request.Year == DateTime.Now.Year && request.Month == DateTime.Now.Month)
                        {
                            dayCount = DateTime.Now.Day;
                        }
                        var timeAndUserCountList = _users.GetUserCountAndTime(startDay, endDay);
                        int i = 0;
                        while (dayCount > 0)
                        {
                            TimeAndUserCount model = new TimeAndUserCount();

                            if (timeAndUserCountList.Any())
                            {
                                if (timeAndUserCountList[i].Time.ToDate() == startDay)
                                {
                                    model.Time = timeAndUserCountList[i].Time;
                                    model.UserCount = timeAndUserCountList[i].UserCount;
                                    if (timeAndUserCountList.Count - 1 > i)
                                    {
                                        i++;
                                    }

                                }
                                else
                                {
                                    model.Time = startDay.ToDateString();
                                    model.UserCount = 0;
                                }

                            }
                            else
                            {
                                model.Time = startDay.ToDateString();
                                model.UserCount = 0;
                            }
                            list.Add(model);
                            startDay = startDay.AddDays(1);
                            dayCount--;
                        }
                    }

                    //Excel头部
                    var headList = new List<string>
                    {
                        "时间",
                        "会员数"
                    };

                    var saveTempPath = "/upload/excel/temp/";
                    //获取导出Excel的地址
                    var excelUrl = ExcelExport.ExportExcel(headList, list, saveTempPath);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(FileHelper.GetFileFullUrl(excelUrl));
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("ExportAdminUserAnalyzeList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<string>("后台导出会员增长情况列表错误");
            }
        }

        #endregion

        #region 获取首页用户注册性别占比

        /// <summary>
        /// 获取首页用户注册性别占比
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexUserRegisterGender")]
        public BaseResponse<QueryDataResponse> AdminGetIndexUserRegisterGender(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);

                    var queryDataResponse = new QueryDataResponse
                    {
                        List = _users.GetOrderCountGroupByDate(startTime, endTime)
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(queryDataResponse);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexUserRegisterGender", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取首页用户注册性别占比错误");
            }
        }

        #endregion

        #region 获取会员累计消费金额分布信息列表

        /// <summary>
        /// 获取会员累计消费金额分布信息列表
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetUserAmountList")]
        public BaseResponse<AdminGetUserAmountListResponse> AdminGetUserAmountList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    List<UserAmount> list = new List<UserAmount>();
                    string[] array = { "0-50元", "51-100元", "101-200元", "201-500元", "501-1000元", "1001-5000元", "5001-1000元", "10001元以上" };



                    var numList = _users.GetUserAmountCount();
                    for (int i = 0; i < numList.Count; i++)
                    {
                        UserAmount model = new UserAmount();
                        model.Name = array[i];
                        model.UserCount = numList[i];
                        list.Add(model);
                    }

                    AdminGetUserAmountListResponse response = new AdminGetUserAmountListResponse()
                    {
                        List = list
                    };
                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetUserAmountListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetUserAmountList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetUserAmountListResponse>("获取会员累计消费金额分布信息列表错误");
            }
        }

        #endregion

        #region 获取首页数据详情

        /// <summary>
        /// 获取首页数据详情
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexDataInfo")]
        public BaseResponse<AdminGetIndexDataInfoResponse> AdminGetIndexDataInfo()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //今日数据
                    var todayEndTime = DateTime.Now;
                    var todayStartTime = new DateTime(todayEndTime.Year, todayEndTime.Month, todayEndTime.Day, 0, 0, 0);
                    var todayOrderAmount = _order.GetOrderAmountByTime(todayStartTime, todayEndTime);
                    var todayOrderCount = _order.GetOrderCountByTime(todayStartTime, todayEndTime);
                    var todayUsers = _users.GetUserCountByTime(todayStartTime, todayEndTime);
                    var todaycouponsUsed = _coupons.GetTotalCouponsUsedByTime(todayStartTime, todayEndTime);


                    //总数据
                    var totalOrderAmount = _order.GetOrderAmountTotal();
                    var totalUsers = _users.GetTotalUsers();
                    var totalProducts = _product.GetProductCountTotal();
                    var totalCoupons = _coupons.GetTotalCoupons();
                    var totalCouponsIsUsed = _coupons.GetTotalCouponsIsUsed();

                    //今天较昨天数据
                    var beforeDate = DateTime.Now.AddDays(-1);
                    var beforeEndTime = new DateTime(beforeDate.Year, beforeDate.Month, beforeDate.Day, 23, 59, 59);
                    var beforeStartTime = new DateTime(beforeDate.Year, beforeDate.Month, beforeDate.Day, 0, 0, 0);
                    var addOrderAmount = todayOrderAmount - _order.GetOrderAmountByTime(beforeStartTime, beforeEndTime);
                    var addOrderCount = todayOrderCount - _order.GetOrderCountByTime(beforeStartTime, beforeEndTime);
                    var addUsers = todayUsers - _users.GetUserCountByTime(beforeStartTime, beforeEndTime);

                    //返回成功结果
                    return BuildResponse.SuccessResponse(new AdminGetIndexDataInfoResponse
                    {
                        AddCouponsUsed = todaycouponsUsed,
                        TotalCoupons = totalCoupons,
                        TotalCouponsIsUsed = totalCouponsIsUsed,
                        AddOrderAmount = addOrderAmount,
                        AddOrderCount = addOrderCount,
                        AddUsers = addUsers,
                        TodayOrderAmount = todayOrderAmount,
                        TodayOrderCount = todayOrderCount,
                        TodayUsers = todayUsers,
                        TotalOrderAmount = totalOrderAmount,
                        TotalProducts = totalProducts,
                        TotalUsers = totalUsers
                    });
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetIndexDataInfoResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexDataInfo", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetIndexDataInfoResponse>("获取首页数据详情错误");
            }
        }

        #endregion

        #region 获取首页订单金额趋势数据

        /// <summary>
        /// 获取首页订单金额趋势数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexOrderAmountTrend")]
        public BaseResponse<QueryDataResponse> AdminGetIndexOrderAmountTrend(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);
                    var queryDataResponse = new QueryDataResponse
                    {
                        List = new List<Data>
                        {
                            new Data
                            {
                                DataList = _order.GetOrderAmountGroupByDate(startTime, endTime),
                                DataName = "订单金额趋势"
                            }.FullDataValue(startTime,endTime)
                        }
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(queryDataResponse);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexOrderAmountTrend", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取首页订单金额趋势数据错误");
            }
        }

        #endregion

        #region 获取首页订单数量趋势数据

        /// <summary>
        /// 获取首页订单数量趋势数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexOrderCountTrend")]
        public BaseResponse<QueryDataResponse> AdminGetIndexOrderCountTrend(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);
                    var queryDataResponse = new QueryDataResponse
                    {
                        List = new List<Data>
                        {
                            new Data
                            {
                                DataList = _order.GetOrderCountGroupByDate(startTime, endTime),
                                DataName = "客户下单趋势"
                            }.FullDataValue(startTime,endTime)
                        }
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(queryDataResponse);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexOrderCountTrend", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取首页订单数量趋势数据错误");
            }
        }

        #endregion

        #region 获取首页产品类别占比数据

        /// <summary>
        /// 获取首页产品类别占比数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexProductCategoryCount")]
        public BaseResponse<QueryDataResponse> AdminGetIndexProductCategoryCount(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);
                    var queryDataResponse = new QueryDataResponse
                    {
                        List = _product.AdminGetProductCategoryCount(startTime, endTime)
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(queryDataResponse);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexProductCategoryCount", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取首页产品类别占比数据错误");
            }
        }

        #endregion

        #region 获取普通商品分析列表

        /// <summary>
        /// 获取普通商品分析列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AdminGetVideoAnalysisList")]
        public BaseResponse<AdminGetProductAnalysisListResponse> AdminGetProductAnalysisList(AdminGetProductAnalysisListRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    var productAnalysisList = _order.GetAdminGetProductAnalysisList(request.StartTime, request.EndTime, request.Name, request.Page, out total);
                    var response = new AdminGetProductAnalysisListResponse
                    {
                        AdminProductAnalysisList = productAnalysisList,
                        Total = total
                    };
                    //返回数据
                    return BuildResponse.SuccessResponse(response);
                }
                //获取验证出错信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetProductAnalysisListResponse>(errorMessage, ResponseCode.DataError);
            }
            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetProductAnalysisList", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetProductAnalysisListResponse>("获取商品分析列表");
            }
        }

        #endregion

        #region 导出普通商品分析列表

        /// <summary>
        /// 导出普通商品分析列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportAdminGetProductAnalysisList")]
        public BaseResponse<string> ExportAdminGetProductAnalysisList(AdminGetProductAnalysisListRequest request)
        {
            var requestString = request.ToJson();
            try
            {
                if (ModelState.IsValid)
                {
                    int total;
                    var productAnalysisList = _order.GetAdminGetProductAnalysisList(request.StartTime, request.EndTime,
                        request.Name, request.Page, out total);
                    var saveTempPath = "/upload/excel/temp/";
                    var url = ExcelExport.ExportCustomCellHeadExcel(new List<CellHead>
                    {
                        new CellHead
                        {
                            ColumnWidth = 5,
                            Head = "商品编号"
                        },
                        new CellHead
                        {
                            ColumnWidth = 30,
                            Head = "订单编号"
                        },
                        new CellHead
                        {
                            ColumnWidth = 30,
                            Head = "商品名称"
                        },
                        new CellHead
                        {
                            ColumnWidth = 18,
                            Head = "销售量"
                        },
                        new CellHead
                        {
                            ColumnWidth = 30,
                            Head = "销售金额"
                        },
                        new CellHead
                        {
                            ColumnWidth = 10,
                            Head = "付款人数"
                        },
                        new CellHead()
                        {
                            ColumnWidth = 10,
                            Head = "创建时间"
                        }
                    }, productAnalysisList, saveTempPath);
                    return BuildResponse.SuccessResponse(FileHelper.GetFileFullUrl(url));
                }
                //获取验证出错信息
                var errorMessage = GetModelErrorMsg();
                //返回失败结果
                return BuildResponse.FailResponse<string>(errorMessage, ResponseCode.DataError);
                }
                catch (Exception ex)
                {
                    ApiLogger.Error("AdminGetProductAnalysisList", requestString, ex.Message, ex);
                    return BuildResponse.FailResponse<string>("获取商品分析列表");
                }
            }

        #endregion

        #region 获取首页产品上架趋势数据

        /// <summary>
        /// 获取首页产品上架趋势数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexProductCountTrend")]
        public BaseResponse<QueryDataResponse> AdminGetIndexProductCountTrend(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);
                    var queryDataResponse = new QueryDataResponse
                    {
                        List = new List<Data>
                                    {
                                        new Data
                                        {
                                            DataList = _product.GetProductCountGroupByDate(startTime, endTime),
                                            DataName = "产品上架日趋势"
                                        }.FullDataValue(startTime,endTime)
                                    }
                    };

                //返回成功结果
                return BuildResponse.SuccessResponse(queryDataResponse);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexProductCountTrend", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取首页产品上架趋势数据错误");
            }
        }

        #endregion

        #region 获取首页产品购买排名数据

        /// <summary>
        /// 获取首页产品购买排名数据
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetIndexProductBuyCount")]
        public BaseResponse<QueryDataResponse> AdminGetIndexProductBuyCount(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);

                    var queryDataResponse = new QueryDataResponse
                    {
                        List = _order.AdminGetProductByCount(startTime, endTime)
                    };

                    //返回成功结果
                    return BuildResponse.SuccessResponse(queryDataResponse);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetIndexProductBuyCount", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取首页产品购买排名数据错误");
            }
        }

        #endregion

        #region 获取订单交易分析

        /// <summary>
        /// 获取订单交易分析
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetOrderTransactionAnalysis")]
        public BaseResponse<AdminGetOrderTransactionAnalysisResponse> AdminGetOrderTransactionAnalysis(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);
                    //订单数据
                    var orderData = _order.GetOrderTransactionAnalysisDetail(startTime, endTime, 0, false);
                    //响应
                    var response =
                        new AdminGetOrderTransactionAnalysisResponse
                        {
                            OrderAmount = orderData.Sum(x => x.SurplusMoney),
                            OrderCount = orderData.Count,
                            OrderUserCount = orderData.GroupBy(x => x.UId).Count(),
                            PayOrderCount = orderData.Count(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled),
                            ProductCount = orderData.Sum(x => x.ProductCount),
                            PayProductCount = orderData.Where(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled)
                                .Sum(x => x.ProductCount),
                            PayAmount = orderData.Where(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled)
                                .Sum(x => x.SurplusMoney),
                            PayUserCount = orderData.Where(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled)
                                .GroupBy(x => x.UId)
                                .Count(),
                        };
                    response.AvgAmount = response.PayUserCount == 0 ? 0 : response.PayAmount / response.PayUserCount;
                    response.AvgAmount = Math.Round(response.AvgAmount, 2);

                    response.OrderConversionRate = Math.Round(response.OrderConversionRate, 2);

                    response.PayConversionRate = response.OrderUserCount == 0 ? 0 : (decimal)response.PayUserCount / response.OrderUserCount * 100;
                    response.PayConversionRate = Math.Round(response.PayConversionRate, 2);

                    response.SuccConversionRate = Math.Round(response.SuccConversionRate, 2);


                    #region 表格计算

                    response.OrderDataTable = new QueryDataResponse
                    {
                        List = new List<Data>()
                        {
                            new Data()
                            {
                                DataName="付款金额",
                                DataList=new List<DataDetail>()
                            },
                            new Data()
                            {
                                DataName="退款金额",
                                DataList=new List<DataDetail>()
                            },
                            new Data()
                            {
                                DataName="付款人数",
                                DataList=new List<DataDetail>()
                            },
                            new Data()
                            {
                                DataName="付款件数",
                                DataList=new List<DataDetail>()
                            },
                            new Data()
                            {
                                DataName="下单转化率",
                                DataList=new List<DataDetail>()
                            },
                            new Data()
                            {
                                DataName="付款转化率",
                                DataList=new List<DataDetail>()
                            },
                            new Data()
                            {
                                DataName="成交转化率",
                                DataList=new List<DataDetail>()
                            },

                        }
                    };

                    for (var i = startTime.Date; i <= endTime.Date; i = i.AddDays(1))
                    {
                        //当天付款人数
                        var payUserCount = orderData.Where(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled &&
                                                                x.AddTime.Date == i)
                            .GroupBy(x => x.UId)
                            .Count();

                        //当天下单人数
                        var orderUserCount = orderData.Where(x =>
                                x.AddTime.Date == i).GroupBy(x => x.UId)
                            .Count();

                        response.OrderDataTable.List.Find(x => x.DataName == "付款金额")
                            .DataList.Add(new DataDetail()
                            {
                                Date = i.ToString("yyyy-MM-dd"),
                                Value = orderData.Where(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled &&
                                                             x.AddTime.Date == i)
                                    .Sum(x => x.SurplusMoney)
                            });

                        response.OrderDataTable.List.Find(x => x.DataName == "付款人数")
                            .DataList.Add(new DataDetail()
                            {
                                Date = i.ToString("yyyy-MM-dd"),
                                Value = payUserCount
                            });

                        response.OrderDataTable.List.Find(x => x.DataName == "付款件数")
                            .DataList.Add(new DataDetail()
                            {
                                Date = i.ToString("yyyy-MM-dd"),
                                Value = orderData.Where(x => x.OrderState > (int)OrderState.WaitPaying && x.OrderState != (byte)OrderState.Cancelled &&
                                                             x.AddTime.Date == i)
                                    .Sum(x => x.ProductCount)
                            });

                        response.OrderDataTable.List.Find(x => x.DataName == "付款转化率")
                            .DataList.Add(new DataDetail()
                            {
                                Date = i.ToString("yyyy-MM-dd"),
                                Value = orderUserCount == 0 ? 0 : (decimal)payUserCount / orderUserCount * 100
                            });
                    }

                    #endregion

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminGetOrderTransactionAnalysisResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetOrderTransactionAnalysis", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<AdminGetOrderTransactionAnalysisResponse>("获取订单交易分析错误");
            }
        }

        #endregion

        #region 获取订单金额分析

        /// <summary>
        /// 获取订单金额分析
        /// </summary>
        /// <returns></returns>
        [Route("AdminGetOrderAmountAnalysis")]
        public BaseResponse<QueryDataResponse> AdminGetOrderAmountAnalysis(QueryDataRequest request)
        {
            //将请求参数转为Json字符串
            var requestString = request.ToJson();
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    //时间范围
                    var startTime = DateTimeHelper.GetDateTimeByTimestamp(request.StartTime);
                    var endTime = DateTimeHelper.GetDateTimeByTimestamp(request.EndTime);
                    //订单数据
                    var orderData = _order.GetOrderTransactionAnalysisDetail(startTime, endTime, 0, true);

                    #region 表格计算

                    var response = new QueryDataResponse
                    {
                        List = new List<Data>()
                        {
                            new Data()
                            {
                                DataName="0-50元",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=0&&x.SurplusMoney<=50)
                            },
                            new Data()
                            {
                                DataName="51-100",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=51&&x.SurplusMoney<=100)
                            },
                            new Data()
                            {
                                DataName="101-200",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=101&&x.SurplusMoney<=200)
                            },
                            new Data()
                            {
                                DataName="201-500",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=201&&x.SurplusMoney<=500)
                            },
                            new Data()
                            {
                                DataName="501-1000",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=501&&x.SurplusMoney<=1000)
                            },
                            new Data()
                            {
                                DataName="1001-5000",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=1001&&x.SurplusMoney<=5000)
                            },
                            new Data()
                            {
                                DataName="5001-10000",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=5001&&x.SurplusMoney<=10000)
                            },
                            new Data()
                            {
                                DataName="10001以上",
                                DataValue=orderData.Count(x=>x.SurplusMoney>=10001)
                            },
                        }
                    };

                    #endregion

                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<QueryDataResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminGetOrderAmountAnalysis", requestString, ex.Message, ex);
                return BuildResponse.FailResponse<QueryDataResponse>("获取订单金额分析错误");
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

        /// <summary>
        /// 判断是闰年还是平年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [NonAction]
        private int JudgeYear(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            {
                //闰年
                return 366;
            }
            else
            {
                return 365;
            }
        }
    }
}
