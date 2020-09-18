using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.AdminOperateLog
{
    /// <summary>
    /// 后台操作日志列表响应类
    /// </summary>
    public class AdminAdminOperateLogListResponse
    {
        /// <summary>
        /// 操作日志列表
        /// </summary>
        public List<AdminOperateLogInfo> AdminOperateLogList { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int Total { get; set; }
    }
}
