using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// （根据姓名查询）用户信息请求类
    /// </summary>
    public class UserInfoByNameRequest
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required(ErrorMessage = "用户姓名不能为空")]
        public string UserName { get; set; }
    }
}
