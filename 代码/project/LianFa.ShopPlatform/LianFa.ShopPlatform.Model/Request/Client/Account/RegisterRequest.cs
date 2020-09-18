using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// 用户账号注册请求类
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required(ErrorMessage = "手机号码不能为空")]
        public string Mobile { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空")]
        public string Code { get; set; }
    }
}
