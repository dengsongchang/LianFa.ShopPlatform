using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.UCenter
{
    /// <summary>
    /// 编辑用户请求类
    /// </summary>
    public class EditUserRequest
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        [DisplayName("用户昵称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MaxLength(11, ErrorMessage = "{0}长度限制在11个字符以内")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

    }
}
