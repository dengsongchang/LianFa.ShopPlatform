using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// 编辑用户请求类
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>  
        /// 用户id  
        /// </summary>
        [Required(ErrorMessage = "用户编号不能为空")]
        public int UId { get; set; }
        /// <summary>  
        /// 用户名称  
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string UserName { get; set; }
        /// <summary>  
        /// 邮箱  
        /// </summary>
        public string Email { get; set; }
        /// <summary>  
        /// 手机  
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [RegularExpression(@"^0{0,1}(13[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9])[0-9]{8}$", ErrorMessage = "手机号格式错误")]
        public string Mobile { get; set; }
        /// <summary>  
        /// 密码  
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string PassWord { get; set; }
        /// <summary>  
        /// 商城管理员组id  
        /// </summary>
        public short AdminGId { get; set; }
        /// <summary>  
        /// 昵称  
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string NickName { get; set; }
        /// <summary>  
        /// 头像  
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string Avatar { get; set; }
        /// <summary>  
        /// 是否验证邮箱  
        /// </summary>
        public byte? VerifyEmail { get; set; }
        /// <summary>  
        /// 是否验证手机  
        /// </summary>
        public byte? VerifyMobile { get; set; }
    }
}
