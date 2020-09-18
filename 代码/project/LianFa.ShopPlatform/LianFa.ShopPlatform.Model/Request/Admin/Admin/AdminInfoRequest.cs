using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Admin
{
    /// <summary>
    /// 后台管理员信息请求类
    /// </summary>
    public class AdminInfoRequest
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int UId { get; set; }
    }
}
