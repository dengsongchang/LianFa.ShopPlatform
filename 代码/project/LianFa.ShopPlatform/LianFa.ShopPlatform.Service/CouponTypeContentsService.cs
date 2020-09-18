
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
	 public partial class CouponTypeContentsService : ICouponTypeContentsService
     {

        #region Fields

        private readonly IRepository<LF_CouponTypeContents> _couponTypeContentsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="couponTypeContentsRepository">礼品卡类型详情仓储类</param>
        public CouponTypeContentsService(IRepository<LF_CouponTypeContents> couponTypeContentsRepository)
        {
            this._couponTypeContentsRepository = couponTypeContentsRepository;
        }

        #endregion

        /// <summary>
        /// 添加礼品卡类型详情 
        /// </summary>
        /// <param name="couponTypeContents">couponTypeContents</param>
        /// <returns>礼品卡类型详情</returns>
        public void AddCouponTypeContents(LF_CouponTypeContents couponTypeContents)
        {
            _couponTypeContentsRepository.Add(couponTypeContents);
        }

		/// <summary>
        /// 批量添加礼品卡类型详情 
        /// </summary>
        /// <param name="couponTypeContentsList">couponTypeContentsList</param>
        /// <returns>礼品卡类型详情列表</returns>
        public void BatchAddCouponTypeContents(IEnumerable<LF_CouponTypeContents> couponTypeContentsList)
        {
            _couponTypeContentsRepository.BatchAdd(couponTypeContentsList);
        }

		/// <summary>
        /// 更新礼品卡类型详情 
        /// </summary>
        /// <param name="couponTypeContents">couponTypeContents</param>
        /// <returns>礼品卡类型详情</returns>
        public void UpdateCouponTypeContents(LF_CouponTypeContents couponTypeContents)
        {
            _couponTypeContentsRepository.Update(couponTypeContents);
        }

		/// <summary>
        /// 批量更新礼品卡类型详情 
        /// </summary>
        /// <param name="couponTypeContentsList">couponTypeContentsList</param>
        /// <returns>礼品卡类型详情列表</returns>
        public void BatchUpdateCouponTypeContents(IEnumerable<LF_CouponTypeContents> couponTypeContentsList)
        {
            _couponTypeContentsRepository.BatchUpdate(couponTypeContentsList);
        }

        /// <summary>
        /// 批量更新礼品卡类型详情列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_CouponTypeContents, bool>> whereLambda, Expression<Func<LF_CouponTypeContents, LF_CouponTypeContents>> updateExpression)
        {
             return _couponTypeContentsRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除礼品卡类型详情 
        /// </summary>
        /// <param name="couponTypeContents">couponTypeContents</param>
        /// <returns>礼品卡类型详情</returns>
        public void DeleteCouponTypeContents(LF_CouponTypeContents couponTypeContents)
        {
            _couponTypeContentsRepository.Delete(couponTypeContents);  
        }

        /// <summary>
        /// 根据查询条件删除礼品卡类型详情 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品卡类型详情</returns>
        public void DeleteCouponTypeContents(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            _couponTypeContentsRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除礼品卡类型详情 
        /// </summary>
        /// <param name="couponTypeContentsList">couponTypeContentsList</param>
        /// <returns>礼品卡类型详情列表</returns>
        public void BatchDeleteCouponTypeContents(IEnumerable<LF_CouponTypeContents> couponTypeContentsList)
        {
            _couponTypeContentsRepository.BatchDelete(couponTypeContentsList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            return _couponTypeContentsRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取礼品卡类型详情 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>礼品卡类型详情</returns>
        public LF_CouponTypeContents GetCouponTypeContentsById(int id)
        {
            return _couponTypeContentsRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条礼品卡类型详情
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品卡类型详情</returns>
        public LF_CouponTypeContents Get(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            return _couponTypeContentsRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有礼品卡类型详情
        /// </summary>
        /// <returns>礼品卡类型详情列表</returns>
        public IEnumerable<LF_CouponTypeContents> GetAll()
        {
            return _couponTypeContentsRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取礼品卡类型详情列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品卡类型详情列表</returns>
        public IEnumerable<LF_CouponTypeContents> GetList(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            return _couponTypeContentsRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_CouponTypeContents LoadEntitieNoTracking(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            return _couponTypeContentsRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_CouponTypeContents> LoadEntitiesNoTracking(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            return _couponTypeContentsRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>礼品卡类型详情列表</returns>
        public IList<LF_CouponTypeContents> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_CouponTypeContents, bool>> whereLambda,
            Expression<Func<LF_CouponTypeContents, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _couponTypeContentsRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_CouponTypeContents, bool>> whereLambda)
        {
            return _couponTypeContentsRepository.Exist(whereLambda);
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
        /// <returns>礼品卡类型详情列表</returns>
        public IList<LF_CouponTypeContents> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_CouponTypeContents, bool>> whereLambda, Expression<Func<LF_CouponTypeContents, TS>> orderbyLambda, bool isAsc)
        {
            return _couponTypeContentsRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
