namespace LianFa.ShopPlatform.Model.Request.Admin.DepartMent
{
    /// <summary>
    /// 修改部门 
    /// </summary>
    public class AdminEditDepartmentRequest
    {
        /// <summary>  
        /// 部门id  
        /// </summary>
        public int AdminGId { get; set; }

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
