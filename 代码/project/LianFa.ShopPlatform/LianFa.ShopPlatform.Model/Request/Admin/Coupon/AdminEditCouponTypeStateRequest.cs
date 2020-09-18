namespace LianFa.ShopPlatform.Model.Request.Admin.Coupon
{
    /// <summary>
    /// 后台编辑礼品卡列表上下架请求类
    /// </summary>
    public class AdminEditCouponTypeStateRequest
    {
        /// <summary>
        /// 礼品卡类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 状态 0-使下架 1-使上架
        /// </summary>
        public int Type { get; set; }
    }
}
