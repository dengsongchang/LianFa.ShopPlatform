
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
	 public partial class CategoriesService : ICategoriesService
     {

        #region Fields

        private readonly IRepository<LF_Categories> _categoriesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="categoriesRepository">分类仓储类</param>
        public CategoriesService(IRepository<LF_Categories> categoriesRepository)
        {
            this._categoriesRepository = categoriesRepository;
        }

        #endregion

        /// <summary>
        /// 添加分类 
        /// </summary>
        /// <param name="categories">categories</param>
        /// <returns>分类</returns>
        public void AddCategories(LF_Categories categories)
        {
            _categoriesRepository.Add(categories);
        }

		/// <summary>
        /// 批量添加分类 
        /// </summary>
        /// <param name="categoriesList">categoriesList</param>
        /// <returns>分类列表</returns>
        public void BatchAddCategories(IEnumerable<LF_Categories> categoriesList)
        {
            _categoriesRepository.BatchAdd(categoriesList);
        }

		/// <summary>
        /// 更新分类 
        /// </summary>
        /// <param name="categories">categories</param>
        /// <returns>分类</returns>
        public void UpdateCategories(LF_Categories categories)
        {
            _categoriesRepository.Update(categories);
        }

		/// <summary>
        /// 批量更新分类 
        /// </summary>
        /// <param name="categoriesList">categoriesList</param>
        /// <returns>分类列表</returns>
        public void BatchUpdateCategories(IEnumerable<LF_Categories> categoriesList)
        {
            _categoriesRepository.BatchUpdate(categoriesList);
        }

        /// <summary>
        /// 批量更新分类列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_Categories, bool>> whereLambda, Expression<Func<LF_Categories, LF_Categories>> updateExpression)
        {
             return _categoriesRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除分类 
        /// </summary>
        /// <param name="categories">categories</param>
        /// <returns>分类</returns>
        public void DeleteCategories(LF_Categories categories)
        {
            _categoriesRepository.Delete(categories);  
        }

        /// <summary>
        /// 根据查询条件删除分类 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>分类</returns>
        public void DeleteCategories(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            _categoriesRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除分类 
        /// </summary>
        /// <param name="categoriesList">categoriesList</param>
        /// <returns>分类列表</returns>
        public void BatchDeleteCategories(IEnumerable<LF_Categories> categoriesList)
        {
            _categoriesRepository.BatchDelete(categoriesList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            return _categoriesRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取分类 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>分类</returns>
        public LF_Categories GetCategoriesById(int id)
        {
            return _categoriesRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条分类
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>分类</returns>
        public LF_Categories Get(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            return _categoriesRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns>分类列表</returns>
        public IEnumerable<LF_Categories> GetAll()
        {
            return _categoriesRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取分类列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>分类列表</returns>
        public IEnumerable<LF_Categories> GetList(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            return _categoriesRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_Categories LoadEntitieNoTracking(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            return _categoriesRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_Categories> LoadEntitiesNoTracking(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            return _categoriesRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>分类列表</returns>
        public IList<LF_Categories> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_Categories, bool>> whereLambda,
            Expression<Func<LF_Categories, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _categoriesRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_Categories, bool>> whereLambda)
        {
            return _categoriesRepository.Exist(whereLambda);
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
        /// <returns>分类列表</returns>
        public IList<LF_Categories> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Categories, bool>> whereLambda, Expression<Func<LF_Categories, TS>> orderbyLambda, bool isAsc)
        {
            return _categoriesRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
