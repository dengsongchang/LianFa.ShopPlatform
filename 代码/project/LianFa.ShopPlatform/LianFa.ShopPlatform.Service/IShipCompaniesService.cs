
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IShipCompaniesService
     {
        /// <summary>
        /// 添加配送公司 
        /// </summary>
        /// <param name="shipCompanies">shipCompanies</param>
        /// <returns>配送公司</returns>
        void AddShipCompanies(LF_ShipCompanies shipCompanies);

		/// <summary>
        /// 批量添加配送公司 
        /// </summary>
        /// <param name="shipCompaniesList">shipCompaniesList</param>
        /// <returns>配送公司列表</returns>
        void BatchAddShipCompanies(IEnumerable<LF_ShipCompanies> shipCompaniesList);

		/// <summary>
        /// 更新配送公司 
        /// </summary>
        /// <param name="shipCompanies">shipCompanies</param>
        /// <returns>配送公司</returns>
        void UpdateShipCompanies(LF_ShipCompanies shipCompanies);

		/// <summary>
        /// 批量更新配送公司 
        /// </summary>
        /// <param name="shipCompaniesList">shipCompaniesList</param>
        /// <returns>配送公司列表</returns>
        void BatchUpdateShipCompanies(IEnumerable<LF_ShipCompanies> shipCompaniesList);

        /// <summary>
        /// 批量更新配送公司列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ShipCompanies, bool>> whereLambda, Expression<Func<LF_ShipCompanies, LF_ShipCompanies>> updateExpression);

        /// <summary>
        /// 删除配送公司 
        /// </summary>
        /// <param name="shipCompanies">shipCompanies</param>
        /// <returns>配送公司</returns>
        void DeleteShipCompanies(LF_ShipCompanies shipCompanies);

        /// <summary>
        /// 根据查询条件删除配送公司 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送公司</returns>
        void DeleteShipCompanies(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

		/// <summary>
        /// 批量删除配送公司 
        /// </summary>
        /// <param name="shipCompaniesList">shipCompaniesList</param>
        /// <returns>配送公司列表</returns>
        void BatchDeleteShipCompanies(IEnumerable<LF_ShipCompanies> shipCompaniesList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取配送公司 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送公司</returns>
        LF_ShipCompanies GetShipCompaniesById(int id);

		/// <summary>
        /// 根据查询条件获取单条配送公司
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送公司</returns>
        LF_ShipCompanies Get(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

        /// <summary>
        /// 获取所有配送公司
        /// </summary>
        /// <returns>配送公司列表</returns>
        IEnumerable<LF_ShipCompanies> GetAll();

        /// <summary>
        /// 根据查询条件获取配送公司列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送公司列表</returns>
        IEnumerable<LF_ShipCompanies> GetList(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ShipCompanies LoadEntitieNoTracking(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ShipCompanies> LoadEntitiesNoTracking(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送公司列表</returns>
        IList<LF_ShipCompanies> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ShipCompanies, bool>> whereLambda,
            Expression<Func<LF_ShipCompanies, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ShipCompanies, bool>> whereLambda);

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
        /// <returns>配送公司列表</returns>
        IList<LF_ShipCompanies> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShipCompanies, bool>> whereLambda, Expression<Func<LF_ShipCompanies, TS>> orderbyLambda, bool isAsc);
     }
}
