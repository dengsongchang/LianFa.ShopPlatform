
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IOrderProductsService
     {
        /// <summary>
        /// 添加订单商品 
        /// </summary>
        /// <param name="orderProducts">orderProducts</param>
        /// <returns>订单商品</returns>
        void AddOrderProducts(LF_OrderProducts orderProducts);

		/// <summary>
        /// 批量添加订单商品 
        /// </summary>
        /// <param name="orderProductsList">orderProductsList</param>
        /// <returns>订单商品列表</returns>
        void BatchAddOrderProducts(IEnumerable<LF_OrderProducts> orderProductsList);

		/// <summary>
        /// 更新订单商品 
        /// </summary>
        /// <param name="orderProducts">orderProducts</param>
        /// <returns>订单商品</returns>
        void UpdateOrderProducts(LF_OrderProducts orderProducts);

		/// <summary>
        /// 批量更新订单商品 
        /// </summary>
        /// <param name="orderProductsList">orderProductsList</param>
        /// <returns>订单商品列表</returns>
        void BatchUpdateOrderProducts(IEnumerable<LF_OrderProducts> orderProductsList);

        /// <summary>
        /// 批量更新订单商品列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_OrderProducts, bool>> whereLambda, Expression<Func<LF_OrderProducts, LF_OrderProducts>> updateExpression);

        /// <summary>
        /// 删除订单商品 
        /// </summary>
        /// <param name="orderProducts">orderProducts</param>
        /// <returns>订单商品</returns>
        void DeleteOrderProducts(LF_OrderProducts orderProducts);

        /// <summary>
        /// 根据查询条件删除订单商品 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单商品</returns>
        void DeleteOrderProducts(Expression<Func<LF_OrderProducts, bool>> whereLambda);

		/// <summary>
        /// 批量删除订单商品 
        /// </summary>
        /// <param name="orderProductsList">orderProductsList</param>
        /// <returns>订单商品列表</returns>
        void BatchDeleteOrderProducts(IEnumerable<LF_OrderProducts> orderProductsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_OrderProducts, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取订单商品 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>订单商品</returns>
        LF_OrderProducts GetOrderProductsById(int id);

		/// <summary>
        /// 根据查询条件获取单条订单商品
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单商品</returns>
        LF_OrderProducts Get(Expression<Func<LF_OrderProducts, bool>> whereLambda);

        /// <summary>
        /// 获取所有订单商品
        /// </summary>
        /// <returns>订单商品列表</returns>
        IEnumerable<LF_OrderProducts> GetAll();

        /// <summary>
        /// 根据查询条件获取订单商品列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>订单商品列表</returns>
        IEnumerable<LF_OrderProducts> GetList(Expression<Func<LF_OrderProducts, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_OrderProducts LoadEntitieNoTracking(Expression<Func<LF_OrderProducts, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_OrderProducts> LoadEntitiesNoTracking(Expression<Func<LF_OrderProducts, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>订单商品列表</returns>
        IList<LF_OrderProducts> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_OrderProducts, bool>> whereLambda,
            Expression<Func<LF_OrderProducts, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_OrderProducts, bool>> whereLambda);

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
        /// <returns>订单商品列表</returns>
        IList<LF_OrderProducts> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_OrderProducts, bool>> whereLambda, Expression<Func<LF_OrderProducts, TS>> orderbyLambda, bool isAsc);
     }
}
