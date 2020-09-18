
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
	 public partial class CouponTypesService : ICouponTypesService
     {

        #region Fields

        private readonly IRepository<LF_CouponTypes> _couponTypesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="couponTypesRepository">礼品劵类型仓储类</param>
        public CouponTypesService(IRepository<LF_CouponTypes> couponTypesRepository)
        {
            this._couponTypesRepository = couponTypesRepository;
        }

        #endregion

        /// <summary>
        /// 添加礼品劵类型 
        /// </summary>
        /// <param name="couponTypes">couponTypes</param>
        /// <returns>礼品劵类型</returns>
        public void AddCouponTypes(LF_CouponTypes couponTypes)
        {
            _couponTypesRepository.Add(couponTypes);
        }

		/// <summary>
        /// 批量添加礼品劵类型 
        /// </summary>
        /// <param name="couponTypesList">couponTypesList</param>
        /// <returns>礼品劵类型列表</returns>
        public void BatchAddCouponTypes(IEnumerable<LF_CouponTypes> couponTypesList)
        {
            _couponTypesRepository.BatchAdd(couponTypesList);
        }

		/// <summary>
        /// 更新礼品劵类型 
        /// </summary>
        /// <param name="couponTypes">couponTypes</param>
        /// <returns>礼品劵类型</returns>
        public void UpdateCouponTypes(LF_CouponTypes couponTypes)
        {
            _couponTypesRepository.Update(couponTypes);
        }

		/// <summary>
        /// 批量更新礼品劵类型 
        /// </summary>
        /// <param name="couponTypesList">couponTypesList</param>
        /// <returns>礼品劵类型列表</returns>
        public void BatchUpdateCouponTypes(IEnumerable<LF_CouponTypes> couponTypesList)
        {
            _couponTypesRepository.BatchUpdate(couponTypesList);
        }

        /// <summary>
        /// 批量更新礼品劵类型列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_CouponTypes, bool>> whereLambda, Expression<Func<LF_CouponTypes, LF_CouponTypes>> updateExpression)
        {
             return _couponTypesRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除礼品劵类型 
        /// </summary>
        /// <param name="couponTypes">couponTypes</param>
        /// <returns>礼品劵类型</returns>
        public void DeleteCouponTypes(LF_CouponTypes couponTypes)
        {
            _couponTypesRepository.Delete(couponTypes);  
        }

        /// <summary>
        /// 根据查询条件删除礼品劵类型 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品劵类型</returns>
        public void DeleteCouponTypes(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            _couponTypesRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除礼品劵类型 
        /// </summary>
        /// <param name="couponTypesList">couponTypesList</param>
        /// <returns>礼品劵类型列表</returns>
        public void BatchDeleteCouponTypes(IEnumerable<LF_CouponTypes> couponTypesList)
        {
            _couponTypesRepository.BatchDelete(couponTypesList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            return _couponTypesRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取礼品劵类型 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>礼品劵类型</returns>
        public LF_CouponTypes GetCouponTypesById(int id)
        {
            return _couponTypesRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条礼品劵类型
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品劵类型</returns>
        public LF_CouponTypes Get(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            return _couponTypesRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有礼品劵类型
        /// </summary>
        /// <returns>礼品劵类型列表</returns>
        public IEnumerable<LF_CouponTypes> GetAll()
        {
            return _couponTypesRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取礼品劵类型列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品劵类型列表</returns>
        public IEnumerable<LF_CouponTypes> GetList(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            return _couponTypesRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_CouponTypes LoadEntitieNoTracking(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            return _couponTypesRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_CouponTypes> LoadEntitiesNoTracking(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            return _couponTypesRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>礼品劵类型列表</returns>
        public IList<LF_CouponTypes> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_CouponTypes, bool>> whereLambda,
            Expression<Func<LF_CouponTypes, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _couponTypesRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_CouponTypes, bool>> whereLambda)
        {
            return _couponTypesRepository.Exist(whereLambda);
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
        /// <returns>礼品劵类型列表</returns>
        public IList<LF_CouponTypes> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_CouponTypes, bool>> whereLambda, Expression<Func<LF_CouponTypes, TS>> orderbyLambda, bool isAsc)
        {
            return _couponTypesRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
