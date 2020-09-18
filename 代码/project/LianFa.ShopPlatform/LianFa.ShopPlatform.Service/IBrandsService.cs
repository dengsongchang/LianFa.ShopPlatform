
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IBrandsService
     {
        /// <summary>
        /// 添加品牌 
        /// </summary>
        /// <param name="brands">brands</param>
        /// <returns>品牌</returns>
        void AddBrands(LF_Brands brands);

		/// <summary>
        /// 批量添加品牌 
        /// </summary>
        /// <param name="brandsList">brandsList</param>
        /// <returns>品牌列表</returns>
        void BatchAddBrands(IEnumerable<LF_Brands> brandsList);

		/// <summary>
        /// 更新品牌 
        /// </summary>
        /// <param name="brands">brands</param>
        /// <returns>品牌</returns>
        void UpdateBrands(LF_Brands brands);

		/// <summary>
        /// 批量更新品牌 
        /// </summary>
        /// <param name="brandsList">brandsList</param>
        /// <returns>品牌列表</returns>
        void BatchUpdateBrands(IEnumerable<LF_Brands> brandsList);

        /// <summary>
        /// 批量更新品牌列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Brands, bool>> whereLambda, Expression<Func<LF_Brands, LF_Brands>> updateExpression);

        /// <summary>
        /// 删除品牌 
        /// </summary>
        /// <param name="brands">brands</param>
        /// <returns>品牌</returns>
        void DeleteBrands(LF_Brands brands);

        /// <summary>
        /// 根据查询条件删除品牌 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>品牌</returns>
        void DeleteBrands(Expression<Func<LF_Brands, bool>> whereLambda);

		/// <summary>
        /// 批量删除品牌 
        /// </summary>
        /// <param name="brandsList">brandsList</param>
        /// <returns>品牌列表</returns>
        void BatchDeleteBrands(IEnumerable<LF_Brands> brandsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Brands, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取品牌 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>品牌</returns>
        LF_Brands GetBrandsById(int id);

		/// <summary>
        /// 根据查询条件获取单条品牌
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>品牌</returns>
        LF_Brands Get(Expression<Func<LF_Brands, bool>> whereLambda);

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns>品牌列表</returns>
        IEnumerable<LF_Brands> GetAll();

        /// <summary>
        /// 根据查询条件获取品牌列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>品牌列表</returns>
        IEnumerable<LF_Brands> GetList(Expression<Func<LF_Brands, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Brands LoadEntitieNoTracking(Expression<Func<LF_Brands, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Brands> LoadEntitiesNoTracking(Expression<Func<LF_Brands, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>品牌列表</returns>
        IList<LF_Brands> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Brands, bool>> whereLambda,
            Expression<Func<LF_Brands, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Brands, bool>> whereLambda);

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
        /// <returns>品牌列表</returns>
        IList<LF_Brands> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Brands, bool>> whereLambda, Expression<Func<LF_Brands, TS>> orderbyLambda, bool isAsc);
     }
}
