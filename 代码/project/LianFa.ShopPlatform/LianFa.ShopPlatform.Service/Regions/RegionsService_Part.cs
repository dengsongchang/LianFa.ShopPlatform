using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Regions;
using LianFa.ShopPlatform.Model.Response.Client.ShipAddress;
using LianFa.ShopPlatform.Code.Cache;
using LianFa.ShopPlatform.Model.Response.Admin.Templates;
using LianFa.ShopPlatform.Model.Response.Client.Order;

namespace LianFa.ShopPlatform.Service
{
    public partial class RegionsService
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 收货地址
        /// </summary>
        private readonly IRepository<LF_ShipAddresses> _shipAddressesRepository;

        /// <summary>
        /// 收货地址
        /// </summary>
        private readonly IRepository<LF_CouponTypes> _couponTypesRepository;

        /// <summary>
        /// 收货地址
        /// </summary>
        private readonly IRepository<LF_CouponDeliveryAreas> _couponDeliveryAreasRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionsRepository">区域信息仓储类</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="shipAddressesRepository">收货地址仓储类</param>
        /// <param name="couponTypesRepository">礼品券类型仓储类</param>
        /// <param name="couponDeliveryAreasRepository">礼品券配送区域仓储类</param>
        public RegionsService(IRepository<LF_Regions> regionsRepository, IUnitOfWork unitOfWork, IRepository<LF_ShipAddresses> shipAddressesRepository,
            IRepository<LF_CouponTypes> couponTypesRepository, IRepository<LF_CouponDeliveryAreas> couponDeliveryAreasRepository)
        {
            this._regionsRepository = regionsRepository;
            this._unitOfWork = unitOfWork;
            this._shipAddressesRepository = shipAddressesRepository;
            this._couponTypesRepository = couponTypesRepository;
            this._couponDeliveryAreasRepository = couponDeliveryAreasRepository;
        }

        /// <summary>
        /// 通过父级id获得区域列表
        /// </summary>
        /// <param name="parentId">父id</param>
        /// <returns></returns>
        public List<LF_Regions> GetRegionList(int parentId)
        {
            return _regionsRepository.GetList(m => m.ParentId == parentId).OrderBy(m => m.DisplayOrder).ToList();
        }

        /// <summary>
        /// 获得全部区域
        /// </summary>
        /// <returns></returns>
        public List<LF_Regions> GetAllRegion()
        {
            return _regionsRepository.GetList(m => m.RegionId > 0).OrderBy(m => m.DisplayOrder).ToList();
        }

        /// <summary>
        /// 根据名称，级别获取区域
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="layer">级别</param>
        /// <returns>区域表对象</returns>
        public LF_Regions GetRegionByNameAndLayer(string name, int layer)
        {
            return _regionsRepository.GetDbSetNoTracking().
                FirstOrDefault(m => name.Contains(m.Name.TrimEnd()) &&
                                    m.Layer == layer &&
                                    name.Contains(m.CityName.TrimEnd()));
        }

        /// <summary>
        /// 获取省/自治区/直辖市列表
        /// </summary>
        /// <returns>省/自治区/直辖市列表</returns>
        public List<RegionsList> GetProvinceList()
        {
            return (from r in _regionsRepository.GetDbSetNoTracking()
                    select new RegionsList
                    {
                        RegionId = r.RegionId,
                        Name = r.Name,
                        Layer = r.Layer,
                        ParentId = r.ParentId
                    }).Where(m => m.Layer == 1)
                .ToList();
        }

        /// <summary>
        /// 根据区域id获取地级市列表
        /// </summary>
        /// <param name="id">区域id</param>
        /// <returns>地级市列表</returns>
        public List<RegionsList> GetCityList(int id)
        {
            return (from r in _regionsRepository.GetDbSetNoTracking()
                    select new RegionsList
                    {
                        RegionId = r.RegionId,
                        Name = r.Name,
                        Layer = r.Layer,
                        ParentId = r.ParentId
                    }).Where(m => m.Layer == 2 && m.ParentId == id)
                .ToList();
        }

