namespace LianFa.ShopPlatform.Model.Request.Admin.Templates
{
    /// <summary>
    /// 计算运费请求类
    /// </summary>
    public class CalculateFreightRequest
    {
        /// <summary>  
        /// 配送模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 金额  
        /// </summary>
        public decimal Price { get; set; }
    }
}
