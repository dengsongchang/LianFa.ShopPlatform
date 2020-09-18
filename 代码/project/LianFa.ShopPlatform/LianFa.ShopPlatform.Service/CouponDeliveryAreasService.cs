
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
	 public partial class CouponDeliveryAreasService : ICouponDeliveryAreasService
     {

        #region Fields

        private readonly IRepository<LF_CouponDeliveryAreas> _couponDeliveryAreasRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="couponDeliveryAreasRepository">仓储类</param>
        public CouponDeliveryAreasService(IRepository<LF_CouponDeliveryAreas> couponDeliveryAreasRepository)
        {
            this._couponDeliveryAreasRepository = couponDeliveryAreasRepository;
        }

        #endregion

        /// <summary>
        /// 添加 
        /// </summary>
        /// <param name="couponDeliveryAreas">couponDeliveryAreas</param>
        /// <returns></returns>
        public void AddCouponDeliveryAreas(LF_CouponDeliveryAreas couponDeliveryAreas)
        {
            _couponDeliveryAreasRepository.Add(couponDeliveryAreas);
        }

		/// <summary>
        /// 批量添加 
        /// </summary>
        /// <param name="couponDeliveryAreasList">couponDeliveryAreasList</param>
        /// <returns>列表</returns>
        public void BatchAddCouponDeliveryAreas(IEnumerable<LF_CouponDeliveryAreas> couponDeliveryAreasList)
        {
            _couponDeliveryAreasRepository.BatchAdd(couponDeliveryAreasList);
        }

		/// <summary>
        /// 更新 
        /// </summary>
        /// <param name="couponDeliveryAreas">couponDeliveryAreas</param>
        /// <returns></returns>
        public void UpdateCouponDeliveryAreas(LF_CouponDeliveryAreas couponDeliveryAreas)
        {
            _couponDeliveryAreasRepository.Update(couponDeliveryAreas);
        }

		/// <summary>
        /// 批量更新 
        /// </summary>
        /// <param name="couponDeliveryAreasList">couponDeliveryAreasList</param>
        /// <returns>列表</returns>
        public void BatchUpdateCouponDeliveryAreas(IEnumerable<LF_CouponDeliveryAreas> couponDeliveryAreasList)
        {
            _couponDeliveryAreasRepository.BatchUpdate(couponDeliveryAreasList);
        }

        /// <summary>
        /// 批量更新列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda, Expression<Func<LF_CouponDeliveryAreas, LF_CouponDeliveryAreas>> updateExpression)
        {
             return _couponDeliveryAreasRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="couponDeliveryAreas">couponDeliveryAreas</param>
        /// <returns></returns>
        public void DeleteCouponDeliveryAreas(LF_CouponDeliveryAreas couponDeliveryAreas)
        {
            _couponDeliveryAreasRepository.Delete(couponDeliveryAreas);  
        }

        /// <summary>
        /// 根据查询条件删除 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public void DeleteCouponDeliveryAreas(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            _couponDeliveryAreasRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除 
        /// </summary>
        /// <param name="couponDeliveryAreasList">couponDeliveryAreasList</param>
        /// <returns>列表</returns>
        public void BatchDeleteCouponDeliveryAreas(IEnumerable<LF_CouponDeliveryAreas> couponDeliveryAreasList)
        {
            _couponDeliveryAreasRepository.BatchDelete(couponDeliveryAreasList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            return _couponDeliveryAreasRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public LF_CouponDeliveryAreas GetCouponDeliveryAreasById(int id)
        {
            return _couponDeliveryAreasRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_CouponDeliveryAreas Get(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            return _couponDeliveryAreasRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns>列表</returns>
        public IEnumerable<LF_CouponDeliveryAreas> GetAll()
        {
            return _couponDeliveryAreasRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>列表</returns>
        public IEnumerable<LF_CouponDeliveryAreas> GetList(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            return _couponDeliveryAreasRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_CouponDeliveryAreas LoadEntitieNoTracking(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            return _couponDeliveryAreasRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_CouponDeliveryAreas> LoadEntitiesNoTracking(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            return _couponDeliveryAreasRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>列表</returns>
        public IList<LF_CouponDeliveryAreas> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda,
            Expression<Func<LF_CouponDeliveryAreas, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _couponDeliveryAreasRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda)
        {
            return _couponDeliveryAreasRepository.Exist(whereLambda);
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
        /// <returns>列表</returns>
        public IList<LF_CouponDeliveryAreas> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda, Expression<Func<LF_CouponDeliveryAreas, TS>> orderbyLambda, bool isAsc)
        {
            return _couponDeliveryAreasRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
