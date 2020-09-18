namespace LianFa.ShopPlatform.Model.Request.Client.Cart
{
    /// <summary>
    /// 添加商品到购物车 请求类
    /// </summary>
    public class AddProductRequest
    {

        /// <summary>
        /// 商品Id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Sku的值
        /// </summary>
        public string Sku { get; set; }

    }
}