using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.Account
{
    /// <summary>
    /// 用户信息请求类
    /// </summary>
    public class UserInfoRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户编号不能为空")]
        public int UId { get; set; }
    }
}

