using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Regions
{
    /// <summary>
    /// 添加商家开放区域请求类
    /// </summary>
    public class AdminAddStoreRegionsRequest
    {
        /// <summary>
        /// 区域id
        /// </summary>
        public List<int> RegionId { get; set; }
    }
}
