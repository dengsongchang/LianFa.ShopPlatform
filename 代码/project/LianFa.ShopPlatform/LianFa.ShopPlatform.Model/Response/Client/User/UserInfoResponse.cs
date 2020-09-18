namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 用户信息 响应类
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }

        /// <summary>  
        /// 用户名称  
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 邮箱  
        /// </summary>
        public string Email { get; set; }

        /// <summary>  
        /// 手机  
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>  
        /// 商城管理员组id  
        /// </summary>
        public short AdminGId { get; set; }

        /// <summary>  
        /// 昵称  
        /// </summary>
        public string NickName { get; set; }

        /// <summary>  
        /// 头像  
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>  
        /// 是否验证邮箱  
        /// </summary>
        public byte VerifyEmail { get; set; }

        /// <summary>  
        /// 是否验证手机  
        /// </summary>
        public byte VerifyMobile { get; set; }

        /// <summary>  
        /// 盐值  
        /// </summary>
        public string Salt { get; set; }

        /// <summary>  
        /// 创建时间  
        /// </summary>
        public string CreateTime { get; set; }
    }
}
