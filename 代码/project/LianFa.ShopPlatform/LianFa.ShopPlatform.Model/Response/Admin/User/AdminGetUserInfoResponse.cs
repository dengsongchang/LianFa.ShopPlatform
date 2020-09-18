using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.User
{
    /// <summary>
    /// 后台获取会员详情响应类
    /// </summary>
    public class AdminGetUserInfoResponse
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public UserInformation UserInfo { get; set; }

        /// <summary>
        /// 统计信息
        /// </summary>
        public Statistics StatisticsInfo { get; set; }

        /// <summary>
        /// 购买记录
        /// </summary>
        public List<BuyRecord> BuyRecordInfo { get; set; }
    }

    /// <summary>
    /// 用户基本信息类
    /// </summary>
    public class UserInformation
    {
        /// <summary>  
    	/// 用户名称  
    	/// </summary>
        public string UserName { get; set; }

        /// <summary>  
        /// 手机  
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
    	/// 性别  
    	/// </summary>
        public string Gender { get; set; }

        /// <summary>  
        /// 头像全路径
        /// </summary>
        public string AvatarFull { get; set; }

        /// <summary>  
    	/// 注册时间  
    	/// </summary>
        public string CreateTime { get; set; }

        /// <summary>  
    	/// 区域id  
    	/// </summary>
        public int RegionId { get; set; }
    }

    /// <summary>
    /// 统计信息
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal? OrderAmount { get; set; }

        /// <summary>  
    	/// 消费次数
    	/// </summary>
        public int OrderSum { get; set; }

        /// <summary>  
    	/// 可用积分
    	/// </summary>
        public int CreditsSum { get; set; }

        /// <summary>  
    	/// 优惠券（张）
    	/// </summary>
        public int CouponSum { get; set; }

        /// <summary>  
    	/// 休眠时间（天）
    	/// </summary>
        public int DormantSum { get; set; }
    }

    /// <summary>
    /// 购买记录
    /// </summary>
    public class BuyRecord
    {
        /// <summary>  
    	/// 订单编号  
    	/// </summary>
        public string OSn { get; set; }

        /// <summary>  
    	/// 下单时间  
    	/// </summary>
        public System.DateTime PayTime { get; set; }

        /// <summary>  
    	/// 完成时间  
    	/// </summary>
        public System.DateTime FinishTime { get; set; }

        /// <summary>  
    	/// 支付方式  
    	/// </summary>
        public string PayFriendName { get; set; }

        /// <summary>  
    	/// 实付金额  
    	/// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>  
    	/// 消费金额  
    	/// </summary>
        public decimal OrderAmount { get; set; }
    }
}
