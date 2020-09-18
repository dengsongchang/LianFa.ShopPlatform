using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Client.UCenter
{
    /// <summary>
    /// 获取会员积分明细列表请求类
    /// </summary>
    public class UserCreditDetailListRequest
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        [Required(ErrorMessage = "分页模型不能为空")]
        public PageModel Page { get; set; }

    }
}
