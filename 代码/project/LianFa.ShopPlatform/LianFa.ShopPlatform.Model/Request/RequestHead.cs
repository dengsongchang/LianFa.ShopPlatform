using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request
{
    /// <summary>
    /// 请求头
    /// </summary>
    public class RequestHead
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        [Required]
        public string TimeStamp { get; set; }

        /// <summary>
        /// 签名串
        /// </summary>
        [Required]
        public string Sign { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        [Required]
        public string MethodName { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }
    }
}
