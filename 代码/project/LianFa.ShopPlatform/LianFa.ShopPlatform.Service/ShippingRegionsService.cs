
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
	 public partial class ShippingRegionsService : IShippingRegionsService
     {

        #region Fields

        private readonly IRepository<LF_ShippingRegions> _shippingRegionsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingRegionsRepository">配送地区仓储类</param>
        public ShippingRegionsService(IRepository<LF_ShippingRegions> shippingRegionsRepository)
        {
            this._shippingRegionsRepository = shippingRegionsRepository;
        }

        #endregion

        /// <summary>
        /// 添加配送地区 
        /// </summary>
        /// <param name="shippingRegions">shippingRegions</param>
        /// <returns>配送地区</returns>
        public void AddShippingRegions(LF_ShippingRegions shippingRegions)
        {
            _shippingRegionsRepository.Add(shippingRegions);
        }

		/// <summary>
        /// 批量添加配送地区 
        /// </summary>
        /// <param name="shippingRegionsList">shippingRegionsList</param>
        /// <returns>配送地区列表</returns>
        public void BatchAddShippingRegions(IEnumerable<LF_ShippingRegions> shippingRegionsList)
        {
            _shippingRegionsRepository.BatchAdd(shippingRegionsList);
        }

		/// <summary>
        /// 更新配送地区 
        /// </summary>
        /// <param name="shippingRegions">shippingRegions</param>
        /// <returns>配送地区</returns>
        public void UpdateShippingRegions(LF_ShippingRegions shippingRegions)
        {
            _shippingRegionsRepository.Update(shippingRegions);
        }

		/// <summary>
        /// 批量更新配送地区 
        /// </summary>
        /// <param name="shippingRegionsList">shippingRegionsList</param>
        /// <returns>配送地区列表</returns>
        public void BatchUpdateShippingRegions(IEnumerable<LF_ShippingRegions> shippingRegionsList)
        {
            _shippingRegionsRepository.BatchUpdate(shippingRegionsList);
        }

        /// <summary>
        /// 批量更新配送地区列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShippingRegions, bool>> whereLambda, Expression<Func<LF_ShippingRegions, LF_ShippingRegions>> updateExpression)
        {
             return _shippingRegionsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除配送地区 
        /// </summary>
        /// <param name="shippingRegions">shippingRegions</param>
        /// <returns>配送地区</returns>
        public void DeleteShippingRegions(LF_ShippingRegions shippingRegions)
        {
            _shippingRegionsRepository.Delete(shippingRegions);  
        }

        /// <summary>
        /// 根据查询条件删除配送地区 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区</returns>
        public void DeleteShippingRegions(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            _shippingRegionsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除配送地区 
        /// </summary>
        /// <param name="shippingRegionsList">shippingRegionsList</param>
        /// <returns>配送地区列表</returns>
        public void BatchDeleteShippingRegions(IEnumerable<LF_ShippingRegions> shippingRegionsList)
        {
            _shippingRegionsRepository.BatchDelete(shippingRegionsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            return _shippingRegionsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取配送地区 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送地区</returns>
        public LF_ShippingRegions GetShippingRegionsById(int id)
        {
            return _shippingRegionsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条配送地区
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区</returns>
        public LF_ShippingRegions Get(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            return _shippingRegionsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有配送地区
        /// </summary>
        /// <returns>配送地区列表</returns>
        public IEnumerable<LF_ShippingRegions> GetAll()
        {
            return _shippingRegionsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取配送地区列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区列表</returns>
        public IEnumerable<LF_ShippingRegions> GetList(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            return _shippingRegionsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShippingRegions LoadEntitieNoTracking(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            return _shippingRegionsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShippingRegions> LoadEntitiesNoTracking(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            return _shippingRegionsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送地区列表</returns>
        public IList<LF_ShippingRegions> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShippingRegions, bool>> whereLambda,
            Expression<Func<LF_ShippingRegions, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shippingRegionsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShippingRegions, bool>> whereLambda)
        {
            return _shippingRegionsRepository.Exist(whereLambda);
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
        /// <returns>配送地区列表</returns>
        public IList<LF_ShippingRegions> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingRegions, bool>> whereLambda, Expression<Func<LF_ShippingRegions, TS>> orderbyLambda, bool isAsc)
        {
            return _shippingRegionsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
