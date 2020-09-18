
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IProductImagesService
     {
        /// <summary>
        /// 添加商品图片 
        /// </summary>
        /// <param name="productImages">productImages</param>
        /// <returns>商品图片</returns>
        void AddProductImages(LF_ProductImages productImages);

		/// <summary>
        /// 批量添加商品图片 
        /// </summary>
        /// <param name="productImagesList">productImagesList</param>
        /// <returns>商品图片列表</returns>
        void BatchAddProductImages(IEnumerable<LF_ProductImages> productImagesList);

		/// <summary>
        /// 更新商品图片 
        /// </summary>
        /// <param name="productImages">productImages</param>
        /// <returns>商品图片</returns>
        void UpdateProductImages(LF_ProductImages productImages);

		/// <summary>
        /// 批量更新商品图片 
        /// </summary>
        /// <param name="productImagesList">productImagesList</param>
        /// <returns>商品图片列表</returns>
        void BatchUpdateProductImages(IEnumerable<LF_ProductImages> productImagesList);

        /// <summary>
        /// 批量更新商品图片列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_ProductImages, bool>> whereLambda, Expression<Func<LF_ProductImages, LF_ProductImages>> updateExpression);

        /// <summary>
        /// 删除商品图片 
        /// </summary>
        /// <param name="productImages">productImages</param>
        /// <returns>商品图片</returns>
        void DeleteProductImages(LF_ProductImages productImages);

        /// <summary>
        /// 根据查询条件删除商品图片 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品图片</returns>
        void DeleteProductImages(Expression<Func<LF_ProductImages, bool>> whereLambda);

		/// <summary>
        /// 批量删除商品图片 
        /// </summary>
        /// <param name="productImagesList">productImagesList</param>
        /// <returns>商品图片列表</returns>
        void BatchDeleteProductImages(IEnumerable<LF_ProductImages> productImagesList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_ProductImages, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取商品图片 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>商品图片</returns>
        LF_ProductImages GetProductImagesById(int id);

		/// <summary>
        /// 根据查询条件获取单条商品图片
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品图片</returns>
        LF_ProductImages Get(Expression<Func<LF_ProductImages, bool>> whereLambda);

        /// <summary>
        /// 获取所有商品图片
        /// </summary>
        /// <returns>商品图片列表</returns>
        IEnumerable<LF_ProductImages> GetAll();

        /// <summary>
        /// 根据查询条件获取商品图片列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>商品图片列表</returns>
        IEnumerable<LF_ProductImages> GetList(Expression<Func<LF_ProductImages, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_ProductImages LoadEntitieNoTracking(Expression<Func<LF_ProductImages, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_ProductImages> LoadEntitiesNoTracking(Expression<Func<LF_ProductImages, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>商品图片列表</returns>
        IList<LF_ProductImages> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_ProductImages, bool>> whereLambda,
            Expression<Func<LF_ProductImages, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_ProductImages, bool>> whereLambda);

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
        /// <returns>商品图片列表</returns>
        IList<LF_ProductImages> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_ProductImages, bool>> whereLambda, Expression<Func<LF_ProductImages, TS>> orderbyLambda, bool isAsc);
     }
}
