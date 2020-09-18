using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Model.Response.Admin.ShipCompanies;

namespace LianFa.ShopPlatform.Service
{
    public partial class ShipCompaniesService
    {
        /// <summary>
        /// 获得配送公司列表
        /// </summary>
        /// <param name="name">配送公司名称</param>
        /// <returns>配送公司列表</returns>
        public List<ShipCompanies> GetShipCompaniesList(string name)
        {
            return (from a in _shipCompaniesRepository.GetDbSetNoTracking()
                    select new ShipCompanies
                    {
                        Name = a.Name.Trim(),
                        ShipCoId = a.ShipCoId,
                        State = a.State,
                        DisplayOrder = a.DisplayOrder
                    }).WhereIf(m => m.Name.Contains(name), !string.IsNullOrEmpty(name))
                      .OrderByDescending(m => m.State)
                      .ThenByDescending(m => m.Name)
                      .ToList();

        }

    }
}
