using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using ShipAddressInfo = LianFa.ShopPlatform.Model.Response.Client.ShipAddress.ShipAddressInfo;
using ShipAddressPartInfo = LianFa.ShopPlatform.Model.Response.Client.ShipAddress.ShipAddressPartInfo;
using LianFa.ShopPlatform.Model.Response.Client.ShipAddress;
using LianFa.ShopPlatform.Model.Response.Client.User;

namespace LianFa.ShopPlatform.Service
{
    public partial class ShipAddressesService
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 区域仓储
        /// </summary>
        private readonly IRepository<LF_Regions> _regionsRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="regionsRepository">区域信息仓储类</param>
        public ShipAddressesService(IUnitOfWork unitOfWork, IRepository<LF_Regions> regionsRepository, IRepository<LF_ShipAddresses> shipAddressesRepository)
        {
            this._unitOfWork = unitOfWork;
            this._regionsRepository = regionsRepository;
            this._shipAddressesRepository = shipAddressesRepository;
        }

        /// <summary>
        /// 获得默认完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public FullShipAddressInfo GetDefaultFullShipAddress(int uid)
        {
            var fullShipAddressInfo = _shipAddressesRepository.Get(x => x.UId == uid && x.IsDefault == 1);
            if (fullShipAddressInfo == null)
                return null;
            return BuildFullShipAddressFromReader(fullShipAddressInfo);
        }

        /// <summary>
        /// 获得完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="saId">配送地址id</param>
        public FullShipAddressInfo GetFullShipAddressBySAId(int uid, int saId)
        {
            if (saId < 1)
                return null;

            var fullShipAddressInfo = _shipAddressesRepository.Get(x => x.UId == uid && x.SAId == saId);
            if (fullShipAddressInfo == null)
                return null;
            return BuildFullShipAddressFromReader(fullShipAddressInfo);
        }

        /// <summary>
        /// 构建完整用户配送地址信息
        /// </summary>
        public FullShipAddressInfo BuildFullShipAddressFromReader(LF_ShipAddresses model)
        {
            var regions = _regionsRepository.GetById(model.RegionId);
            FullShipAddressInfo fullShipAddressInfo = new FullShipAddressInfo();

            fullShipAddressInfo.SAId = model.SAId;
            fullShipAddressInfo.Uid = model.UId;
            fullShipAddressInfo.RegionId = model.RegionId;
            fullShipAddressInfo.IsDefault = model.IsDefault;
            fullShipAddressInfo.Mobile = model.Mobile;
            fullShipAddressInfo.Address = model.Address;
            fullShipAddressInfo.Consignee = model.Consignee;
            //fullShipAddressInfo.Alias = model.Alias;
            // fullShipAddressInfo.Phone = model.Phone;
            //fullShipAddressInfo.Email = model.Email;
            //fullShipAddressInfo.ZipCode = model.Zipcode;

            fullShipAddressInfo.ProvinceId = regions.ProvinceId;
            fullShipAddressInfo.ProvinceName = regions.ProvinceName.Trim();
            fullShipAddressInfo.CityId = regions.CityId;
            fullShipAddressInfo.CityName = regions.CityName.Trim();
            fullShipAddressInfo.CountyId = 0;
            fullShipAddressInfo.CountyName = regions.Name.Trim();

            return fullShipAddressInfo;
        }

        /// <summary>
        /// 获取用户的收货地址列表
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        public List<ShipAddressPartInfo> GetShipAddressList(int uid)
        {
            var data = (from shipAddressese in _shipAddressesRepository.GetDbSetNoTracking()
                        join regionse in _regionsRepository.GetDbSetNoTracking() on shipAddressese.RegionId equals regionse.RegionId
                        select new { shipAddressese, regionse }
                )
                .Where(a => a.shipAddressese.UId == uid);
            List<ShipAddressPartInfo> list = data.Select(a => new ShipAddressPartInfo
            {
                Address = a.shipAddressese.Address,
                CityName = a.regionse.CityName.Trim(),
                Consignee = a.shipAddressese.Consignee,
                IsDefault = a.shipAddressese.IsDefault,
                Mobile = a.shipAddressese.Mobile,
                ProvinceName = a.regionse.ProvinceName.Trim(),
                RName = a.regionse.Name.Trim(),
                SAId = a.shipAddressese.SAId
            }).OrderByDescending(m => m.IsDefault).ThenByDescending(m => m.SAId).ToList();
            return list;
        }

        /// <summary>
        /// 用户中心获取用户的收货地址列表
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        public List<UCenterAddressInfo> UCenterGetShipAddressList(int uid)
        {
            var data = (from shipAddressese in _shipAddressesRepository.GetDbSetNoTracking()
                    join regionse in _regionsRepository.GetDbSetNoTracking() on shipAddressese.RegionId equals regionse.RegionId
                    select new { shipAddressese, regionse }
                )
                .Where(a => a.shipAddressese.UId == uid);
            List<UCenterAddressInfo> list = data.Select(a => new UCenterAddressInfo
                {
                    Address = a.shipAddressese.Address,
                    SaId = a.shipAddressese.SAId,
                    Region = a.regionse.ProvinceName.Trim() + a.regionse.CityName.Trim() + a.regionse.Name.Trim(),
                    IsDefault = a.shipAddressese.IsDefault
                })
                .OrderByDescending(m => m.IsDefault)
                .ThenByDescending(m => m.SaId)
                .ToList();
            return list;
        }

        /// <summary>
        /// 获取用户地址信息
        /// </summary>
        /// <param name="sAId">配送地址Id</param>
        /// <returns></returns>
        public ShipAddressInfo GetShipAddressInfo(int sAId, int uId)
        {
            var data = (from shipAddressese in _shipAddressesRepository.GetDbSetNoTracking()
                        join regionse in _regionsRepository.GetDbSetNoTracking() on shipAddressese.RegionId equals regionse.RegionId
                        select new { shipAddressese, regionse }
                )
                .Where(a => a.shipAddressese.UId == uId)
                .FirstOrDefault(a => a.shipAddressese.SAId == sAId);

            if (data == null)
                return null;

            ShipAddressInfo info = new ShipAddressInfo()
            {
                Address = data.shipAddressese.Address,
                CityName = data.regionse.CityName.Trim(),
                Consignee = data.shipAddressese.Consignee,
                IsDefault = data.shipAddressese.IsDefault,
                Mobile = data.shipAddressese.Mobile,
                ProvinceName = data.regionse.ProvinceName.Trim(),
                RegionId = data.regionse.RegionId,
                RName = data.regionse.Name.Trim(),
                SAId = data.shipAddressese.SAId,
                Flag = new List<FlagInfo>()
            };

            if (data.shipAddressese.Flag.Split(',').Length > 2)
            {
                info.Flag.Add(new FlagInfo
                {
                    Flag1 = data.shipAddressese.Flag.Split(',')[0],
                    Flag2 = data.shipAddressese.Flag.Split(',')[1],
                    Flag3 = data.shipAddressese.Flag.Split(',')[2]
                });
            }
            return info;
        }

        /// <summary>
        /// 获取当前用户收货地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public UserDefaultAddresses GetUserDefaultAddresses(int uid)
        {
            var data = _shipAddressesRepository.GetDbSetNoTracking()
                .Where(x => x.UId == uid)
                .Select(s => new UserDefaultAddresses
                {
                    Mobile = s.Mobile,
                    Consignee = s.Consignee,
                    SAId = s.SAId,
                    Addresses = s.Address,
                }).ToList();
            return data.Count > 0 ? data.FirstOrDefault() : null;
        }
    }
}

