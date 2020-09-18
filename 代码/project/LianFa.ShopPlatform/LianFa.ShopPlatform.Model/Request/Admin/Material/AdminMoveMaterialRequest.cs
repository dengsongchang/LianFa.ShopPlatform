using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 移动素材 请求类
    /// </summary>
    public class AdminMoveMaterialRequest
    {
        /// <summary>
        /// 素材Id列表
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// 目标分类Id
        /// </summary>
        public int CategoryId { get; set; }
    }
}
