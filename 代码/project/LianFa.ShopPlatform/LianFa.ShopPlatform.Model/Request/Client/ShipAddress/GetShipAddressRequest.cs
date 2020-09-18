using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.ShipAddress
{
    /// <summary>
    /// 获取用户信息请求类
    /// </summary>
    public class GetShipAddressRequest
    {
        /// <summary>
        /// 配送地址Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "配送地址Id大于1")]
        public int SaId { get; set; }
    }
}
