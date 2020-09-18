using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.AdminOperateLog
{
    /// <summary>
    /// 后台删除管理员操作日志响应类
    /// </summary>
    public class AdminDelAdminOperateLogRequest
    {
        /// <summary>
        /// 日志id列表
        /// </summary>
        [Required(ErrorMessage = "日志id不能为空")]
        public List<int> LogIdList { get; set; }
    }
}
