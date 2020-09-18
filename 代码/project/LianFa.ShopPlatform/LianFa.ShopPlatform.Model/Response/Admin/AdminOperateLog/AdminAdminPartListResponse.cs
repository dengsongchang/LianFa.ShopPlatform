using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.AdminOperateLog
{
    /// <summary>
    /// 后台管理员列表响应类
    /// </summary>
    public class AdminAdminPartListResponse
    {
        /// <summary>
        /// 管理员列表
        /// </summary>
        public List<AdminPartInfo> AdminList { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AdminPartInfo
    {
        /// <summary>  
        /// 用户id  
        /// </summary>
        public int Uid { get; set; }
        /// <summary>  
        /// 用户昵称  
        /// </summary>
        public string NickName { get; set; }
    }

}
