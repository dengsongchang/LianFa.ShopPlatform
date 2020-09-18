using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.DepartMent
{
    /// <summary>
    /// 部门管理列表响应类
    /// </summary>
    public class AdminDepartmentListResponse
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        public List<Department> DepartmentList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 部门类
    /// </summary>
    public class Department
    {
        /// <summary>  
        /// 管理员组id  
        /// </summary>
        public short AdminGId { get; set; }
        /// <summary>  
        /// 标题  
        /// </summary>
        public string Title { get; set; }
        /// <summary>  
        /// 权限动作Id列表  
        /// </summary>
        public string ActionIds { get; set; }
        /// <summary>  
        /// 职位说明  
        /// </summary>
        public string Description { get; set; }
    }
}
