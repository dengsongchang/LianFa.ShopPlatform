using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Category
{
    /// <summary>
    /// 后台转移分类商品接口
    /// </summary>
    public class AdminTransferCategoryProductRequest
    {
        /// <summary>
        /// 旧分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public short OldCateId { get; set; }

        /// <summary>
        /// 新分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public short NewCateId { get; set; }
    }
}
