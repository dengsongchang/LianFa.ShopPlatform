using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Brand
{
    /// <summary>
    /// 后台品牌列表请求类
    /// </summary>
    public class AdminBrandListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Name { get; set; }
    }
}