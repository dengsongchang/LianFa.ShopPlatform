using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.Material
{
    /// <summary>
    /// 获取素材列表 响应类
    /// </summary>
    public class AdminGetMaterialListResponse
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<MaterialModel> List { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 素材模型
    /// </summary>
    public class MaterialModel
    {
        /// <summary>  
        /// 素材Id  
        /// </summary>
        public int MaterialId { get; set; }
        /// <summary>  
        /// 素材类型  
        /// </summary>
        public byte Type { get; set; }
        /// <summary>  
        /// 展示名称  
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>  
        /// 文件大小(kb)  
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>  
        /// 创建时间  
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>  
        /// 分类Id  
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>  
        /// 更新时间  
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
    }
}
