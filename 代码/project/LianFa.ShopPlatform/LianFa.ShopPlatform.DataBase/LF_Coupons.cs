//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LianFa.ShopPlatform.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class LF_Coupons
    {
        public int CouponId { get; set; }
        public int UId { get; set; }
        public int CouponTypeId { get; set; }
        public int OId { get; set; }
        public System.DateTime UseTime { get; set; }
        public string UseIp { get; set; }
        public System.DateTime AddTime { get; set; }
        public string PassWord { get; set; }
        public byte State { get; set; }
        public int UseUId { get; set; }
        public System.DateTime UseStartTime { get; set; }
        public System.DateTime UseEndTime { get; set; }
        public string CouponSn { get; set; }
    }
}
