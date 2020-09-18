
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
	 public partial class ProductStocksService : IProductStocksService
     {

        #region Fields

        private readonly IRepository<LF_ProductStocks> _productStocksRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productStocksRepository">商品库存仓储类</param>
        public ProductStocksService(IRepository<LF_ProductStocks> productStocksRepository)
        {
            this._productStocksRepository = productStocksRepository;
        }

        #endregion

        /// <summary>
        /// 添加商品库存 
        /// </summary>
        /// <param name="productStocks">productStocks</param>
        /// <returns>商品库存</returns>
        public void AddProductStocks(LF_ProductStocks productStocks)
        {
            _productStocksRepository.Add(productStocks);
        }

		/// <summary>
        /// 批量添加商品库存 
        /// </summary>
        /// <param name="productStocksList">productStocksList</param>
        /// <returns>商品库存列表</returns>
        public void BatchAddProductStocks(IEnumerable<LF_ProductStocks> productStocksList)
        {
            _productStocksRepository.BatchAdd(productStocksList);
        }

		/// <summary>
        /// 更新商品库存 
        /// </summary>
        /// <param name="productStocks">productStocks</param>
        /// <returns>商品库存</returns>
        public void UpdateProductStocks(LF_ProductStocks productStocks)
        {
            _productStocksRepository.Update(productStocks);
        }

		/// <summary>
        /// 批量更新商品库存 
        /// </summary>
        /// <param name="productStocksList">productStocksList</param>
        /// <returns>商品库存列表</returns>
        public void BatchUpdateProductStocks(IEnumerable<LF_ProductStocks> productStocksList)
        {
            _productStocksRepository.BatchUpdate(productStocksList);
        }

        /// <summary>
        /// 批量更新商品库存列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ProductStocks, bool>> whereLambda, Expression<Func<LF_ProductStocks, LF_ProductStocks>> updateExpression)
        {
             return _productStocksRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除商品库存 
        /// </summary>
        /// <param name="productStocks">productStocks</param>
        /// <returns>商品库存</returns>
        public void DeleteProductStocks(LF_ProductStocks productStocks)
        {
            _productStocksRepository.Delete(productStocks);  
        }

        /// <summary>
        /// 根据查询条件删除商品库存 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品库存</returns>
        public void DeleteProductStocks(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            _productStocksRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除商品库存 
        /// </summary>
        /// <param name="productStocksList">productStocksList</param>
        /// <returns>商品库存列表</returns>
        public void BatchDeleteProductStocks(IEnumerable<LF_ProductStocks> productStocksList)
        {
            _productStocksRepository.BatchDelete(productStocksList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            return _productStocksRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取商品库存 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>商品库存</returns>
        public LF_ProductStocks GetProductStocksById(int id)
        {
            return _productStocksRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条商品库存
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品库存</returns>
        public LF_ProductStocks Get(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            return _productStocksRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有商品库存
        /// </summary>
        /// <returns>商品库存列表</returns>
        public IEnumerable<LF_ProductStocks> GetAll()
        {
            return _productStocksRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取商品库存列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品库存列表</returns>
        public IEnumerable<LF_ProductStocks> GetList(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            return _productStocksRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ProductStocks LoadEntitieNoTracking(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            return _productStocksRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ProductStocks> LoadEntitiesNoTracking(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            return _productStocksRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商品库存列表</returns>
        public IList<LF_ProductStocks> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ProductStocks, bool>> whereLambda,
            Expression<Func<LF_ProductStocks, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _productStocksRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ProductStocks, bool>> whereLambda)
        {
            return _productStocksRepository.Exist(whereLambda);
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
        /// <returns>商品库存列表</returns>
        public IList<LF_ProductStocks> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ProductStocks, bool>> whereLambda, Expression<Func<LF_ProductStocks, TS>> orderbyLambda, bool isAsc)
        {
            return _productStocksRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
