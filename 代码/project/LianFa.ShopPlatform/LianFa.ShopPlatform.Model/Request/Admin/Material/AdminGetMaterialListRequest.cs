using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Material
{
    /// <summary>
    /// 获取素材列表 请求类
    /// </summary>
    public class AdminGetMaterialListRequest
    {
        /// <summary>  
        /// 文件名称  
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 排序方式（1上传时间，2图片名，3修改时间，4图片）
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否顺序（0，1）
        /// </summary>
        public int IsAsc { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }
    }
}
