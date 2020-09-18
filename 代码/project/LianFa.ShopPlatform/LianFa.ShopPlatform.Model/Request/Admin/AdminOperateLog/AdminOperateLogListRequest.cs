using System;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.AdminOperateLog
{
    /// <summary>
    /// 后台操作日志列表请求类
    /// </summary>
    public class AdminOperateLogListRequest
    {
        /// <summary>
        /// 分页
        /// </summary>
        public PageModel Page { get; set; }

        /// <summary>
        /// 开始时间，空代表全部
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间 ，空代表全部
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 管理员Id，0代表全部
        /// </summary>
        public int UId { get; set; }
    }
}
