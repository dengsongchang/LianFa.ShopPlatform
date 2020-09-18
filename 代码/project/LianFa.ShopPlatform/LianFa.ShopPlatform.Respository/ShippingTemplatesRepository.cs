using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LianFa.ShopPlatform.DataBase;
using HuCheng.Util.Core.Datas;
using HuCheng.Util.Core.Dependency;
using HuCheng.Util.Datas.EntityFramework;

namespace LianFa.ShopPlatform.Repository
{
    [DependencyRegister]
	 public partial class ShippingTemplatesRepository :  EfRepositoryBase<LF_ShippingTemplates>, IShippingTemplatesRepository
     {
        public ShippingTemplatesRepository(IDataBaseContextFactory databaseFactory, IUnitOfWork unitOfWork)
            : base(databaseFactory,unitOfWork)
        {

        }
     }
}
