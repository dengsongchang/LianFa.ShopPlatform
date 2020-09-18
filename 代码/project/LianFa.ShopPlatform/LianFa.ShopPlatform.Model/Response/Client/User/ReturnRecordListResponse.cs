using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 退款记录响应类
    /// </summary>
    public class ReturnRecordListResponse
    {
        /// <summary>
        /// 退款订单列表
        /// </summary>
        public List<ReturnRecordInfo> ReturnRecordList { get; set; }
    }

    /// <summary>
    /// 订单动作信息
    /// </summary>
    public class ReturnRecordInfo
    {
        /// <summary>  
    	/// 订单动作id  
    	/// </summary>
        public int AId { get; set; }
        /// <summary>  
        /// 订单id  
        /// </summary>
        public int OId { get; set; }
        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }
        /// <summary>  
        /// 真实名称  
        /// </summary>
        public string RealName { get; set; }
        /// <summary>  
        /// 管理员组id  
        /// </summary>
        public short AdminGId { get; set; }
        /// <summary>  
        /// 管理员组标题  
        /// </summary>
        public string AdminGTitle { get; set; }
        /// <summary>  
        /// 动作类型  
        /// </summary>
        public byte ActionType { get; set; }
        /// <summary>  
        /// 动作时间  
        /// </summary>
        public System.DateTime ActionTime { get; set; }
        /// <summary>  
        /// 动作描述  
        /// </summary>
        public string ActionDes { get; set; }
    }
}
