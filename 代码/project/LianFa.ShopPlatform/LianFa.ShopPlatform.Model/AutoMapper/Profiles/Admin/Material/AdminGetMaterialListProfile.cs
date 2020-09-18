using AutoMapper;
using HuCheng.Util.Core.Helper;
using LianFa.ShopPlatform.Model.Response.Admin.Material;

namespace LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin.Material
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminGetMaterialListProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public AdminGetMaterialListProfile()
        {
            CreateMap<MaterialModel, MaterialModel>()
                .ForMember(dest => dest.FileUrl,
                    opt => opt.MapFrom(src => FileHelper.GetFileFullUrl(src.FileUrl)));
        }
    }
}
