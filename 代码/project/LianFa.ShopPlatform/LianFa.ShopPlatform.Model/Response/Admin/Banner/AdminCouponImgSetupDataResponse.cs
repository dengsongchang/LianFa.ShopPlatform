using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Banner
{
    /// <summary>
    /// 后台编辑礼品卡banner图响应类
    /// </summary>
    public class AdminCouponImgSetupDataResponse
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string CouponImg { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string CouponFullImg { get; set; }
    }
}
