using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 获取会员端口来源信息响应类
    /// </summary>
    public class AdminGetUserPortSourceListResponse
    {
        /// <summary>
        /// 会员端口来源信息列表
        /// </summary>
       public List<UserPortSourceInfo> List { get; set; }
    }

    /// <summary>
    /// 会员端口来源信息类
    /// </summary>
    public class UserPortSourceInfo
    {
        /// <summary>
        /// 来源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 来源数量
        /// </summary>
        public int Count { get; set; }
    }
}
