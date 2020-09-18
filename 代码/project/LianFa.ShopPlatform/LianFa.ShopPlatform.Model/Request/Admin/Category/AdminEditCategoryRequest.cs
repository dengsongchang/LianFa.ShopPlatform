using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Category
{
    /// <summary>
    /// 后台编辑分类请求类
    /// </summary>
    public class AdminEditCategoryRequest
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public short CateId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(20, ErrorMessage = "名称长度不能大于20")]
        public string Name { get; set; }

        /// <summary>  
        /// 排序  
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}