using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Account
{
    /// <summary>
    /// 管理员登录请求类
    /// </summary>
    public class AdminLoginRequest
    {
        /// <summary>  
        /// 会员编号 
        /// </summary>
        [Required(ErrorMessage = "会员编号不能为空")]
        public string UserName { get; set; }

        /// <summary>  
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
