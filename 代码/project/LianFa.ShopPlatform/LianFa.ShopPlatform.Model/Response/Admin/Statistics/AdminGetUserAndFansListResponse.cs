namespace LianFa.ShopPlatform.Model.Response.Admin.Statistics
{
    /// <summary>
    /// 获取粉丝与会员构成信息响应类
    /// </summary>
    public class AdminGetUserAndFansListResponse
    {
        /// <summary>
        /// 会员数量
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int FansCount { get; set; }

        /// <summary>
        /// 粉丝会员数量
        /// </summary>
        public int UserFansCount { get; set; }
    }
}
