
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IOrderActionsService
     {
        /// <summary>
        /// 添加订单动作 
        /// </summary>
        /// <param name="orderActions">orderActions</param>
        /// <returns>订单动作</returns>
        void AddOrderActions(LF_OrderActions orderActions);

		/// <summary>
        /// 批量添加订单动作 
        /// </summary>
        /// <param name="orderActionsList">orderActionsList</param>
        /// <returns>订单动作列表</returns>
        void BatchAddOrderActions(IEnumerable<LF_OrderActions> orderActionsList);

		/// <summary>
        /// 更新订单动作 
        /// </summary>
        /// <param name="orderActions">orderActions</param>
        /// <returns>订单动作</returns>
        void UpdateOrderActions(LF_OrderActions orderActions);

		/// <summary>
        /// 批量更新订单动作 
        /// </summary>
        /// <param name="orderActionsList">orderActionsList</param>
        /// <returns>订单动作列表</returns>
        void BatchUpdateOrderActions(IEnumerable<LF_OrderActions> orderActionsList);

        /// <summary>
        /// 批量更新订单动作列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_OrderActions, bool>> whereLambda, Expression<Func<LF_OrderActions, LF_OrderActions>> updateExpression);

        /// <summary>
        /// 删除订单动作 
        /// </summary>
        /// <param name="orderActions">orderActions</param>
        /// <returns>订单动作</returns>
        void DeleteOrderActions(LF_OrderActions orderActions);

        /// <summary>
        /// 根据查询条件删除订单动作 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单动作</returns>
        void DeleteOrderActions(Expression<Func<LF_OrderActions, bool>> whereLambda);

		/// <summary>
        /// 批量删除订单动作 
        /// </summary>
        /// <param name="orderActionsList">orderActionsList</param>
        /// <returns>订单动作列表</returns>
        void BatchDeleteOrderActions(IEnumerable<LF_OrderActions> orderActionsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_OrderActions, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取订单动作 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>订单动作</returns>
        LF_OrderActions GetOrderActionsById(int id);

		/// <summary>
        /// 根据查询条件获取单条订单动作
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单动作</returns>
        LF_OrderActions Get(Expression<Func<LF_OrderActions, bool>> whereLambda);

        /// <summary>
        /// 获取所有订单动作
        /// </summary>
        /// <returns>订单动作列表</returns>
        IEnumerable<LF_OrderActions> GetAll();

        /// <summary>
        /// 根据查询条件获取订单动作列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单动作列表</returns>
        IEnumerable<LF_OrderActions> GetList(Expression<Func<LF_OrderActions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_OrderActions LoadEntitieNoTracking(Expression<Func<LF_OrderActions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_OrderActions> LoadEntitiesNoTracking(Expression<Func<LF_OrderActions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>订单动作列表</returns>
        IList<LF_OrderActions> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_OrderActions, bool>> whereLambda,
            Expression<Func<LF_OrderActions, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_OrderActions, bool>> whereLambda);

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
        /// <returns>订单动作列表</returns>
        IList<LF_OrderActions> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_OrderActions, bool>> whereLambda, Expression<Func<LF_OrderActions, TS>> orderbyLambda, bool isAsc);
     }
}
