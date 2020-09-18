using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Admin.Brand
{
    /// <summary>
    /// 后台品牌信息请求类
    /// </summary>
    public class AdminBrandInfoRequest
    {
        /// <summary>
        /// 品牌id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择品牌")]
        public int BrandId { get; set; }
    }
}
