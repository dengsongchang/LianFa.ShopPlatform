
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
	 public partial class AdminActionsService : IAdminActionsService
     {

        #region Fields

        private readonly IRepository<LF_AdminActions> _adminActionsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="adminActionsRepository">后台管理权限动作仓储类</param>
        public AdminActionsService(IRepository<LF_AdminActions> adminActionsRepository)
        {
            this._adminActionsRepository = adminActionsRepository;
        }

        #endregion

        /// <summary>
        /// 添加后台管理权限动作 
        /// </summary>
        /// <param name="adminActions">adminActions</param>
        /// <returns>后台管理权限动作</returns>
        public void AddAdminActions(LF_AdminActions adminActions)
        {
            _adminActionsRepository.Add(adminActions);
        }

		/// <summary>
        /// 批量添加后台管理权限动作 
        /// </summary>
        /// <param name="adminActionsList">adminActionsList</param>
        /// <returns>后台管理权限动作列表</returns>
        public void BatchAddAdminActions(IEnumerable<LF_AdminActions> adminActionsList)
        {
            _adminActionsRepository.BatchAdd(adminActionsList);
        }

		/// <summary>
        /// 更新后台管理权限动作 
        /// </summary>
        /// <param name="adminActions">adminActions</param>
        /// <returns>后台管理权限动作</returns>
        public void UpdateAdminActions(LF_AdminActions adminActions)
        {
            _adminActionsRepository.Update(adminActions);
        }

		/// <summary>
        /// 批量更新后台管理权限动作 
        /// </summary>
        /// <param name="adminActionsList">adminActionsList</param>
        /// <returns>后台管理权限动作列表</returns>
        public void BatchUpdateAdminActions(IEnumerable<LF_AdminActions> adminActionsList)
        {
            _adminActionsRepository.BatchUpdate(adminActionsList);
        }

        /// <summary>
        /// 批量更新后台管理权限动作列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_AdminActions, bool>> whereLambda, Expression<Func<LF_AdminActions, LF_AdminActions>> updateExpression)
        {
             return _adminActionsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除后台管理权限动作 
        /// </summary>
        /// <param name="adminActions">adminActions</param>
        /// <returns>后台管理权限动作</returns>
        public void DeleteAdminActions(LF_AdminActions adminActions)
        {
            _adminActionsRepository.Delete(adminActions);  
        }

        /// <summary>
        /// 根据查询条件删除后台管理权限动作 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>后台管理权限动作</returns>
        public void DeleteAdminActions(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            _adminActionsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除后台管理权限动作 
        /// </summary>
        /// <param name="adminActionsList">adminActionsList</param>
        /// <returns>后台管理权限动作列表</returns>
        public void BatchDeleteAdminActions(IEnumerable<LF_AdminActions> adminActionsList)
        {
            _adminActionsRepository.BatchDelete(adminActionsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            return _adminActionsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取后台管理权限动作 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>后台管理权限动作</returns>
        public LF_AdminActions GetAdminActionsById(int id)
        {
            return _adminActionsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条后台管理权限动作
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>后台管理权限动作</returns>
        public LF_AdminActions Get(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            return _adminActionsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有后台管理权限动作
        /// </summary>
        /// <returns>后台管理权限动作列表</returns>
        public IEnumerable<LF_AdminActions> GetAll()
        {
            return _adminActionsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取后台管理权限动作列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>后台管理权限动作列表</returns>
        public IEnumerable<LF_AdminActions> GetList(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            return _adminActionsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_AdminActions LoadEntitieNoTracking(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            return _adminActionsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_AdminActions> LoadEntitiesNoTracking(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            return _adminActionsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>后台管理权限动作列表</returns>
        public IList<LF_AdminActions> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_AdminActions, bool>> whereLambda,
            Expression<Func<LF_AdminActions, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _adminActionsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_AdminActions, bool>> whereLambda)
        {
            return _adminActionsRepository.Exist(whereLambda);
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
        /// <returns>后台管理权限动作列表</returns>
        public IList<LF_AdminActions> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_AdminActions, bool>> whereLambda, Expression<Func<LF_AdminActions, TS>> orderbyLambda, bool isAsc)
        {
            return _adminActionsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
