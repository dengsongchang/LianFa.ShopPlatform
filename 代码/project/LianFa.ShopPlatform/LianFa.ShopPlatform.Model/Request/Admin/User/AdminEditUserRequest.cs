namespace LianFa.ShopPlatform.Model.Request.Admin.User
{
    /// <summary>
    /// 后台编辑会员请求类
    /// </summary>
    public class AdminEditUserRequest
    {
        /// <summary>  
    	/// 用户id  
    	/// </summary>
        public int UId { get; set; }

        /// <summary>  
        ///   昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>  
        ///   用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        ///   手机号码
        /// </summary>
        public string Mobile { get; set; }
    }
}
