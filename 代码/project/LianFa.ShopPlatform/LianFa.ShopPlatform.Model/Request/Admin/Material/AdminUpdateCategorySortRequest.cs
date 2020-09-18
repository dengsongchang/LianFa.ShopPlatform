namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 更新分类排序 请求类
    /// </summary>
    public class AdminUpdateCategorySortRequest
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Sort { get; set; }
    }
}
