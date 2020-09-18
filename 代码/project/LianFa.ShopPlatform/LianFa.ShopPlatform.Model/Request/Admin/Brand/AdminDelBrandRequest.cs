using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Brand
{
    /// <summary>
    /// 后台删除品牌请求类
    /// </summary>
    public class AdminDelBrandRequest
    {
        /// <summary>
        /// 品牌id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择品牌")]
        public short BrandId { get; set; }
    }
}