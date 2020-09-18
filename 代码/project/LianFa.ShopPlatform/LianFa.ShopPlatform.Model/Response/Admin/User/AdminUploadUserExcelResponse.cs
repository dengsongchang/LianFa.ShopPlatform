namespace LianFa.ShopPlatform.Model.Response.Admin.User
{
    /// <summary>
    /// 后台上传会员Excel文件响应类
    /// </summary>
    public class AdminUploadUserExcelResponse
    {
        /// <summary>
        /// Excel文件路径
        /// </summary>
        public string ExcelUrl { get; set; }
    }

    /// <summary>
    /// 后台导入会员格式信息
    /// </summary>
    public class AdminUploadUserInfo
    {
        /// <summary>  
        /// 会员帐号  
        /// </summary>
        public string 会员帐号 { get; set; }
        /// <summary>  
        /// 会员密码  
        /// </summary>
        public string 会员密码 { get; set; }
        /// <summary>  
        /// 真实姓名  
        /// </summary>
        public string 真实姓名 { get; set; }
        /// <summary>  
        /// 会员性别  
        /// </summary>
        public string 会员性别 { get; set; }
        /// <summary>  
        /// 会员昵称 
        /// </summary>
        public string 会员昵称 { get; set; }
        /// <summary>  
        /// 会员生日
        /// </summary>
        public string 会员生日 { get; set; }
        /// <summary>  
        /// 手机号码  
        /// </summary>
        public string 手机号码 { get; set; }
        /// <summary>  
        /// 会员邮箱 
        /// </summary>
        public string 会员邮箱 { get; set; }

        /// <summary>  
        /// 会员积分
        /// </summary>
        public string 会员积分 { get; set; }
    }
}
