using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LianFa.ShopPlatform.Model.Request.Client.UCenter
{
    /// <summary>
    /// 评价订单请求类
    /// </summary>
    public class ReviewProductRequest
    { 
        /// <summary>
        /// 订单id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "订单id不正确")]
        public int Oid { get; set; }

        /// <summary>
        /// 订单记录Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "订单记录Id不正确")]
        public int RecordId { get; set; }

        /// <summary>
        /// 星星
        /// </summary>
        [Range(1, 5, ErrorMessage = "星星范围1-5")]
        public byte Star { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        [StringLength(400,ErrorMessage = "评论长度不正确1-400",MinimumLength = 1)]
        public string Message { get; set; }

        /// <summary>
        /// 评论图片列表
        /// </summary>
        public List<string> ImgList { get; set; }
    }
}
