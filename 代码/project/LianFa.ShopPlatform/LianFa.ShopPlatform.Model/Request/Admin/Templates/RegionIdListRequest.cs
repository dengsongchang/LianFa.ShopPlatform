using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Templates
{
    /// <summary>
    /// 筛选区域id列表请求类
    /// </summary>
    public class RegionIdListRequest
    {
        /// <summary>
        /// 筛选区域id列表
        /// </summary>
        public List<short> RegionIdList { get; set; }
    }
}
