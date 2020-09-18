using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Service
{
    public partial class CouponsService
    {
        /// <summary>
        /// 礼品卡类型仓储类
        /// </summary>
        private readonly IRepository<LF_CouponTypes> _couponTypesRepository;

        /// <summary>
        /// 用户仓储类
        /// </summary>
        private readonly IRepository<LF_Users> _usersRepository;

        public CouponsService(IRepository<LF_Coupons> couponsRepository, IRepository<LF_CouponTypes> couponTypesRepository,
            IRepository<LF_Users> usersRepository)
        {
            _couponTypesRepository = couponTypesRepository;
            _couponsRepository = couponsRepository;
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// 后台获取礼品卡列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="couponSn"></param>
        /// <param name="state"></param>
        /// <param name="name"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CouponInfo> GetCouponList(PageModel page, string couponSn, int state, string name,out int total)
        {
            var data = _couponsRepository.GetDbSetNoTracking()
                .WhereIf(c => c.CouponSn.Contains(couponSn), !string.IsNullOrEmpty(couponSn))
                .WhereIf(c => c.State == state, state <= (int) CouponState.Due)
                .Join(_couponTypesRepository.GetDbSetNoTracking(), x => x.CouponTypeId, y => y.CouponTypeId,
                    (x, y) => new CouponInfo
                    {
                        CouponId = x.CouponId,
                        CouponSn = x.CouponSn,
                        Password = x.PassWord,
                        Name = y.Name,
                        AddTime = x.AddTime,
                        UseTime = x.UseTime,
                        State = x.State,
                        UseEndTime = x.UseEndTime
                    })
                    .WhereIf(a => a.Name.Contains(name),!string.IsNullOrEmpty(name))
                .OrderByDescending(x => x.CouponId)
                .LoadPage(page, out total)
                .ToList();
            if (!data.Any())
                return null;
            foreach (var coupon in data)
            {
                coupon.AddTimes = coupon.AddTime.ToDateString();
                coupon.UseEndTimes = coupon.UseEndTime.ToDateTimeString();
                coupon.UseTimes = coupon.UseTime.ToDateString() == "1900-01-01" ? "-" : coupon.UseTime.ToDateString();
                coupon.StateDec = ((CouponState) coupon.State).GetDescription();
            }
            return data;
        }

        /// <summary>
        /// 礼品卡获取今日兑换数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCouponsUsedByTime(DateTime startTime, DateTime endTime)
        {
            return _couponsRepository.GetDbSetNoTracking().Where(u=>u.State==(byte)WhetherType.Yes)
                .Count(m => m.UseTime >= startTime && m.UseTime <= endTime);
        }

        /// <summary>
        /// 卡片总数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCoupons()
        {
           return _couponsRepository.Count();
        }

        /// <summary>
        /// 卡片兑换总数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCouponsIsUsed()
        {
            return _couponsRepository.GetDbSetNoTracking().Count(u => u.State == (byte)WhetherType.Yes);
        }
        /// <summary>
        /// 后台获取管理员兑换礼品卡列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="couponSn"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<AdminCouponInfo> GetAdminCouponList(PageModel page, string couponSn, out int total)
        {
            var data = _couponsRepository.GetDbSetNoTracking()
                .Where(c => c.UseUId > 0)
                .WhereIf(c => c.CouponSn.Contains(couponSn), !string.IsNullOrEmpty(couponSn))
                .Join(_usersRepository.GetDbSetNoTracking().Where(u => u.AdminGId != (byte) UserType.User),
                    x => x.UseUId, y => y.UId, (x, y) => new
                    {
                        x.CouponTypeId,
                        x.CouponId,
                        x.CouponSn,
                        x.AddTime,
                        x.UseTime,
                        x.OId,
                        y.UserName
                    })
                .Join(_couponTypesRepository.GetDbSetNoTracking(), x => x.CouponTypeId, y => y.CouponTypeId,
                    (x, y) => new AdminCouponInfo
                    {
                        CouponId = x.CouponId,
                        CouponSn = x.CouponSn,
                        Name = y.Name,
                        AddTime = x.AddTime,
                        UseTime = x.UseTime,
                        OId = x.OId,
                        AdminUser = x.UserName
                    })
                    .OrderByDescending(x => x.CouponId)
                .LoadPage(page, out total)
                .ToList();
            if (!data.Any())
                return null;
            foreach (var coupon in data)
            {
                coupon.AddTimes = coupon.AddTime.ToDateString();
                coupon.UseTimes = coupon.UseTime.ToDateString();
            }
            return data;
        }

    }
}
