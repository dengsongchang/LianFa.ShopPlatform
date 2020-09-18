using System;
using LianFa.ShopPlatform.Code.Data;

namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 获取订单交易分析 详情类
    /// </summary>
    public class AdminGetOrderTransactionAnalysisResponse
    {
        /// <summary>
        /// 下单人数
        /// </summary>
        public int OrderUserCount { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 下单件数
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 付款人数
        /// </summary>
        public int PayUserCount { get; set; }

        /// <summary>
        /// 付款订单数
        /// </summary>
        public int PayOrderCount { get; set; }

        /// <summary>
        /// 付款件数
        /// </summary>
        public int PayProductCount { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 客平均价
        /// </summary>
        public decimal AvgAmount { get; set; }

        /// <summary>
        /// 下单转化率
        /// </summary>
        public decimal OrderConversionRate { get; set; }

        /// <summary>
        /// 付款转化率
        /// </summary>
        public decimal PayConversionRate { get; set; }

        /// <summary>
        /// 成交转化率
        /// </summary>
        public decimal SuccConversionRate { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int BroswerCount { get; set; }

        /// <summary>
        /// 订单分析表格
        /// </summary>
        public QueryDataResponse OrderDataTable { get; set; }
    }

    /// <summary>
    /// 交易分析详情
    /// </summary>
    public class OrderTransactionAnalysis
    {

    }

    /// <summary>
    /// 订单分析详情
    /// </summary>
    public class OrderAnalysisDetal
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>
        /// 产品数
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
