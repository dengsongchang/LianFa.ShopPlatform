
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
	 public partial class ProductsService : IProductsService
     {

        #region Fields

        private readonly IRepository<LF_Products> _productsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productsRepository">商品仓储类</param>
        public ProductsService(IRepository<LF_Products> productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        #endregion

        /// <summary>
        /// 添加商品 
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>商品</returns>
        public void AddProducts(LF_Products products)
        {
            _productsRepository.Add(products);
        }

		/// <summary>
        /// 批量添加商品 
        /// </summary>
        /// <param name="productsList">productsList</param>
        /// <returns>商品列表</returns>
        public void BatchAddProducts(IEnumerable<LF_Products> productsList)
        {
            _productsRepository.BatchAdd(productsList);
        }

		/// <summary>
        /// 更新商品 
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>商品</returns>
        public void UpdateProducts(LF_Products products)
        {
            _productsRepository.Update(products);
        }

		/// <summary>
        /// 批量更新商品 
        /// </summary>
        /// <param name="productsList">productsList</param>
        /// <returns>商品列表</returns>
        public void BatchUpdateProducts(IEnumerable<LF_Products> productsList)
        {
            _productsRepository.BatchUpdate(productsList);
        }

        /// <summary>
        /// 批量更新商品列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_Products, bool>> whereLambda, Expression<Func<LF_Products, LF_Products>> updateExpression)
        {
             return _productsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除商品 
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>商品</returns>
        public void DeleteProducts(LF_Products products)
        {
            _productsRepository.Delete(products);  
        }

        /// <summary>
        /// 根据查询条件删除商品 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品</returns>
        public void DeleteProducts(Expression<Func<LF_Products, bool>> whereLambda)
        {
            _productsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除商品 
        /// </summary>
        /// <param name="productsList">productsList</param>
        /// <returns>商品列表</returns>
        public void BatchDeleteProducts(IEnumerable<LF_Products> productsList)
        {
            _productsRepository.BatchDelete(productsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_Products, bool>> whereLambda)
        {
            return _productsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取商品 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>商品</returns>
        public LF_Products GetProductsById(int id)
        {
            return _productsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条商品
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品</returns>
        public LF_Products Get(Expression<Func<LF_Products, bool>> whereLambda)
        {
            return _productsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <returns>商品列表</returns>
        public IEnumerable<LF_Products> GetAll()
        {
            return _productsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取商品列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品列表</returns>
        public IEnumerable<LF_Products> GetList(Expression<Func<LF_Products, bool>> whereLambda)
        {
            return _productsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_Products LoadEntitieNoTracking(Expression<Func<LF_Products, bool>> whereLambda)
        {
            return _productsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_Products> LoadEntitiesNoTracking(Expression<Func<LF_Products, bool>> whereLambda)
        {
            return _productsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商品列表</returns>
        public IList<LF_Products> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_Products, bool>> whereLambda,
            Expression<Func<LF_Products, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _productsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_Products, bool>> whereLambda)
        {
            return _productsRepository.Exist(whereLambda);
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
        /// <returns>商品列表</returns>
        public IList<LF_Products> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Products, bool>> whereLambda, Expression<Func<LF_Products, TS>> orderbyLambda, bool isAsc)
        {
            return _productsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
