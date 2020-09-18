
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IRegionsService
     {
        /// <summary>
        /// 添加区域 
        /// </summary>
        /// <param name="regions">regions</param>
        /// <returns>区域</returns>
        void AddRegions(LF_Regions regions);

		/// <summary>
        /// 批量添加区域 
        /// </summary>
        /// <param name="regionsList">regionsList</param>
        /// <returns>区域列表</returns>
        void BatchAddRegions(IEnumerable<LF_Regions> regionsList);

		/// <summary>
        /// 更新区域 
        /// </summary>
        /// <param name="regions">regions</param>
        /// <returns>区域</returns>
        void UpdateRegions(LF_Regions regions);

		/// <summary>
        /// 批量更新区域 
        /// </summary>
        /// <param name="regionsList">regionsList</param>
        /// <returns>区域列表</returns>
        void BatchUpdateRegions(IEnumerable<LF_Regions> regionsList);

        /// <summary>
        /// 批量更新区域列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Regions, bool>> whereLambda, Expression<Func<LF_Regions, LF_Regions>> updateExpression);

        /// <summary>
        /// 删除区域 
        /// </summary>
        /// <param name="regions">regions</param>
        /// <returns>区域</returns>
        void DeleteRegions(LF_Regions regions);

        /// <summary>
        /// 根据查询条件删除区域 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>区域</returns>
        void DeleteRegions(Expression<Func<LF_Regions, bool>> whereLambda);

		/// <summary>
        /// 批量删除区域 
        /// </summary>
        /// <param name="regionsList">regionsList</param>
        /// <returns>区域列表</returns>
        void BatchDeleteRegions(IEnumerable<LF_Regions> regionsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Regions, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取区域 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>区域</returns>
        LF_Regions GetRegionsById(int id);

		/// <summary>
        /// 根据查询条件获取单条区域
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>区域</returns>
        LF_Regions Get(Expression<Func<LF_Regions, bool>> whereLambda);

        /// <summary>
        /// 获取所有区域
        /// </summary>
        /// <returns>区域列表</returns>
        IEnumerable<LF_Regions> GetAll();

        /// <summary>
        /// 根据查询条件获取区域列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>区域列表</returns>
        IEnumerable<LF_Regions> GetList(Expression<Func<LF_Regions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Regions LoadEntitieNoTracking(Expression<Func<LF_Regions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Regions> LoadEntitiesNoTracking(Expression<Func<LF_Regions, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>区域列表</returns>
        IList<LF_Regions> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Regions, bool>> whereLambda,
            Expression<Func<LF_Regions, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Regions, bool>> whereLambda);

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
        /// <returns>区域列表</returns>
        IList<LF_Regions> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Regions, bool>> whereLambda, Expression<Func<LF_Regions, TS>> orderbyLambda, bool isAsc);
     }
}
