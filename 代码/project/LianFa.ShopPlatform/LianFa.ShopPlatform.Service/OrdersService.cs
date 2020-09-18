
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
	 public partial class OrdersService : IOrdersService
     {

        #region Fields

        private readonly IRepository<LF_Orders> _ordersRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ordersRepository">订单仓储类</param>
        public OrdersService(IRepository<LF_Orders> ordersRepository)
        {
            this._ordersRepository = ordersRepository;
        }

        #endregion

        /// <summary>
        /// 添加订单 
        /// </summary>
        /// <param name="orders">orders</param>
        /// <returns>订单</returns>
        public void AddOrders(LF_Orders orders)
        {
            _ordersRepository.Add(orders);
        }

		/// <summary>
        /// 批量添加订单 
        /// </summary>
        /// <param name="ordersList">ordersList</param>
        /// <returns>订单列表</returns>
        public void BatchAddOrders(IEnumerable<LF_Orders> ordersList)
        {
            _ordersRepository.BatchAdd(ordersList);
        }

		/// <summary>
        /// 更新订单 
        /// </summary>
        /// <param name="orders">orders</param>
        /// <returns>订单</returns>
        public void UpdateOrders(LF_Orders orders)
        {
            _ordersRepository.Update(orders);
        }

		/// <summary>
        /// 批量更新订单 
        /// </summary>
        /// <param name="ordersList">ordersList</param>
        /// <returns>订单列表</returns>
        public void BatchUpdateOrders(IEnumerable<LF_Orders> ordersList)
        {
            _ordersRepository.BatchUpdate(ordersList);
        }

        /// <summary>
        /// 批量更新订单列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_Orders, bool>> whereLambda, Expression<Func<LF_Orders, LF_Orders>> updateExpression)
        {
             return _ordersRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除订单 
        /// </summary>
        /// <param name="orders">orders</param>
        /// <returns>订单</returns>
        public void DeleteOrders(LF_Orders orders)
        {
            _ordersRepository.Delete(orders);  
        }

        /// <summary>
        /// 根据查询条件删除订单 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单</returns>
        public void DeleteOrders(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            _ordersRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除订单 
        /// </summary>
        /// <param name="ordersList">ordersList</param>
        /// <returns>订单列表</returns>
        public void BatchDeleteOrders(IEnumerable<LF_Orders> ordersList)
        {
            _ordersRepository.BatchDelete(ordersList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            return _ordersRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取订单 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>订单</returns>
        public LF_Orders GetOrdersById(int id)
        {
            return _ordersRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条订单
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单</returns>
        public LF_Orders Get(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            return _ordersRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有订单
        /// </summary>
        /// <returns>订单列表</returns>
        public IEnumerable<LF_Orders> GetAll()
        {
            return _ordersRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取订单列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单列表</returns>
        public IEnumerable<LF_Orders> GetList(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            return _ordersRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_Orders LoadEntitieNoTracking(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            return _ordersRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_Orders> LoadEntitiesNoTracking(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            return _ordersRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>订单列表</returns>
        public IList<LF_Orders> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_Orders, bool>> whereLambda,
            Expression<Func<LF_Orders, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _ordersRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_Orders, bool>> whereLambda)
        {
            return _ordersRepository.Exist(whereLambda);
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
        /// <returns>订单列表</returns>
        public IList<LF_Orders> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Orders, bool>> whereLambda, Expression<Func<LF_Orders, TS>> orderbyLambda, bool isAsc)
        {
            return _ordersRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
