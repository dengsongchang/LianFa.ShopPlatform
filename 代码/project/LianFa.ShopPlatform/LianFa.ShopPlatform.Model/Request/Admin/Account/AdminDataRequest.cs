using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Account
{
    /// <summary>  
    /// 管理员信息请求类
    /// </summary>
    public class AdminInfoRequest
    {
        /// <summary>  
        /// 管理员编号
        /// </summary>
        [Required(ErrorMessage = "管理员编号不能为空")]
        public int UId { get; set; }
    }
}
