
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IOrdersService
     {
        /// <summary>
        /// 添加订单 
        /// </summary>
        /// <param name="orders">orders</param>
        /// <returns>订单</returns>
        void AddOrders(LF_Orders orders);

		/// <summary>
        /// 批量添加订单 
        /// </summary>
        /// <param name="ordersList">ordersList</param>
        /// <returns>订单列表</returns>
        void BatchAddOrders(IEnumerable<LF_Orders> ordersList);

		/// <summary>
        /// 更新订单 
        /// </summary>
        /// <param name="orders">orders</param>
        /// <returns>订单</returns>
        void UpdateOrders(LF_Orders orders);

		/// <summary>
        /// 批量更新订单 
        /// </summary>
        /// <param name="ordersList">ordersList</param>
        /// <returns>订单列表</returns>
        void BatchUpdateOrders(IEnumerable<LF_Orders> ordersList);

        /// <summary>
        /// 批量更新订单列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Orders, bool>> whereLambda, Expression<Func<LF_Orders, LF_Orders>> updateExpression);

        /// <summary>
        /// 删除订单 
        /// </summary>
        /// <param name="orders">orders</param>
        /// <returns>订单</returns>
        void DeleteOrders(LF_Orders orders);

        /// <summary>
        /// 根据查询条件删除订单 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单</returns>
        void DeleteOrders(Expression<Func<LF_Orders, bool>> whereLambda);

		/// <summary>
        /// 批量删除订单 
        /// </summary>
        /// <param name="ordersList">ordersList</param>
        /// <returns>订单列表</returns>
        void BatchDeleteOrders(IEnumerable<LF_Orders> ordersList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Orders, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取订单 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>订单</returns>
        LF_Orders GetOrdersById(int id);

		/// <summary>
        /// 根据查询条件获取单条订单
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单</returns>
        LF_Orders Get(Expression<Func<LF_Orders, bool>> whereLambda);

        /// <summary>
        /// 获取所有订单
        /// </summary>
        /// <returns>订单列表</returns>
        IEnumerable<LF_Orders> GetAll();

        /// <summary>
        /// 根据查询条件获取订单列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单列表</returns>
        IEnumerable<LF_Orders> GetList(Expression<Func<LF_Orders, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Orders LoadEntitieNoTracking(Expression<Func<LF_Orders, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Orders> LoadEntitiesNoTracking(Expression<Func<LF_Orders, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>订单列表</returns>
        IList<LF_Orders> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Orders, bool>> whereLambda,
            Expression<Func<LF_Orders, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Orders, bool>> whereLambda);

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
        /// <returns>订单列表</returns>
        IList<LF_Orders> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Orders, bool>> whereLambda, Expression<Func<LF_Orders, TS>> orderbyLambda, bool isAsc);
     }
}
