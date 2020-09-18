using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.ShipAddress
{
    /// <summary>
    /// 删除收货地址请求类
    /// </summary>
    public class DelShipAddressRequest
    {
        /// <summary>
        /// 配送地址ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "配送id应大于1")]
        public int SaId { get; set; }
    }
}
