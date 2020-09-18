
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IShippingPriceService
     {
        /// <summary>
        /// 添加运费模板计价 
        /// </summary>
        /// <param name="shippingPrice">shippingPrice</param>
        /// <returns>运费模板计价</returns>
        void AddShippingPrice(LF_ShippingPrice shippingPrice);

		/// <summary>
        /// 批量添加运费模板计价 
        /// </summary>
        /// <param name="shippingPriceList">shippingPriceList</param>
        /// <returns>运费模板计价列表</returns>
        void BatchAddShippingPrice(IEnumerable<LF_ShippingPrice> shippingPriceList);

		/// <summary>
        /// 更新运费模板计价 
        /// </summary>
        /// <param name="shippingPrice">shippingPrice</param>
        /// <returns>运费模板计价</returns>
        void UpdateShippingPrice(LF_ShippingPrice shippingPrice);

		/// <summary>
        /// 批量更新运费模板计价 
        /// </summary>
        /// <param name="shippingPriceList">shippingPriceList</param>
        /// <returns>运费模板计价列表</returns>
        void BatchUpdateShippingPrice(IEnumerable<LF_ShippingPrice> shippingPriceList);

        /// <summary>
        /// 批量更新运费模板计价列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ShippingPrice, bool>> whereLambda, Expression<Func<LF_ShippingPrice, LF_ShippingPrice>> updateExpression);

        /// <summary>
        /// 删除运费模板计价 
        /// </summary>
        /// <param name="shippingPrice">shippingPrice</param>
        /// <returns>运费模板计价</returns>
        void DeleteShippingPrice(LF_ShippingPrice shippingPrice);

        /// <summary>
        /// 根据查询条件删除运费模板计价 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板计价</returns>
        void DeleteShippingPrice(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

		/// <summary>
        /// 批量删除运费模板计价 
        /// </summary>
        /// <param name="shippingPriceList">shippingPriceList</param>
        /// <returns>运费模板计价列表</returns>
        void BatchDeleteShippingPrice(IEnumerable<LF_ShippingPrice> shippingPriceList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取运费模板计价 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>运费模板计价</returns>
        LF_ShippingPrice GetShippingPriceById(int id);

		/// <summary>
        /// 根据查询条件获取单条运费模板计价
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板计价</returns>
        LF_ShippingPrice Get(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

        /// <summary>
        /// 获取所有运费模板计价
        /// </summary>
        /// <returns>运费模板计价列表</returns>
        IEnumerable<LF_ShippingPrice> GetAll();

        /// <summary>
        /// 根据查询条件获取运费模板计价列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板计价列表</returns>
        IEnumerable<LF_ShippingPrice> GetList(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ShippingPrice LoadEntitieNoTracking(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ShippingPrice> LoadEntitiesNoTracking(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>运费模板计价列表</returns>
        IList<LF_ShippingPrice> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ShippingPrice, bool>> whereLambda,
            Expression<Func<LF_ShippingPrice, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ShippingPrice, bool>> whereLambda);

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
        /// <returns>运费模板计价列表</returns>
        IList<LF_ShippingPrice> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingPrice, bool>> whereLambda, Expression<Func<LF_ShippingPrice, TS>> orderbyLambda, bool isAsc);
     }
}
