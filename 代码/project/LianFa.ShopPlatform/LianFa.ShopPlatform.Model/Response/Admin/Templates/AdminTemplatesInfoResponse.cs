using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Templates
{
    /// <summary>
    /// 后台运费模板信息响应类
    /// </summary>
    public class AdminTemplatesInfoResponse
    {
        /// <summary>
        /// 后台运费模板信息
        /// </summary>
        public AdminTemplatesInfo AdminTemplatesInfo { get; set; }
    }

    /// <summary>
    /// 后台运费模板信息类
    /// </summary>
    public class AdminTemplatesInfo
    {
        /// <summary>
        /// 运费模板信息
        /// </summary>
        public ShippingTemplatesInfo ShippingTemplates { get; set; }

        /// <summary>
        /// 配送地区价格列表
        /// </summary>
        public List<ShippingRegionsGroups> ShippingRegionsGroupsList { get; set; }

        /// <summary>
        /// 指定包邮地区列表
        /// </summary>
        public List<ShippingAppointRegions> ShippingAppointRegionsList { get; set; }

        /// <summary>
        /// 金额区间列表
        /// </summary>
        public ShippingPriceInfo ShippingPriceInfo { get; set; }
    }

    /// <summary>  
    /// 运费模板信息类  
    /// </summary>
    public class ShippingTemplatesInfo
    {
        /// <summary>  
        /// 配送模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 模板名称  
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>  
        /// 起步重量  
        /// </summary>
        public int DefaultNumber { get; set; }
        /// <summary>  
        /// 加价重量  
        /// </summary>
        public int AddNumber { get; set; }
        /// <summary>  
        /// 默认起步价  
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>  
        /// 加价  
        /// </summary>
        public decimal AddPrice { get; set; }
        /// <summary>  
        /// 是否包邮  0 自定义 1 包邮  
        /// </summary>
        public bool IsfreeShipping { get; set; }
        /// <summary>  
        /// 计价方式  1 按件计  2 按重量  3 按体积 4 按百分比 5 按固定运费  
        /// </summary>
        public int ValuationMethod { get; set; }
        /// <summary>  
        /// 是否指定城市包邮  0 是 1 否  
        /// </summary>
        public bool IsAppoint { get; set; }
    }

    /// <summary>
    /// 配送地区价格信息类
    /// </summary>
    public class ShippingRegionsGroups
    {
        /// <summary>  
        /// 地区组id  
        /// </summary>
        public decimal GroupId { get; set; }
        /// <summary>  
        /// 运费模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 起步重量  
        /// </summary>
        public int DefaultNumber { get; set; }
        /// <summary>  
        /// 加价重量  
        /// </summary>
        public int AddNumber { get; set; }
        /// <summary>  
        /// 起步价  
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>  
        /// 加价  
        /// </summary>
        public decimal AddPrice { get; set; }

        /// <summary>
        /// 配送地区列表
        /// </summary>
        public List<Regions> ShippingRegionsList { get; set; }
    }

    /// <summary>
    /// 指定包邮地区信息类
    /// </summary>
    public class ShippingAppointRegions
    {
        /// <summary>  
        /// 包邮地区id  
        /// </summary>
        public int AppId { get; set; }
        /// <summary>  
        /// 运费模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 包邮数量  
        /// </summary>
        public int MeetNum { get; set; }
        /// <summary>  
        /// 包邮价格  
        /// </summary>
        public decimal MeetPrice { get; set; }
        /// <summary>  
        /// 类型(0- 按件数 1-按金额 2按金额+件数)  
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 指定包邮地区列表
        /// </summary>
        public List<Regions> AppointRegionsList { get; set; }
    }

    /// <summary>
    /// 区域信息
    /// </summary>
    public class Regions
    {
        /// <summary>  
        /// 地区id  
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>  
        /// 名称 
        /// </summary>
        public string Name { get; set; }
        /// <summary>  
        /// 省id  
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>  
        /// 市id  
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 上一级id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 子元素
        /// </summary>
        /// <returns></returns>
        public List<Regions> Sub { get; set; }
    }

    /// <summary>  
    /// 金额区间信息类
    /// </summary>
    public class ShippingPriceInfo
    {
        /// <summary>  
        /// 记录id  
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>  
        /// 运费模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 起始价格  
        /// </summary>
        public decimal StartPrice { get; set; }
        /// <summary>  
        /// 结束价格  
        /// </summary>
        public decimal EndPrice { get; set; }
        /// <summary>  
        /// 区间运费  
        /// </summary>
        public decimal Freight { get; set; }
        /// <summary>  
        /// 满免价格  
        /// </summary>
        public decimal WithFree { get; set; }
        /// <summary>  
        /// 计费方式(0- 百分比 1- 固定运费)  
        /// </summary>
        public int Type { get; set; }
        /// <summary>  
        /// 最小价格  
        /// </summary>
        public decimal SPrice { get; set; }
        /// <summary>  
        /// 最小价格运费  
        /// </summary>
        public decimal SFreight { get; set; }
    }
}
