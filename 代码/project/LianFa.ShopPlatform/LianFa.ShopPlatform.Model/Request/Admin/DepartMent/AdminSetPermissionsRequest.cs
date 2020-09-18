namespace LianFa.ShopPlatform.Model.Request.Admin.DepartMent
{
    /// <summary>
    /// 设置限列表请求类
    /// </summary>
    public class AdminSetPermissionsRequest
    {
        /// <summary>  
        /// 部门id  
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>  
        /// 权限动作列表  
        /// </summary>
        public string ActionIds { get; set; }
    }
}
