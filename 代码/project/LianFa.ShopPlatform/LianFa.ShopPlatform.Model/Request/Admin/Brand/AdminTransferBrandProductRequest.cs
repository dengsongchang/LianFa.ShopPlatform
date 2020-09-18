using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Brand
{
    /// <summary>
    /// 后台转移品牌商品接口
    /// </summary>
    public class AdminTransferBrandProductRequest
    {
        /// <summary>
        /// 旧品牌id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择品牌")]
        public short OldBrandId { get; set; }

        /// <summary>
        /// 新品牌id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择品牌")]
        public short NewBrandId { get; set; }
    }
}
