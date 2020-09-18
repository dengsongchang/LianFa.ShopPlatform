
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
	 public partial class ShipCompaniesService : IShipCompaniesService
     {

        #region Fields

        private readonly IRepository<LF_ShipCompanies> _shipCompaniesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shipCompaniesRepository">配送公司仓储类</param>
        public ShipCompaniesService(IRepository<LF_ShipCompanies> shipCompaniesRepository)
        {
            this._shipCompaniesRepository = shipCompaniesRepository;
        }

        #endregion

        /// <summary>
        /// 添加配送公司 
        /// </summary>
        /// <param name="shipCompanies">shipCompanies</param>
        /// <returns>配送公司</returns>
        public void AddShipCompanies(LF_ShipCompanies shipCompanies)
        {
            _shipCompaniesRepository.Add(shipCompanies);
        }

		/// <summary>
        /// 批量添加配送公司 
        /// </summary>
        /// <param name="shipCompaniesList">shipCompaniesList</param>
        /// <returns>配送公司列表</returns>
        public void BatchAddShipCompanies(IEnumerable<LF_ShipCompanies> shipCompaniesList)
        {
            _shipCompaniesRepository.BatchAdd(shipCompaniesList);
        }

		/// <summary>
        /// 更新配送公司 
        /// </summary>
        /// <param name="shipCompanies">shipCompanies</param>
        /// <returns>配送公司</returns>
        public void UpdateShipCompanies(LF_ShipCompanies shipCompanies)
        {
            _shipCompaniesRepository.Update(shipCompanies);
        }

		/// <summary>
        /// 批量更新配送公司 
        /// </summary>
        /// <param name="shipCompaniesList">shipCompaniesList</param>
        /// <returns>配送公司列表</returns>
        public void BatchUpdateShipCompanies(IEnumerable<LF_ShipCompanies> shipCompaniesList)
        {
            _shipCompaniesRepository.BatchUpdate(shipCompaniesList);
        }

        /// <summary>
        /// 批量更新配送公司列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShipCompanies, bool>> whereLambda, Expression<Func<LF_ShipCompanies, LF_ShipCompanies>> updateExpression)
        {
             return _shipCompaniesRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除配送公司 
        /// </summary>
        /// <param name="shipCompanies">shipCompanies</param>
        /// <returns>配送公司</returns>
        public void DeleteShipCompanies(LF_ShipCompanies shipCompanies)
        {
            _shipCompaniesRepository.Delete(shipCompanies);  
        }

        /// <summary>
        /// 根据查询条件删除配送公司 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送公司</returns>
        public void DeleteShipCompanies(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            _shipCompaniesRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除配送公司 
        /// </summary>
        /// <param name="shipCompaniesList">shipCompaniesList</param>
        /// <returns>配送公司列表</returns>
        public void BatchDeleteShipCompanies(IEnumerable<LF_ShipCompanies> shipCompaniesList)
        {
            _shipCompaniesRepository.BatchDelete(shipCompaniesList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            return _shipCompaniesRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取配送公司 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送公司</returns>
        public LF_ShipCompanies GetShipCompaniesById(int id)
        {
            return _shipCompaniesRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条配送公司
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送公司</returns>
        public LF_ShipCompanies Get(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            return _shipCompaniesRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有配送公司
        /// </summary>
        /// <returns>配送公司列表</returns>
        public IEnumerable<LF_ShipCompanies> GetAll()
        {
            return _shipCompaniesRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取配送公司列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送公司列表</returns>
        public IEnumerable<LF_ShipCompanies> GetList(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            return _shipCompaniesRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShipCompanies LoadEntitieNoTracking(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            return _shipCompaniesRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShipCompanies> LoadEntitiesNoTracking(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            return _shipCompaniesRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送公司列表</returns>
        public IList<LF_ShipCompanies> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShipCompanies, bool>> whereLambda,
            Expression<Func<LF_ShipCompanies, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shipCompaniesRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShipCompanies, bool>> whereLambda)
        {
            return _shipCompaniesRepository.Exist(whereLambda);
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
        /// <returns>配送公司列表</returns>
        public IList<LF_ShipCompanies> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShipCompanies, bool>> whereLambda, Expression<Func<LF_ShipCompanies, TS>> orderbyLambda, bool isAsc)
        {
            return _shipCompaniesRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
