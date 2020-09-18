using System;
using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 
    /// </summary>
    public class AfterServicesListResponse
    {
        /// <summary>
        /// 售后列表
        /// </summary>
        public List<UserAfterSalesModel> List{ get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 售后列表
    /// </summary>
    public class UserAfterSalesModel
    {
        /// <summary>  
        /// 售后服务id  
        /// </summary>
        public int ASId { get; set; }
        /// <summary>  
        /// 状态  
        /// </summary>
        public byte State { get; set; }

        /// <summary>  
        /// 状态  
        /// </summary>
        public string StateStr { get; set; }

        /// <summary>  
        /// 订单SN 
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal Money { get; set; }
       
        /// <summary>  
        /// 商品名称  
        /// </summary>
        public string PName { get; set; }
        /// <summary>  
        /// 商品图片  
        /// </summary>
        public string PShowImg { get; set; }
        /// <summary>  
        /// 数量  
        /// </summary>
        public int Count { get; set; }

        /// <summary>  
        /// 类型(0代表退货,1代表换货,2代表维修)  
        /// </summary>
        public string TypeStr { get; set; }

        /// <summary>  
        /// 类型(0代表退货,1代表换货,2代表维修)  
        /// </summary>
        public byte Type { get; set; }
       
        /// <summary>  
        /// 申请时间  
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>  
        /// 申请时间  
        /// </summary>
        public string ApplyTimeStr { get; set; }

        /// <summary>  
        /// 审核结果  
        /// </summary>
        public string CheckResult { get; set; }

        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerNote { get; set; }
    }
}
    