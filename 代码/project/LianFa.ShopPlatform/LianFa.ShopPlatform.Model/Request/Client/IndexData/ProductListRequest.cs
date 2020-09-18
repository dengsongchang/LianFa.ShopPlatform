using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Client.IndexData
{
    /// <summary>
    /// 首页获取商品列表请求类
    /// </summary>
    public class ProductListRequest
    {
        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
}
