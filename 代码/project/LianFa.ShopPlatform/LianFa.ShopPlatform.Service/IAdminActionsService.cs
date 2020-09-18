
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IAdminActionsService
     {
        /// <summary>
        /// 添加后台管理权限动作 
        /// </summary>
        /// <param name="adminActions">adminActions</param>
        /// <returns>后台管理权限动作</returns>
        void AddAdminActions(LF_AdminActions adminActions);

		/// <summary>
        /// 批量添加后台管理权限动作 
        /// </summary>
        /// <param name="adminActionsList">adminActionsList</param>
        /// <returns>后台管理权限动作列表</returns>
        void BatchAddAdminActions(IEnumerable<LF_AdminActions> adminActionsList);

		/// <summary>
        /// 更新后台管理权限动作 
        /// </summary>
        /// <param name="adminActions">adminActions</param>
        /// <returns>后台管理权限动作</returns>
        void UpdateAdminActions(LF_AdminActions adminActions);

		/// <summary>
        /// 批量更新后台管理权限动作 
        /// </summary>
        /// <param name="adminActionsList">adminActionsList</param>
        /// <returns>后台管理权限动作列表</returns>
        void BatchUpdateAdminActions(IEnumerable<LF_AdminActions> adminActionsList);

        /// <summary>
        /// 批量更新后台管理权限动作列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_AdminActions, bool>> whereLambda, Expression<Func<LF_AdminActions, LF_AdminActions>> updateExpression);

        /// <summary>
        /// 删除后台管理权限动作 
        /// </summary>
        /// <param name="adminActions">adminActions</param>
        /// <returns>后台管理权限动作</returns>
        void DeleteAdminActions(LF_AdminActions adminActions);

        /// <summary>
        /// 根据查询条件删除后台管理权限动作 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>后台管理权限动作</returns>
        void DeleteAdminActions(Expression<Func<LF_AdminActions, bool>> whereLambda);

		/// <summary>
        /// 批量删除后台管理权限动作 
        /// </summary>
        /// <param name="adminActionsList">adminActionsList</param>
        /// <returns>后台管理权限动作列表</returns>
        void BatchDeleteAdminActions(IEnumerable<LF_AdminActions> adminActionsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_AdminActions, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取后台管理权限动作 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>后台管理权限动作</returns>
        LF_AdminActions GetAdminActionsById(int id);

		/// <summary>
        /// 根据查询条件获取单条后台管理权限动作
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>后台管理权限动作</returns>
        LF_AdminActions Get(Expression<Func<LF_AdminActions, bool>> whereLambda);

        /// <summary>
        /// 获取所有后台管理权限动作
        /// </summary>
        /// <returns>后台管理权限动作列表</returns>
        IEnumerable<LF_AdminActions> GetAll();

        /// <summary>
        /// 根据查询条件获取后台管理权限动作列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>后台管理权限动作列表</returns>
        IEnumerable<LF_AdminActions> GetList(Expression<Func<LF_AdminActions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_AdminActions LoadEntitieNoTracking(Expression<Func<LF_AdminActions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_AdminActions> LoadEntitiesNoTracking(Expression<Func<LF_AdminActions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>后台管理权限动作列表</returns>
        IList<LF_AdminActions> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_AdminActions, bool>> whereLambda,
            Expression<Func<LF_AdminActions, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_AdminActions, bool>> whereLambda);

		/// <summary>
        /// 根据查询条件分页获取实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="pageSize">每页数量(页大小)</param>
        /// <param name="pageIndex">页数(第几页)</param>
        /// <param name="total">总数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>后台管理权限动作列表</returns>
        IList<LF_AdminActions> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_AdminActions, bool>> whereLambda, Expression<Func<LF_AdminActions, TS>> orderbyLambda, bool isAsc);
     }
}
