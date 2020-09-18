namespace LianFa.ShopPlatform.Model.Request.Admin.Admin
{
    /// <summary>
    /// 后台编辑管理员基本信息
    /// </summary>
    public class AdminEditAdminInfoRequest
    { 
        /// <summary>  
        /// 部门id  
        /// </summary>
        public short AdmingId { get; set; }

        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }
    }
}
