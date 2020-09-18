using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HuCheng.Util.Core.Config;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Config;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Request.Admin.OrderSetting;
using LianFa.ShopPlatform.Model.Response;
using LianFa.ShopPlatform.Model.Response.Admin.OrderSetting;
using LianFa.ShopPlatform.WebApi.Core;
using LianFa.ShopPlatform.WebApi.Filters;

namespace LianFa.ShopPlatform.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台订单设置控制器
    /// </summary>
    [ControllerGroup("后台订单设置相关接口", "用于后台订单设置操作")]
    [RoutePrefix("api/admin/orderSetting")]
    public class OrderSettingController : ApiController
    {
        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 配置文件管理器
        /// </summary>
        public IConfigManager ConfigManager = Bootstrapper.GetFromFac<IConfigManager>();

        #region 后台获取订单设置列表

        /// <summary>
        /// 后台获取订单设置列表
        /// </summary>
        [Route("AdminOrderSettingList")]
        public BaseResponse<AdminOrderSettingListResponse> AdminOrderSettingList()
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    var response = new AdminOrderSettingListResponse();

                    List<OrderSettingInfo> list = new List<OrderSettingInfo>();
                    OrderSettingInfo setting = new OrderSettingInfo()
                    {
                        AutoGoodComments = ConfigMap.AutoGoodComments,
                        Autorefund = ConfigMap.Autorefund,
                        AutoSendTip = ConfigMap.AutoSendTip,
                        CompleteOrder = ConfigMap.CompleteOrder,
                        GroupBuylimitTime = ConfigMap.GroupBuylimitTime,
                        OrderLimitTime = ConfigMap.OrderLimitTime,
                        SendTime = ConfigMap.SendTime
                    };
                    list.Add(setting);

                    response.OrderSettingList = list;
                    //返回成功结果
                    return BuildResponse.SuccessResponse(response);
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<AdminOrderSettingListResponse>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminOrderSettingList", "", ex.Message, ex);
                return BuildResponse.FailResponse<AdminOrderSettingListResponse>("后台获取订单设置列表错误");
            }
        }

        #endregion

        #region 后台编辑订单设置

        /// <summary>
        /// 后台编辑订单设置 
        /// </summary>
        [Route("AdminEditOrderSetting")]
        [AddOperateLog("后台编辑订单设置")]

        public BaseResponse<object> AdminEditOrderSetting(AdminEditOrderSettingRequest request)
        {
            try
            {
                //验证数据格式是否正确
                if (ModelState.IsValid)
                {
                    ConfigManager.WriteConfig("OrderSetting", "configuration/AutoGoodComments", request.AutoGoodComments);
                    ConfigManager.WriteConfig("OrderSetting", "configuration/Autorefund", request.Autorefund);
                    ConfigManager.WriteConfig("OrderSetting", "configuration/AutoSendTip", request.AutoSendTip);
                    ConfigManager.WriteConfig("OrderSetting", "configuration/CompleteOrder", request.CompleteOrder);
                    ConfigManager.WriteConfig("OrderSetting", "configuration/GroupBuylimitTime", request.GroupBuylimitTime);
                    ConfigManager.WriteConfig("OrderSetting", "configuration/OrderLimitTime", request.OrderLimitTime);
                    ConfigManager.WriteConfig("OrderSetting", "configuration/SendTime", request.SendTime);

                    //返回成功结果
                    return BuildResponse.SuccessResponse<object>("后台编辑订单设置成功");
                }

                //获取验证错误信息
                var errorMessage = GetModelErrorMsg();

                //返回失败结果
                return BuildResponse.FailResponse<object>(errorMessage, ResponseCode.DataError);
            }

            catch (Exception ex)
            {
                ApiLogger.Error("AdminEditOrderSetting", "", ex.Message, ex);
                return BuildResponse.FailResponse<object>("后台编辑订单设置错误");
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