namespace LianFa.ShopPlatform.Model.Request.Client.UCenter
{
    /// <summary>
    /// 填写退换货邮寄信息 请求类
    /// </summary>
    public class BeenSentAfterSalesServiceRequest
    {
        /// <summary>
        /// 售后单Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string ShipCoName1 { get; set; }
        /// <summary>
        /// 快递编号
        /// </summary>
        public string ShipSn1 { get; set; }
    }
}
