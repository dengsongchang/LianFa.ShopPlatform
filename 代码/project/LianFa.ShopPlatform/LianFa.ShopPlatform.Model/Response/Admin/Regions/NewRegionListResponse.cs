using Newtonsoft.Json;

namespace LianFa.ShopPlatform.Model.Response.Admin.Regions
{
    /// <summary>
    /// 新区域列表
    /// </summary>
    public class NewRegionListResponse
    {
        /// <summary>
        /// 省
        /// </summary>
        [JsonProperty("province_list")]
        public object ProvinceList { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [JsonProperty("city_list")]
        public object CityList { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [JsonProperty("county_list")]
        public object CountyList { get; set; }
    }
}
