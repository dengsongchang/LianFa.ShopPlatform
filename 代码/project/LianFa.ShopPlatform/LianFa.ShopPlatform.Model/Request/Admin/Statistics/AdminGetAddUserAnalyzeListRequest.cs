using System;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Statistics
{
    /// <summary>
    /// 获取会员增长情况列表请求类
    /// </summary>
    public class AdminGetAddUserAnalyzeListRequest
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
