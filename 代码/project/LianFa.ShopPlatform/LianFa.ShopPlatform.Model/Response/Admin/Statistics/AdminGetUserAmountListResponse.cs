using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 获取会员累计消费金额分布信息列表响应类
    /// </summary>
    public class AdminGetUserAmountListResponse
    {
        /// <summary>
        /// 会员累计消费金额分布信息列表
        /// </summary>
        public List<UserAmount> List { get; set; }
    }

    /// <summary>
    /// 会员累计消费金额分布信息类
    /// </summary>
    public class UserAmount
    {
        /// <summary>
        /// 价格区间
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户数量
        /// </summary>
        public int UserCount { get; set; }
    }
    
}
