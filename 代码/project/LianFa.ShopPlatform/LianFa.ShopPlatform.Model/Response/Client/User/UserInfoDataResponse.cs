namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 查看用户信息响应类
    /// </summary>
    public class UserInfoDataResponse
    {
        /// <summary>  
    	/// 昵称  
    	/// </summary>
        public string NickName { get; set; }

        /// <summary>  
    	/// 会员编号
    	/// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 手机  
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>  
    	/// 其他联系方式  
    	/// </summary>
        public string Phone { get; set; }
        /// <summary>  
        /// 头像 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>  
        /// 头像 
        /// </summary>
        public string AvatarFull { get; set; }
    }
}
