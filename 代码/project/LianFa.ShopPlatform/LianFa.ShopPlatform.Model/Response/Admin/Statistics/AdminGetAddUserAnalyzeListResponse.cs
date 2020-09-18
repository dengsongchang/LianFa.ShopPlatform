using System.Collections.Generic;
using LianFa.ShopPlatform.Code.Data;

namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 获取会员增长情况列表响应类
    /// </summary>
    public class AdminGetAddUserAnalyzeListResponse
    {
        /// <summary>
        /// 注册时间和会员数量列表
        /// </summary>
        public List<TimeAndUserCount> List { get; set; }
    }

    /// <summary>
    /// 注册时间和会员数量类
    /// </summary>
    public class TimeAndUserCount
    {
        /// <summary>
        /// 注册时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 会员数量
        /// </summary>
        public int UserCount { get; set; }
    }
}
