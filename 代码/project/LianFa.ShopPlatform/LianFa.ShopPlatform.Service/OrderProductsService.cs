
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
	 public partial class OrderProductsService : IOrderProductsService
     {

        #region Fields

        private readonly IRepository<LF_OrderProducts> _orderProductsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="orderProductsRepository">订单商品仓储类</param>
        public OrderProductsService(IRepository<LF_OrderProducts> orderProductsRepository)
        {
            this._orderProductsRepository = orderProductsRepository;
        }

        #endregion

        /// <summary>
        /// 添加订单商品 
        /// </summary>
        /// <param name="orderProducts">orderProducts</param>
        /// <returns>订单商品</returns>
        public void AddOrderProducts(LF_OrderProducts orderProducts)
        {
            _orderProductsRepository.Add(orderProducts);
        }

		/// <summary>
        /// 批量添加订单商品 
        /// </summary>
        /// <param name="orderProductsList">orderProductsList</param>
        /// <returns>订单商品列表</returns>
        public void BatchAddOrderProducts(IEnumerable<LF_OrderProducts> orderProductsList)
        {
            _orderProductsRepository.BatchAdd(orderProductsList);
        }

		/// <summary>
        /// 更新订单商品 
        /// </summary>
        /// <param name="orderProducts">orderProducts</param>
        /// <returns>订单商品</returns>
        public void UpdateOrderProducts(LF_OrderProducts orderProducts)
        {
            _orderProductsRepository.Update(orderProducts);
        }

		/// <summary>
        /// 批量更新订单商品 
        /// </summary>
        /// <param name="orderProductsList">orderProductsList</param>
        /// <returns>订单商品列表</returns>
        public void BatchUpdateOrderProducts(IEnumerable<LF_OrderProducts> orderProductsList)
        {
            _orderProductsRepository.BatchUpdate(orderProductsList);
        }

        /// <summary>
        /// 批量更新订单商品列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_OrderProducts, bool>> whereLambda, Expression<Func<LF_OrderProducts, LF_OrderProducts>> updateExpression)
        {
             return _orderProductsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除订单商品 
        /// </summary>
        /// <param name="orderProducts">orderProducts</param>
        /// <returns>订单商品</returns>
        public void DeleteOrderProducts(LF_OrderProducts orderProducts)
        {
            _orderProductsRepository.Delete(orderProducts);  
        }

        /// <summary>
        /// 根据查询条件删除订单商品 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单商品</returns>
        public void DeleteOrderProducts(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            _orderProductsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除订单商品 
        /// </summary>
        /// <param name="orderProductsList">orderProductsList</param>
        /// <returns>订单商品列表</returns>
        public void BatchDeleteOrderProducts(IEnumerable<LF_OrderProducts> orderProductsList)
        {
            _orderProductsRepository.BatchDelete(orderProductsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            return _orderProductsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取订单商品 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>订单商品</returns>
        public LF_OrderProducts GetOrderProductsById(int id)
        {
            return _orderProductsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条订单商品
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单商品</returns>
        public LF_OrderProducts Get(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            return _orderProductsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有订单商品
        /// </summary>
        /// <returns>订单商品列表</returns>
        public IEnumerable<LF_OrderProducts> GetAll()
        {
            return _orderProductsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取订单商品列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单商品列表</returns>
        public IEnumerable<LF_OrderProducts> GetList(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            return _orderProductsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_OrderProducts LoadEntitieNoTracking(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            return _orderProductsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_OrderProducts> LoadEntitiesNoTracking(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            return _orderProductsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>订单商品列表</returns>
        public IList<LF_OrderProducts> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_OrderProducts, bool>> whereLambda,
            Expression<Func<LF_OrderProducts, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _orderProductsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_OrderProducts, bool>> whereLambda)
        {
            return _orderProductsRepository.Exist(whereLambda);
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
        /// <returns>订单商品列表</returns>
        public IList<LF_OrderProducts> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_OrderProducts, bool>> whereLambda, Expression<Func<LF_OrderProducts, TS>> orderbyLambda, bool isAsc)
        {
            return _orderProductsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
