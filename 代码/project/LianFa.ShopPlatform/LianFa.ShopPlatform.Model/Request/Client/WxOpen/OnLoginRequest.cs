using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.WxOpen
{
    /// <summary>
    /// 用户登录信息请求类
    /// </summary>
    public class OnLoginRequest
    {
        /// <summary>
        /// Code
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
