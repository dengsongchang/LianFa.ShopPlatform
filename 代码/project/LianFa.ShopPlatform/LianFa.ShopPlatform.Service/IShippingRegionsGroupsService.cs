
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IShippingRegionsGroupsService
     {
        /// <summary>
        /// 添加配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroups">shippingRegionsGroups</param>
        /// <returns>配送地区价格</returns>
        void AddShippingRegionsGroups(LF_ShippingRegionsGroups shippingRegionsGroups);

		/// <summary>
        /// 批量添加配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroupsList">shippingRegionsGroupsList</param>
        /// <returns>配送地区价格列表</returns>
        void BatchAddShippingRegionsGroups(IEnumerable<LF_ShippingRegionsGroups> shippingRegionsGroupsList);

		/// <summary>
        /// 更新配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroups">shippingRegionsGroups</param>
        /// <returns>配送地区价格</returns>
        void UpdateShippingRegionsGroups(LF_ShippingRegionsGroups shippingRegionsGroups);

		/// <summary>
        /// 批量更新配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroupsList">shippingRegionsGroupsList</param>
        /// <returns>配送地区价格列表</returns>
        void BatchUpdateShippingRegionsGroups(IEnumerable<LF_ShippingRegionsGroups> shippingRegionsGroupsList);

        /// <summary>
        /// 批量更新配送地区价格列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda, Expression<Func<LF_ShippingRegionsGroups, LF_ShippingRegionsGroups>> updateExpression);

        /// <summary>
        /// 删除配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroups">shippingRegionsGroups</param>
        /// <returns>配送地区价格</returns>
        void DeleteShippingRegionsGroups(LF_ShippingRegionsGroups shippingRegionsGroups);

        /// <summary>
        /// 根据查询条件删除配送地区价格 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区价格</returns>
        void DeleteShippingRegionsGroups(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

		/// <summary>
        /// 批量删除配送地区价格 
        /// </summary>
        /// <param name="shippingRegionsGroupsList">shippingRegionsGroupsList</param>
        /// <returns>配送地区价格列表</returns>
        void BatchDeleteShippingRegionsGroups(IEnumerable<LF_ShippingRegionsGroups> shippingRegionsGroupsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取配送地区价格 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>配送地区价格</returns>
        LF_ShippingRegionsGroups GetShippingRegionsGroupsById(int id);

		/// <summary>
        /// 根据查询条件获取单条配送地区价格
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区价格</returns>
        LF_ShippingRegionsGroups Get(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

        /// <summary>
        /// 获取所有配送地区价格
        /// </summary>
        /// <returns>配送地区价格列表</returns>
        IEnumerable<LF_ShippingRegionsGroups> GetAll();

        /// <summary>
        /// 根据查询条件获取配送地区价格列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>配送地区价格列表</returns>
        IEnumerable<LF_ShippingRegionsGroups> GetList(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ShippingRegionsGroups LoadEntitieNoTracking(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ShippingRegionsGroups> LoadEntitiesNoTracking(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>配送地区价格列表</returns>
        IList<LF_ShippingRegionsGroups> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda,
            Expression<Func<LF_ShippingRegionsGroups, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda);

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
        /// <returns>配送地区价格列表</returns>
        IList<LF_ShippingRegionsGroups> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingRegionsGroups, bool>> whereLambda, Expression<Func<LF_ShippingRegionsGroups, TS>> orderbyLambda, bool isAsc);
     }
}
