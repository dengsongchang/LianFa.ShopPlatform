namespace LianFa.ShopPlatform.Model.Response.Client.WxOpen
{
    /// <summary>
    /// 获取用户信息响应类
    /// </summary>
    public class GetUserInfoResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UId { get; set; }
    }
}