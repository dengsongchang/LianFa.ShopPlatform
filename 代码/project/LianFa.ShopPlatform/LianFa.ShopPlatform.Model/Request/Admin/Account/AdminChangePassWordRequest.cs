using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Account
{
    /// <summary>
    /// 后台修改密码请求类
    /// </summary>
    public class AdminChangePassWordRequest
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "旧密码不能为空")]
        public string EnterPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "确认密码不能为空")]
        public string AffirmPassword { get; set; }
    }
}
