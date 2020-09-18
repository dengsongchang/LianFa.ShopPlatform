
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
	 public partial class ShippingPriceService : IShippingPriceService
     {

        #region Fields

        private readonly IRepository<LF_ShippingPrice> _shippingPriceRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingPriceRepository">运费模板计价仓储类</param>
        public ShippingPriceService(IRepository<LF_ShippingPrice> shippingPriceRepository)
        {
            this._shippingPriceRepository = shippingPriceRepository;
        }

        #endregion

        /// <summary>
        /// 添加运费模板计价 
        /// </summary>
        /// <param name="shippingPrice">shippingPrice</param>
        /// <returns>运费模板计价</returns>
        public void AddShippingPrice(LF_ShippingPrice shippingPrice)
        {
            _shippingPriceRepository.Add(shippingPrice);
        }

		/// <summary>
        /// 批量添加运费模板计价 
        /// </summary>
        /// <param name="shippingPriceList">shippingPriceList</param>
        /// <returns>运费模板计价列表</returns>
        public void BatchAddShippingPrice(IEnumerable<LF_ShippingPrice> shippingPriceList)
        {
            _shippingPriceRepository.BatchAdd(shippingPriceList);
        }

		/// <summary>
        /// 更新运费模板计价 
        /// </summary>
        /// <param name="shippingPrice">shippingPrice</param>
        /// <returns>运费模板计价</returns>
        public void UpdateShippingPrice(LF_ShippingPrice shippingPrice)
        {
            _shippingPriceRepository.Update(shippingPrice);
        }

		/// <summary>
        /// 批量更新运费模板计价 
        /// </summary>
        /// <param name="shippingPriceList">shippingPriceList</param>
        /// <returns>运费模板计价列表</returns>
        public void BatchUpdateShippingPrice(IEnumerable<LF_ShippingPrice> shippingPriceList)
        {
            _shippingPriceRepository.BatchUpdate(shippingPriceList);
        }

        /// <summary>
        /// 批量更新运费模板计价列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShippingPrice, bool>> whereLambda, Expression<Func<LF_ShippingPrice, LF_ShippingPrice>> updateExpression)
        {
             return _shippingPriceRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除运费模板计价 
        /// </summary>
        /// <param name="shippingPrice">shippingPrice</param>
        /// <returns>运费模板计价</returns>
        public void DeleteShippingPrice(LF_ShippingPrice shippingPrice)
        {
            _shippingPriceRepository.Delete(shippingPrice);  
        }

        /// <summary>
        /// 根据查询条件删除运费模板计价 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板计价</returns>
        public void DeleteShippingPrice(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            _shippingPriceRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除运费模板计价 
        /// </summary>
        /// <param name="shippingPriceList">shippingPriceList</param>
        /// <returns>运费模板计价列表</returns>
        public void BatchDeleteShippingPrice(IEnumerable<LF_ShippingPrice> shippingPriceList)
        {
            _shippingPriceRepository.BatchDelete(shippingPriceList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            return _shippingPriceRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取运费模板计价 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>运费模板计价</returns>
        public LF_ShippingPrice GetShippingPriceById(int id)
        {
            return _shippingPriceRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条运费模板计价
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板计价</returns>
        public LF_ShippingPrice Get(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            return _shippingPriceRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有运费模板计价
        /// </summary>
        /// <returns>运费模板计价列表</returns>
        public IEnumerable<LF_ShippingPrice> GetAll()
        {
            return _shippingPriceRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取运费模板计价列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板计价列表</returns>
        public IEnumerable<LF_ShippingPrice> GetList(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            return _shippingPriceRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShippingPrice LoadEntitieNoTracking(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            return _shippingPriceRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShippingPrice> LoadEntitiesNoTracking(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            return _shippingPriceRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>运费模板计价列表</returns>
        public IList<LF_ShippingPrice> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShippingPrice, bool>> whereLambda,
            Expression<Func<LF_ShippingPrice, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shippingPriceRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShippingPrice, bool>> whereLambda)
        {
            return _shippingPriceRepository.Exist(whereLambda);
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
        /// <returns>运费模板计价列表</returns>
        public IList<LF_ShippingPrice> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingPrice, bool>> whereLambda, Expression<Func<LF_ShippingPrice, TS>> orderbyLambda, bool isAsc)
        {
            return _shippingPriceRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
