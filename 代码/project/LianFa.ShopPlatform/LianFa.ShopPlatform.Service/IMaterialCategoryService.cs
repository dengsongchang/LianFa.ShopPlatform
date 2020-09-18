
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IMaterialCategoryService
     {
        /// <summary>
        /// 添加素材分类 
        /// </summary>
        /// <param name="materialCategory">materialCategory</param>
        /// <returns>素材分类</returns>
        void AddMaterialCategory(LF_MaterialCategory materialCategory);

		/// <summary>
        /// 批量添加素材分类 
        /// </summary>
        /// <param name="materialCategoryList">materialCategoryList</param>
        /// <returns>素材分类列表</returns>
        void BatchAddMaterialCategory(IEnumerable<LF_MaterialCategory> materialCategoryList);

		/// <summary>
        /// 更新素材分类 
        /// </summary>
        /// <param name="materialCategory">materialCategory</param>
        /// <returns>素材分类</returns>
        void UpdateMaterialCategory(LF_MaterialCategory materialCategory);

		/// <summary>
        /// 批量更新素材分类 
        /// </summary>
        /// <param name="materialCategoryList">materialCategoryList</param>
        /// <returns>素材分类列表</returns>
        void BatchUpdateMaterialCategory(IEnumerable<LF_MaterialCategory> materialCategoryList);

        /// <summary>
        /// 批量更新素材分类列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_MaterialCategory, bool>> whereLambda, Expression<Func<LF_MaterialCategory, LF_MaterialCategory>> updateExpression);

        /// <summary>
        /// 删除素材分类 
        /// </summary>
        /// <param name="materialCategory">materialCategory</param>
        /// <returns>素材分类</returns>
        void DeleteMaterialCategory(LF_MaterialCategory materialCategory);

        /// <summary>
        /// 根据查询条件删除素材分类 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材分类</returns>
        void DeleteMaterialCategory(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

		/// <summary>
        /// 批量删除素材分类 
        /// </summary>
        /// <param name="materialCategoryList">materialCategoryList</param>
        /// <returns>素材分类列表</returns>
        void BatchDeleteMaterialCategory(IEnumerable<LF_MaterialCategory> materialCategoryList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取素材分类 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>素材分类</returns>
        LF_MaterialCategory GetMaterialCategoryById(int id);

		/// <summary>
        /// 根据查询条件获取单条素材分类
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材分类</returns>
        LF_MaterialCategory Get(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

        /// <summary>
        /// 获取所有素材分类
        /// </summary>
        /// <returns>素材分类列表</returns>
        IEnumerable<LF_MaterialCategory> GetAll();

        /// <summary>
        /// 根据查询条件获取素材分类列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材分类列表</returns>
        IEnumerable<LF_MaterialCategory> GetList(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_MaterialCategory LoadEntitieNoTracking(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_MaterialCategory> LoadEntitiesNoTracking(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>素材分类列表</returns>
        IList<LF_MaterialCategory> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_MaterialCategory, bool>> whereLambda,
            Expression<Func<LF_MaterialCategory, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_MaterialCategory, bool>> whereLambda);

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
        /// <returns>素材分类列表</returns>
        IList<LF_MaterialCategory> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_MaterialCategory, bool>> whereLambda, Expression<Func<LF_MaterialCategory, TS>> orderbyLambda, bool isAsc);
     }
}
