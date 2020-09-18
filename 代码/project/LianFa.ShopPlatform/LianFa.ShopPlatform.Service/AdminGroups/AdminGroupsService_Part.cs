using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Cache;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Admin.DepartMent;

namespace LianFa.ShopPlatform.Service
{
    public partial class AdminGroupsService
    {
        /// <summary>
        /// 商城管理组动作仓储类
        /// </summary>
        private readonly IRepository<LF_AdminActions> _adminActionsRepository;

        //一、二级菜单类型列表
        private readonly List<int> _menuTypeList = new List<int>
        {
            (int) ActionTypeEnum.FirstLevelMenu,
            (int) ActionTypeEnum.SecondLevelMenu
        };

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="adminGroupsRepository">部门仓储类</param>
        /// <param name="adminActionsRepository">后台管理动作仓储类</param>
        public AdminGroupsService(IRepository<LF_AdminGroups> adminGroupsRepository, IRepository<LF_AdminActions> adminActionsRepository)
        {
            this._adminGroupsRepository = adminGroupsRepository;
            this._adminActionsRepository = adminActionsRepository;
        }

        /// <summary>
        /// 检查当前动作的授权
        /// </summary>
        /// <param name="adminGId">商城管理员组id</param>
        /// <param name="actionRoute">动作方法路由</param>
        /// <returns></returns>
        public bool CheckAuthority(int adminGId, string actionRoute)
        {
            //非管理员
            if (adminGId == (int)UserType.User)
                return false;

            //系统管理员具有一切权限
            if (adminGId == (int)UserType.SystemManager)
                return true;

            //查询后台所有权限动作
            var allAdminActions = GetAllAdminActionList();
            var adminGroupActions = GetAdminGroupActionList(adminGId);

            //找到当前路由对应的记录
            var currentAction = allAdminActions.FirstOrDefault(m => m.Action.ToLower().Contains(actionRoute.ToLower()));

            //找不到记录返回未授权
            if (currentAction == null)
            {
                return false;
            }

            //匹配当前管理员是否具备该权限，不存在权限则找其上级,存在权限返回授权
            if (adminGroupActions.Exists(m => m.Action == currentAction.Action)) return true;

            //找到路由对应的上级记录
            var parentAction = allAdminActions.FirstOrDefault(m => m.AdminAId == currentAction.ParentId);

            //如果上级权限拥有子级所有权限，返回授权结果
            return parentAction != null && parentAction.IsAllChildAction == (int)WhetherType.Yes;
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <returns></returns>
        public List<LF_AdminGroups> GetAllDepartmentList()
        {
            var data = _adminGroupsRepository.GetDbSetNoTracking().Where(m => m.AdminGId != (int)UserType.User && m.AdminGId != (int)UserType.SystemManager).OrderByDescending(d => d.AdminGId).ToList();
            return data;
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="page">分页模型</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public List<LF_AdminGroups> GetDepartmentList(PageModel page, out int total)
        {
            var data = _adminGroupsRepository.GetDbSetNoTracking().Where(m => m.AdminGId != (int)UserType.User && m.AdminGId != (int)UserType.SystemManager).OrderByDescending(d => d.AdminGId)
                .LoadPage(page, out total).ToList();
            return data;
        }

        /// <summary>
        /// 获取所有后台管理菜单动作权限列表
        /// </summary>
        /// <returns></returns>
        public List<Permissions> GetAllAdminActionMenuList()
        {
            //所有后台管理菜单权限列表
            var actionMenuList = CacheHelper.GetCache(CacheKeys.ADMIN_ADMINMENU_LIST) as List<Permissions>;
            if (actionMenuList != null) return actionMenuList;

            //查询管理员组对应的菜单动作列表
            var adminActions = GetAllAdminActionList().Where(m => _menuTypeList.Contains(m.ActionType)).ToList();

            //动作权限列表
            actionMenuList = new List<Permissions>();

            //获取一级权限动作
            var firstActions = adminActions.Where(m => m.ParentId == 0);

            //获取管理员动作权限列表
            foreach (var firstAction in firstActions)
            {
                //转换为Dto
                var firstActionDto = new Permissions
                {
                    AdminAId = firstAction.AdminAId,
                    Action = firstAction.Action,
                    DisplayOrder = firstAction.DisplayOrder,
                    IconClass = firstAction.IconClass,
                    Title = firstAction.Title
                };

                //获取子级权限菜单
                var childList = adminActions.Where(m => m.ParentId == firstAction.AdminAId).Select(d => new Permissions
                {
                    AdminAId = d.AdminAId,
                    Action = d.Action,
                    DisplayOrder = d.DisplayOrder,
                    IconClass = d.IconClass,
                    Title = d.Title
                }).ToList();

                firstActionDto.ChildList = childList;
                actionMenuList.Add(firstActionDto);
            }

            //设置缓存
            CacheHelper.SetCache(CacheKeys.ADMIN_ADMINMENU_LIST, actionMenuList);
            return actionMenuList;
        }

        /// <summary>
        /// 查询管理员动作权限列表
        /// </summary>
        /// <param name="adminGId">管理员id</param>
        /// <returns></returns>
        public List<Permissions> GetAdminActionPermissionsList(int adminGId)
        {
            //所有后台管理菜单权限列表
            var actionMenuList = CacheHelper.GetCache(CacheKeys.ADMIN_ADMINMENU_LIST + adminGId) as List<Permissions>;
            if (actionMenuList != null) return actionMenuList;

            //动作权限列表
            actionMenuList = new List<Permissions>();

            //查询管理员组对应的权限动作列表
            var adminGroup = _adminGroupsRepository.LoadEntitiesNoTracking(m => m.AdminGId == adminGId).FirstOrDefault();
            if (adminGroup == null)
            {
                return actionMenuList;
            }

            //权限动作Id列表
            var actionIds = adminGroup.ActionIds.Split(',');
            var adminActions = GetAllAdminActionList().Where(m => actionIds.Contains(m.AdminAId.ToString()) && _menuTypeList.Contains(m.ActionType)).ToList();

            //获取一级权限动作
            var firstActions = adminActions.Where(m => m.ParentId == 0);

            //获取管理员动作权限列表
            foreach (var firstAction in firstActions)
            {
                //转换为Dto
                var firstActionDto = new Permissions
                {
                    AdminAId = firstAction.AdminAId,
                    Action = firstAction.Action,
                    DisplayOrder = firstAction.DisplayOrder,
                    IconClass = firstAction.IconClass,
                    Title = firstAction.Title
                };

                //获取子级权限菜单
                var childList = adminActions.Where(m => m.ParentId == firstAction.AdminAId).Select(d => new Permissions
                {
                    AdminAId = d.AdminAId,
                    Action = d.Action,
                    DisplayOrder = d.DisplayOrder,
                    IconClass = d.IconClass,
                    Title = d.Title
                }).ToList();

                firstActionDto.ChildList = childList;
                actionMenuList.Add(firstActionDto);
            }

            //设置缓存
            CacheHelper.SetCache(CacheKeys.ADMIN_ADMINMENU_LIST + adminGId, actionMenuList);
            return actionMenuList;
        }

        /// <summary>
        /// 查询管理员菜单动作权限
        /// </summary>
        /// <param name="adminGId">管理员id</param>
        /// <returns></returns>
        public List<Permissions> GetAdminActionMenuPermissions(int adminGId)
        {
            //获取所有动作权限列表
            var permissions = GetAllAdminActionMenuList();

            //如果为系统管理员则获取所有权限列表,否则取对应权限
            var adminPermissions = adminGId == (int)UserType.SystemManager ? GetAllAdminActionMenuList() : GetAdminActionPermissionsList(adminGId);

            //获取管理员动作权限列表选中
            foreach (var permission in permissions)
            {
                //判断是否具有一级权限
                var firstPermissions = adminPermissions.FirstOrDefault(m => m.AdminAId == permission.AdminAId);
                if (firstPermissions == null)
                {
                    permission.Selected = false;

                    //没有一级，则二级都不选中
                    foreach (var child in permission.ChildList)
                    {
                        child.Selected = false;
                    }

                    continue;
                }

                permission.Selected = firstPermissions.AdminAId == permission.AdminAId;

                //判断是否具有二级权限
                foreach (var child in permission.ChildList)
                {
                    child.Selected = firstPermissions.ChildList.Exists(m => m.AdminAId == child.AdminAId);
                }
            }

            return permissions;
        }

        /// <summary>
        /// 查询管理员动作权限
        /// </summary>
        /// <param name="adminGId">管理员id</param>
        /// <returns></returns>
        public void ClearPermissionsCache(int adminGId = (int)UserType.SystemManager)
        {
            //如果为系统管理员时
            if (adminGId == (int)UserType.SystemManager)
            {
                //清除权限相关缓存
                CacheHelper.ClearCahe(CacheKeys.ADMIN_ADMINGROUPACTION_LIST);
                CacheHelper.ClearCahe(CacheKeys.ADMIN_ADMINMENU_LIST);
            }
            else
            {
                //清除指定管理员权限相关缓存
                CacheHelper.ClearCahe(CacheKeys.ADMIN_ADMINGROUPACTION_LIST + adminGId);
                CacheHelper.ClearCahe(CacheKeys.ADMIN_ADMINMENU_LIST + adminGId);
            }
        }

        /// <summary>
        /// 获取所有后台管理动作权限列表
        /// </summary>
        /// <returns></returns>
        private List<LF_AdminActions> GetAllAdminActionList()
        {
            //所有后台管理菜单权限列表
            var adminActions = CacheHelper.GetCache(CacheKeys.ADMIN_ADMINACTION_LIST) as List<LF_AdminActions>;
            if (adminActions != null) return adminActions;

            //查询管理员组对应的动作列表
            adminActions = _adminActionsRepository.GetDbSetNoTracking().OrderByDescending(m => m.DisplayOrder).ThenBy(m => m.AdminAId).ToList();

            //设置缓存
            CacheHelper.SetCache(CacheKeys.ADMIN_ADMINACTION_LIST, adminActions);
            return adminActions;
        }

        /// <summary>
        /// 获得后台管理员动作权限列表
        /// </summary>
        /// <param name="adminGId">商城管理员组id</param>
        /// <returns></returns>
        private List<LF_AdminActions> GetAdminGroupActionList(int adminGId)
        {
            var adminActions = CacheHelper.GetCache(CacheKeys.ADMIN_ADMINGROUPACTION_LIST + adminGId) as List<LF_AdminActions>;
            if (adminActions != null) return adminActions;

            //获取管理员组操作
            var mallAdminGroupInfo = _adminGroupsRepository.LoadEntitiesNoTracking(m => m.AdminGId == adminGId).FirstOrDefault();
            if (mallAdminGroupInfo == null)
            {
                return null;
            }

            //查询管理员组对应的动作Id列表
            var actionIds = mallAdminGroupInfo.ActionIds.Split(',').ToList();

            //查询管理员组对应的动作列表
            adminActions = GetAllAdminActionList().Where(m => actionIds.Contains(m.AdminAId.ToString())).ToList();

            //设置缓存
            CacheHelper.SetCache(CacheKeys.ADMIN_ADMINGROUPACTION_LIST + adminGId, adminActions);
            return adminActions;
        }
    }
}
