using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.ShipCompanies
{
    /// <summary>
    /// 后台编辑配送公司请求类
    /// </summary>
    public class AdminEditShipCompanieRequest
    {
        /// <summary>  
        /// 配送公司id  
        /// </summary>
        public int ShipCoId { get; set; }
        /// <summary>  
        /// 配送公司名称  
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(30, ErrorMessage = "名称长度不能大于30")]
        public string Name { get; set; }
    }
}
