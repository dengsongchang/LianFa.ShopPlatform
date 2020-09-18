
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
	 public partial class ShippingTemplatesService : IShippingTemplatesService
     {

        #region Fields

        private readonly IRepository<LF_ShippingTemplates> _shippingTemplatesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingTemplatesRepository">运费模板仓储类</param>
        public ShippingTemplatesService(IRepository<LF_ShippingTemplates> shippingTemplatesRepository)
        {
            this._shippingTemplatesRepository = shippingTemplatesRepository;
        }

        #endregion

        /// <summary>
        /// 添加运费模板 
        /// </summary>
        /// <param name="shippingTemplates">shippingTemplates</param>
        /// <returns>运费模板</returns>
        public void AddShippingTemplates(LF_ShippingTemplates shippingTemplates)
        {
            _shippingTemplatesRepository.Add(shippingTemplates);
        }

		/// <summary>
        /// 批量添加运费模板 
        /// </summary>
        /// <param name="shippingTemplatesList">shippingTemplatesList</param>
        /// <returns>运费模板列表</returns>
        public void BatchAddShippingTemplates(IEnumerable<LF_ShippingTemplates> shippingTemplatesList)
        {
            _shippingTemplatesRepository.BatchAdd(shippingTemplatesList);
        }

		/// <summary>
        /// 更新运费模板 
        /// </summary>
        /// <param name="shippingTemplates">shippingTemplates</param>
        /// <returns>运费模板</returns>
        public void UpdateShippingTemplates(LF_ShippingTemplates shippingTemplates)
        {
            _shippingTemplatesRepository.Update(shippingTemplates);
        }

		/// <summary>
        /// 批量更新运费模板 
        /// </summary>
        /// <param name="shippingTemplatesList">shippingTemplatesList</param>
        /// <returns>运费模板列表</returns>
        public void BatchUpdateShippingTemplates(IEnumerable<LF_ShippingTemplates> shippingTemplatesList)
        {
            _shippingTemplatesRepository.BatchUpdate(shippingTemplatesList);
        }

        /// <summary>
        /// 批量更新运费模板列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_ShippingTemplates, bool>> whereLambda, Expression<Func<LF_ShippingTemplates, LF_ShippingTemplates>> updateExpression)
        {
             return _shippingTemplatesRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除运费模板 
        /// </summary>
        /// <param name="shippingTemplates">shippingTemplates</param>
        /// <returns>运费模板</returns>
        public void DeleteShippingTemplates(LF_ShippingTemplates shippingTemplates)
        {
            _shippingTemplatesRepository.Delete(shippingTemplates);  
        }

        /// <summary>
        /// 根据查询条件删除运费模板 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板</returns>
        public void DeleteShippingTemplates(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            _shippingTemplatesRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除运费模板 
        /// </summary>
        /// <param name="shippingTemplatesList">shippingTemplatesList</param>
        /// <returns>运费模板列表</returns>
        public void BatchDeleteShippingTemplates(IEnumerable<LF_ShippingTemplates> shippingTemplatesList)
        {
            _shippingTemplatesRepository.BatchDelete(shippingTemplatesList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            return _shippingTemplatesRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取运费模板 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>运费模板</returns>
        public LF_ShippingTemplates GetShippingTemplatesById(int id)
        {
            return _shippingTemplatesRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条运费模板
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板</returns>
        public LF_ShippingTemplates Get(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            return _shippingTemplatesRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有运费模板
        /// </summary>
        /// <returns>运费模板列表</returns>
        public IEnumerable<LF_ShippingTemplates> GetAll()
        {
            return _shippingTemplatesRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取运费模板列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板列表</returns>
        public IEnumerable<LF_ShippingTemplates> GetList(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            return _shippingTemplatesRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_ShippingTemplates LoadEntitieNoTracking(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            return _shippingTemplatesRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_ShippingTemplates> LoadEntitiesNoTracking(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            return _shippingTemplatesRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>运费模板列表</returns>
        public IList<LF_ShippingTemplates> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_ShippingTemplates, bool>> whereLambda,
            Expression<Func<LF_ShippingTemplates, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _shippingTemplatesRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_ShippingTemplates, bool>> whereLambda)
        {
            return _shippingTemplatesRepository.Exist(whereLambda);
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
        /// <returns>运费模板列表</returns>
        public IList<LF_ShippingTemplates> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingTemplates, bool>> whereLambda, Expression<Func<LF_ShippingTemplates, TS>> orderbyLambda, bool isAsc)
        {
            return _shippingTemplatesRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
