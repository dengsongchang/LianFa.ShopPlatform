namespace LianFa.ShopPlatform.Model.Request.Admin.DepartMent
{
    /// <summary>
    /// 新增部门 
    /// </summary>
    public class AdminAddDepartmentRequest
    {
        /// <summary>  
        /// 部门名称  
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>  
        /// 职位说明  
        /// </summary>
        public string Description { get; set; }
    }
}
