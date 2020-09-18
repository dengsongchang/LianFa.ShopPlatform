using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Code.Deppon.Models.Request;
using LianFa.ShopPlatform.Code.Deppon.Models.Response;
using LianFa.ShopPlatform.Code.KuaiDi100;

namespace LianFa.ShopPlatform.Code.Deppon
{
    public class DepponApi
    {
        private const string CompanyCode = "EWBSZSLFXMYYXGS";

        private const string AppKey = "2e7c02ee1c6e0fd44c9f18c51d938ab5";

        /// <summary>
        /// 查询所有接口下单的信息
        /// </summary>
        /// <param name="request">请求类</param>
        /// <returns></returns>
        public static DepponResponse<QueryOrderResponse> QueryOrder(QueryOrderRequest request)
        {
            //请求地址
            var url = "http://dpsanbox.deppon.com/sandbox-web/standard-order/queryOrder.action";

            var timestamp = ConvertDateTimeInt(DateTime.Now).ToString();

            var param = request.ToJson();

            //生成摘要
            string plainText = param + AppKey + timestamp;

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("companyCode", CompanyCode);
            parameters.Add("params", param);
            parameters.Add("digest", GetDigest(plainText));
            parameters.Add("timestamp", timestamp);

            var requestData = WebHelper.BuildQuery(parameters, "utf-8");

            var result = WebHelper.GetFormRequestDataUsePost(url, requestData);
            return result.ToObject<DepponResponse<QueryOrderResponse>>();
        }

        /// <summary>
        /// 新标准轨迹查询
        /// </summary>
        /// <param name="mailNo">运单号</param>
        /// <returns></returns>
        public static KuaiDiModel NewTraceQuery(string mailNo)
        {
            //请求地址
            var request = new NewTraceQueryRequest
            {
                MailNo = mailNo
            };

            //新标准轨迹查询
            var response = NewTraceQuery(request);

            //转换信息
            var traceList = new List<KuaiDiInfoBody>();
            var state = "";
            if (response?.ResponseParam != null)
            {
                //倒序列表
                response.ResponseParam.trace_list = response.ResponseParam.trace_list.Reverse().ToArray();
                traceList.AddRange(response.ResponseParam.trace_list.Select(
                    trace => new KuaiDiInfoBody { context = trace.description, ftime = trace.time, time = trace.time })
                );

                //获取状态
                if (response.ResponseParam.trace_list.Any())
                {
                    var trace = response.ResponseParam.trace_list.FirstOrDefault();
                    state = trace != null ? GetLogisticState(trace.status) : "未知";
                }
            }

            var result = new KuaiDiModel
            {
                com = "DEPPON",
                nu = mailNo,
                state = state,
                data = traceList
            };
            return result;
        }

        /// <summary>
        /// 新标准轨迹查询
        /// </summary>
        /// <param name="request">请求类</param>
        /// <returns></returns>
        public static DepponResponse<NewTraceQueryResponse> NewTraceQuery(NewTraceQueryRequest request)
        {
            //请求地址
            var url = "http://dpapi.deppon.com/dop-interface-sync/standard-query/newTraceQuery.action";

            var timestamp = ConvertDateTimeInt(DateTime.Now).ToString();

            var param = request.ToJson();

            //生成摘要
            string plainText = param + AppKey + timestamp;

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("companyCode", CompanyCode);
            parameters.Add("params", param);
            parameters.Add("digest", GetDigest(plainText));
            parameters.Add("timestamp", timestamp);

            var requestData = WebHelper.BuildQuery(parameters, "utf-8");

            var result = WebHelper.GetFormRequestDataUsePost(url, requestData);
            return result.ToObject<DepponResponse<NewTraceQueryResponse>>();
        }

        /// <summary>
        /// 获取物流状态信息
        /// </summary>
        /// <param name="state">状态码</param>
        /// <returns></returns>
        public static string GetLogisticState(string state)
        {
            switch (state)
            {
                case "GOT": return "开单";
                case "ARRIVAL": return "进站";
                case "DEPARTURE": return "出站";
                case "SENT_SCAN": return "派送";
                case "SIGNED": return "签收";
                case "ERROR": return "滞留,延时派送";
                case "FAILED": return "客户拒签";
                default: return "未知";
            }
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000; //除10000调整为13位
            return t;
        }

        /// <summary>
        /// 获取摘要
        /// </summary>
        /// <param name="plainText">加密原文</param>
        /// <returns>摘要</returns>
        private static string GetDigest(string plainText)
        {
            return StringHelper.ToBase64(Md5Hex(plainText));
        }

        private static string Md5Hex(string data)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dataHash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in dataHash)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
