using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Admin.ShipCompanies
{
    /// <summary>
    /// 后台配送公司信息响应类
    /// </summary>
    public class AdminShipCompanieInfoResponse
    {
        /// <summary>
        /// 配送公司信息
        /// </summary>
        public LF_ShipCompanies ShipCompanieInfo { get; set; }
    }
}
