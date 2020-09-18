using System;
using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.User
{
    /// <summary>
    /// 后台获取会员列表响应类
    /// </summary>
    public class AdminUserListResponse
    {
        /// <summary>
        /// 会员列表
        /// </summary>
        public List<UsersInfo> UserList { get; set; }

        /// <summary>  
        /// 总条数 
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 会员信息
    /// </summary>
    public class UsersInfo
    {
        /// <summary>  
    	/// 用户id  
    	/// </summary>
        public int UId { get; set; }

        /// <summary>  
        /// 用户名称  
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 手机号码  
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>  
    	/// 昵称  
    	/// </summary>
        public string NickName { get; set; }

        /// <summary>  
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>  
        /// 消费金额  
        /// </summary>
        public decimal? OrderAmount { get; set; }

        /// <summary>  
        /// 订单总数
        /// </summary>
        public int OrderSum { get; set; }

        /// <summary>  
        /// 卡片订单数
        /// </summary>
        public int CouponsOrders { get; set; }
    }


    /// <summary>
    /// 会员信息
    /// </summary>
    public class ExportUserInfo
    {
        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }
        /// <summary>  
        /// 用户名称  
        /// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 昵称  
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 会员信息
    /// </summary>
    public class OrderTotal
    {
        /// <summary>  
        /// 用户id  
        /// </summary>
        public int UId { get; set; }

        /// <summary>  
        /// 用户id  
        /// </summary>
        public int TotalOrderCount { get; set; }

        /// <summary>  
        /// 用户id  
        /// </summary>
        public decimal TotalOrderAmount { get; set; }
    }
}
