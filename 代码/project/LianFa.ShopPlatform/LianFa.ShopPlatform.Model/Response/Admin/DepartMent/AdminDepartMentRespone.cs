using System.Collections.Generic;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Admin.DepartMent
{
    /// <summary>
    /// 部门管理列表响应类
    /// </summary>
    public class AdminDepartMentListRespone
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        public List<LF_AdminGroups> DepartMentList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 权限列表响应类
    /// </summary>
    public class AdminPermissionsListRespone
    {
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<PermissionsList> PermissionsList { get; set; }
    }

    /// <summary>
    /// 权限列表
    /// </summary>
    public class PermissionsList
    {
        /// <summary>  
        /// 动作id  
        /// </summary>
        public int AdminAid { get; set; }

        /// <summary>  
        /// 动作标题  
        /// </summary>
        public string Title { get; set; }

        /// <summary>  
        /// 动作代码  
        /// </summary>
        public string Action { get; set; }

        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否会选中(true:选中false：没有选中)
        /// </summary>
        public bool selected { get; set; }

        /// <summary>
        /// 子级权限列表
        /// </summary>
        public List<ChildPermissionsList> ChildPermissionsList { get; set; }
    }

    /// <summary>
    /// 子级权限列表
    /// </summary>
    public class ChildPermissionsList
    {
        /// <summary>  
        /// 动作id  
        /// </summary>
        public int AdminAid { get; set; }

        /// <summary>  
        /// 动作标题  
        /// </summary>
        public string Title { get; set; }

        /// <summary>  
        /// 动作代码  
        /// </summary>
        public string Action { get; set; }

        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否会选中(true:选中false：没有选中)
        /// </summary>
        public bool selected { get; set; }
    }
}
