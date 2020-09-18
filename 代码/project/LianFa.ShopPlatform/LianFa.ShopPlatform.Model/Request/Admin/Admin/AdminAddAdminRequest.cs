using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Admin
{
    /// <summary>
    /// 后台添加管理员请求类
    /// </summary>
    public class AdminAddAdminRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(50,ErrorMessage = "请确定用户名的有效长度用户名1-50",MinimumLength =1)]
        public string UserName { get; set; }

        /// <summary>  
        /// 密码  
        /// </summary>
        [StringLength(50,ErrorMessage = "请确定用户名的有效长度密码6-50", MinimumLength =6)]
        public string PassWord { get; set; }

        /// <summary>  
        /// 部门Id  
        /// </summary>
        [Range(1,int.MaxValue,ErrorMessage ="部门id应在1-int.max")]
        public short AdminGId { get; set; }
    }
}
