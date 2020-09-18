namespace LianFa.ShopPlatform.Model.Request.Admin.Regions
{
    /// <summary>
    /// 后台获取地级市请求类
    /// </summary>
    public class AdminCityRequest
    {
        /// <summary>
        /// 调用类型(0-小程序端接口，1-后台接口)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentId { get; set; }
    }

    /// <summary>
    /// 新增区域请求类
    /// </summary>
    public class AdminAddRegionRequest
    {
        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }

        /// <summary>  
        /// 父id  
        /// </summary>
        public short ParentId { get; set; }

        /// <summary>  
        /// 级别  
        /// </summary>
        public byte Layer { get; set; }

        /// <summary>  
        /// 省id  
        /// </summary>
        public short ProvinceId { get; set; }

        ///// <summary>  
        ///// 省名称  
        ///// </summary>
        //public string ProvinceName { get; set; }

        /// <summary>  
        /// 市id  
        /// </summary>
        public short CityId { get; set; }

        ///// <summary>  
        ///// 市名称  
        ///// </summary>
        //public string CityName { get; set; }
    }

    /// <summary>
    /// 编辑区域请求类
    /// </summary>
    public class AdminUpRegionRequest
    {
        /// <summary>  
    	/// 区域id  
    	/// </summary>
        public short RegionId { get; set; }

        /// <summary>  
        /// 名称  
        /// </summary>
        public string Name { get; set; }
    }
}
