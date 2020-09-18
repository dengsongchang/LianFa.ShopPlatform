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
	 public partial class MaterialCategoryRepository :  EfRepositoryBase<LF_MaterialCategory>, IMaterialCategoryRepository
     {
        public MaterialCategoryRepository(IDataBaseContextFactory databaseFactory, IUnitOfWork unitOfWork)
            : base(databaseFactory,unitOfWork)
        {

        }
     }
}
