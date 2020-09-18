using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Offices;

namespace LianFa.ShopPlatform.Model.Request.Admin.Product
{
    /// <summary>
    /// 后台商品id数组请求类
    /// </summary>
    public class AdminProductIdArrayRequest
    {
        /// <summary>  
        /// 商品id组 
        /// </summary>
        public int[] PId { get; set; }

    }
}
