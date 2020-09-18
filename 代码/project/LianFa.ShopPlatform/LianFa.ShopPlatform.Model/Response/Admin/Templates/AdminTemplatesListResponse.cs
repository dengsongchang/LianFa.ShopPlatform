using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Templates
{
    /// <summary>
    /// 后台配送模板列表响应类
    /// </summary>
    public class AdminTemplatesListResponse
    {
        /// <summary>
        /// 配送模板列表
        /// </summary>
        public List<TemplatesListInfo> TemplatesList { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 配送模板列表信息类
    /// </summary>
    public class TemplatesListInfo
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
        /// 计价方式  1 按件计  2 按重量  3 按体积  
        /// </summary>
        public int ValuationMethod { get; set; }
        /// <summary>  
        /// 计价方式  1 按件计  2 按重量  3 按体积  
        /// </summary>
        public string ValuationMethodDesc { get; set; }
    }
}
