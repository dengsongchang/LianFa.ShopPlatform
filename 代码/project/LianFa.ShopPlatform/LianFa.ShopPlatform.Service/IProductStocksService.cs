
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IProductStocksService
     {
        /// <summary>
        /// 添加商品库存 
        /// </summary>
        /// <param name="productStocks">productStocks</param>
        /// <returns>商品库存</returns>
        void AddProductStocks(LF_ProductStocks productStocks);

		/// <summary>
        /// 批量添加商品库存 
        /// </summary>
        /// <param name="productStocksList">productStocksList</param>
        /// <returns>商品库存列表</returns>
        void BatchAddProductStocks(IEnumerable<LF_ProductStocks> productStocksList);

		/// <summary>
        /// 更新商品库存 
        /// </summary>
        /// <param name="productStocks">productStocks</param>
        /// <returns>商品库存</returns>
        void UpdateProductStocks(LF_ProductStocks productStocks);

		/// <summary>
        /// 批量更新商品库存 
        /// </summary>
        /// <param name="productStocksList">productStocksList</param>
        /// <returns>商品库存列表</returns>
        void BatchUpdateProductStocks(IEnumerable<LF_ProductStocks> productStocksList);

        /// <summary>
        /// 批量更新商品库存列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ProductStocks, bool>> whereLambda, Expression<Func<LF_ProductStocks, LF_ProductStocks>> updateExpression);

        /// <summary>
        /// 删除商品库存 
        /// </summary>
        /// <param name="productStocks">productStocks</param>
        /// <returns>商品库存</returns>
        void DeleteProductStocks(LF_ProductStocks productStocks);

        /// <summary>
        /// 根据查询条件删除商品库存 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品库存</returns>
        void DeleteProductStocks(Expression<Func<LF_ProductStocks, bool>> whereLambda);

		/// <summary>
        /// 批量删除商品库存 
        /// </summary>
        /// <param name="productStocksList">productStocksList</param>
        /// <returns>商品库存列表</returns>
        void BatchDeleteProductStocks(IEnumerable<LF_ProductStocks> productStocksList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ProductStocks, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取商品库存 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>商品库存</returns>
        LF_ProductStocks GetProductStocksById(int id);

		/// <summary>
        /// 根据查询条件获取单条商品库存
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品库存</returns>
        LF_ProductStocks Get(Expression<Func<LF_ProductStocks, bool>> whereLambda);

        /// <summary>
        /// 获取所有商品库存
        /// </summary>
        /// <returns>商品库存列表</returns>
        IEnumerable<LF_ProductStocks> GetAll();

        /// <summary>
        /// 根据查询条件获取商品库存列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品库存列表</returns>
        IEnumerable<LF_ProductStocks> GetList(Expression<Func<LF_ProductStocks, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ProductStocks LoadEntitieNoTracking(Expression<Func<LF_ProductStocks, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ProductStocks> LoadEntitiesNoTracking(Expression<Func<LF_ProductStocks, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商品库存列表</returns>
        IList<LF_ProductStocks> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ProductStocks, bool>> whereLambda,
            Expression<Func<LF_ProductStocks, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ProductStocks, bool>> whereLambda);

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
        /// <returns>商品库存列表</returns>
        IList<LF_ProductStocks> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ProductStocks, bool>> whereLambda, Expression<Func<LF_ProductStocks, TS>> orderbyLambda, bool isAsc);
     }
}
