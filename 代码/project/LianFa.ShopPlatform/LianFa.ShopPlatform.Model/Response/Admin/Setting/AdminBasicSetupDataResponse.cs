using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Setting
{
    /// <summary>
    /// 后台平台设置响应类
    /// </summary>
    public class AdminBasicSetupDataResponse
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
