using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Setting
{
    /// <summary>
    /// 后台设置平台数据请求类
    /// </summary>
    public class AdminBasicSetupRequest
    {
        /// <summary>
        /// 平台地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 固定电话电话
        /// </summary>
        public string CustomerPhone { get; set; }
    }
}
