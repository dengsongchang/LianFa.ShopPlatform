using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 确认上传图片 请求类
    /// </summary>
    public class AdminSaveMaterialRequest
    {
        /// <summary>
        /// 临时图片列表
        /// </summary>
        public List<string> TempImageList { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
    }
}
