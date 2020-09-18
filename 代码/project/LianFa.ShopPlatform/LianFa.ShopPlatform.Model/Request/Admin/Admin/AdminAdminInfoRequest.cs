using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Admin
{
    /// <summary>
    /// 后台管理员信息请求类
    /// </summary>
    public class AdminAdminInfoRequest
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        [Range(0,int.MaxValue,ErrorMessage ="管理员Id大于1")]
        public int UId { get; set; }
    }
}
