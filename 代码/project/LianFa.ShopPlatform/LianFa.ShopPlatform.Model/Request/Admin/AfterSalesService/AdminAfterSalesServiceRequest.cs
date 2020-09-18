using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.AfterSalesService
{
    /// <summary>
    /// 退货列表请求类
    /// </summary>
    public class AdminAfterSalesServiceListRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSn { get; set; }

        /// <summary>
        /// 状态（1审核中/2审核通过/3审核拒绝/4客户已寄出/5商城已收货/6商城已发货/7完成）
        /// </summary>
        [Range(0, 7, ErrorMessage = "状态不能小于0和大于7")]
        public int AsState { get; set; }

        /// <summary>
        /// 开始时间(yyyy-MM-dd格式)
        /// </summary>
        [RegularExpression(@"^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$", ErrorMessage = "开始时间时间格式不正确")]
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间(yyyy-MM-dd格式)
        /// </summary>
        [RegularExpression(@"^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$", ErrorMessage = "结束时间时间格式不正确")]
        public string EndTime { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel Page { get; set; }

        /// <summary>
        /// 售后类型（0退货/1换货/2维修）
        /// </summary>
        public int AsType { get; set; }
    }

    /// <summary>
    /// 删除售后服务
    /// </summary>
    public class AdminDelAfterSalesServiceRequest
    {
        /// <summary>
        /// 售后服务id
        /// </summary>
        public int asSid { get; set; }
    }

    /// <summary>
    /// 审核售后服务
    /// </summary>
    public class AdminAuditAfterAfterSalesRequest
    {
        /// <summary>
        /// 售后服务id
        /// </summary>
        public List<int> AsSId { get; set; }

        /// <summary>
        /// 审核状态(2：通过 3拒绝)
        /// </summary>
        public int AuditState { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string AuditRemark { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Consignee { get; set; }
    }

    /// <summary>
    /// 发货订单基本信息请求类
    /// </summary>
    public class AdminGetDeliveryListRequest
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int oid { get; set; }
    }

    /// <summary>
    /// 发货请求类
    /// </summary>
    public class AdminSendGoodsRequest
    {
        /// <summary>
        /// 售后服务id
        /// </summary>
        public int AsSid { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string ShipCoName { get; set; }

        /// <summary>
        /// 快递编号
        /// </summary>
        public string ShipSn { get; set; }
    }

    /// <summary>
    /// 确认收货
    /// </summary>
    public class AdminBeenShippedRequest
    {
        /// <summary>
        /// 售后服务id
        /// </summary>
        public int asSid { get; set; }
    }

    /// <summary>
    /// 售后服务导出请求
    /// </summary>
    public class ExReturnApplyListingRequest
    {
        /// <summary>  
        /// 售后id列表  
        /// </summary>
        [Required(ErrorMessage = "订单列表id列表不能为空")]
        public List<int> AsSIdList { get; set; }
    }
}
