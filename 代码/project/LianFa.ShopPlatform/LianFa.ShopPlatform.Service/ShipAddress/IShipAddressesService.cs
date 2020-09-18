using System.Collections.Generic;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Model.Response.Client.ShipAddress;
using LianFa.ShopPlatform.Model.Response.Client.User;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IShipAddressesService
    {
        /// <summary>
        /// 获得完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="saId">配送地址id</param>
        FullShipAddressInfo GetFullShipAddressBySAId(int uid, int saId);

        /// <summary>
        /// 获得默认完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        FullShipAddressInfo GetDefaultFullShipAddress(int uid);

        /// <summary>
        /// 获取用户的收货地址列表
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        List<ShipAddressPartInfo> GetShipAddressList(int uid);

        /// <summary>
        /// 用户中心获取用户的收货地址列表
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        List<UCenterAddressInfo> UCenterGetShipAddressList(int uid);

        /// <summary>
        /// 获取用户地址信息
        /// </summary>
        /// <param name="sAId">配送地址Id</param>
        /// <returns></returns>
        Model.Response.Client.ShipAddress.ShipAddressInfo GetShipAddressInfo(int sAId, int uId);

        /// <summary>
        /// 获取当前用户收货地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        UserDefaultAddresses GetUserDefaultAddresses(int uid);

    }
}
