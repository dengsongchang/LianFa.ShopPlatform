namespace LianFa.ShopPlatform.Code.Cache
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public class CacheKeys
    {
        /// <summary>
        /// 微信OpenId缓存键
        /// </summary>
        public const string WECHAT_OPENID = "/WeChat/OpenId/";

        /// <summary>
        /// 分类列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_LIST = "/Shop/CategoryList";

        /// <summary>
        /// 区域HashSet缓存键
        /// </summary>
        public const string REGION_LIST = "REGION_LIST";

        /// <summary>
        /// 区域HashSet缓存键
        /// </summary>
        public const string NEWREGION_LIST = "NEWREGION_LIST";


        /// <summary>
        /// 后台操作HashSet缓存键
        /// </summary>
        public const string ADMIN_ADMINACTION_LIST = "/Admin/AdminActionList";

        /// <summary>
        /// 后台管理员组操作HashSet缓存键
        /// </summary>
        public const string ADMIN_ADMINGROUPACTION_LIST = "/Admin/AdminGroupActionList/";

        /// <summary>
        /// 后台菜单列表缓存键
        /// </summary>
        public const string ADMIN_ADMINMENU_LIST = "/Admin/AdminActionMenuList";

        /// <summary>
        /// 缓存时间为一年
        /// </summary>
        public const int YearCacheTime = 365 * 24 * 3600;

        /// <summary>
        /// 短信验证码有效时间(分钟)
        /// </summary>
        public const int SmsCodeValidTime = 10;
    }
}
