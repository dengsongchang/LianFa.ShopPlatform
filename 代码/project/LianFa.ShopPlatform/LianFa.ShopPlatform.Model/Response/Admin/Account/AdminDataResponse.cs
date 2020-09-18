
namespace LianFa.ShopPlatform.Model.Response.Admin.Account
{
    /// <summary>
    /// 管理员信息响应类
    /// </summary>
    public class AdminInfoResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminGId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否验证邮箱
        /// </summary>
        public int VerifyEmail { get; set; }

        /// <summary>
        /// 是否验证手机
        /// </summary>
        public int VerifyMobile { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        public string Salt { get; set; }
    }
}
