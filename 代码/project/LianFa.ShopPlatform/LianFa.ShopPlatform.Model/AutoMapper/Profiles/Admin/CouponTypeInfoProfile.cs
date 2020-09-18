using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Helper;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Coupon;

namespace LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin
{
    /// <summary>
    /// 礼品卡类型详情映射规则
    /// </summary>
    public class CouponTypeInfoProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public CouponTypeInfoProfile()
        {
            CreateMap<LF_CouponTypes, CouponTypeDetailInfo>()
                .ForMember(dest => dest.UseStartTimes,
                    opt => opt.MapFrom(src => src.UseStartTime.ToDateTimeString(false)))
                .ForMember(dest => dest.UseEndTimes,
                    opt => opt.MapFrom(src => src.UseEndTime.ToDateTimeString(false)))
                .ForMember(dest => dest.CouponImgFull,
                    opt => opt.MapFrom(src => CommonHelper.GetFileFullUrl(src.CouponImg)))
                .ForMember(dest => dest.ProductImgFull,
                    opt => opt.MapFrom(src => CommonHelper.GetFileFullUrl(src.ProductImg)));
        }
    }
}
