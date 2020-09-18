using AutoMapper;
using LianFa.ShopPlatform.Code.Helper;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Client.User;

namespace LianFa.ShopPlatform.Model.AutoMapper.Profiles.Client
{
    /// <summary>
    /// 用户信息映射规则
    /// </summary>
    public class UserInfoProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public UserInfoProfile()
        {
            CreateMap<LF_Users, UserCenterInfo>()
                .ForMember(dest => dest.AvatarStr,
                    opt => opt.MapFrom(src => CommonHelper.GetFileFullUrl(src.Avatar)));
        }
    }
}
