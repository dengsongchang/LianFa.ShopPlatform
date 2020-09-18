using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Statistics
{
    /// <summary>
    /// 获取会员端口来源信息请求类
    /// </summary>
    public class AdminGetUserPortSourceListRequest
    {
        /// <summary>
        /// 年
        /// </summary>
        [Required(ErrorMessage = "年份不能为空")]
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [Required(ErrorMessage = "月份不能为空")]
        public int Month { get; set; }
    }
}
