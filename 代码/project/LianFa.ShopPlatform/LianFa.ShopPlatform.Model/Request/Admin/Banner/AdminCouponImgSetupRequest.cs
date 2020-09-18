using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Banner
{
    /// <summary>
    /// 后台编辑礼品卡banner图请求类
    /// </summary>
    public class AdminCouponImgSetupRequest
    {
        /// <summary>
        /// 礼品卡banner图片
        /// </summary>
        public string CouponImg { get; set; }
    }
}
