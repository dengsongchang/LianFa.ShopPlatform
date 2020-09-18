using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Admin;

namespace LianFa.ShopPlatform.Service
{
    public partial class AdminOperateLogsService
    {
        #region Fields

        private readonly IRepository<LF_Users> _usersRepository;

        private readonly IRepository<LF_AdminGroups> _adminGroupsRepository;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="adminOperateLogsRepository">商城管理日志仓储类</param>
        /// <param name="usersRepository">用户仓储类</param>
        /// <param name="adminGroupsRepository">管理员组仓储类</param>
        /// <param name="unitOfWork">unitOfWork</param>
        public AdminOperateLogsService(IRepository<LF_AdminOperateLogs> adminOperateLogsRepository, IRepository<LF_Users> usersRepository,
            IRepository<LF_AdminGroups> adminGroupsRepository, IUnitOfWork unitOfWork)
        {
            _adminOperateLogsRepository = adminOperateLogsRepository;
            _usersRepository = usersRepository;
            _adminGroupsRepository = adminGroupsRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        /// <summary>
        /// 后台获取管理员操作日志
        /// </summary>
        /// <param name="page">分页</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="uid">管理员Id</param>
        /// <param name="total">总行数</param>
        /// <returns></returns>
        public IEnumerable<LF_AdminOperateLogs> GetAdminOperateLogsList(PageModel page, DateTime? startTime, DateTime? endTime, int uid, out int total)
        {
            var data = _adminOperateLogsRepository.GetDbSetNoTracking()
                .WhereIf(a => a.OperateTime > startTime, startTime != null)
                .WhereIf(a => a.OperateTime < endTime, endTime != null)
                .WhereIf(a => a.Uid == uid, uid > 0)
                .OrderByDescending(a => a.OperateTime)
                .LoadPage(page, out total);
            return data;
        }

        /// <summary>
        /// 后台获取管理员列表
        /// </summary>
        /// <returns></returns>
        public List<AdminsInfo> GetAdminList()
        {
            var data = _adminOperateLogsRepository.GetDbSetNoTracking().Select(a => new AdminsInfo
            {
                UserName = a.NickName,
                UId = a.Uid
            })
            .Distinct()
            .ToList();
            return data;
        }

        /// <summary>
        /// 添加管理员操作日志(未提交)
        /// </summary>
        /// <param name="uId">用户Id</param>
        /// <param name="content">操作内容</param>
        /// <param name="ip">ip</param>
        public void AddAdminOperateLogs(int uId, string content, string ip)
        {
            var user = (from u in _usersRepository.GetDbSetNoTracking()
                        join g in _adminGroupsRepository.GetDbSetNoTracking() on u.AdminGId equals g.AdminGId
                        where u.UId == uId && u.AdminGId != (int)UserType.User
                        select new { u, g }).FirstOrDefault();

            if (user != null)
            {
                _adminOperateLogsRepository.Add(new LF_AdminOperateLogs
                {
                    Uid = uId,
                    AdminGId = user.u.AdminGId,
                    AdminGTitle = user.g.Title,
                    Description = $"{content}",
                    Ip = ip,
                    NickName = user.u.UserName,
                    Operation = content,
                    OperateTime = DateTime.Now
                });
                _unitOfWork.Commit();
            }
        }
    }
}
