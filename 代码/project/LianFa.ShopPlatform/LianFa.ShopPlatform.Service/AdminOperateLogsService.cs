
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Dependency;


namespace LianFa.ShopPlatform.Service
{
    [DependencyRegister]
	 public partial class AdminOperateLogsService : IAdminOperateLogsService
     {

        #region Fields

        private readonly IRepository<LF_AdminOperateLogs> _adminOperateLogsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="adminOperateLogsRepository">商城管理日志仓储类</param>
        public AdminOperateLogsService(IRepository<LF_AdminOperateLogs> adminOperateLogsRepository)
        {
            this._adminOperateLogsRepository = adminOperateLogsRepository;
        }

        #endregion

        /// <summary>
        /// 添加商城管理日志 
        /// </summary>
        /// <param name="adminOperateLogs">adminOperateLogs</param>
        /// <returns>商城管理日志</returns>
        public void AddAdminOperateLogs(LF_AdminOperateLogs adminOperateLogs)
        {
            _adminOperateLogsRepository.Add(adminOperateLogs);
        }

		/// <summary>
        /// 批量添加商城管理日志 
        /// </summary>
        /// <param name="adminOperateLogsList">adminOperateLogsList</param>
        /// <returns>商城管理日志列表</returns>
        public void BatchAddAdminOperateLogs(IEnumerable<LF_AdminOperateLogs> adminOperateLogsList)
        {
            _adminOperateLogsRepository.BatchAdd(adminOperateLogsList);
        }

		/// <summary>
        /// 更新商城管理日志 
        /// </summary>
        /// <param name="adminOperateLogs">adminOperateLogs</param>
        /// <returns>商城管理日志</returns>
        public void UpdateAdminOperateLogs(LF_AdminOperateLogs adminOperateLogs)
        {
            _adminOperateLogsRepository.Update(adminOperateLogs);
        }

		/// <summary>
        /// 批量更新商城管理日志 
        /// </summary>
        /// <param name="adminOperateLogsList">adminOperateLogsList</param>
        /// <returns>商城管理日志列表</returns>
        public void BatchUpdateAdminOperateLogs(IEnumerable<LF_AdminOperateLogs> adminOperateLogsList)
        {
            _adminOperateLogsRepository.BatchUpdate(adminOperateLogsList);
        }

        /// <summary>
        /// 批量更新商城管理日志列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda, Expression<Func<LF_AdminOperateLogs, LF_AdminOperateLogs>> updateExpression)
        {
             return _adminOperateLogsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除商城管理日志 
        /// </summary>
        /// <param name="adminOperateLogs">adminOperateLogs</param>
        /// <returns>商城管理日志</returns>
        public void DeleteAdminOperateLogs(LF_AdminOperateLogs adminOperateLogs)
        {
            _adminOperateLogsRepository.Delete(adminOperateLogs);  
        }

        /// <summary>
        /// 根据查询条件删除商城管理日志 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商城管理日志</returns>
        public void DeleteAdminOperateLogs(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            _adminOperateLogsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除商城管理日志 
        /// </summary>
        /// <param name="adminOperateLogsList">adminOperateLogsList</param>
        /// <returns>商城管理日志列表</returns>
        public void BatchDeleteAdminOperateLogs(IEnumerable<LF_AdminOperateLogs> adminOperateLogsList)
        {
            _adminOperateLogsRepository.BatchDelete(adminOperateLogsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            return _adminOperateLogsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取商城管理日志 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>商城管理日志</returns>
        public LF_AdminOperateLogs GetAdminOperateLogsById(int id)
        {
            return _adminOperateLogsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条商城管理日志
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商城管理日志</returns>
        public LF_AdminOperateLogs Get(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            return _adminOperateLogsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有商城管理日志
        /// </summary>
        /// <returns>商城管理日志列表</returns>
        public IEnumerable<LF_AdminOperateLogs> GetAll()
        {
            return _adminOperateLogsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取商城管理日志列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商城管理日志列表</returns>
        public IEnumerable<LF_AdminOperateLogs> GetList(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            return _adminOperateLogsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_AdminOperateLogs LoadEntitieNoTracking(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            return _adminOperateLogsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_AdminOperateLogs> LoadEntitiesNoTracking(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            return _adminOperateLogsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商城管理日志列表</returns>
        public IList<LF_AdminOperateLogs> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda,
            Expression<Func<LF_AdminOperateLogs, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _adminOperateLogsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_AdminOperateLogs, bool>> whereLambda)
        {
            return _adminOperateLogsRepository.Exist(whereLambda);
        }

		/// <summary>
        /// 根据查询条件分页获取实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="pageSize">每页数量(页大小)</param>
        /// <param name="pageIndex">页数(第几页)</param>
        /// <param name="total">总数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商城管理日志列表</returns>
        public IList<LF_AdminOperateLogs> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_AdminOperateLogs, bool>> whereLambda, Expression<Func<LF_AdminOperateLogs, TS>> orderbyLambda, bool isAsc)
        {
            return _adminOperateLogsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
