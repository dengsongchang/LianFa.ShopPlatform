
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IShippingTemplatesService
     {
        /// <summary>
        /// 添加运费模板 
        /// </summary>
        /// <param name="shippingTemplates">shippingTemplates</param>
        /// <returns>运费模板</returns>
        void AddShippingTemplates(LF_ShippingTemplates shippingTemplates);

		/// <summary>
        /// 批量添加运费模板 
        /// </summary>
        /// <param name="shippingTemplatesList">shippingTemplatesList</param>
        /// <returns>运费模板列表</returns>
        void BatchAddShippingTemplates(IEnumerable<LF_ShippingTemplates> shippingTemplatesList);

		/// <summary>
        /// 更新运费模板 
        /// </summary>
        /// <param name="shippingTemplates">shippingTemplates</param>
        /// <returns>运费模板</returns>
        void UpdateShippingTemplates(LF_ShippingTemplates shippingTemplates);

		/// <summary>
        /// 批量更新运费模板 
        /// </summary>
        /// <param name="shippingTemplatesList">shippingTemplatesList</param>
        /// <returns>运费模板列表</returns>
        void BatchUpdateShippingTemplates(IEnumerable<LF_ShippingTemplates> shippingTemplatesList);

        /// <summary>
        /// 批量更新运费模板列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ShippingTemplates, bool>> whereLambda, Expression<Func<LF_ShippingTemplates, LF_ShippingTemplates>> updateExpression);

        /// <summary>
        /// 删除运费模板 
        /// </summary>
        /// <param name="shippingTemplates">shippingTemplates</param>
        /// <returns>运费模板</returns>
        void DeleteShippingTemplates(LF_ShippingTemplates shippingTemplates);

        /// <summary>
        /// 根据查询条件删除运费模板 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板</returns>
        void DeleteShippingTemplates(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

		/// <summary>
        /// 批量删除运费模板 
        /// </summary>
        /// <param name="shippingTemplatesList">shippingTemplatesList</param>
        /// <returns>运费模板列表</returns>
        void BatchDeleteShippingTemplates(IEnumerable<LF_ShippingTemplates> shippingTemplatesList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取运费模板 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>运费模板</returns>
        LF_ShippingTemplates GetShippingTemplatesById(int id);

		/// <summary>
        /// 根据查询条件获取单条运费模板
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板</returns>
        LF_ShippingTemplates Get(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

        /// <summary>
        /// 获取所有运费模板
        /// </summary>
        /// <returns>运费模板列表</returns>
        IEnumerable<LF_ShippingTemplates> GetAll();

        /// <summary>
        /// 根据查询条件获取运费模板列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>运费模板列表</returns>
        IEnumerable<LF_ShippingTemplates> GetList(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ShippingTemplates LoadEntitieNoTracking(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ShippingTemplates> LoadEntitiesNoTracking(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>运费模板列表</returns>
        IList<LF_ShippingTemplates> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ShippingTemplates, bool>> whereLambda,
            Expression<Func<LF_ShippingTemplates, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ShippingTemplates, bool>> whereLambda);

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
        /// <returns>运费模板列表</returns>
        IList<LF_ShippingTemplates> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ShippingTemplates, bool>> whereLambda, Expression<Func<LF_ShippingTemplates, TS>> orderbyLambda, bool isAsc);
     }
}
