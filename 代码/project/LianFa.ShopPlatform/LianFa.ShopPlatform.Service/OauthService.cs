
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
	 public partial class OauthService : IOauthService
     {

        #region Fields

        private readonly IRepository<LF_Oauth> _oauthRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="oauthRepository">开放授权仓储类</param>
        public OauthService(IRepository<LF_Oauth> oauthRepository)
        {
            this._oauthRepository = oauthRepository;
        }

        #endregion

        /// <summary>
        /// 添加开放授权 
        /// </summary>
        /// <param name="oauth">oauth</param>
        /// <returns>开放授权</returns>
        public void AddOauth(LF_Oauth oauth)
        {
            _oauthRepository.Add(oauth);
        }

		/// <summary>
        /// 批量添加开放授权 
        /// </summary>
        /// <param name="oauthList">oauthList</param>
        /// <returns>开放授权列表</returns>
        public void BatchAddOauth(IEnumerable<LF_Oauth> oauthList)
        {
            _oauthRepository.BatchAdd(oauthList);
        }

		/// <summary>
        /// 更新开放授权 
        /// </summary>
        /// <param name="oauth">oauth</param>
        /// <returns>开放授权</returns>
        public void UpdateOauth(LF_Oauth oauth)
        {
            _oauthRepository.Update(oauth);
        }

		/// <summary>
        /// 批量更新开放授权 
        /// </summary>
        /// <param name="oauthList">oauthList</param>
        /// <returns>开放授权列表</returns>
        public void BatchUpdateOauth(IEnumerable<LF_Oauth> oauthList)
        {
            _oauthRepository.BatchUpdate(oauthList);
        }

        /// <summary>
        /// 批量更新开放授权列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_Oauth, bool>> whereLambda, Expression<Func<LF_Oauth, LF_Oauth>> updateExpression)
        {
             return _oauthRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除开放授权 
        /// </summary>
        /// <param name="oauth">oauth</param>
        /// <returns>开放授权</returns>
        public void DeleteOauth(LF_Oauth oauth)
        {
            _oauthRepository.Delete(oauth);  
        }

        /// <summary>
        /// 根据查询条件删除开放授权 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>开放授权</returns>
        public void DeleteOauth(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            _oauthRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除开放授权 
        /// </summary>
        /// <param name="oauthList">oauthList</param>
        /// <returns>开放授权列表</returns>
        public void BatchDeleteOauth(IEnumerable<LF_Oauth> oauthList)
        {
            _oauthRepository.BatchDelete(oauthList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            return _oauthRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取开放授权 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>开放授权</returns>
        public LF_Oauth GetOauthById(int id)
        {
            return _oauthRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条开放授权
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>开放授权</returns>
        public LF_Oauth Get(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            return _oauthRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有开放授权
        /// </summary>
        /// <returns>开放授权列表</returns>
        public IEnumerable<LF_Oauth> GetAll()
        {
            return _oauthRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取开放授权列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>开放授权列表</returns>
        public IEnumerable<LF_Oauth> GetList(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            return _oauthRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_Oauth LoadEntitieNoTracking(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            return _oauthRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_Oauth> LoadEntitiesNoTracking(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            return _oauthRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>开放授权列表</returns>
        public IList<LF_Oauth> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_Oauth, bool>> whereLambda,
            Expression<Func<LF_Oauth, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _oauthRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_Oauth, bool>> whereLambda)
        {
            return _oauthRepository.Exist(whereLambda);
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
        /// <returns>开放授权列表</returns>
        public IList<LF_Oauth> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Oauth, bool>> whereLambda, Expression<Func<LF_Oauth, TS>> orderbyLambda, bool isAsc)
        {
            return _oauthRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
