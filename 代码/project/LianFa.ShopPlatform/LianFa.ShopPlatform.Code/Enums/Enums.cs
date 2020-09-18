using System.ComponentModel;

namespace LianFa.ShopPlatform.Code.Enums
{
    /// <summary>
    /// 响应码
    /// </summary>
    public enum ResponseCode
    {
        #region 系统响应码 100开始
        /// <summary>
        /// 成功
        /// </summary> 
        [Description("成功")]
        Success = 100,
        /// <summary>
        /// 系统错误
        /// </summary> 
        [Description("系统错误")]
        ServerError = 101,
        /// <summary>
        /// 超时错误
        /// </summary> 
        [Description("超时错误")]
        TimeOut = 102,
        /// <summary>
        /// 未知错误
        /// </summary> 
        [Description("未知错误")]
        UnknownError = 103,
        /// <summary> 
        /// 未知请求
        /// </summary> 
        [Description("未知请求")]
        UnknownRequest = 104,
        /// <summary>
        /// 不支持的数据类型
        /// </summary> 
        [Description("不支持的数据类型")]
        DataTypeError = 105,
        /// <summary>
        /// 方法名错误
        /// </summary> 
        [Description("方法名错误")]
        MethodError = 106,
        /// <summary>
        /// 签名验证失败
        /// </summary> 
        [Description("签名验证失败")]
        SignError = 107,
        /// <summary>
        /// 无效
        /// </summary> 
        [Description("Token无效")]
        TokenError = 108,
        /// <summary>
        /// Token过期
        /// </summary> 
        [Description("Token过期")]
        TokenTimeOut = 109,
        /// <summary>
        /// Session过期
        /// </summary> 
        [Description("Session过期")]
        SessionTimeOut = 110,
        #endregion

        #region 公共响应码 200 开始
        /// <summary>
        /// 时间戳无效或过期
        /// </summary> 
        [Description("时间戳无效或过期")]
        TimestampUnknown = 200,
        /// <summary>
        /// 管理员不存在
        /// </summary> 
        [Description("管理员不存在")]
        ManagerNoExist = 201,
        /// <summary>
        /// 密码错误
        /// </summary> 
        [Description("密码错误")]
        PasswordError = 202,
        /// <summary>
        /// 密码格式错误
        /// </summary> 
        [Description("密码格式错误")]
        PasswordFormatError = 203,
        /// <summary>
        /// 新密码不能与旧密码相同
        /// </summary> 
        [Description("新密码不能与旧密码相同")]
        PasswordCanNoSame = 204,
        /// <summary>
        /// 旧密码不正确
        /// </summary> 
        [Description("旧密码不正确")]
        OldPasswordNoTrue = 205,
        /// <summary>
        /// 上传失败
        /// </summary> 
        [Description("上传失败")]
        UploadFail = 206,
        /// <summary>
        /// 请选择上传文件
        /// </summary> 
        [Description("请选择上传文件")]
        PleaseSelectFile = 207,
        /// <summary>
        /// 上传文件大小超过限制
        /// </summary> 
        [Description("上传文件大小超过限制")]
        UploadFileTooBig = 208,
        /// <summary>
        /// 不支持上传该文件类型
        /// </summary> 
        [Description("不支持上传该文件类型")]
        UnSupportFileType = 209,
        /// <summary>
        /// 上传成功
        /// </summary> 
        [Description("上传成功")]
        UploadSuccess = 210,
        /// <summary>
        /// 文件已存在
        /// </summary> 
        [Description("文件已存在")]
        FileExist = 211,
        /// <summary>
        /// 您已登录,请勿重复登录
        /// </summary> 
        [Description("您已登录,请勿重复登录")]
        AlreadyLogin = 297,
        /// <summary>
        /// 数据验证错误
        /// </summary> 
        [Description("数据验证错误")]
        DataError = 298,
        /// <summary>
        /// 未登录
        /// </summary> 
        [Description("您还未登录,请登录")]
        NoLogin = 299,
        #endregion

        #region 系统错误响应码 900 开始
        /// <summary>
        /// 失败
        /// </summary> 
        [Description("失败")]
        Fail = 999,
        #endregion
    }

    #region 公有枚举

    /// <summary>
    /// 记录状态
    /// </summary>
    public enum RecordStatus
    {
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Delete = 1,

        /// <summary>
        /// 未删除
        /// </summary>
        [Description("未删除")]
        UnDelete = 0
    }

