using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 获取会员积分明细列表响应类
    /// </summary>
    public class UserCreditDetailListResponse
    {
        /// <summary>  
        /// 会员现拥有总积分
        /// </summary>
        public decimal UserCreditTotal { get; set; }

        /// <summary>
        /// 会员积分明细列表
        /// </summary>
        public List<UserCreditPartInfo> UserCreditDetailList { get; set; }

        /// <summary>  
        /// 总条数 
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 前端和导出需要的字段类
    /// </summary>
    public class UserCreditPartInfo
    {

        /// <summary>  
        /// 积分来源名称  
        /// </summary>
        public string Title { get; set; }

        /// <summary>  
        /// 积分变化
        /// </summary>
        public string PayCredits { get; set; }

        /// <summary>  
    	/// 动作时间  
    	/// </summary>
        public System.DateTime ActionTime { get; set; }

        /// <summary>  
        /// 动作时间格式化  
        /// </summary>
        public string ActionTimeStr { get; set; }

    }
}
