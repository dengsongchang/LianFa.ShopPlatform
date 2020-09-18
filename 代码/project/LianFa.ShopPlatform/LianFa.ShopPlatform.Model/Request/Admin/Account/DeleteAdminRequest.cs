using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Account
{
    /// <summary>  
    /// 删除管理员
    /// </summary>
    public class DeleteAdminRequest
    {
        /// <summary>  
        /// 管理员编号
        /// </summary>
        [Required(ErrorMessage = "管理员编号不能为空")]
        public int UId { get; set; }
    }
}
