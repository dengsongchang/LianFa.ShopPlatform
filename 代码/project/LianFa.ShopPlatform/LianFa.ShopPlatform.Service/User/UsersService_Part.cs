using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using HuCheng.Util.AutoMapper;
using LianFa.ShopPlatform.DataBase;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Encrypts;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Enums;
using HuCheng.Util.Datas.EntityFramework;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Model.AutoMapper.Profiles.Client;
using LianFa.ShopPlatform.Model.Response.Client.User;
using LianFa.ShopPlatform.Model.Response.Admin.User;
using LianFa.ShopPlatform.Model.Response.Admin.Admin;
using LianFa.ShopPlatform.Model.Response.Admin.Statistics;

namespace LianFa.ShopPlatform.Service
{
    public partial class UsersService
    {
        #region Fields
        private readonly IRepository<LF_AdminGroups> _adminGroupsRepository;
        private readonly IRepository<LF_Orders> _ordersRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="adminGroupsRepository"></param>
        public UsersService(IRepository<LF_Users> usersRepository, IRepository<LF_AdminGroups> adminGroupsRepository, IRepository<LF_Orders> ordersRepository)
        {
            this._usersRepository = usersRepository;
            this._adminGroupsRepository = adminGroupsRepository;
            _ordersRepository = ordersRepository;
        }
        #endregion

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 创建用户密码
        /// </summary>
        /// <param name="password">真实密码</param>
        /// <param name="salt">散列盐值</param>
        /// <returns>密码</returns>
        public string CreateUserPassword(string password, string salt)
        {
            return Md5Encrypt.Md5By32(password + salt);
        }

