
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IOauthService
     {
        /// <summary>
        /// 添加开放授权 
        /// </summary>
        /// <param name="oauth">oauth</param>
        /// <returns>开放授权</returns>
        void AddOauth(LF_Oauth oauth);

		/// <summary>
        /// 批量添加开放授权 
        /// </summary>
        /// <param name="oauthList">oauthList</param>
        /// <returns>开放授权列表</returns>
        void BatchAddOauth(IEnumerable<LF_Oauth> oauthList);

		/// <summary>
        /// 更新开放授权 
        /// </summary>
        /// <param name="oauth">oauth</param>
        /// <returns>开放授权</returns>
        void UpdateOauth(LF_Oauth oauth);

		/// <summary>
        /// 批量更新开放授权 
        /// </summary>
        /// <param name="oauthList">oauthList</param>
        /// <returns>开放授权列表</returns>
        void BatchUpdateOauth(IEnumerable<LF_Oauth> oauthList);

        /// <summary>
        /// 批量更新开放授权列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Oauth, bool>> whereLambda, Expression<Func<LF_Oauth, LF_Oauth>> updateExpression);

        /// <summary>
        /// 删除开放授权 
        /// </summary>
        /// <param name="oauth">oauth</param>
        /// <returns>开放授权</returns>
        void DeleteOauth(LF_Oauth oauth);

        /// <summary>
        /// 根据查询条件删除开放授权 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>开放授权</returns>
        void DeleteOauth(Expression<Func<LF_Oauth, bool>> whereLambda);

		/// <summary>
        /// 批量删除开放授权 
        /// </summary>
        /// <param name="oauthList">oauthList</param>
        /// <returns>开放授权列表</returns>
        void BatchDeleteOauth(IEnumerable<LF_Oauth> oauthList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Oauth, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取开放授权 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>开放授权</returns>
        LF_Oauth GetOauthById(int id);

		/// <summary>
        /// 根据查询条件获取单条开放授权
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>开放授权</returns>
        LF_Oauth Get(Expression<Func<LF_Oauth, bool>> whereLambda);

        /// <summary>
        /// 获取所有开放授权
        /// </summary>
        /// <returns>开放授权列表</returns>
        IEnumerable<LF_Oauth> GetAll();

        /// <summary>
        /// 根据查询条件获取开放授权列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>开放授权列表</returns>
        IEnumerable<LF_Oauth> GetList(Expression<Func<LF_Oauth, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Oauth LoadEntitieNoTracking(Expression<Func<LF_Oauth, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Oauth> LoadEntitiesNoTracking(Expression<Func<LF_Oauth, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>开放授权列表</returns>
        IList<LF_Oauth> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Oauth, bool>> whereLambda,
            Expression<Func<LF_Oauth, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Oauth, bool>> whereLambda);

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
        /// <returns>开放授权列表</returns>
        IList<LF_Oauth> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Oauth, bool>> whereLambda, Expression<Func<LF_Oauth, TS>> orderbyLambda, bool isAsc);
     }
}
