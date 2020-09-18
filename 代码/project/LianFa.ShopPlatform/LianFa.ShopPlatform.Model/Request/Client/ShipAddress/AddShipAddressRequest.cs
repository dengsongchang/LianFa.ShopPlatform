using LianFa.ShopPlatform.Model.Response.Client.ShipAddress;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Client.ShipAddress
{
    /// <summary>
    /// 添加收货地址请求类
    /// </summary>
    public class AddShipAddressRequest
    {
        /// <summary>  
        /// 区域id  
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "不是有效的区域Id")]
        public short RegionId { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        [StringLength(20, ErrorMessage = "收货人不是有效的字符长度", MinimumLength = 1)]
        public string Consignee { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(150, ErrorMessage = "地址不是有效的字符长度", MinimumLength = 1)]
        public string Address { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage = "手机号码不能为空")]
        public string Mobile { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        [Required(ErrorMessage = "标记是必须的")]
        public List<FlagInfo> Flag { get; set; }

        /// <summary>  
        /// 是否默认(0 -否 1 -是)  
        /// </summary>
        [Range(0, 1, ErrorMessage = "不是有效的状态")]
        public byte IsDefault { get; set; }
    }
}
