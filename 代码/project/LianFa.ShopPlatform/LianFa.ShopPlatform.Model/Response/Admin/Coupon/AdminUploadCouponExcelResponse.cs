using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Response.Admin.Coupon
{
    /// <summary>
    /// 后台上传礼品卡Excel文件响应类
    /// </summary>
    public class AdminUploadCouponExcelResponse
    {
        /// <summary>
        /// Excel文件路径
        /// </summary>
        public string ExcelUrl { get; set; }
    }

    /// <summary>
    /// 后台导入礼品卡格式信息
    /// </summary>
    public class AdminUploadCouponInfo
    {
        /// <summary>  
        /// 序列号  
        /// </summary>
        public string 序列号 { get; set; }

        /// <summary>  
        /// 密码  
        /// </summary>
        public string 密码 { get; set; }

        /// <summary>  
        /// 手机号码  
        /// </summary>
        public string 兑换截止日期 { get; set; }
    }

    /// <summary>
    /// 后台批量兑换礼品卡格式信息
    /// </summary>
    public class AdminUploadCouponOrderListInfo
    {
        /// <summary>
        /// 礼品卡序列号
        /// </summary>
        public string 序列号 { get; set; }
        /// <summary>
        /// 验证码/密码
        /// </summary>
        public string 密码 { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string 省 { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string 市 { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string 区 { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string 收货人 { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string 收货地址 { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string 手机号码 { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string 买家备注 { get; set; }
    }
}
