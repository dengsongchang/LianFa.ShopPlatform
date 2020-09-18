using AutoMapper;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.AdminOperateLog;

namespace LianFa.ShopPlatform.Model.AutoMapper.Profiles.Admin.OperateLog
{
    /// <summary>
    /// 操作日志列表转换规则
    /// </summary>
    public class AdminAdminOperateLogListProfle : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public AdminAdminOperateLogListProfle()
        {
            CreateMap<LF_AdminOperateLogs, AdminOperateLogInfo>()
                .ForMember(dest => dest.OperateTime,
                    opt => opt.MapFrom(src => src.OperateTime.ToDateTimeString(true)));
        }
    }
}
