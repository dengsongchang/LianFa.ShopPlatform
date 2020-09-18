
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Service
{
	 public partial interface IUsersService
     {
        /// <summary>
        /// 添加用户 
        /// </summary>
        /// <param name="users">users</param>
        /// <returns>用户</returns>
        void AddUsers(LF_Users users);

		/// <summary>
        /// 批量添加用户 
        /// </summary>
        /// <param name="usersList">usersList</param>
        /// <returns>用户列表</returns>
        void BatchAddUsers(IEnumerable<LF_Users> usersList);

		/// <summary>
        /// 更新用户 
        /// </summary>
        /// <param name="users">users</param>
        /// <returns>用户</returns>
        void UpdateUsers(LF_Users users);

		/// <summary>
        /// 批量更新用户 
        /// </summary>
        /// <param name="usersList">usersList</param>
        /// <returns>用户列表</returns>
        void BatchUpdateUsers(IEnumerable<LF_Users> usersList);

        /// <summary>
        /// 批量更新用户列表(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>返回影响条数</returns>
        int BatchUpdate(Expression<Func<LF_Users, bool>> whereLambda, Expression<Func<LF_Users, LF_Users>> updateExpression);

        /// <summary>
        /// 删除用户 
        /// </summary>
        /// <param name="users">users</param>
        /// <returns>用户</returns>
        void DeleteUsers(LF_Users users);

        /// <summary>
        /// 根据查询条件删除用户 
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>用户</returns>
        void DeleteUsers(Expression<Func<LF_Users, bool>> whereLambda);

		/// <summary>
        /// 批量删除用户 
        /// </summary>
        /// <param name="usersList">usersList</param>
        /// <returns>用户列表</returns>
        void BatchDeleteUsers(IEnumerable<LF_Users> usersList);

        /// <summary>
        /// 根据查询条件批量删除(推荐)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        int BatchDelete(Expression<Func<LF_Users, bool>> whereLambda);

        /// <summary>
        /// 根据Id获取用户 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>用户</returns>
        LF_Users GetUsersById(int id);

		/// <summary>
        /// 根据查询条件获取单条用户
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>用户</returns>
        LF_Users Get(Expression<Func<LF_Users, bool>> whereLambda);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns>用户列表</returns>
        IEnumerable<LF_Users> GetAll();

        /// <summary>
        /// 根据查询条件获取用户列表
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>用户列表</returns>
        IEnumerable<LF_Users> GetList(Expression<Func<LF_Users, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取单个实体(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LF_Users LoadEntitieNoTracking(Expression<Func<LF_Users, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取实体列表(不追踪)
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IList<LF_Users> LoadEntitiesNoTracking(Expression<Func<LF_Users, bool>> whereLambda);

        /// <summary>
        /// 根据查询条件获取前几条实体数据(不追踪)
        /// </summary>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="topList">前几条数据数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>用户列表</returns>
        IList<LF_Users> LoadTopEntitiesNoTracking<S>(Expression<Func<LF_Users, bool>> whereLambda,
            Expression<Func<LF_Users, S>> orderbyLambda, int topList, bool isAsc);

        /// <summary>
        /// 根据查询条件实体判断是否存在
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>true-存在,false-不存在</returns>
        bool Exist(Expression<Func<LF_Users, bool>> whereLambda);

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
        /// <returns>用户列表</returns>
        IList<LF_Users> LoadPageEntities<TS>(int pageSize, int pageIndex, out int total,
            Expression<Func<LF_Users, bool>> whereLambda, Expression<Func<LF_Users, TS>> orderbyLambda, bool isAsc);
     }
}

