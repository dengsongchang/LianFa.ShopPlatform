using AutoMapper;
using HuCheng.Util.Core.Enums;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Model.Response.Admin.Templates;

namespace LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin.TimeProductActivity
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminTemplatesListProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public AdminTemplatesListProfile()
        {
            CreateMap<TemplatesListInfo, TemplatesListInfo>()
                .ForMember(dest => dest.ValuationMethodDesc,
                    opt => opt.MapFrom(src => Enuming.GetDescription((typeof(ValuationMethod)), src.ValuationMethod)));
        }
    }
}
