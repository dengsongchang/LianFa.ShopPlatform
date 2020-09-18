using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Templates
{
    /// <summary>
    /// 后台删除运费模板请求类
    /// </summary>
    public class AdminDelTemplatesRequest
    {
        /// <summary>
        /// 运费模板id列表
        /// </summary>
        public List<int> TemplatesIdList { get; set; }
    }
}
