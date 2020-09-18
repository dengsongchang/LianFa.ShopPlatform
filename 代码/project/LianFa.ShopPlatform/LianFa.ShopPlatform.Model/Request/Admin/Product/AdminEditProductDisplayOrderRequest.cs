namespace LianFa.ShopPlatform.Model.Request.Admin.Product
{
    /// <summary>
    /// 后台编辑商品排序请求类
    /// </summary>
    public class AdminEditProductDisplayOrderRequest
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
