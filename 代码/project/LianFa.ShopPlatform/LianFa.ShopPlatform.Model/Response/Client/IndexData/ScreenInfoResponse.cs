using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Client.IndexData
{
    /// <summary>
    /// 获取弹屏信息响应类
    /// </summary>
    public class ScreenInfoResponse
    {
        /// <summary>
        /// 弹屏图片
        /// </summary>
        public string ScreenImg { get; set; }

        /// <summary>
        /// 弹屏图片
        /// </summary>
        public string ScreenImgFull { get; set; }

        /// <summary>
        /// 弹屏链接
        /// </summary>
        public string ScreenLink { get; set; }

    }
}
