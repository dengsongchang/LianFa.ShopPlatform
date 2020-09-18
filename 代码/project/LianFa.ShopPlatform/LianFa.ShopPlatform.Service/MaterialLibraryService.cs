
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
	 public partial class MaterialLibraryService : IMaterialLibraryService
     {

        #region Fields

        private readonly IRepository<LF_MaterialLibrary> _materialLibraryRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="materialLibraryRepository">素材库仓储类</param>
        public MaterialLibraryService(IRepository<LF_MaterialLibrary> materialLibraryRepository)
        {
            this._materialLibraryRepository = materialLibraryRepository;
        }

        #endregion

        /// <summary>
        /// 添加素材库 
        /// </summary>
        /// <param name="materialLibrary">materialLibrary</param>
        /// <returns>素材库</returns>
        public void AddMaterialLibrary(LF_MaterialLibrary materialLibrary)
        {
            _materialLibraryRepository.Add(materialLibrary);
        }

		/// <summary>
        /// 批量添加素材库 
        /// </summary>
        /// <param name="materialLibraryList">materialLibraryList</param>
        /// <returns>素材库列表</returns>
        public void BatchAddMaterialLibrary(IEnumerable<LF_MaterialLibrary> materialLibraryList)
        {
            _materialLibraryRepository.BatchAdd(materialLibraryList);
        }

		/// <summary>
        /// 更新素材库 
        /// </summary>
        /// <param name="materialLibrary">materialLibrary</param>
        /// <returns>素材库</returns>
        public void UpdateMaterialLibrary(LF_MaterialLibrary materialLibrary)
        {
            _materialLibraryRepository.Update(materialLibrary);
        }

		/// <summary>
        /// 批量更新素材库 
        /// </summary>
        /// <param name="materialLibraryList">materialLibraryList</param>
        /// <returns>素材库列表</returns>
        public void BatchUpdateMaterialLibrary(IEnumerable<LF_MaterialLibrary> materialLibraryList)
        {
            _materialLibraryRepository.BatchUpdate(materialLibraryList);
        }

        /// <summary>
        /// 批量更新素材库列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        public int BatchUpdate(Expression<Func<LF_MaterialLibrary, bool>> whereLambda, Expression<Func<LF_MaterialLibrary, LF_MaterialLibrary>> updateExpression)
        {
             return _materialLibraryRepository.BatchUpdate(whereLambda , updateExpression);
        }

        /// <summary>
        /// 删除素材库 
        /// </summary>
        /// <param name="materialLibrary">materialLibrary</param>
        /// <returns>素材库</returns>
        public void DeleteMaterialLibrary(LF_MaterialLibrary materialLibrary)
        {
            _materialLibraryRepository.Delete(materialLibrary);  
        }

        /// <summary>
        /// 根据查询条件删除素材库 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材库</returns>
        public void DeleteMaterialLibrary(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            _materialLibraryRepository.Delete(whereLambda);  
        }

		/// <summary>
        /// 批量删除素材库 
        /// </summary>
        /// <param name="materialLibraryList">materialLibraryList</param>
        /// <returns>素材库列表</returns>
        public void BatchDeleteMaterialLibrary(IEnumerable<LF_MaterialLibrary> materialLibraryList)
        {
            _materialLibraryRepository.BatchDelete(materialLibraryList);  
        }

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        public int BatchDelete(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            return _materialLibraryRepository.BatchDelete(whereLambda);  
        }

        /// <summary>
        /// 根据Id获取素材库 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>素材库</returns>
        public LF_MaterialLibrary GetMaterialLibraryById(int id)
        {
            return _materialLibraryRepository.GetById(id);  
        }

		/// <summary>
        /// 根据查询条件获取单条素材库
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材库</returns>
        public LF_MaterialLibrary Get(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            return _materialLibraryRepository.Get(whereLambda);  
        }

        /// <summary>
        /// 获取所有素材库
        /// </summary>
        /// <returns>素材库列表</returns>
        public IEnumerable<LF_MaterialLibrary> GetAll()
        {
            return _materialLibraryRepository.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取素材库列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>素材库列表</returns>
        public IEnumerable<LF_MaterialLibrary> GetList(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            return _materialLibraryRepository.GetList(whereLambda);
        }

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LF_MaterialLibrary LoadEntitieNoTracking(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            return _materialLibraryRepository.LoadEntitiesNoTracking(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IList<LF_MaterialLibrary> LoadEntitiesNoTracking(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            return _materialLibraryRepository.LoadEntitiesNoTracking(whereLambda).ToList();
        }

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="TS">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>素材库列表</returns>
        public IList<LF_MaterialLibrary> LoadTopEntitiesNoTracking<TS>(Expression<Func<LF_MaterialLibrary, bool>> whereLambda,
            Expression<Func<LF_MaterialLibrary, TS>> orderbyLambda, int topList, bool isAsc)
        {
            return _materialLibraryRepository.LoadTopEntitiesNoTracking(whereLambda, orderbyLambda, topList, isAsc).ToList();
        }

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        public bool Exist(Expression<Func<LF_MaterialLibrary, bool>> whereLambda)
        {
            return _materialLibraryRepository.Exist(whereLambda);
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
        /// <returns>素材库列表</returns>
        public IList<LF_MaterialLibrary> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_MaterialLibrary, bool>> whereLambda, Expression<Func<LF_MaterialLibrary, TS>> orderbyLambda, bool isAsc)
        {
            return _materialLibraryRepository.LoadPageEntities(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
     }
}
