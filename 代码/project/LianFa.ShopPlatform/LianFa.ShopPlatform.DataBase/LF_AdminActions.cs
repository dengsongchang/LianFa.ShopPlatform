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
    
    public partial class LF_AdminActions
    {
        public int AdminAId { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public int ParentId { get; set; }
        public int Layer { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; }
        public int ActionType { get; set; }
        public byte IsAllChildAction { get; set; }
    }
}
