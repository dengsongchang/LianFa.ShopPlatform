using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.WxOpen
{
    /// <summary>
    /// 获取用户信息请求类
    /// </summary>
    public class GetUserInfoRequest
    {
        /// <summary>
        /// EncryptedData
        /// </summary>
        [Required]
        public string EncryptedData { get; set; }

        /// <summary>
        /// Iv
        /// </summary>
        [Required]
        public string Iv { get; set; }

        /// <summary>
        /// SessionId
        /// </summary>
        [Required]
        public string SessionId { get; set; } 
    }
}