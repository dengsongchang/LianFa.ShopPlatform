
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
	 public partial class AdminGroupsService : IAdminGroupsService
     {

        #region Fields

        private readonly IRepository<LF_AdminGroups> _adminGroupsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="adminGroupsRepository">商城管理组仓储类</param>
        public AdminGroupsService(IRepository<LF_AdminGroups> adminGroupsRepository)
        {
            this._adminGroupsRepository = adminGroupsRepository;
        }

        #endregion

        /// <summary>
        /// 添加商城管理组 
        /// </summary>
        /// <param name="adminGroups">adminGroups</param>
        /// <returns>商城管理组</returns>
        public void AddAdminGroups(LF_AdminGroups adminGroups)
        {
            _adminGroupsRepository.Add(adminGroups);
        }

		/// <summary>
        /// 批量添加商城管理组 
        /// </summary>
        /// <param name="adminGroupsList">adminGroupsList</param>
        /// <returns>商城管理组列表</returns>
        public void BatchAddAdminGroups(IEnumerable<LF_AdminGroups> adminGroupsList)
        {
            _adminGroupsRepository.BatchAdd(adminGroupsList);
        }

		/// <summary>
        /// 更新商城管理组 
        /// </summary>
        /// <param name="adminGroups">adminGroups</param>
        /// <returns>商城管理组</returns>
        public void UpdateAdminGroups(LF_AdminGroups adminGroups)
        {
            _adminGroupsRepository.Update(adminGroups);
        }

		/// <summary>
        /// 批量更新商城管理组 
        /// </summary>
        /// <param name="adminGroupsList">adminGroupsList</param>
        /// <returns>商城管理组列表</returns>
        public void BatchUpdateAdminGroups(IEnumerable<LF_AdminGroups> adminGroupsList)
        {
            _adminGroupsRepository.BatchUpdate(adminGroupsList);
        }

        /// <summary>
        /// 批量更新商城管理组列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_AdminGroups, bool>> whereLambda, Expression<Func<LF_AdminGroups, LF_AdminGroups>> updateExpression)
        {
             return _adminGroupsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除商城管理组 
        /// </summary>
        /// <param name="adminGroups">adminGroups</param>
        /// <returns>商城管理组</returns>
        public void DeleteAdminGroups(LF_AdminGroups adminGroups)
        {
            _adminGroupsRepository.Delete(adminGroups);  
        }

        /// <summary>
        /// 根据查询条件删除商城管理组 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商城管理组</returns>
        public void DeleteAdminGroups(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            _adminGroupsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除商城管理组 
        /// </summary>
        /// <param name="adminGroupsList">adminGroupsList</param>
        /// <returns>商城管理组列表</returns>
        public void BatchDeleteAdminGroups(IEnumerable<LF_AdminGroups> adminGroupsList)
        {
            _adminGroupsRepository.BatchDelete(adminGroupsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            return _adminGroupsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取商城管理组 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>商城管理组</returns>
        public LF_AdminGroups GetAdminGroupsById(int id)
        {
            return _adminGroupsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条商城管理组
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商城管理组</returns>
        public LF_AdminGroups Get(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            return _adminGroupsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有商城管理组
        /// </summary>
        /// <returns>商城管理组列表</returns>
        public IEnumerable<LF_AdminGroups> GetAll()
        {
            return _adminGroupsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取商城管理组列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商城管理组列表</returns>
        public IEnumerable<LF_AdminGroups> GetList(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            return _adminGroupsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_AdminGroups LoadEntitieNoTracking(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            return _adminGroupsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_AdminGroups> LoadEntitiesNoTracking(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            return _adminGroupsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商城管理组列表</returns>
        public IList<LF_AdminGroups> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_AdminGroups, bool>> whereLambda,
            Expression<Func<LF_AdminGroups, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _adminGroupsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_AdminGroups, bool>> whereLambda)
        {
            return _adminGroupsRepository.Exist(whereLambda);
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
        /// <returns>商城管理组列表</returns>
        public IList<LF_AdminGroups> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_AdminGroups, bool>> whereLambda, Expression<Func<LF_AdminGroups, TS>> orderbyLambda, bool isAsc)
        {
            return _adminGroupsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
