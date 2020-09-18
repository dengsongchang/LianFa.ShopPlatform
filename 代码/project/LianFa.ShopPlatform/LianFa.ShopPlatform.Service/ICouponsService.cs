
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface ICouponsService
     {
        /// <summary>
        /// 添加礼品劵 
        /// </summary>
        /// <param name="coupons">coupons</param>
        /// <returns>礼品劵</returns>
        void AddCoupons(LF_Coupons coupons);

		/// <summary>
        /// 批量添加礼品劵 
        /// </summary>
        /// <param name="couponsList">couponsList</param>
        /// <returns>礼品劵列表</returns>
        void BatchAddCoupons(IEnumerable<LF_Coupons> couponsList);

		/// <summary>
        /// 更新礼品劵 
        /// </summary>
        /// <param name="coupons">coupons</param>
        /// <returns>礼品劵</returns>
        void UpdateCoupons(LF_Coupons coupons);

		/// <summary>
        /// 批量更新礼品劵 
        /// </summary>
        /// <param name="couponsList">couponsList</param>
        /// <returns>礼品劵列表</returns>
        void BatchUpdateCoupons(IEnumerable<LF_Coupons> couponsList);

        /// <summary>
        /// 批量更新礼品劵列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Coupons, bool>> whereLambda, Expression<Func<LF_Coupons, LF_Coupons>> updateExpression);

        /// <summary>
        /// 删除礼品劵 
        /// </summary>
        /// <param name="coupons">coupons</param>
        /// <returns>礼品劵</returns>
        void DeleteCoupons(LF_Coupons coupons);

        /// <summary>
        /// 根据查询条件删除礼品劵 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品劵</returns>
        void DeleteCoupons(Expression<Func<LF_Coupons, bool>> whereLambda);

		/// <summary>
        /// 批量删除礼品劵 
        /// </summary>
        /// <param name="couponsList">couponsList</param>
        /// <returns>礼品劵列表</returns>
        void BatchDeleteCoupons(IEnumerable<LF_Coupons> couponsList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Coupons, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取礼品劵 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>礼品劵</returns>
        LF_Coupons GetCouponsById(int id);

		/// <summary>
        /// 根据查询条件获取单条礼品劵
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品劵</returns>
        LF_Coupons Get(Expression<Func<LF_Coupons, bool>> whereLambda);

        /// <summary>
        /// 获取所有礼品劵
        /// </summary>
        /// <returns>礼品劵列表</returns>
        IEnumerable<LF_Coupons> GetAll();

        /// <summary>
        /// 根据查询条件获取礼品劵列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>礼品劵列表</returns>
        IEnumerable<LF_Coupons> GetList(Expression<Func<LF_Coupons, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Coupons LoadEntitieNoTracking(Expression<Func<LF_Coupons, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Coupons> LoadEntitiesNoTracking(Expression<Func<LF_Coupons, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>礼品劵列表</returns>
        IList<LF_Coupons> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Coupons, bool>> whereLambda,
            Expression<Func<LF_Coupons, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Coupons, bool>> whereLambda);

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
        /// <returns>礼品劵列表</returns>
        IList<LF_Coupons> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Coupons, bool>> whereLambda, Expression<Func<LF_Coupons, TS>> orderbyLambda, bool isAsc);
     }
}