        /// <summary>
        /// 后台通过用户名获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public LF_Users GetUserInfoByName(string userName)
        {
            return _usersRepository.Get(a => a.UserName == userName);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public LF_Users GetPartUserByName(string userName)
        {
            return _usersRepository.LoadEntitiesNoTracking(m => m.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public LF_Users GetPartUserByMobile(string mobile)
        {
            return _usersRepository.LoadEntitiesNoTracking(m => m.Mobile == mobile).FirstOrDefault();
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <returns>用户分页列表</returns>
        public PagerList<LF_Users> GetUserList(Pager pager)
        {
            return _usersRepository.GetDbSetNoTracking()
                .Where(m => m.AdminGId == (int) UserType.User)
                .OrderByDescending(m => m.UId)
                .LoadPagerEntities(pager).ToPageList(pager);
        }

        /// <summary>
        /// 获取会员详情
        /// </summary>
        /// <returns></returns>
        public UserCenterInfo GetUserInfo(int uId)
        {
            var user = _usersRepository.LoadEntitiesNoTracking(x => x.UId == uId).FirstOrDefault();

            //映射数据
            var info = user?.MapTo<UserCenterInfo, UserInfoProfile>();

            return info;
        }

        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="userName"></param>
        /// <param name="nickName"></param>
        /// <param name="start"></param>
        /// <param name="endTime"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<UsersInfo> AdminGetUserList(string mobile, string userName, string nickName, DateTime? start, DateTime? endTime, PageModel page, out int total)
        {
            var userList = _usersRepository.GetDbSetNoTracking().Where(u => u.AdminGId == (int)UserType.User)
                .GroupJoin(_ordersRepository.GetDbSetNoTracking(),x=>x.UId,y=>y.UId,(a,y)=>new UsersInfo
                {
                    UId = a.UId,
                    UserName = a.UserName,
                    Mobile = a.Mobile,
                    NickName = a.NickName,
                    Avatar = a.Avatar,
                    CreateTime = a.RegisterTime,
                    CouponsOrders = y.Count(u=>u.Type==(byte)OrderType.CardOrder)
                })
                .WhereIf(a => a.Mobile.Contains(mobile), !string.IsNullOrEmpty(mobile))
                .WhereIf(a => a.UserName.Contains(userName), !string.IsNullOrEmpty(userName))
                .WhereIf(a => a.NickName.Contains(nickName), !string.IsNullOrEmpty(nickName))
                .WhereIf(a => a.CreateTime >= start, start != null)
                .WhereIf(a => a.CreateTime <= endTime, endTime != null)
                .OrderByDescending(u => u.CreateTime).LoadPage(page, out total)
                .ToList();

            //订单统计
            var orderTotalList = _ordersRepository.GetDbSetNoTracking()
                .GroupBy(a => a.UId)
                .Select(a => new OrderTotal
                {
                    UId = a.Key,
                    TotalOrderCount = a.Count(),
                    TotalOrderAmount = a.Sum(m => m.OrderAmount)
                })
                .ToList();

            //添加订单数
            foreach (var item in userList)
            {
                item.OrderSum = orderTotalList.FirstOrDefault(a => a.UId == item.UId)?.TotalOrderCount ?? 0;
                item.OrderAmount = orderTotalList.FirstOrDefault(a => a.UId == item.UId)?.TotalOrderAmount ?? 0;
            }
            return userList;
        }


        /// <summary>
        /// 获取导出商品列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<ExportUserInfo> ExportAdminGetUserList()
        {
            var userList = _usersRepository.GetDbSetNoTracking().Where(u => u.AdminGId == (int)UserType.User)
                .Select(a => new ExportUserInfo
                {
                    UId = a.UId,
                    NickName = a.NickName,
                    UserName = a.UserName,
                    CreateTime = a.RegisterTime
                }).OrderByDescending(u => u.CreateTime)
                .ToList();
            return userList;
        }

        /// <summary>
        /// 后台获取管理员信息列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="adminGId">部门Id</param>
        /// <returns></returns>
        public List<AdminsInfo> GetAdminList(string userName, short adminGId)
        {
            var data = (from u in _usersRepository.GetDbSetNoTracking()
                    join ag in _adminGroupsRepository.GetDbSetNoTracking()
                        on u.AdminGId equals ag.AdminGId
                    select new { u, ag }
                )
                .Where(m => m.u.AdminGId != (int)UserType.User && m.u.AdminGId != (int)UserType.SystemManager)
                .WhereIf(a => a.u.UserName.Contains(userName), !string.IsNullOrEmpty(userName))
                .WhereIf(a => a.u.AdminGId == adminGId, adminGId > 0);
            var list = data.Select(a => new AdminsInfo
            {
                    UId = a.u.UId,
                    UserName = a.u.UserName,
                    RegisterTime = a.u.RegisterTime,
                    Title = a.ag.Title
                })
                .ToList();
            return list;
        }


        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="uId">管理员Id</param>
        /// <returns></returns>
        public AdminsInfo GetAdminInfo(int uId)
        {
            var data = (from u in _usersRepository.GetDbSetNoTracking()
                    join ag in _adminGroupsRepository.GetDbSetNoTracking()
                        on u.AdminGId equals ag.AdminGId
                    select new { u, ag }
                )
                .WhereIf(a => a.u.UId == uId, uId > 0);
            var info = data.Select(a => new AdminsInfo
            {
                UId = a.u.UId,
                UserName = a.u.UserName,
                RegisterTime = a.u.RegisterTime,
                Title = a.ag.Title
            }).FirstOrDefault();
            return info;
        }

        /// <summary>
        /// 获取当前日期的注册人数
        /// </summary>
        /// <param name="startTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <returns></returns>
        public List<TimeAndUserCount> GetUserCountAndTime(DateTime startTime, DateTime endTime)
        {
            string sql = $@"select convert(varchar(10), RegisterTime, 120) as Time,count(UId) as UserCount 
                            from LF_Users where RegisterTime>='{startTime}' and RegisterTime<='{endTime}' 
                            GROUP BY  convert(varchar(10), RegisterTime, 120) ";
            return _usersRepository.GetUnitOfWork().SqlQueryResult<TimeAndUserCount>(sql).ToList();
        }

        /// <summary>
        /// 获取会员性别占比
        /// </summary>
        /// <returns></returns>
        public List<Data> GetOrderCountGroupByDate(DateTime startTime, DateTime endTime)
        {
            var model = _usersRepository.GetDbSetNoTracking()
                .Where(m => m.AdminGId == 1)
                .GroupBy(x => x.Gender)
                .Select(x => new
                {
                    Gender = x.Key,
                    Count = x.Count(),
                })
                .ToList();
            return model.Select(x => new Data
            {
                DataName = ((UserGender)x.Gender).GetDescription(),
                DataValue = x.Count
            }).ToList();
        }

        /// <summary>
        /// 获取会员累计消费金额
        /// </summary>
        /// <returns></returns>
        public List<int> GetUserAmountCount()
        {
            string sql = $@"(select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 0 and 50)as a)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 51 and 100)as b)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 101 and 200)as c)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 201 and 500)as d)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 501 and 1000)as e)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 1001 and 5000)as f)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2)) between 5001 and 1000)as g)
                        union all
                        (select COUNT(UId) from (SELECT u.UId from LF_Users as u 
                        left join LF_Orders as o on o.UId = u.UId
                        GROUP BY u.UId
						having CAST( IsNull(SUM(o.OrderAmount),0)as decimal(18,2))>=10001)as j)";
            return _usersRepository.GetUnitOfWork().SqlQueryResult<int>(sql).ToList();
        }

        /// <summary>
        /// 获取会员数量
        /// </summary>
        /// <returns></returns>
        public int GetUserCountByTime(DateTime startTime, DateTime endTime)
        {
            return _usersRepository.GetDbSetNoTracking()
                .Count(m => m.RegisterTime >= startTime && m.RegisterTime <= endTime);
        }

        /// <summary>
        /// 用户数
        /// </summary>
        /// <returns></returns>
        public int GetTotalUsers()
        {
            return _usersRepository.Count();
        }

    }
}