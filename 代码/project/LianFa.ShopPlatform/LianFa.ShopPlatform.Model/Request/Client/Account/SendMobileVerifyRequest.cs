using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// 发送验证码请求类
    /// </summary>
    public class SendMobileVerifyRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; }

        /// <summary>
        /// 类型 0-注册 1-登录
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        public int Type { get; set; }
    }
}
