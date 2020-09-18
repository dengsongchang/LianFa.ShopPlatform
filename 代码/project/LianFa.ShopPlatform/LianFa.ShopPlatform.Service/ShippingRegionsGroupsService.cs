
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
	 public partial class ShippingRegionsGroupsService : IShippingRegionsGroupsService
     {

        #region Fields

        private readonly IRepository<LF_ShippingRegionsGroups> _shippingRegionsGroupsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingRegionsGroupsRepository">配送地区价格仓储类</param>
        public ShippingRegionsGroupsService(IRepository<LF_ShippingRegionsGroups> shippingRegionsGroupsRepository)
        {
            this._shippingRegionsGroupsRepository = shippingRegionsGroupsRepository;
        }

        #endregion

        /// <summary>
        /// 添加配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroups">shippingRegionsGroups</param>
        /// <returns>配送地区价格</returns>
        public void AddShippingRegionsGroups(LF_ShippingRegionsGroups shippingRegionsGroups)
        {
            _shippingRegionsGroupsRepository.Add(shippingRegionsGroups);
        }

		/// <summary>
        /// 批量添加配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroupsList">shippingRegionsGroupsList</param>
        /// <returns>配送地区价格列表</returns>
        public void BatchAddShippingRegionsGroups(IEnumerable<LF_ShippingRegionsGroups> shippingRegionsGroupsList)
        {
            _shippingRegionsGroupsRepository.BatchAdd(shippingRegionsGroupsList);
        }

		/// <summary>
        /// 更新配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroups">shippingRegionsGroups</param>
        /// <returns>配送地区价格</returns>
        public void UpdateShippingRegionsGroups(LF_ShippingRegionsGroups shippingRegionsGroups)
        {
            _shippingRegionsGroupsRepository.Update(shippingRegionsGroups);
        }

		/// <summary>
        /// 批量更新配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroupsList">shippingRegionsGroupsList</param>
        /// <returns>配送地区价格列表</returns>
        public void BatchUpdateShippingRegionsGroups(IEnumerable<LF_ShippingRegionsGroups> shippingRegionsGroupsList)
        {
            _shippingRegionsGroupsRepository.BatchUpdate(shippingRegionsGroupsList);
        }

        /// <summary>
        /// 批量更新配送地区价格列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda, Expression<Func<LF_ShippingRegionsGroups, LF_ShippingRegionsGroups>> updateExpression)
        {
             return _shippingRegionsGroupsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroups">shippingRegionsGroups</param>
        /// <returns>配送地区价格</returns>
        public void DeleteShippingRegionsGroups(LF_ShippingRegionsGroups shippingRegionsGroups)
        {
            _shippingRegionsGroupsRepository.Delete(shippingRegionsGroups);  
        }

        /// <summary>
        /// 根据查询条件删除配送地区价格 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区价格</returns>
        public void DeleteShippingRegionsGroups(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            _shippingRegionsGroupsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroupsList">shippingRegionsGroupsList</param>
        /// <returns>配送地区价格列表</returns>
        public void BatchDeleteShippingRegionsGroups(IEnumerable<LF_ShippingRegionsGroups> shippingRegionsGroupsList)
        {
            _shippingRegionsGroupsRepository.BatchDelete(shippingRegionsGroupsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            return _shippingRegionsGroupsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取配送地区价格 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送地区价格</returns>
        public LF_ShippingRegionsGroups GetShippingRegionsGroupsById(int id)
        {
            return _shippingRegionsGroupsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条配送地区价格
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区价格</returns>
        public LF_ShippingRegionsGroups Get(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            return _shippingRegionsGroupsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有配送地区价格
        /// </summary>
        /// <returns>配送地区价格列表</returns>
        public IEnumerable<LF_ShippingRegionsGroups> GetAll()
        {
            return _shippingRegionsGroupsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取配送地区价格列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区价格列表</returns>
        public IEnumerable<LF_ShippingRegionsGroups> GetList(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            return _shippingRegionsGroupsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShippingRegionsGroups LoadEntitieNoTracking(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            return _shippingRegionsGroupsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShippingRegionsGroups> LoadEntitiesNoTracking(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            return _shippingRegionsGroupsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送地区价格列表</returns>
        public IList<LF_ShippingRegionsGroups> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda,
            Expression<Func<LF_ShippingRegionsGroups, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shippingRegionsGroupsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda)
        {
            return _shippingRegionsGroupsRepository.Exist(whereLambda);
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
        /// <returns>配送地区价格列表</returns>
        public IList<LF_ShippingRegionsGroups> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda, Expression<Func<LF_ShippingRegionsGroups, TS>> orderbyLambda, bool isAsc)
        {
            return _shippingRegionsGroupsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
