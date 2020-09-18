using System;
using System.Collections.Generic;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.Admin;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IAdminOperateLogsService
    {
        /// <summary>
        /// 后台获取管理员操作日志
        /// </summary>
        /// <param name="page">分页</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="uid">管理员Id</param>
        /// <param name="total">总行数</param>
        /// <returns></returns>
        IEnumerable<LF_AdminOperateLogs> GetAdminOperateLogsList(PageModel page, DateTime? startTime, DateTime? endTime, int uid, out int total);

        /// <summary>
        /// 后台获取管理员列表
        /// </summary>
        /// <returns></returns>
        List<AdminsInfo> GetAdminList();

        /// <summary>
        /// 添加管理员操作日志(未提交)
        /// </summary>
        /// <param name="uId">用户Id</param>
        /// <param name="content">操作内容</param>
        /// <param name="ip">ip</param>
        void AddAdminOperateLogs(int uId, string content, string ip);
    }
}
