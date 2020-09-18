using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 批量删除素材 请求类
    /// </summary>
    public class AdminBatchDeleteMaterialRequest
    {
        /// <summary>
        /// 素材Id列表
        /// </summary>
        public List<int> Ids { get; set; }
    }
}
