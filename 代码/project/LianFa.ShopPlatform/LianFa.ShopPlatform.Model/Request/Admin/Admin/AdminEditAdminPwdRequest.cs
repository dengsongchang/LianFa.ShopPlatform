namespace LianFa.ShopPlatform.Model.Request.Admin.Admin
{
    /// <summary>
    /// 后台修改管理员密码请求类
    /// </summary>
   public class AdminEditAdminPwdRequest
    {

        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }

        /// <summary>  
        /// 密码  
        /// </summary>
        public string PassWord { get; set; }
    }
}
