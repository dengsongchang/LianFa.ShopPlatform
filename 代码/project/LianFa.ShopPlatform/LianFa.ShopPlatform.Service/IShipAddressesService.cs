
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IShipAddressesService
     {
        /// <summary>
        /// 添加配送地址 
        /// </summary>
        /// <param name="shipAddresses">shipAddresses</param>
        /// <returns>配送地址</returns>
        void AddShipAddresses(LF_ShipAddresses shipAddresses);

		/// <summary>
        /// 批量添加配送地址 
        /// </summary>
        /// <param name="shipAddressesList">shipAddressesList</param>
        /// <returns>配送地址列表</returns>
        void BatchAddShipAddresses(IEnumerable<LF_ShipAddresses> shipAddressesList);

		/// <summary>
        /// 更新配送地址 
        /// </summary>
        /// <param name="shipAddresses">shipAddresses</param>
        /// <returns>配送地址</returns>
        void UpdateShipAddresses(LF_ShipAddresses shipAddresses);

		/// <summary>
        /// 批量更新配送地址 
        /// </summary>
        /// <param name="shipAddressesList">shipAddressesList</param>
        /// <returns>配送地址列表</returns>
        void BatchUpdateShipAddresses(IEnumerable<LF_ShipAddresses> shipAddressesList);

        /// <summary>
        /// 批量更新配送地址列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ShipAddresses, bool>> whereLambda, Expression<Func<LF_ShipAddresses, LF_ShipAddresses>> updateExpression);

        /// <summary>
        /// 删除配送地址 
        /// </summary>
        /// <param name="shipAddresses">shipAddresses</param>
        /// <returns>配送地址</returns>
        void DeleteShipAddresses(LF_ShipAddresses shipAddresses);

        /// <summary>
        /// 根据查询条件删除配送地址 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地址</returns>
        void DeleteShipAddresses(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

		/// <summary>
        /// 批量删除配送地址 
        /// </summary>
        /// <param name="shipAddressesList">shipAddressesList</param>
        /// <returns>配送地址列表</returns>
        void BatchDeleteShipAddresses(IEnumerable<LF_ShipAddresses> shipAddressesList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取配送地址 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送地址</returns>
        LF_ShipAddresses GetShipAddressesById(int id);

		/// <summary>
        /// 根据查询条件获取单条配送地址
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地址</returns>
        LF_ShipAddresses Get(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

        /// <summary>
        /// 获取所有配送地址
        /// </summary>
        /// <returns>配送地址列表</returns>
        IEnumerable<LF_ShipAddresses> GetAll();

        /// <summary>
        /// 根据查询条件获取配送地址列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地址列表</returns>
        IEnumerable<LF_ShipAddresses> GetList(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ShipAddresses LoadEntitieNoTracking(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ShipAddresses> LoadEntitiesNoTracking(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送地址列表</returns>
        IList<LF_ShipAddresses> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ShipAddresses, bool>> whereLambda,
            Expression<Func<LF_ShipAddresses, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ShipAddresses, bool>> whereLambda);

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
        /// <returns>配送地址列表</returns>
        IList<LF_ShipAddresses> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShipAddresses, bool>> whereLambda, Expression<Func<LF_ShipAddresses, TS>> orderbyLambda, bool isAsc);
     }
}
