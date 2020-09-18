namespace LianFa.ShopPlatform.Model.Response.Admin.User
{
    /// <summary>
    /// 后台获取会员分层响应类
    /// </summary>
    public class AdminGetUserLayerResponse
    {
        /// <summary>
        /// 新注册会员
        /// </summary>
        public NewRegisterUser NewRegisterUserInfo { get; set; }

        /// <summary>
        /// 消费会员
        /// </summary>
        public ConsumeUser ConsumeUserInfo { get; set; }

        /// <summary>
        /// 活跃会员
        /// </summary>
        public ActiveUser ActiveUserInfo { get; set; }

        /// <summary>
        /// 休眠会员
        /// </summary>
        public DormantUser DormantUserInfo { get; set; }

    }

    /// <summary>
    /// 新注册会员
    /// </summary>
    public class NewRegisterUser
    {
        /// <summary>
        /// 今日新注册会员
        /// </summary>
        public int DayUserNum { get; set; }

        /// <summary>
        /// 本周新注册会员
        /// </summary>
        public int WeekUserNum { get; set; }

        /// <summary>
        /// 本月新注册会员
        /// </summary>
        public int MonthUserNum { get; set; }
    }

    /// <summary>
    /// 消费会员
    /// </summary>
    public class ConsumeUser
    {
        /// <summary>
        /// 有消费会员数量
        /// </summary>
        public int ConsumeUserNum { get; set; }

        /// <summary>
        /// 未消费会员数量
        /// </summary>
        public int NoConsumeUserNum { get; set; }

    }

    /// <summary>
    /// 活跃会员
    /// </summary>
    public class ActiveUser
    {
        /// <summary>
        /// 一个月活跃会员数量
        /// </summary>
        public int OneMonthActiveUserNum { get; set; }

        /// <summary>
        /// 三个月活跃会员数量
        /// </summary>
        public int ThreeMonthActiveUserNum { get; set; }

        /// <summary>
        /// 六个月活跃会员数量
        /// </summary>
        public int SixMonthActiveUserNum { get; set; }

    }

    /// <summary>
    /// 休眠会员
    /// </summary>
    public class DormantUser
    {
        /// <summary>
        /// 一个月休眠会员数量
        /// </summary>
        public int OneMonthDormantUserNum { get; set; }

        /// <summary>
        /// 三个月休眠会员数量
        /// </summary>
        public int ThreeMonthDormantUserNum { get; set; }

        /// <summary>
        /// 六个月休眠会员数量
        /// </summary>
        public int SixMonthDormantUserNum { get; set; }

        /// <summary>
        /// 九个月休眠会员数量
        /// </summary>
        public int NineMonthDormantUserNum { get; set; }

        /// <summary>
        /// 十二个月休眠会员数量
        /// </summary>
        public int TwelveMonthDormantUserNum { get; set; }
    }
}
