using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Category
{
    /// <summary>
    /// 后台删除分类请求类
    /// </summary>
    public class AdminDelCategoryRequest
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public short CateId { get; set; }
    }
}