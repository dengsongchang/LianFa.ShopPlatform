using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.ShipCompanies
{
    /// <summary>
    /// 后台配送公司列表响应类
    /// </summary>
    public class AdminShipCompanieListResponse
    {
        /// <summary>
        /// 配送公司列表
        /// </summary>
        public List<ShipCompanies> ShipCompanieList { get; set; }
    }

    /// <summary>
    /// 后台配送公司信息类
    /// </summary>
    public class ShipCompanies
    {
        /// <summary>  
        /// 配送公司id  
        /// </summary>
        public int ShipCoId { get; set; }
        /// <summary>  
        /// 配送公司名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>  
        /// 状态  
        /// </summary>
        public int State { get; set; }
    }
}
