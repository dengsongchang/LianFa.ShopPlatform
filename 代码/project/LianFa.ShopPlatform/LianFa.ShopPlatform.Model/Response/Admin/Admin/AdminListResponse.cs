using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Admin
{
    /// <summary>
    /// 管理员列表响应类
    /// </summary>
    public class AdminListsResponse
    {
        /// <summary>
        /// 管理员列表
        /// </summary>
        public List<AdminsInfo> AdminList { get; set; }
    }

    /// <summary>
    /// 管理员信息类
    /// </summary>
    public class AdminsInfo
    {
        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 注册时间  
        /// </summary>
        public System.DateTime RegisterTime { get; set; }

        /// <summary>  
        /// 注册时间格式化  
        /// </summary>
        public string RegisterTimeStr { get; set; }

        /// <summary>  
        /// 部门标题  
        /// </summary>
        public string Title { get; set; }
    }
}
