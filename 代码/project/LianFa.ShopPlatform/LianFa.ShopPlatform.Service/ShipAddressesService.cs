
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
	 public partial class ShipAddressesService : IShipAddressesService
     {

        #region Fields

        private readonly IRepository<LF_ShipAddresses> _shipAddressesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shipAddressesRepository">配送地址仓储类</param>
        public ShipAddressesService(IRepository<LF_ShipAddresses> shipAddressesRepository)
        {
            this._shipAddressesRepository = shipAddressesRepository;
        }

        #endregion

        /// <summary>
        /// 添加配送地址 
        /// </summary>
        /// <param name="shipAddresses">shipAddresses</param>
        /// <returns>配送地址</returns>
        public void AddShipAddresses(LF_ShipAddresses shipAddresses)
        {
            _shipAddressesRepository.Add(shipAddresses);
        }

		/// <summary>
        /// 批量添加配送地址 
        /// </summary>
        /// <param name="shipAddressesList">shipAddressesList</param>
        /// <returns>配送地址列表</returns>
        public void BatchAddShipAddresses(IEnumerable<LF_ShipAddresses> shipAddressesList)
        {
            _shipAddressesRepository.BatchAdd(shipAddressesList);
        }

		/// <summary>
        /// 更新配送地址 
        /// </summary>
        /// <param name="shipAddresses">shipAddresses</param>
        /// <returns>配送地址</returns>
        public void UpdateShipAddresses(LF_ShipAddresses shipAddresses)
        {
            _shipAddressesRepository.Update(shipAddresses);
        }

		/// <summary>
        /// 批量更新配送地址 
        /// </summary>
        /// <param name="shipAddressesList">shipAddressesList</param>
        /// <returns>配送地址列表</returns>
        public void BatchUpdateShipAddresses(IEnumerable<LF_ShipAddresses> shipAddressesList)
        {
            _shipAddressesRepository.BatchUpdate(shipAddressesList);
        }

        /// <summary>
        /// 批量更新配送地址列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShipAddresses, bool>> whereLambda, Expression<Func<LF_ShipAddresses, LF_ShipAddresses>> updateExpression)
        {
             return _shipAddressesRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除配送地址 
        /// </summary>
        /// <param name="shipAddresses">shipAddresses</param>
        /// <returns>配送地址</returns>
        public void DeleteShipAddresses(LF_ShipAddresses shipAddresses)
        {
            _shipAddressesRepository.Delete(shipAddresses);  
        }

        /// <summary>
        /// 根据查询条件删除配送地址 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地址</returns>
        public void DeleteShipAddresses(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            _shipAddressesRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除配送地址 
        /// </summary>
        /// <param name="shipAddressesList">shipAddressesList</param>
        /// <returns>配送地址列表</returns>
        public void BatchDeleteShipAddresses(IEnumerable<LF_ShipAddresses> shipAddressesList)
        {
            _shipAddressesRepository.BatchDelete(shipAddressesList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            return _shipAddressesRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取配送地址 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送地址</returns>
        public LF_ShipAddresses GetShipAddressesById(int id)
        {
            return _shipAddressesRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条配送地址
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地址</returns>
        public LF_ShipAddresses Get(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            return _shipAddressesRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有配送地址
        /// </summary>
        /// <returns>配送地址列表</returns>
        public IEnumerable<LF_ShipAddresses> GetAll()
        {
            return _shipAddressesRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取配送地址列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地址列表</returns>
        public IEnumerable<LF_ShipAddresses> GetList(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            return _shipAddressesRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShipAddresses LoadEntitieNoTracking(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            return _shipAddressesRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShipAddresses> LoadEntitiesNoTracking(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            return _shipAddressesRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送地址列表</returns>
        public IList<LF_ShipAddresses> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShipAddresses, bool>> whereLambda,
            Expression<Func<LF_ShipAddresses, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shipAddressesRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShipAddresses, bool>> whereLambda)
        {
            return _shipAddressesRepository.Exist(whereLambda);
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
        /// <returns>配送地址列表</returns>
        public IList<LF_ShipAddresses> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShipAddresses, bool>> whereLambda, Expression<Func<LF_ShipAddresses, TS>> orderbyLambda, bool isAsc)
        {
            return _shipAddressesRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
