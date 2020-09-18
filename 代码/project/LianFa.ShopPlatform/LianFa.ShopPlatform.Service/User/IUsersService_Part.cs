using System;
using System.Collections.Generic;
using LianFa.ShopPlatform.DataBase;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Code.Data;
using LianFa.ShopPlatform.Model.Response.Admin.User;
using LianFa.ShopPlatform.Model.Response.Client.User;
using LianFa.ShopPlatform.Model.Response.Admin.Admin;
using LianFa.ShopPlatform.Model.Response.Admin.Statistics;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IUsersService
    {
        /// <summary>
        /// 创建用户密码
        /// </summary>
        /// <param name="password">真实密码</param>
        /// <param name="salt">散列盐值</param>
        /// <returns>密码</returns>
        string CreateUserPassword(string password, string salt);

        /// <summary>
        /// 后台通过用户名获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        LF_Users GetUserInfoByName(string userName);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        LF_Users GetPartUserByName(string userName);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        LF_Users GetPartUserByMobile(string mobile);

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <returns>用户分页列表</returns>
        PagerList<LF_Users> GetUserList(Pager pager);

        /// <summary>
        /// 获取会员详情
        /// </summary>
        /// <returns></returns>
        UserCenterInfo GetUserInfo(int uId);

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
        List<UsersInfo> AdminGetUserList(string mobile, string userName,string nickName,DateTime? start,DateTime? endTime,PageModel page, out int total);

        /// <summary>
        /// 获取会员导出列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ExportUserInfo> ExportAdminGetUserList();

        /// <summary>
        /// 后台获取管理员信息列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="adminGId">部门Id</param>
        /// <returns></returns>
        List<AdminsInfo> GetAdminList(string userName, short adminGId);

        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="uId">管理员Id</param>
        /// <returns></returns>
        AdminsInfo GetAdminInfo(int uId);

        /// <summary>
        /// 获取当前日期的注册人数
        /// </summary>
        /// <param name="startTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <returns></returns>
        List<TimeAndUserCount> GetUserCountAndTime(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取会员性别占比
        /// </summary>
        /// <returns></returns>
        List<Data> GetOrderCountGroupByDate(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取会员累计消费金额
        /// </summary>
        /// <returns></returns>
        List<int> GetUserAmountCount();

        /// <summary>
        /// 获取会员数量
        /// </summary>
        /// <returns></returns>
        int GetUserCountByTime(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 用户数
        /// </summary>
        /// <returns></returns>
        int GetTotalUsers();

    }

}