using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 退货发货详情
    /// </summary>
    public class BeenSendDetailResponse
    {
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
        /// 金钱  
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>  
        /// 收货区域id  
        /// </summary>
        public short RegionId { get; set; }
        /// <summary>  
        /// 收货人  
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>  
        /// 手机  
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>  
        /// 收货详细地址  
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> ShipList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OSn { get; set; }
    }
}
