using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Templates
{
    /// <summary>
    /// 区域列表响应类
    /// </summary>
    public class RegionDataListResponse
    {
        /// <summary>
        /// 区域列表
        /// </summary>
        public List<Regions> RegionList { get; set; }

        /// <summary>
        /// 区域Id列表
        /// </summary>
        public List<short> RIdList { get; set; }
    }
}
