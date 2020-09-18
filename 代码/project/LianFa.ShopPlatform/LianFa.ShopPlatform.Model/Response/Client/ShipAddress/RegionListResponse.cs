using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.ShipAddress
{
    /// <summary>
    /// 区域列表响应类
    /// </summary>
    public class RegionListResponse
    {
        /// <summary>
        /// 区域列表
        /// </summary>
        public List<RegionInfo> RegionList { get; set; }
    }

    /// <summary>
    /// 区域信息
    /// </summary>
    public class RegionInfo
    {
        /// <summary>
        /// code
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 上一级id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 子元素
        /// </summary>
        /// <returns></returns>
        public List<RegionInfo> sub { get; set; }
    }
}