    /// <summary>
    /// 是否
    /// </summary>
    public enum WhetherType
    {
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes = 1,

        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 0
    }

    #endregion

    #region 用户相关枚举

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User = 1,

        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        SystemManager = 2
    }

    /// <summary>
    /// 用户性别
    /// </summary>
    public enum UserGender
    {
        /// <summary>
        /// 保密
        /// </summary>
        [Description("保密")]
        Nothing = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 2
    }

    /// <summary>
    /// 功能动作类型
    /// </summary>
    public enum ActionTypeEnum
    {
        /// <summary>
        /// 一级菜单
        /// </summary>
        [Description("一级菜单")]
        FirstLevelMenu = 1,

        /// <summary>
        /// 二级菜单
        /// </summary>
        [Description("二级菜单")]
        SecondLevelMenu = 2,

        /// <summary>
        /// 接口请求
        /// </summary>
        [Description("接口请求")]
        InterfaceRequest = 3,

        /// <summary>
        /// Tab页
        /// </summary>
        [Description("Tab页")]
        Tab = 4,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 5,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 6
    }
    #endregion

    #region 区域级别相关枚举

    /// <summary>
    /// 区域级别相关枚举
    /// </summary>
    public enum RegionLayer
    {
        /// <summary>
        /// 精确到省
        /// </summary>
        ToProvince = 1,

        /// <summary>
        /// 精确到市
        /// </summary>
        ToCity = 2,

        /// <summary>
        /// 精确到区或县
        /// </summary>
        ToAreaOrCounty = 3
    }
    #endregion

    #region 是否为默认的相关枚举
    /// <summary>
    /// 是否为默认的相关枚举
    /// </summary>
    public enum Default
    {
        /// <summary>
        /// 非默认
        /// </summary> 
        [Desc("非默认")]
        NotDefault = 0,

        /// <summary>
        /// 默认
        /// </summary> 
        [Desc("默认")]
        IsDefault = 1
    }
    #endregion

    #region 商品状态相关枚举

    /// <summary>
    /// 商品状态
    /// </summary>
    public enum ProductsStatus
    {
        /// <summary>
        /// 上架
        /// </summary>
        [Description("上架")]
        OnSale = 1,

        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        OutSale = 0,

        /// <summary>
        /// 回收站
        /// </summary>
        [Description("回收站")]
        RecycleBin = 3,
    }
    #endregion

    #region 配送模板计价方式相关枚举
    /// <summary>
    /// 配送模板计价方式相关枚举
    /// </summary>
    public enum ValuationMethod
    {
        /// <summary>
        /// 按件计
        /// </summary> 
        [Description("按件计")]
        Number = 1,

        /// <summary>
        /// 按重量
        /// </summary> 
        [Description("按重量")]
        Weight = 2,

        /// <summary>
        /// 按百分比
        /// </summary> 
        [Description("按百分比")]
        Percentage = 4,

        /// <summary>
        /// 按固定运费
        /// </summary> 
        [Description("按固定运费")]
        Fixed = 5
    }
    /// <summary>
    /// 配送地区类型相关枚举
    /// </summary>
    public enum AppointType
    {
        /// <summary>
        /// 按件数
        /// </summary> 
        [Description("按件数")]
        Number = 0,

        /// <summary>
        /// 按金额
        /// </summary> 
        [Description("按金额")]
        Price = 1,

        /// <summary>
        /// 按金额+件数
        /// </summary> 
        [Description("按金额+件数")]
        NumPrice = 2
    }

    #endregion

    #region 物流公司状态相关枚举

    /// <summary>
    /// 物流公司状态
    /// </summary>
    public enum ShipCompaniesStatusa
    {
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Close = 0,

        /// <summary>
        /// 开启
        /// </summary>
        [Description("开启")]
        Open = 1,
    }
    #endregion

    #region 提现

    /// <summary>
    /// 提现审核状态
    /// </summary>
    public enum WithdrawAuditStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Desc("待审核")]
        Init = 0,

        /// <summary>
        /// 不通过
        /// </summary>
        [Desc("不通过")]
        NotThrough = 2,

        /// <summary>
        /// 通过
        /// </summary>
        [Desc("通过")]
        Pass = 1
    }

    /// <summary>
    /// 提现审核状态
    /// </summary>
    public enum WithdrawAuditType
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Desc("支付宝")]
        AliPay = 1,
        /// <summary>
        /// 微信
        /// </summary>
        [Desc("微信")]
        Wechat = 2,
    }
    #endregion

    #region 物流状态
    /// <summary>
    /// 物流状态
    /// </summary>
    public enum LogisticState
    {
        /// <summary>
        /// 在途
        /// </summary> 
        [Description("在途")]
        Onway = 0,
        /// <summary>
        /// 揽件
        /// </summary> 
        [Description("揽件")]
        IncludeProduct = 1,
        /// <summary>
        /// 疑难
        /// </summary> 
        [Description("疑难")]
        Difficult = 2,
        /// <summary>
        /// 签收
        /// </summary> 
        [Description("签收")]
        SignFor = 3,
        /// <summary>
        /// 退签
        /// </summary> 
        [Description("退签")]
        SignOut = 4,
        /// <summary>
        /// 派件
        /// </summary> 
        [Description("派件")]
        Delivery = 5,
        /// <summary>
        /// 退回
        /// </summary> 
        [Description("退回")]
        SendBack = 6
    }
    #endregion

    #region 价格区间计价类型相关枚举
    /// <summary>
    /// 价格区间计价类型相关枚举
    /// </summary>
    public enum PriceType
    {
        /// <summary>
        /// 按百分比
        /// </summary> 
        [Description("按百分比")]
        Percentage = 0,

        /// <summary>
        /// 按固定运费
        /// </summary> 
        [Description("按固定运费")]
        Fixed = 1,
    }
    #endregion

    #region 订单相关枚举

    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 普通订单
        /// </summary>
        [Description("普通订单")]
        RegularOrders = 0,
        /// <summary>
        /// 卡片订单
        /// </summary>
        [Description("卡片订单")]
        CardOrder = 1,

        /// <summary>
        /// 购买卡片订单
        /// </summary>
        [Description("购买卡片订单")]
        BuyCardOrder = 2,
    }

    /// <summary>
    /// 订单来源
    /// </summary>
    public enum OrderSource
    {
        /// <summary>
        /// PC
        /// </summary>
        [Description("PC")]
        PC = 1,
        /// <summary>
        /// 微信小程序
        /// </summary>
        [Description("微信小程序")]
        WxMini = 2,
        /// <summary>
        /// 拼团
        /// </summary>
        [Description("拼团")]
        GroupBuy = 3
    }

    #endregion

    #region 礼品卡相关枚举

    /// <summary>
    /// 礼品卡类型状态
    /// </summary>
    public enum CouponTypeState
    {
        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        SoldOut = 0,
        /// <summary>
        /// 上架
        /// </summary>
        [Description("上架")]
        OnSale = 1
    }

    /// <summary>
    /// 礼品卡状态
    /// </summary>
    public enum CouponState
    {
        /// <summary>
        /// 未兑换
        /// </summary>
        [Description("未兑换")]
        UnUse = 0,
        /// <summary>
        /// 已兑换
        /// </summary>
        [Description("已兑换")]
        Used = 1,
        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Due = 2,
        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")]
        Cancel = 3
    }

    #endregion

    #region 订单相关

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayModeType
    {
        /// <summary>
        /// 货到付款
        /// </summary>
        [Description("货到付款")]
        Delivery = 0,
        /// <summary>
        /// 在线付款
        /// </summary>
        [Description("在线付款")]
        Online = 1
    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderTypes
    {
        /// <summary>
        /// 普通订单
        /// </summary>
        [Description("普通订单")]
        Common = 0,
        /// <summary>
        /// 礼品卡订单
        /// </summary>
        [Description("礼品卡订单")]
        Coupon = 1
    }

    #endregion

    #region 素材相关枚举
    /// <summary>
    /// 素材类型
    /// </summary>
    public enum MaterialType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Image = 1,
    }
    /// <summary>
    /// 素材类型（1上传时间，2图片名，3修改时间，4图片）
    /// </summary>
    public enum MaterialListOrder
    {
        /// <summary>
        /// 上传时间
        /// </summary>
        [Description("上传时间")]
        CreateTime = 1,

        /// <summary>
        /// 2图片名
        /// </summary>
        [Description("2图片名")]
        FileName = 2,

        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        UpdateTime = 3,

        /// <summary>
        /// 图片大小
        /// </summary>
        [Description("图片大小")]
        ImageSize = 4,


    }
    #endregion

}