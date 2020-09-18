namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 更新分类名 请求类
    /// </summary>
    public class AdminUpdateCategoryNameRequest
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
    }
}
