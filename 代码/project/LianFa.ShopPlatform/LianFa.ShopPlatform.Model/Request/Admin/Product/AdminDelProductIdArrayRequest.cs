using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Model.Request.Admin.Product
{
    /// <summary>
    /// 后台删除商品请求类
    /// </summary>
    public class AdminDelProductIdArrayRequest
    {
        /// <summary>  
        /// 商品id组 
        /// </summary>
        public int[] PId { get; set; }
    }
}
