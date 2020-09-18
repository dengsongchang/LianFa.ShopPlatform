
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
	 public partial class MaterialCategoryService : IMaterialCategoryService
     {

        #region Fields

        private readonly IRepository<LF_MaterialCategory> _materialCategoryRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="materialCategoryRepository">素材分类仓储类</param>
        public MaterialCategoryService(IRepository<LF_MaterialCategory> materialCategoryRepository)
        {
            this._materialCategoryRepository = materialCategoryRepository;
        }

        #endregion

        /// <summary>
        /// 添加素材分类 
        /// </summary>
        /// <param name="materialCategory">materialCategory</param>
        /// <returns>素材分类</returns>
        public void AddMaterialCategory(LF_MaterialCategory materialCategory)
        {
            _materialCategoryRepository.Add(materialCategory);
        }

		/// <summary>
        /// 批量添加素材分类 
        /// </summary>
        /// <param name="materialCategoryList">materialCategoryList</param>
        /// <returns>素材分类列表</returns>
        public void BatchAddMaterialCategory(IEnumerable<LF_MaterialCategory> materialCategoryList)
        {
            _materialCategoryRepository.BatchAdd(materialCategoryList);
        }

		/// <summary>
        /// 更新素材分类 
        /// </summary>
        /// <param name="materialCategory">materialCategory</param>
        /// <returns>素材分类</returns>
        public void UpdateMaterialCategory(LF_MaterialCategory materialCategory)
        {
            _materialCategoryRepository.Update(materialCategory);
        }

		/// <summary>
        /// 批量更新素材分类 
        /// </summary>
        /// <param name="materialCategoryList">materialCategoryList</param>
        /// <returns>素材分类列表</returns>
        public void BatchUpdateMaterialCategory(IEnumerable<LF_MaterialCategory> materialCategoryList)
        {
            _materialCategoryRepository.BatchUpdate(materialCategoryList);
        }

        /// <summary>
        /// 批量更新素材分类列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_MaterialCategory, bool>> whereLambda, Expression<Func<LF_MaterialCategory, LF_MaterialCategory>> updateExpression)
        {
             return _materialCategoryRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除素材分类 
        /// </summary>
        /// <param name="materialCategory">materialCategory</param>
        /// <returns>素材分类</returns>
        public void DeleteMaterialCategory(LF_MaterialCategory materialCategory)
        {
            _materialCategoryRepository.Delete(materialCategory);  
        }

        /// <summary>
        /// 根据查询条件删除素材分类 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材分类</returns>
        public void DeleteMaterialCategory(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            _materialCategoryRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除素材分类 
        /// </summary>
        /// <param name="materialCategoryList">materialCategoryList</param>
        /// <returns>素材分类列表</returns>
        public void BatchDeleteMaterialCategory(IEnumerable<LF_MaterialCategory> materialCategoryList)
        {
            _materialCategoryRepository.BatchDelete(materialCategoryList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            return _materialCategoryRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取素材分类 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>素材分类</returns>
        public LF_MaterialCategory GetMaterialCategoryById(int id)
        {
            return _materialCategoryRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条素材分类
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材分类</returns>
        public LF_MaterialCategory Get(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            return _materialCategoryRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有素材分类
        /// </summary>
        /// <returns>素材分类列表</returns>
        public IEnumerable<LF_MaterialCategory> GetAll()
        {
            return _materialCategoryRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取素材分类列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材分类列表</returns>
        public IEnumerable<LF_MaterialCategory> GetList(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            return _materialCategoryRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_MaterialCategory LoadEntitieNoTracking(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            return _materialCategoryRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_MaterialCategory> LoadEntitiesNoTracking(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            return _materialCategoryRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>素材分类列表</returns>
        public IList<LF_MaterialCategory> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_MaterialCategory, bool>> whereLambda,
            Expression<Func<LF_MaterialCategory, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _materialCategoryRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_MaterialCategory, bool>> whereLambda)
        {
            return _materialCategoryRepository.Exist(whereLambda);
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
        /// <returns>素材分类列表</returns>
        public IList<LF_MaterialCategory> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_MaterialCategory, bool>> whereLambda, Expression<Func<LF_MaterialCategory, TS>> orderbyLambda, bool isAsc)
        {
            return _materialCategoryRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