        /// <summary>
        /// 根据区域id获取市辖区/县(县市级)列表
        /// </summary>
        /// <param name="id">区域id</param>
        /// <returns>市辖区/县(县市级)列表</returns>
        public List<RegionsList> GetMunicipalDistrictList(int id)
        {
            return (from r in _regionsRepository.GetDbSetNoTracking()
                    select new RegionsList
                    {
                        RegionId = r.RegionId,
                        Name = r.Name,
                        Layer = r.Layer,
                        ParentId = r.ParentId
                    }).Where(m => m.Layer == 3 && m.ParentId == id)
                .ToList();
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public List<RegionInfo> GetTreeRegionDataList()
        {
            //查找缓存数据
            var regionList = CacheHelper.GetCache(CacheKeys.REGION_LIST) as List<RegionInfo>;

            if (regionList == null)
            {
                //获取数据
                var allRegionList = _regionsRepository.LoadEntitiesNoTracking(m => m.RegionId > 0)
                    .Select(a => new RegionInfo
                    {
                        code = a.RegionId,
                        name = a.Name.Trim(),
                        PId = a.ParentId,
                    }).ToList();

                //获取第一级数据
                regionList = allRegionList
                    .Where(a => a.PId == 0)
                    .ToList();

                //遍历获取子元素，已知层级数固定为3
                foreach (var item in regionList)
                {
                    //第二级
                    item.sub = allRegionList.Where(a => a.PId == item.code).ToList();

                    //第三级
                    foreach (var temp in item.sub)
                    {
                        temp.sub = allRegionList.Where(a => a.PId == temp.code).ToList();
                    }
                }

                //设置缓存,永久缓存
                CacheHelper.SetCache(CacheKeys.REGION_LIST, regionList, CacheKeys.YearCacheTime);
            }

            //返回列表
            return regionList;
        }

        /// <summary>
        /// 根据筛选局域id获取层级区域列表
        /// </summary>
        /// <param name="regionIdList">区域id列表</param>
        /// <returns></returns>
        public List<Regions> GetRegionDataListByIdList(List<short> regionIdList)
        {
            //定义存放id对应的上级id列表
            var supRId =
                (from regionId in regionIdList
                 where _regionsRepository.Exist(m => m.RegionId == regionId)
                 select _regionsRepository.GetById(regionId).ParentId).Where(m => !m.Equals(0))
                .ToList();

            //拿到最终的最上级省id
            var firstRId = _regionsRepository.GetList(m => supRId.Contains(m.RegionId) && m.ParentId > 0)
                .Select(m => m.ParentId)
                .ToList();

            //获得当前id列表对应的所有下级区域id
            var allRegionIdList = _regionsRepository.GetList(m => regionIdList.Contains(m.ParentId))
                .Select(m => m.RegionId)
                .ToList();

            //获得当前id列表所有省id下的城市id列表
            var cityRegionIdList = _regionsRepository.GetList(m => regionIdList.Contains(m.ParentId) && m.Layer == 2)
                .Select(m => m.RegionId)
                .ToList();

            //城市id列表包含的地级市id列表
            var lastRegionIdList = _regionsRepository.GetList(m => cityRegionIdList.Contains(m.ParentId))
                .Select(m => m.RegionId)
                .ToList();

            //全部组合为最新的区域第列表
            var newRegionIdList = new List<short>();
            newRegionIdList.AddRange(firstRId);
            newRegionIdList.AddRange(supRId);
            newRegionIdList.AddRange(regionIdList);
            newRegionIdList.AddRange(allRegionIdList);
            newRegionIdList.AddRange(lastRegionIdList);

            //根据筛选的区域id获取全部的区域列表
            var regionList = _regionsRepository.GetDbSetNoTracking()
                .Where(m => newRegionIdList.Contains(m.RegionId))
                .Select(a => new Regions
                {
                    RegionId = a.RegionId,
                    Name = a.Name,
                    PId = a.ParentId
                })
                .OrderBy(m => m.RegionId)
                .ToList();

            //获取分层后的列表
            //拿到第一级省id列表
            var regionListByIdList = regionList
                .Where(a => a.PId == 0)
                .ToList();

            //遍历获取子元素，已知层级数固定为3
            foreach (var item in regionListByIdList)
            {
                //第二级
                item.Sub = regionList.Where(a => a.PId == item.RegionId).ToList();

                //第三级
                foreach (var temp in item.Sub)
                {
                    temp.Sub = regionList.Where(a => a.PId == temp.RegionId).ToList();
                }
            }

            //返回列表
            return regionListByIdList;
        }

        /// <summary>
        /// 根据筛选局域id获取未筛选层级区域列表
        /// </summary>
        /// <param name="regionIdList">区域id列表</param>
        /// <returns></returns>
        public List<Regions> GetNotRegionDataListByIdList(List<short> regionIdList)
        {
            //获得当前id列表对应的所有下级区域id
            var allRegionIdList = _regionsRepository.GetList(m => regionIdList.Contains(m.ParentId)).Select(m => m.RegionId).ToList();

            //获得当前id列表所有省id下的城市id列表
            var cityRegionIdList = _regionsRepository.GetList(m => regionIdList.Contains(m.ParentId) && m.Layer == 2).Select(m => m.RegionId).ToList();

            //城市id列表包含的地级市id列表
            var lastRegionIdList = _regionsRepository.GetList(m => cityRegionIdList.Contains(m.ParentId)).Select(m => m.RegionId).ToList();

            //全部组合为最新的区域第列表
            var newRegionIdList = new List<short>();
            newRegionIdList.AddRange(regionIdList);
            newRegionIdList.AddRange(allRegionIdList);
            newRegionIdList.AddRange(lastRegionIdList);

            //根据筛选的区域id获取全部的区域列表
            var regionList = _regionsRepository.GetDbSetNoTracking().Where(m => !newRegionIdList.Contains(m.RegionId))
                .Select(a => new Model.Response.Admin.Templates.Regions
                {
                    RegionId = a.RegionId,
                    Name = a.Name,
                    PId = a.ParentId
                }).OrderBy(m => m.RegionId).ToList();

            //获取分层后的列表
            //拿到第一级省id列表
            var regionListByIdList = regionList
                .Where(a => a.PId == 0)
                .ToList();

            //遍历获取子元素，已知层级数固定为3
            foreach (var item in regionListByIdList)
            {
                //第二级
                item.Sub = regionList.Where(a => a.PId == item.RegionId).ToList();

                //第三级
                foreach (var temp in item.Sub)
                {
                    temp.Sub = regionList.Where(a => a.PId == temp.RegionId).ToList();
                }
            }

            //返回列表
            return regionListByIdList;
        }

        /// <summary>
        /// 根据筛选局域id获取展示筛选层级区域列表
        /// </summary>
        /// <param name="regionIdList">区域id列表</param>
        /// <returns></returns>
        public List<Regions> GetShowRegionDataListByIdList(List<short> regionIdList)
        {
            /*对于传递参数省id处理*/

            //获得传递参数的省id列表
            var firstRId = _regionsRepository.GetDbSetNoTracking()
                .Where(m => regionIdList.Contains(m.RegionId) && m.ParentId == 0 && m.Layer == 1)
                .Select(m => m.RegionId)
                .ToList();

            List<short> ruleSecondRId;
            //如果有省id，则不需要下级城市id及区id
            //获得省id下的市id
            if (firstRId.Count > 0)
            {
                ruleSecondRId = _regionsRepository.GetDbSetNoTracking()
                    .Where(m => firstRId.Contains(m.ParentId) && m.Layer == 2)
                    .Select(m => m.RegionId)
                    .ToList();
            }
            else
            {
                ruleSecondRId = new List<short>();
            }

            List<short> ruleThirdRId;
            //获得市id下的区id
            if (ruleSecondRId.Count > 0)
            {
                ruleThirdRId = _regionsRepository.GetDbSetNoTracking()
                    .Where(m => ruleSecondRId.Contains(m.ParentId) && m.Layer == 3)
                    .Select(m => m.RegionId)
                    .ToList();
            }
            else
            {
                ruleThirdRId = new List<short>();
            }

            //整理不显示的市id和区id
            var hideRId = new List<short>();
            hideRId.AddRange(ruleSecondRId);
            hideRId.AddRange(ruleThirdRId);

            /*对于传递参数市id处理*/
            //排除不显示的市id和区id
            var newRId = hideRId.Count > 0 ? regionIdList.Except(hideRId).ToList() : regionIdList;

            //获得传递参数的市id列表
            var secondRId = _regionsRepository.GetDbSetNoTracking()
                .Where(m => newRId.Contains(m.RegionId) && m.Layer == 2)
                .Select(m => m.RegionId)
                .ToList();

            //如果有单独的市id，则不需要下级区id
            List<short> ruleNewThirdRId;


            if (secondRId.Count > 0)
            {
                //获得市id下的区id
                ruleNewThirdRId = _regionsRepository.GetDbSetNoTracking()
                    .Where(m => secondRId.Contains(m.ParentId) && m.Layer == 3)
                    .Select(m => m.RegionId)
                    .ToList();
            }
            else
            {
                ruleNewThirdRId = new List<short>();
            }
            //排除不显示的区id
            newRId = hideRId.Count > 0 ? newRId.Except(ruleNewThirdRId).ToList() : newRId;

            //获得市id对应的省id,用于外层显示
            var newPrId = _regionsRepository.GetList(m => secondRId.Contains(m.RegionId)).Select(m => m.ParentId).ToList();

            /*对于传递参数区id处理*/
            //获取传递参数的区id
            var thirdRId = _regionsRepository.GetDbSetNoTracking()
                .Where(m => newRId.Contains(m.RegionId) && m.Layer == 3)
                .Select(m => m.RegionId)
                .ToList();

            if (thirdRId.Count <= 0) return null;
            {
                var secondPId = _regionsRepository.GetDbSetNoTracking()
                    .Where(m => newRId.Contains(m.RegionId) && m.Layer == 3)
                    .Select(m => m.ParentId)
                    .ToList();

                var firstPId = _regionsRepository.GetDbSetNoTracking()
                    .Where(m => secondPId.Contains(m.RegionId) && m.Layer == 2)
                    .Select(m => m.ParentId)
                    .ToList();

                secondRId.AddRange(secondPId);
                firstRId.AddRange(firstPId);
            }
            //整理全部省id
            firstRId.AddRange(newPrId);

            //合并全部区域id
            var rId = new List<short>();
            rId.AddRange(firstRId);
            rId.AddRange(secondRId);
            rId.AddRange(thirdRId);

            rId = rId.Distinct().OrderBy(m => m).ToList();

            //根据筛选的区域id获取全部的区域列表
            var regionList = _regionsRepository.GetDbSetNoTracking().Where(m => rId.Contains(m.RegionId))
                .Select(a => new Regions
                {
                    RegionId = a.RegionId,
                    Name = a.Name,
                    PId = a.ParentId
                }).OrderBy(m => m.RegionId).ToList();

            //获取分层后的列表
            //拿到第一级省id列表
            var regionListByIdList = regionList
                .Where(a => a.PId == 0)
                .ToList();

            //遍历获取子元素，已知层级数固定为3
            foreach (var item in regionListByIdList)
            {
                //第二级
                item.Sub = regionList.Where(a => a.PId == item.RegionId).ToList();

                //第三级
                foreach (var temp in item.Sub)
                {
                    temp.Sub = regionList.Where(a => a.PId == temp.RegionId).ToList();
                }
            }

            return regionListByIdList;
        }

        /// <summary>
        /// 校验省市区的级别
        /// </summary>
        /// <param name="regionId">区域id</param>
        /// <param name="layer">级别</param>
        /// <returns></returns>
        public bool CheckRegionLayer(int regionId, int layer)
        {
            return _regionsRepository.LoadEntitiesNoTracking(a => a.RegionId == regionId && a.Layer == 3) != null;
        }

        /// <summary>
        /// 判断配送地址是否在配送范围内
        /// </summary>
        /// <param name="sAId"></param>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        public bool VerifyShipAddress(FullShipAddressInfo sAId, int couponTypeId)
        {
            //获取礼品卡类型
            var couponType = _couponTypesRepository.LoadEntitiesNoTracking(y => y.CouponTypeId == couponTypeId);

            //获取礼品卡配送区域编码
            var areaSn = couponType.FirstOrDefault()?.DeliveryAreaSn;

            if (string.IsNullOrEmpty(areaSn))
                return false;

            //全国配送
            if (areaSn.Contains("L2"))
                return true;

            //获取广东省深圳市的区域id
            var shenzhenRegionId = _regionsRepository
                .LoadEntitiesNoTracking(x => x.ProvinceName == "广东省" && x.CityName == "深圳市")
                .Select(x => x.RegionId)
                .ToList();

            if(!shenzhenRegionId.Any())
                return false;

            //深圳区域配送
            if (shenzhenRegionId.Contains((short)sAId.RegionId))
                return true;

            return false;
        }

        /// <summary>
        /// 判断配送地址是否在配送范围内
        /// </summary>
        /// <param name="region"></param>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        public bool AdminVerifyShipAddress(int region, int couponTypeId)
        {
            //获取礼品卡类型
            var couponType = _couponTypesRepository.LoadEntitiesNoTracking(y => y.CouponTypeId == couponTypeId);

            var areaId = couponType.FirstOrDefault()?.DeliveryAreaId ?? 0;

            //获取礼品卡类型配送范围
            var regionArea = _couponDeliveryAreasRepository.LoadEntitiesNoTracking(y => y.DeliveryAreaId == areaId).FirstOrDefault();

            if (regionArea == null)
                return false;

            //全国配送
            if (regionArea.RegionIds == "0" || string.IsNullOrEmpty(regionArea.RegionIds))
                return true;

            //部分区域配送
            if (regionArea.RegionIds.Contains(region.ToString()))
                return true;

            return false;
        }

        /// <summary>
        /// 根据省市区获取regionId
        /// </summary>
        /// <param name="provinceName"></param>
        /// <param name="cityName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int AdminGetRegionId(string provinceName, string cityName, string name)
        {
            var regionInfo = _regionsRepository
                .GetDbSetNoTracking()
                .FirstOrDefault(x => x.ProvinceName.Contains(provinceName) && x.CityName.Contains(cityName) && x.Name.Contains(name));
            if (regionInfo == null)
                return 0;
            return regionInfo.RegionId;

        }
    }
}
