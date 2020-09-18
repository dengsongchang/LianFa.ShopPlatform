using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Regions;
using LianFa.ShopPlatform.Model.Response.Admin.Templates;
using LianFa.ShopPlatform.Model.Response.Client.ShipAddress;
using System.Collections.Generic;
using LianFa.ShopPlatform.Model.Response.Client.Order;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IRegionsService
    {
        /// <summary>
        /// 通过父级id获得区域列表
        /// </summary>
        /// <param name="parentId">父id</param>
        /// <returns></returns>
        List<LF_Regions> GetRegionList(int parentId);

        /// <summary>
        /// 获得全部区域
        /// </summary>
        /// <returns></returns>
        List<LF_Regions> GetAllRegion();

        /// <summary>
        /// 根据名称，级别获取区域
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="layer">级别</param>
        /// <returns>区域表对象</returns>
        LF_Regions GetRegionByNameAndLayer(string name, int layer);

        /// <summary>
        /// 获取省/自治区/直辖市列表
        /// </summary>
        /// <returns>省/自治区/直辖市列表</returns>
        List<RegionsList> GetProvinceList();

        /// <summary>
        /// 根据区域id获取地级市列表
        /// </summary>
        /// <param name="id">区域id</param>
        /// <returns>地级市列表</returns>
        List<RegionsList> GetCityList(int id);

        /// <summary>
        /// 根据区域id获取市辖区/县(县市级)列表
        /// </summary>
        /// <param name="id">区域id</param>
        /// <returns>市辖区/县(县市级)列表</returns>
        List<RegionsList> GetMunicipalDistrictList(int id);


        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        List<RegionInfo> GetTreeRegionDataList();

        /// <summary>
        /// 根据筛选局域id获取层级区域列表
        /// </summary>
        /// <param name="regionIdList">区域id列表</param>
        /// <returns></returns>
        List<Regions> GetRegionDataListByIdList(List<short> regionIdList);

        /// <summary>
        /// 根据筛选局域id获取未筛选层级区域列表
        /// </summary>
        /// <param name="regionIdList">区域id列表</param>
        /// <returns></returns>
        List<Regions> GetNotRegionDataListByIdList(List<short> regionIdList);

        /// <summary>
        /// 根据筛选局域id获取展示筛选层级区域列表
        /// </summary>
        /// <param name="regionIdList">区域id列表</param>
        /// <returns></returns>
        List<Regions> GetShowRegionDataListByIdList(List<short> regionIdList);

        /// <summary>
        /// 校验省市区的级别
        /// </summary>
        /// <param name="regionId">区域id</param>
        /// <param name="layer">级别</param>
        /// <returns></returns>
        bool CheckRegionLayer(int regionId, int layer);

        /// <summary>
        /// 判断配送地址是否在配送范围内
        /// </summary>
        /// <param name="shipAddressInfo"></param>
        /// <param name="couponTypeId"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        bool VerifyShipAddress(FullShipAddressInfo shipAddressInfo, int couponTypeId);

        /// <summary>
        /// 判断配送地址是否在配送范围内
        /// </summary>
        /// <param name="region"></param>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        bool AdminVerifyShipAddress(int region, int couponTypeId);

        /// <summary>
        /// 根据省市区获取regionId
        /// </summary>
        /// <param name="provinceName"></param>
        /// <param name="cityName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        int AdminGetRegionId(string provinceName, string cityName, string name);
    }
}
