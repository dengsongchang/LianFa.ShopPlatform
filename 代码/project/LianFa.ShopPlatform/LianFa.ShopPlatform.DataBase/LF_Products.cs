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
    
    public partial class LF_Products
    {
        public int PId { get; set; }
        public string PSn { get; set; }
        public short CateId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public byte State { get; set; }
        public int DisplayOrder { get; set; }
        public int Weight { get; set; }
        public string ShowImg { get; set; }
        public System.DateTime AddTime { get; set; }
        public string Description { get; set; }
        public decimal ShopPrice { get; set; }
        public int TemplateId { get; set; }
        public int IsCostPrice { get; set; }
        public string Summary { get; set; }
    }
}
