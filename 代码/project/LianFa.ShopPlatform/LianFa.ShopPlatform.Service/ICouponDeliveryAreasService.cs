
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface ICouponDeliveryAreasService
     {
        /// <summary>
        /// 添加 
        /// </summary>
        /// <param name="couponDeliveryAreas">couponDeliveryAreas</param>
        /// <returns></returns>
        void AddCouponDeliveryAreas(LF_CouponDeliveryAreas couponDeliveryAreas);

		/// <summary>
        /// 批量添加 
        /// </summary>
        /// <param name="couponDeliveryAreasList">couponDeliveryAreasList</param>
        /// <returns>列表</returns>
        void BatchAddCouponDeliveryAreas(IEnumerable<LF_CouponDeliveryAreas> couponDeliveryAreasList);

		/// <summary>
        /// 更新 
        /// </summary>
        /// <param name="couponDeliveryAreas">couponDeliveryAreas</param>
        /// <returns></returns>
        void UpdateCouponDeliveryAreas(LF_CouponDeliveryAreas couponDeliveryAreas);

		/// <summary>
        /// 批量更新 
        /// </summary>
        /// <param name="couponDeliveryAreasList">couponDeliveryAreasList</param>
        /// <returns>列表</returns>
        void BatchUpdateCouponDeliveryAreas(IEnumerable<LF_CouponDeliveryAreas> couponDeliveryAreasList);

        /// <summary>
        /// 批量更新列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda, Expression<Func<LF_CouponDeliveryAreas, LF_CouponDeliveryAreas>> updateExpression);

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="couponDeliveryAreas">couponDeliveryAreas</param>
        /// <returns></returns>
        void DeleteCouponDeliveryAreas(LF_CouponDeliveryAreas couponDeliveryAreas);

        /// <summary>
        /// 根据查询条件删除 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        void DeleteCouponDeliveryAreas(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

		/// <summary>
        /// 批量删除 
        /// </summary>
        /// <param name="couponDeliveryAreasList">couponDeliveryAreasList</param>
        /// <returns>列表</returns>
        void BatchDeleteCouponDeliveryAreas(IEnumerable<LF_CouponDeliveryAreas> couponDeliveryAreasList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        LF_CouponDeliveryAreas GetCouponDeliveryAreasById(int id);

		/// <summary>
        /// 根据查询条件获取单条
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_CouponDeliveryAreas Get(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns>列表</returns>
        IEnumerable<LF_CouponDeliveryAreas> GetAll();

        /// <summary>
        /// 根据查询条件获取列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>列表</returns>
        IEnumerable<LF_CouponDeliveryAreas> GetList(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_CouponDeliveryAreas LoadEntitieNoTracking(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_CouponDeliveryAreas> LoadEntitiesNoTracking(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>列表</returns>
        IList<LF_CouponDeliveryAreas> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda,
            Expression<Func<LF_CouponDeliveryAreas, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda);

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
        /// <returns>列表</returns>
        IList<LF_CouponDeliveryAreas> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_CouponDeliveryAreas, bool>> whereLambda, Expression<Func<LF_CouponDeliveryAreas, TS>> orderbyLambda, bool isAsc);
     }
}
