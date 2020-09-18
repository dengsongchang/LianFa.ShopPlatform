using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.DepartMent
{
    /// <summary>
    /// 权限列表响应类
    /// </summary>
    public class AdminPermissionsListResponse
    {
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<Permissions> PermissionsList { get; set; }
    }

    /// <summary>
    /// 权限列表
    /// </summary>
    public class Permissions
    {
        /// <summary>  
        /// 动作id  
        /// </summary>
        public int AdminAId { get; set; }

        /// <summary>  
        /// 动作方法  
        /// </summary>
        public string Action { get; set; }

        /// <summary>  
        /// 动作标题  
        /// </summary>
        public string Title { get; set; }

        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>  
        /// 图标样式  
        /// </summary>
        public string IconClass { get; set; }

        /// <summary>
        /// 是否会选中(true:选中false：没有选中)
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 子级权限列表
        /// </summary>
        public List<Permissions> ChildList { get; set; }
    }
}
