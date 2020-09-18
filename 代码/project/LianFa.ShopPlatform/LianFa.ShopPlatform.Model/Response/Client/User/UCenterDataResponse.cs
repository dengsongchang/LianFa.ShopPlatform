using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Client.User
{
    /// <summary>
    /// 用户中心数据响应类
    /// </summary>
    public class UCenterDataResponse
    {

        /// <summary>
        /// 详情
        /// </summary>
        public UserCenterInfo Info { get; set; }

        /// <summary>
        /// 地址列表
        /// </summary>
        public List<UCenterAddressInfo> AddressList { get; set; }
    }

    /// <summary>
    /// 用户中心
    /// </summary>
    public class UserCenterInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 头像全路径
        /// </summary>
        public string AvatarStr { get; set; }
    }
    /// <summary>
    /// 收货地址列表信息
    /// </summary>
    public class UCenterAddressInfo
    {
        /// <summary>
        /// 地址ID
        /// </summary>
        public int SaId { get; set; }
        /// <summary>
        /// 省市区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 是否默认地址
        /// </summary>
        public int IsDefault { get; set; }
    }
}
