using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;
using LianFa.ShopPlatform.Model.Response.Client.IndexData;

namespace LianFa.ShopPlatform.Service
{
    public partial class CouponTypesService
    {
        /// <summary>
        /// 礼品卡仓储类
        /// </summary>
        private readonly IRepository<LF_Coupons> _couponRepository;

        /// <summary>
        /// 礼品卡仓储类
        /// </summary>
        private readonly IRepository<LF_CouponDeliveryAreas> _couponDeliveryAreasRepository;

        public CouponTypesService(IRepository<LF_CouponTypes> couponTypesRepository, IRepository<LF_Coupons> couponRepository,
            IRepository<LF_CouponDeliveryAreas> couponDeliveryAreasRepository)
        {
            _couponTypesRepository = couponTypesRepository;
            _couponRepository = couponRepository;
            _couponDeliveryAreasRepository = couponDeliveryAreasRepository;
        }

        /// <summary>
        /// 首页礼品卡列表
        /// </summary>
        /// <returns></returns>
        public List<CouponListInfo> CouponTypeList(PageModel page, out int total)
        {
            var list = _couponTypesRepository.GetDbSetNoTracking()
                .Where(d => d.State == (byte) WhetherType.Yes)
                .Select(d => new CouponListInfo
                {
                    CouponTypeId = d.CouponTypeId,
                    CouponImg = d.CouponImg,
                    Name = d.Name
                })
                .OrderByDescending(x => x.CouponTypeId)
                .LoadPage(page, out total)
                .ToList();

            if (!list.Any())
                return null;

            list.ForEach(m => m.CouponImg = FileHelper.GetFileFullUrl(m.CouponImg));

            return list;
        }

        /// <summary>
        /// 后台获取礼品卡类型列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="couponName"></param>
        /// <param name="areaId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CouponTypeInfo> GetCouponTypeList(PageModel page, string couponName, int areaId, out int total)
        {
            var data = _couponTypesRepository.GetDbSetNoTracking()
                .WhereIf(c => c.Name.Contains(couponName), !string.IsNullOrEmpty(couponName))
                .WhereIf(c => c.DeliveryAreaId == areaId, areaId > 0)
                .Join(_couponDeliveryAreasRepository.GetDbSetNoTracking(), x => x.DeliveryAreaId, y => y.DeliveryAreaId,
                    (x, y) => new CouponTypeInfo
                    {
                        CouponTypeId = x.CouponTypeId,
                        Name = x.Name,
                        CouponTypeSn = x.CouponTypeSn,
                        Money = x.Money,
                        DeliveryArea = y.Name,
                        State = x.State
                    })
                .OrderByDescending(x => x.CouponTypeId)
                .LoadPage(page, out total)
                .ToList();

            if (!data.Any())
                return null;

            foreach (var coupon in data)
            {
                coupon.StateDec = ((CouponTypeState) coupon.State).GetDescription();
            }
            return data;
        }

        /// <summary>
        /// 后台获取礼品卡类型列表
        /// </summary>
        /// <returns></returns>
        public List<MiniCouponTypeInfo> GetMiniCouponTypeList()
        {
            return _couponTypesRepository.GetDbSetNoTracking()
                .OrderByDescending(x => x.AddTime)
                .Select(x => new MiniCouponTypeInfo
                {
                    CouponTypeId = x.CouponTypeId,
                    Name = x.Name
                })
                .ToList();
        }

        /// <summary>
        /// 获取礼品卡类型详情
        /// </summary>
        /// <param name="couponTypeId"></param>
        /// <returns></returns>
        public CouponTypeDetailInfo AdminGetCouponTypeDetail(int couponTypeId)
        {
            var coupon = _couponTypesRepository.LoadEntitiesNoTracking(c => c.CouponTypeId == couponTypeId).FirstOrDefault();
            //映射数据
            var info = coupon?.MapTo<CouponTypeDetailInfo, CouponTypeInfoProfile>();
            return info;
        }
    }
}
