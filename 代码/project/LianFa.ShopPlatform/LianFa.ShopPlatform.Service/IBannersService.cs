
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IBannersService
     {
        /// <summary>
        /// 添加轮播图 
        /// </summary>
        /// <param name="banners">banners</param>
        /// <returns>轮播图</returns>
        void AddBanners(LF_Banners banners);

		/// <summary>
        /// 批量添加轮播图 
        /// </summary>
        /// <param name="bannersList">bannersList</param>
        /// <returns>轮播图列表</returns>
        void BatchAddBanners(IEnumerable<LF_Banners> bannersList);

		/// <summary>
        /// 更新轮播图 
        /// </summary>
        /// <param name="banners">banners</param>
        /// <returns>轮播图</returns>
        void UpdateBanners(LF_Banners banners);

		/// <summary>
        /// 批量更新轮播图 
        /// </summary>
        /// <param name="bannersList">bannersList</param>
        /// <returns>轮播图列表</returns>
        void BatchUpdateBanners(IEnumerable<LF_Banners> bannersList);

        /// <summary>
        /// 批量更新轮播图列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Banners, bool>> whereLambda, Expression<Func<LF_Banners, LF_Banners>> updateExpression);

        /// <summary>
        /// 删除轮播图 
        /// </summary>
        /// <param name="banners">banners</param>
        /// <returns>轮播图</returns>
        void DeleteBanners(LF_Banners banners);

        /// <summary>
        /// 根据查询条件删除轮播图 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>轮播图</returns>
        void DeleteBanners(Expression<Func<LF_Banners, bool>> whereLambda);

		/// <summary>
        /// 批量删除轮播图 
        /// </summary>
        /// <param name="bannersList">bannersList</param>
        /// <returns>轮播图列表</returns>
        void BatchDeleteBanners(IEnumerable<LF_Banners> bannersList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Banners, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取轮播图 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>轮播图</returns>
        LF_Banners GetBannersById(int id);

		/// <summary>
        /// 根据查询条件获取单条轮播图
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>轮播图</returns>
        LF_Banners Get(Expression<Func<LF_Banners, bool>> whereLambda);

        /// <summary>
        /// 获取所有轮播图
        /// </summary>
        /// <returns>轮播图列表</returns>
        IEnumerable<LF_Banners> GetAll();

        /// <summary>
        /// 根据查询条件获取轮播图列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>轮播图列表</returns>
        IEnumerable<LF_Banners> GetList(Expression<Func<LF_Banners, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Banners LoadEntitieNoTracking(Expression<Func<LF_Banners, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Banners> LoadEntitiesNoTracking(Expression<Func<LF_Banners, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>轮播图列表</returns>
        IList<LF_Banners> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Banners, bool>> whereLambda,
            Expression<Func<LF_Banners, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Banners, bool>> whereLambda);

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
        /// <returns>轮播图列表</returns>
        IList<LF_Banners> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Banners, bool>> whereLambda, Expression<Func<LF_Banners, TS>> orderbyLambda, bool isAsc);
     }
}
