using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Regions
{
    /// <summary>
    /// 后台区域管理响应类
    /// </summary>
    public class AdminRegionsResponse
    {
        /// <summary>
        /// 区域列表
        /// </summary>
        public List<RegionsList> RegionsList { get; set; }
    }
    /// <summary>
    /// 区域类
    /// </summary>
    public class RegionsList
    {
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>  
        /// 父id  
        /// </summary>
        public short ParentId { get; set; }
    }

    /// <summary>
    /// 新增成功返回
    /// </summary>
    public class AdminAddReguinsResponse
    {
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId { get; set; }
    }

}
