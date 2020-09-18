
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
	 public partial class ShippingAppointRegionsService : IShippingAppointRegionsService
     {

        #region Fields

        private readonly IRepository<LF_ShippingAppointRegions> _shippingAppointRegionsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingAppointRegionsRepository">指定包邮地区仓储类</param>
        public ShippingAppointRegionsService(IRepository<LF_ShippingAppointRegions> shippingAppointRegionsRepository)
        {
            this._shippingAppointRegionsRepository = shippingAppointRegionsRepository;
        }

        #endregion

        /// <summary>
        /// 添加指定包邮地区 
        /// </summary>
        /// <param name="shippingAppointRegions">shippingAppointRegions</param>
        /// <returns>指定包邮地区</returns>
        public void AddShippingAppointRegions(LF_ShippingAppointRegions shippingAppointRegions)
        {
            _shippingAppointRegionsRepository.Add(shippingAppointRegions);
        }

		/// <summary>
        /// 批量添加指定包邮地区 
        /// </summary>
        /// <param name="shippingAppointRegionsList">shippingAppointRegionsList</param>
        /// <returns>指定包邮地区列表</returns>
        public void BatchAddShippingAppointRegions(IEnumerable<LF_ShippingAppointRegions> shippingAppointRegionsList)
        {
            _shippingAppointRegionsRepository.BatchAdd(shippingAppointRegionsList);
        }

		/// <summary>
        /// 更新指定包邮地区 
        /// </summary>
        /// <param name="shippingAppointRegions">shippingAppointRegions</param>
        /// <returns>指定包邮地区</returns>
        public void UpdateShippingAppointRegions(LF_ShippingAppointRegions shippingAppointRegions)
        {
            _shippingAppointRegionsRepository.Update(shippingAppointRegions);
        }

		/// <summary>
        /// 批量更新指定包邮地区 
        /// </summary>
        /// <param name="shippingAppointRegionsList">shippingAppointRegionsList</param>
        /// <returns>指定包邮地区列表</returns>
        public void BatchUpdateShippingAppointRegions(IEnumerable<LF_ShippingAppointRegions> shippingAppointRegionsList)
        {
            _shippingAppointRegionsRepository.BatchUpdate(shippingAppointRegionsList);
        }

        /// <summary>
        /// 批量更新指定包邮地区列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda, Expression<Func<LF_ShippingAppointRegions, LF_ShippingAppointRegions>> updateExpression)
        {
             return _shippingAppointRegionsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除指定包邮地区 
        /// </summary>
        /// <param name="shippingAppointRegions">shippingAppointRegions</param>
        /// <returns>指定包邮地区</returns>
        public void DeleteShippingAppointRegions(LF_ShippingAppointRegions shippingAppointRegions)
        {
            _shippingAppointRegionsRepository.Delete(shippingAppointRegions);  
        }

        /// <summary>
        /// 根据查询条件删除指定包邮地区 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>指定包邮地区</returns>
        public void DeleteShippingAppointRegions(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            _shippingAppointRegionsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除指定包邮地区 
        /// </summary>
        /// <param name="shippingAppointRegionsList">shippingAppointRegionsList</param>
        /// <returns>指定包邮地区列表</returns>
        public void BatchDeleteShippingAppointRegions(IEnumerable<LF_ShippingAppointRegions> shippingAppointRegionsList)
        {
            _shippingAppointRegionsRepository.BatchDelete(shippingAppointRegionsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            return _shippingAppointRegionsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取指定包邮地区 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>指定包邮地区</returns>
        public LF_ShippingAppointRegions GetShippingAppointRegionsById(int id)
        {
            return _shippingAppointRegionsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条指定包邮地区
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>指定包邮地区</returns>
        public LF_ShippingAppointRegions Get(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            return _shippingAppointRegionsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有指定包邮地区
        /// </summary>
        /// <returns>指定包邮地区列表</returns>
        public IEnumerable<LF_ShippingAppointRegions> GetAll()
        {
            return _shippingAppointRegionsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取指定包邮地区列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>指定包邮地区列表</returns>
        public IEnumerable<LF_ShippingAppointRegions> GetList(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            return _shippingAppointRegionsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShippingAppointRegions LoadEntitieNoTracking(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            return _shippingAppointRegionsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShippingAppointRegions> LoadEntitiesNoTracking(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            return _shippingAppointRegionsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>指定包邮地区列表</returns>
        public IList<LF_ShippingAppointRegions> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda,
            Expression<Func<LF_ShippingAppointRegions, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shippingAppointRegionsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda)
        {
            return _shippingAppointRegionsRepository.Exist(whereLambda);
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
        /// <returns>指定包邮地区列表</returns>
        public IList<LF_ShippingAppointRegions> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingAppointRegions, bool>> whereLambda, Expression<Func<LF_ShippingAppointRegions, TS>> orderbyLambda, bool isAsc)
        {
            return _shippingAppointRegionsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
