using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Code.Enums
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderState
    {
        /// <summary>
        /// 售后
        /// </summary>
        [Description("售后")]
        AfterSale = 5,
        /// <summary>
        /// 待付款
        /// </summary>
        [Description("待付款")]
        WaitPaying = 20,
        /// <summary>
        /// 确认中
        /// </summary>
        [Description("确认中")]
        Confirming = 40,
        /// <summary>
        /// 已确认
        /// </summary>
        [Description("已确认")]
        Confirmed = 60,
        /// <summary>
        /// 已发货
        /// </summary>
        [Description("已发货")]
        Send = 75,
        /// <summary>
        /// 等待配送
        /// </summary>
        [Description("等待配送")]
        PreProducting = 80,
        /// <summary>
        /// 配送中
        /// </summary>
        [Description("配送中")]
        Sended = 100,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Received = 120,
        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        Locked = 140,
        /// <summary>
        /// 已关闭
        /// </summary>
        [Description("已关闭")]
        Cancelled = 160,
        /// <summary>
        /// 待评价
        /// </summary>
        [Description("待评价")]
        Evaluate = 180,
        /// <summary>
        /// 退款中
        /// </summary>
        [Description("退款中")]
        Refunding = 200,

        /// <summary>
        /// 退款完成
        /// </summary>
        [Description("退款完成")]
        RefundComplete = 220,

        /// <summary>
        /// 取消退款
        /// </summary> 
        [Description("取消退款")]
        CancellRefund = 240,

        /// <summary>
        /// 申请退款
        /// </summary>
        [Description("申请退款")]
        Refund = 250,

        /// <summary>
        /// 备货中
        /// </summary>
        [Description("备货中")]
        PreProduct = 65
    }

    /// <summary>
    /// 订单处理类型
    /// </summary>
    public enum OrderActionType
    {
        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Submit = 20,
        /// <summary>
        /// 支付
        /// </summary>
        [Description("支付")]
        Pay = 40,
        /// <summary>
        /// 确认
        /// </summary>
        [Description("确认")]
        Confirm = 60,
        /// <summary>
        /// 备货
        /// </summary>
        [Description("备货")]
        PreProduct = 80,
        /// <summary>
        /// 发货
        /// </summary>
        [Description("发货")]
        Send = 100,
        /// <summary>
        /// 收货
        /// </summary>
        [Description("收货")]
        Receive = 120,
        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        Lock = 140,
        /// <summary>
        /// 取消
        /// </summary>
        [Description("取消")]
        Cancel = 160,
        /// <summary>
        /// 更新配送费用
        /// </summary>
        [Description("更新配送费用")]
        UpdateShipFee = 180,
        /// <summary>
        /// 更新折扣
        /// </summary>
        [Description("更新折扣")]
        UpdateDiscount = 200,
        /// <summary>
        /// 开始配送
        /// </summary>
        [Description("开始配送")]
        AcceptOrder = 220,
        /// <summary>
        /// 拒绝配送
        /// </summary>
        [Description("拒绝配送")]
        RefuseDelivery = 240,
        /// <summary>
        /// 申请退款
        /// </summary>
        [Description("申请退款")]
        Refund = 250
    }

    /// <summary>
    /// 积分动作枚举
    /// </summary>
    public enum CreditAction
    {
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 1,
        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login = 2,
        /// <summary>
        /// 验证邮箱
        /// </summary>
        [Description("验证邮箱")]
        VerifyEmail = 3,
        /// <summary>
        /// 验证手机
        /// </summary>
        [Description("验证手机")]
        VerifyMobile = 4,
        /// <summary>
        /// 完善用户资料
        /// </summary>
        [Description("完善用户资料")]
        CompleteUserInfo = 5,
        /// <summary>
        /// 支付订单
        /// </summary>
        [Description("支付订单")]
        PayOrder = 6,
        /// <summary>
        /// 完成订单
        /// </summary>
        [Description("完成订单")]
        CompleteOrder = 7,
        /// <summary>
        /// 评价商品
        /// </summary>
        [Description("评价商品")]
        ReviewProduct = 8,
        /// <summary>
        /// 单品促销活动
        /// </summary>
        [Description("单品促销活动")]
        SinglePromotion = 9,
        /// <summary>
        /// 退回订单使用
        /// </summary>
        [Description("退回订单使用")]
        ReturnOrderUse = 10,
        /// <summary>
        /// 退回订单发放
        /// </summary>
        [Description("退回订单发放")]
        ReturnOrderSend = 11,
        /// <summary>
        /// 管理员发放
        /// </summary>
        [Description("管理员发放")]
        AdminSend = 12,
        /// <summary>
        /// 管理员修改
        /// </summary>
        [Description("管理员修改")]
        AdminEdit = 13
    }
}
