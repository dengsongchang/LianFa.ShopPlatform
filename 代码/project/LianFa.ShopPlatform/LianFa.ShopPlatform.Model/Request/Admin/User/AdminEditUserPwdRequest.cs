using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.User
{
    /// <summary>
    /// 后台编辑会员登陆密码请求类
    /// </summary>
    public class AdminEditUserPwdRequest
    {
        /// <summary>  
        /// 用户id
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>  
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "确认密码不能为空")]
        public string ConfirmPwd { get; set; }
    }
}
