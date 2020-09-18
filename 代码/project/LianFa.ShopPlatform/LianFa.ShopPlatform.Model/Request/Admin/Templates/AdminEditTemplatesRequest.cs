using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Templates
{
    /// <summary>
    /// 后台编辑运费模板请求类
    /// </summary>
    public class AdminEditTemplatesRequest
    {
        /// <summary>  
        /// 配送模板id  
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>  
        /// 模板名称  
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string TemplateName { get; set; }
        /// <summary>  
        /// 是否包邮    true 自定义 false 包邮
        /// </summary>
        public bool IsfreeShipping { get; set; }
        /// <summary>  
        /// 计价方式  1 按件计  2 按重量  3 按体积  
        /// </summary>
        public int ValuationMethod { get; set; }
        /// <summary>  
        /// 起步重量  
        /// </summary>
        [Required(ErrorMessage = "默认数量不能为空")]
        public int DefaultNumber { get; set; }
        /// <summary>  
        /// 默认起步价  
        /// </summary>
        [Required(ErrorMessage = "默认运费不能为空")]
        public decimal Price { get; set; }
        /// <summary>  
        /// 加价重量  
        /// </summary>
        public int AddNumber { get; set; }
        /// <summary>  
        /// 加价  
        /// </summary>
        public decimal AddPrice { get; set; }

        /// <summary>
        /// 配送地区价格列表
        /// </summary>
        public List<AddShippingRegionsGroups> ShippingRegionsGroupsList { get; set; }

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
        /// 最小价格  
        /// </summary>
        public decimal SPrice { get; set; }
        /// <summary>  
        /// 最小价格运费  
        /// </summary>
        public decimal SFreight { get; set; }

        /// <summary>
        /// 指定包邮地区列表
        /// </summary>
        public List<AddShippingAppointRegions> ShippingAppointRegionsList { get; set; }

        /// <summary>  
        /// 是否指定包邮  true 是 false 否  
        /// </summary>
        public bool IsAppoint { get; set; }
    }
}
