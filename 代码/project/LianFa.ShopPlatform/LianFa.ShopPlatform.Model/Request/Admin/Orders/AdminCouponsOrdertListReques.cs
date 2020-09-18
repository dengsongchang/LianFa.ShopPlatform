using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Orders
{
    /// <summary>
    /// 兑换订单请求类
    /// </summary>
    public class AdminCouponsOrdertListReques
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string oSn { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int orderState { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel page { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UId { get; set; }
    }
}
