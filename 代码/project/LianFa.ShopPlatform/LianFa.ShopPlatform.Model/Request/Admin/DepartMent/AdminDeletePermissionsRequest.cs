using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.DepartMent
{
    /// <summary>
    /// 删除限列表请求类
    /// </summary>
    public class AdminDeletePermissionsRequest
    {
        /// <summary>  
        /// 部门id  
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>  
        /// 权限动作  
        /// </summary>
        public List<string> ActionList { get; set; }
    }
}
