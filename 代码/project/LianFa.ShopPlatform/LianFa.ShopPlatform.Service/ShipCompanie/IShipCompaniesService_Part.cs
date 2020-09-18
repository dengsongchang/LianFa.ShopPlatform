using System.Collections.Generic;
using LianFa.ShopPlatform.Model.Response.Admin.ShipCompanies;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IShipCompaniesService
    {
        /// <summary>
        /// 获得配送公司列表
        /// </summary>
        /// <param name="name">配送公司名称</param>
        /// <returns>配送公司列表</returns>
        List<ShipCompanies> GetShipCompaniesList(string name);
    }
}
