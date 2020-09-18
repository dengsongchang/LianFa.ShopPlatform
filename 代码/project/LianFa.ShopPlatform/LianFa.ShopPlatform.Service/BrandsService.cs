
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
	 public partial class BrandsService : IBrandsService
     {

        #region Fields

        private readonly IRepository<LF_Brands> _brandsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="brandsRepository">品牌仓储类</param>
        public BrandsService(IRepository<LF_Brands> brandsRepository)
        {
            this._brandsRepository = brandsRepository;
        }

        #endregion

        /// <summary>
        /// 添加品牌 
        /// </summary>
        /// <param name="brands">brands</param>
        /// <returns>品牌</returns>
        public void AddBrands(LF_Brands brands)
        {
            _brandsRepository.Add(brands);
        }

		/// <summary>
        /// 批量添加品牌 
        /// </summary>
        /// <param name="brandsList">brandsList</param>
        /// <returns>品牌列表</returns>
        public void BatchAddBrands(IEnumerable<LF_Brands> brandsList)
        {
            _brandsRepository.BatchAdd(brandsList);
        }

		/// <summary>
        /// 更新品牌 
        /// </summary>
        /// <param name="brands">brands</param>
        /// <returns>品牌</returns>
        public void UpdateBrands(LF_Brands brands)
        {
            _brandsRepository.Update(brands);
        }

		/// <summary>
        /// 批量更新品牌 
        /// </summary>
        /// <param name="brandsList">brandsList</param>
        /// <returns>品牌列表</returns>
        public void BatchUpdateBrands(IEnumerable<LF_Brands> brandsList)
        {
            _brandsRepository.BatchUpdate(brandsList);
        }

        /// <summary>
        /// 批量更新品牌列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_Brands, bool>> whereLambda, Expression<Func<LF_Brands, LF_Brands>> updateExpression)
        {
             return _brandsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除品牌 
        /// </summary>
        /// <param name="brands">brands</param>
        /// <returns>品牌</returns>
        public void DeleteBrands(LF_Brands brands)
        {
            _brandsRepository.Delete(brands);  
        }

        /// <summary>
        /// 根据查询条件删除品牌 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>品牌</returns>
        public void DeleteBrands(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            _brandsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除品牌 
        /// </summary>
        /// <param name="brandsList">brandsList</param>
        /// <returns>品牌列表</returns>
        public void BatchDeleteBrands(IEnumerable<LF_Brands> brandsList)
        {
            _brandsRepository.BatchDelete(brandsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            return _brandsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取品牌 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>品牌</returns>
        public LF_Brands GetBrandsById(int id)
        {
            return _brandsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条品牌
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>品牌</returns>
        public LF_Brands Get(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            return _brandsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns>品牌列表</returns>
        public IEnumerable<LF_Brands> GetAll()
        {
            return _brandsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取品牌列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>品牌列表</returns>
        public IEnumerable<LF_Brands> GetList(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            return _brandsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_Brands LoadEntitieNoTracking(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            return _brandsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_Brands> LoadEntitiesNoTracking(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            return _brandsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>品牌列表</returns>
        public IList<LF_Brands> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_Brands, bool>> whereLambda,
            Expression<Func<LF_Brands, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _brandsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_Brands, bool>> whereLambda)
        {
            return _brandsRepository.Exist(whereLambda);
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
        /// <returns>品牌列表</returns>
        public IList<LF_Brands> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Brands, bool>> whereLambda, Expression<Func<LF_Brands, TS>> orderbyLambda, bool isAsc)
        {
            return _brandsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
