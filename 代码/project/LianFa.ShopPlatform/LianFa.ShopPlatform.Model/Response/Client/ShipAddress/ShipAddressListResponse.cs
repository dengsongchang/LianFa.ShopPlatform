using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.ShipAddress
{
    /// <summary>
    /// 收货地址列表响应类
    /// </summary>
    public class ShipAddressListResponse
    {
        /// <summary>
        /// 配送地址列表
        /// </summary>
        public List<ShipAddressPartInfo> ShipAddressList { get; set; }

    }

    /// <summary>
    /// 收货地址信息类
    /// </summary>
    public class ShipAddressPartInfo
    {
        /// <summary>  
    	/// 收货地址id  
    	/// </summary>
        public int SAId { get; set; }
        /// <summary>  
        /// 收货人  
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>  
        /// 收货人电话  
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 省名称 
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 县或区名称
        /// </summary>
        public string RName { get; set; }
        /// <summary>  
        /// 详细地址  
        /// </summary>
        public string Address { get; set; }
        /// <summary>  
        /// 是否为默认地址 ，0否，1是 
        /// </summary>
        public byte IsDefault { get; set; }
    }

    /// <summary>
    /// 收货地址信息类
    /// </summary>
    public class ShipAddressInfo : ShipAddressPartInfo
    {
        /// <summary>  
        /// 区域id  
        /// </summary>
        public short RegionId { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public List<FlagInfo> Flag { get; set; }
    }
    /// <summary>
    /// 标记信息类
    /// </summary>
    public class FlagInfo
    {
        /// <summary>
        /// 标记1
        /// </summary>
        public string Flag1 { get; set; }

        /// <summary>
        /// 标记2
        /// </summary>
        public string Flag2 { get; set; }

        /// <summary>
        /// 标记3
        /// </summary>
        public string Flag3 { get; set; }
    }
}