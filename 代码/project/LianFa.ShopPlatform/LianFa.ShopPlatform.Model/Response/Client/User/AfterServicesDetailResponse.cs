using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 售后服务详情 响应类
    /// </summary>
    public class AfterServicesDetailResponse
    {
        /// <summary>  
        /// 售后服务id  
        /// </summary>
        public int ASId { get; set; }

        /// <summary>  
        /// 状态  
        /// </summary>
        public int State { get; set; }

        /// <summary>  
        /// 金钱  
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>  
        /// 类型(0代表退货,1代表换货,2代表维修)  
        /// </summary>
        public byte Type { get; set; }

        /// <summary>  
        /// 申请原因  
        /// </summary>
        public string ApplyReason { get; set; }

        /// <summary>  
        /// 申请时间  
        /// </summary>
        public string ApplyTime { get; set; }

        /// <summary>  
        /// 审核时间  
        /// </summary>
        public string CheckTime { get; set; }

        /// <summary>  
        /// 邮寄给商城时间  
        /// </summary>
        public string SendTime { get; set; }
        /// <summary>  
        /// 商城收货时间  
        /// </summary>
        public string ReceiveTime { get; set; }
      
        /// <summary>  
        /// 邮寄给客户时间  
        /// </summary>
        public string BackTime { get; set; }
        /// <summary>  
        /// 买家备注  
        /// </summary>
        public string BuyerNote { get; set; }
        /// <summary>  
        /// 图片列表  
        /// </summary>
        public List<string> ImgList { get; set; }

        /// <summary>  
        /// 商家名称  
        /// </summary>
        public string MName { get; set; }

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
        /// 订单号
        /// </summary>
        public string OSn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StateStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompleteTime { get; set; }
    }
}
