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
	 public partial class CouponTypeContentsRepository :  EfRepositoryBase<LF_CouponTypeContents>, ICouponTypeContentsRepository
     {
        public CouponTypeContentsRepository(IDataBaseContextFactory databaseFactory, IUnitOfWork unitOfWork)
            : base(databaseFactory,unitOfWork)
        {

        }
     }
}
