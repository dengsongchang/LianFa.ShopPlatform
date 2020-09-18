using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 商品分析响应类
    /// </summary>
   public class AdminGetProductAnalysisListResponse
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<AdminProductAnalysisInfo> AdminProductAnalysisList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 商品分析详情类
    /// </summary>
    public class AdminProductAnalysisInfo
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 销售量
        /// </summary>
        public int SalesCount { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SalesAmount { get; set; }

        /// <summary>
        /// 付款人数
        /// </summary>
        public int BuyCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
