using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.ShipAddress
{
    /// <summary>
    /// 用户信息响应类
    /// </summary>
    public class GetShipAddressResponse
    {
        /// <summary>
        /// 地址信息
        /// </summary>
        public ShipAddressInfo ShipAddressInfo { get; set; }
    }
}
