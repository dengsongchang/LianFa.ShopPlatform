using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Statistics
{
    /// <summary>
    /// 获取首页订单数量趋势数据请求类
    /// </summary>
    public class AdminGetIndexOrderCountTrendRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 订单类型（0普通订单，1卡片订单,2为购买卡片订单，3为全部订单）
        /// </summary>
        public byte Type { get; set; }
    }
}
