using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.AdminOperateLog
{
    /// <summary>
    /// 后台操作日志列表响应类
    /// </summary>
    public class AdminOperateLogListResponse
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

    /// <summary>
    /// 操作日志信息类
    /// </summary>
    public class AdminOperateLogInfo
    {
        /// <summary>  
        /// 日志id  
        /// </summary>
        public int LogId { get; set; }

        /// <summary>  
        /// 用户昵称  
        /// </summary>
        public string NickName { get; set; }

        /// <summary>  
        /// 操作ip  
        /// </summary>
        public string Ip { get; set; }

        /// <summary>  
        /// 操作时间   
        /// </summary>
        public string OperateTime { get; set; }

        /// <summary>  
        /// 操作  
        /// </summary>
        public string Operation { get; set; }
    }
}
