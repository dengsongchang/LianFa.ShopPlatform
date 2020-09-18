namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 修改素材名称 请求类
    /// </summary>
    public class AdminUpdateMaterialNameRequest
    {
        /// <summary>
        ///素材Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 素材名称
        /// </summary>
        public string Name { get; set; }
    }
}
