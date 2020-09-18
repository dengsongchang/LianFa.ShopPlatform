using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Regions
{
    /// <summary>
    /// 获取开放区域列表请求类
    /// </summary>
    public class GetAdminStoreRegionListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        [Required(ErrorMessage = "分页模型不能为空")]
        public PageModel Page { get; set; }
    }
}
