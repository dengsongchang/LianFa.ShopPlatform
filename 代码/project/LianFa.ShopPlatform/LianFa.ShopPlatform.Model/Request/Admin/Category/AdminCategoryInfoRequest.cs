using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Category
{
    /// <summary>
    /// 后台分类信息请求类
    /// </summary>
    public class AdminCategoryInfoRequest
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public int CateId { get; set; }
    }
}
