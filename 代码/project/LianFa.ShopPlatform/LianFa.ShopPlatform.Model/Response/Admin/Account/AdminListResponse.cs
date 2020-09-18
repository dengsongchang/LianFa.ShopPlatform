using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Account
{
   /// <summary>
   /// 用户列表响应类
   /// </summary>
   public class AdminListResponse
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<AdminInfoResponse> AdminList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
