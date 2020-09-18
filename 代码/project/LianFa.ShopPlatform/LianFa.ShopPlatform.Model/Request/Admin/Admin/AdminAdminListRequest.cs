namespace LianFa.ShopPlatform.Model.Request.Admin.Admin
{
    /// <summary>
    /// 后台获取管理员列表请求类
    /// </summary>
    public class AdminAdminListRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 部门id  
        /// </summary>
        public short AdminGId { get; set; }
    }
  
}
