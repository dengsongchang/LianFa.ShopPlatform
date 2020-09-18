using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// 账号登录请求类
    /// </summary>
    public class LoginRequest
    {
        /// <summary>  
        /// 手机号 
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; }

        /// <summary>  
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空")]
        public string Code { get; set; }
    }
}
