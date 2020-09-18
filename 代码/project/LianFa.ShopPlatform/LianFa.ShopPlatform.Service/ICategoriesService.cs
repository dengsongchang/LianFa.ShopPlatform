
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface ICategoriesService
     {
        /// <summary>
        /// 添加分类 
        /// </summary>
        /// <param name="categories">categories</param>
        /// <returns>分类</returns>
        void AddCategories(LF_Categories categories);

		/// <summary>
        /// 批量添加分类 
        /// </summary>
        /// <param name="categoriesList">categoriesList</param>
        /// <returns>分类列表</returns>
        void BatchAddCategories(IEnumerable<LF_Categories> categoriesList);

		/// <summary>
        /// 更新分类 
        /// </summary>
        /// <param name="categories">categories</param>
        /// <returns>分类</returns>
        void UpdateCategories(LF_Categories categories);

		/// <summary>
        /// 批量更新分类 
        /// </summary>
        /// <param name="categoriesList">categoriesList</param>
        /// <returns>分类列表</returns>
        void BatchUpdateCategories(IEnumerable<LF_Categories> categoriesList);

        /// <summary>
        /// 批量更新分类列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Categories, bool>> whereLambda, Expression<Func<LF_Categories, LF_Categories>> updateExpression);

        /// <summary>
        /// 删除分类 
        /// </summary>
        /// <param name="categories">categories</param>
        /// <returns>分类</returns>
        void DeleteCategories(LF_Categories categories);

        /// <summary>
        /// 根据查询条件删除分类 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>分类</returns>
        void DeleteCategories(Expression<Func<LF_Categories, bool>> whereLambda);

		/// <summary>
        /// 批量删除分类 
        /// </summary>
        /// <param name="categoriesList">categoriesList</param>
        /// <returns>分类列表</returns>
        void BatchDeleteCategories(IEnumerable<LF_Categories> categoriesList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Categories, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取分类 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>分类</returns>
        LF_Categories GetCategoriesById(int id);

		/// <summary>
        /// 根据查询条件获取单条分类
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>分类</returns>
        LF_Categories Get(Expression<Func<LF_Categories, bool>> whereLambda);

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns>分类列表</returns>
        IEnumerable<LF_Categories> GetAll();

        /// <summary>
        /// 根据查询条件获取分类列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>分类列表</returns>
        IEnumerable<LF_Categories> GetList(Expression<Func<LF_Categories, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Categories LoadEntitieNoTracking(Expression<Func<LF_Categories, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Categories> LoadEntitiesNoTracking(Expression<Func<LF_Categories, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>分类列表</returns>
        IList<LF_Categories> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Categories, bool>> whereLambda,
            Expression<Func<LF_Categories, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Categories, bool>> whereLambda);

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
        /// <returns>分类列表</returns>
        IList<LF_Categories> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Categories, bool>> whereLambda, Expression<Func<LF_Categories, TS>> orderbyLambda, bool isAsc);
     }
}
