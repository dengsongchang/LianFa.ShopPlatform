using System;
using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.User
{
    /// <summary>
    /// 后台用户列表请求类
    /// </summary>
   public class AdminUserListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        [Required(ErrorMessage ="分页模型不能为空")]
        public PageModel Page { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 昵称  
        /// </summary>
        public string NickName { get; set; }

        /// <summary>  
        /// 手机号  
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 注册开始时间
        /// </summary>
        public DateTime? RegisterTimeStart { get; set; }

        /// <summary>
        /// 注册结束时间
        /// </summary>
        public DateTime? RegisterTimeEnd { get; set; }
    }
}
