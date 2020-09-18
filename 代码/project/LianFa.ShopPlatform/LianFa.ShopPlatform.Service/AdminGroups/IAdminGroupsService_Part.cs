using System.Collections.Generic;
using HuCheng.Util.Core.Datas.Repositories;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.DepartMent;

namespace LianFa.ShopPlatform.Service
{
    public partial interface IAdminGroupsService
    {
        /// <summary>
        /// 查询所有部门列表
        /// </summary>
        /// <returns></returns>
        List<LF_AdminGroups> GetAllDepartmentList();

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="page">分页模型</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        List<LF_AdminGroups> GetDepartmentList(PageModel page, out int total);

        /// <summary>
        /// 检查当前动作的授权
        /// </summary>
        /// <param name="adminGId">商城管理员组id</param>
        /// <param name="actionRoute">动作方法路由</param>
        /// <returns></returns>
        bool CheckAuthority(int adminGId, string actionRoute);

        /// <summary>
        /// 获取所有后台管理动作权限列表
        /// </summary>
        /// <returns></returns>
        List<Permissions> GetAllAdminActionMenuList();

        /// <summary>
        /// 查询管理员菜单动作权限
        /// </summary>
        /// <param name="adminGId">管理员id</param>
        /// <returns></returns>
        List<Permissions> GetAdminActionMenuPermissions(int adminGId);

        /// <summary>
        /// 查询管理员动作权限列表
        /// </summary>
        /// <param name="adminGId">管理员id</param>
        /// <returns></returns>
        List<Permissions> GetAdminActionPermissionsList(int adminGId);

        /// <summary>
        /// 查询管理员动作权限
        /// </summary>
        /// <param name="adminGId">管理员id</param>
        /// <returns></returns>
        void ClearPermissionsCache(int adminGId = (int)UserType.SystemManager);
    }
}
