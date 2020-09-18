using System.Collections.Generic;
using System.Linq;
using HuCheng.Util.AutoMapper;
using HuCheng.Util.Core.Datas.Repositories;
using HuCheng.Util.Core.Extension;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.DataBase;
using LianFa.ShopPlatform.Model.Response.Client.Order;
using LianFa.ShopPlatform.Model.Response.Admin.Templates;

namespace LianFa.ShopPlatform.Service
{
    public partial class ShippingTemplatesService
    {
        #region Fields

        private readonly IRepository<LF_Products> _productsRepository;
        private readonly IRepository<LF_ShippingPrice> _shippingPriceRepository;
        private readonly IRepository<LF_ShippingRegions> _shippingRegionsRepository;
        private readonly IRepository<LF_ShippingRegionsGroups> _shippingRegionsGroupsRepository;
        private readonly IRepository<LF_OrderProducts> _ordersProductsRepository;
        private readonly IRepository<LF_ShippingAppointRegions> _shippingAppointRegions;
        private readonly IRepository<LF_Orders> _orders;
        private readonly IRepository<LF_CouponTypes> _couponTypesRepository;
        private readonly IRepository<LF_Regions> _regionsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingTemplatesRepository">运费模板仓储类</param>
        /// <param name="productsRepository">商品仓储类</param>
        /// <param name="shippingPriceRepository">运费模板计价表仓储类</param>
        /// <param name="shippingRegionsRepository">配送地区表仓储类</param>
        /// <param name="shippingRegionsGroupsRepository">配送地区价格表仓储类</param>
        /// <param name="ordersProductsRepository">订单表仓储类</param>
        /// <param name="shippingAppointRegions">指定城市包邮表仓储类</param>
        /// <param name="orders">订单仓储类</param>
        /// <param name="couponTypesRepository">订单仓储类</param>
        /// <param name="regionsRepository"></param>
        public ShippingTemplatesService(IRepository<LF_ShippingTemplates> shippingTemplatesRepository, IRepository<LF_Products> productsRepository,
                                        IRepository<LF_ShippingPrice> shippingPriceRepository, IRepository<LF_ShippingRegions> shippingRegionsRepository,
                                        IRepository<LF_ShippingRegionsGroups> shippingRegionsGroupsRepository, IRepository<LF_OrderProducts> ordersProductsRepository,
                                        IRepository<LF_ShippingAppointRegions> shippingAppointRegions, IRepository<LF_Orders> orders, IRepository<LF_CouponTypes> couponTypesRepository,
            IRepository<LF_Regions> regionsRepository)
        {
            this._shippingTemplatesRepository = shippingTemplatesRepository;
            this._productsRepository = productsRepository;
            this._shippingPriceRepository = shippingPriceRepository;
            this._shippingRegionsRepository = shippingRegionsRepository;
            this._shippingRegionsGroupsRepository = shippingRegionsGroupsRepository;
            this._ordersProductsRepository = ordersProductsRepository;
            this._shippingAppointRegions = shippingAppointRegions;
            this._orders = orders;
            this._couponTypesRepository = couponTypesRepository;
            this._regionsRepository = regionsRepository;
        }

        #endregion

        /// <summary>
        /// 获得配送模板列表
        /// </summary>
        /// <param name="page">分页模型</param>
        /// <param name="total">总数</param>
        /// <returns>配送模板列表</returns>
        public List<TemplatesListInfo> GetTemplatesList(PageModel page, out int total)
        {
            return (from s in _shippingTemplatesRepository.GetDbSetNoTracking()
                    select new TemplatesListInfo
                    {
                        TemplateId = s.TemplateId,
                        TemplateName = s.TemplateName,
                        ValuationMethod = s.ValuationMethod
                    }).OrderByDescending(m => m.TemplateId)
                      .LoadPage(page, out total)
                      .ToList();
        }

        /// <summary>
        /// 计算运费
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="defaultFullShipAddressInfo">用户完整地址信息</param>
        /// <returns>计算运费</returns>
        public decimal CalculateFreight(List<LF_OrderProducts> orderProductList, FullShipAddressInfo defaultFullShipAddressInfo)
        {
            decimal freight = 0;

            if (defaultFullShipAddressInfo == null)
            {
                return freight;
            }
            if (!orderProductList.Any())
            {
                return freight;
            }
            var rId = defaultFullShipAddressInfo.RegionId;
            foreach (var orderProduct in orderProductList)
            {
                //获取运费模板
                var templates = _productsRepository.LoadEntitiesNoTracking(m => m.PId == orderProduct.PId).FirstOrDefault();
                if (templates == null)
                {
                    continue;
                }

                //获取运费模板Id
                var templateId = templates.TemplateId;

                //获取运费模板
                var shippingTemplates = _shippingTemplatesRepository.LoadEntitiesNoTracking(m => m.TemplateId == templateId).FirstOrDefault();
                if (shippingTemplates == null)
                {
                    continue;
                }

                //商品总价
                var price = orderProduct.ShopPrice * orderProduct.BuyCount;

                //商品件数
                var number = orderProduct.BuyCount;

                //商品重量
                var weight = orderProduct.BuyCount * orderProduct.Weight;

                //商品体积
                var volume = 0m;

                switch (shippingTemplates.ValuationMethod)
                {
                    case (int)ValuationMethod.Percentage:
                        freight = CalculatePercentageFreight(templateId, price, freight);
                        break;
                    case (int)ValuationMethod.Fixed:
                        {
                            freight = CalculateFixedFreight(templateId, price, freight);
                        }
                        break;
                    default:
                        freight = CalculateFreight(templateId, number, weight, price, volume, freight, rId);
                        break;
                }
            }

            return freight;
        }

        /// <summary>
        /// 计算单种商品运费
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <param name="defaultFullShipAddressInfo"></param>
        /// <returns>计算运费</returns>
        public decimal ProductCalculateFreight(LF_OrderProducts orderProduct, FullShipAddressInfo defaultFullShipAddressInfo)
        {
            decimal freight = 0;

            //获取运费模板
            var templates = _productsRepository.LoadEntitiesNoTracking(m => m.PId == orderProduct.PId).FirstOrDefault();
            if (templates == null)
            {
                return 0;
            }

            //获取运费模板Id
            var templateId = templates.TemplateId;

            //获取运费模板
            var shippingTemplates = _shippingTemplatesRepository.LoadEntitiesNoTracking(m => m.TemplateId == templateId).FirstOrDefault();
            if (shippingTemplates == null)
            {
                return 0;
            }

            var rId = defaultFullShipAddressInfo.RegionId;
            //商品总价
            var price = orderProduct.ShopPrice * orderProduct.BuyCount;

            //商品件数
            var number = orderProduct.BuyCount;

            //商品重量
            var weight = orderProduct.BuyCount * orderProduct.Weight;

            //商品体积
            var volume = 0m;

            switch (shippingTemplates.ValuationMethod)
            {
                case (int)ValuationMethod.Percentage:
                    freight = CalculatePercentageFreight(templateId, price, freight);
                    break;
                case (int)ValuationMethod.Fixed:
                    {
                        freight = CalculateFixedFreight(templateId, price, freight);
                    }
                    break;
                default:
                    freight = CalculateFreight(templateId, number, weight, price, volume, freight, rId);
                    break;
            }

            return freight;
        }

        /// <summary>
        /// 计算单种礼品卡运费
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <param name="defaultFullShipAddressInfo"></param>
        /// <returns>计算运费</returns>
        public decimal CouponCalculateFreight(LF_OrderProducts orderProduct, FullShipAddressInfo defaultFullShipAddressInfo)
        {
            decimal freight = 0;

            //获取运费模板
            var templates = _couponTypesRepository.LoadEntitiesNoTracking(m => m.CouponTypeId == orderProduct.PId).FirstOrDefault();
            if (templates == null)
            {
                return 0;
            }

            //获取运费模板Id
            var templateId = templates.TemplateId;

            //获取运费模板
            var shippingTemplates = _shippingTemplatesRepository.LoadEntitiesNoTracking(m => m.TemplateId == templateId).FirstOrDefault();
            if (shippingTemplates == null)
            {
                return 0;
            }

            var rId = defaultFullShipAddressInfo.RegionId;
            //商品总价
            var price = orderProduct.ShopPrice * orderProduct.BuyCount;

            //商品件数
            var number = orderProduct.BuyCount;

            //商品重量
            var weight = orderProduct.BuyCount * orderProduct.Weight;

            //商品体积
            var volume = 0m;

            switch (shippingTemplates.ValuationMethod)
            {
                case (int)ValuationMethod.Percentage:
                    freight = CalculatePercentageFreight(templateId, price, freight);
                    break;
                case (int)ValuationMethod.Fixed:
                {
                    freight = CalculateFixedFreight(templateId, price, freight);
                }
                    break;
                default:
                    freight = CalculateFreight(templateId, number, weight, price, volume, freight, rId);
                    break;
            }

            return freight;
        }

        /// <summary>
        /// 计算固定运费
        /// </summary>
        /// <param name="templates"></param>
        /// <param name="price"></param>
        /// <param name="freight"></param>
        /// <returns></returns>
        private decimal CalculateFixedFreight(int templates, decimal price, decimal freight)
        {
            //获得该运费模板的金额区间信息
            var shippingPriceInfo = _shippingPriceRepository.LoadEntitiesNoTracking(m => m.TemplateId == templates).FirstOrDefault().MapTo<ShippingPriceInfo>();

            //判断
            if (shippingPriceInfo == null)
            {
                return freight;
            }

            //计算运费
            if (price >= shippingPriceInfo.WithFree && shippingPriceInfo.WithFree != 0)
            {
                freight += 0m;
            }
            if (price < shippingPriceInfo.SPrice && shippingPriceInfo.SPrice != 0)
            {
                freight += shippingPriceInfo.SFreight;
            }
            if (price > shippingPriceInfo.StartPrice && price < shippingPriceInfo.EndPrice && shippingPriceInfo.StartPrice != 0 &&
                shippingPriceInfo.EndPrice != 0)
            {
                freight += shippingPriceInfo.Freight;
            }
            return freight;
        }

        /// <summary>
        /// 计算百分比运费
        /// </summary>
        /// <param name="templates"></param>
        /// <param name="price"></param>
        /// <param name="freight"></param>
        /// <returns></returns>
        private decimal CalculatePercentageFreight(int templates, decimal price, decimal freight)
        {
            //获得该运费模板的金额区间信息
            var shippingPriceInfo = _shippingPriceRepository.LoadEntitiesNoTracking(m => m.TemplateId == templates).FirstOrDefault().MapTo<ShippingPriceInfo>();

            //判断
            if (shippingPriceInfo == null)
            {
                return freight;
            }

            //计算运费
            if (price >= shippingPriceInfo.WithFree && shippingPriceInfo.WithFree != 0)
            {
                freight += 0m;
            }
            if (price <= shippingPriceInfo.SPrice && shippingPriceInfo.SPrice != 0)
            {
                freight += shippingPriceInfo.SFreight / 100 * price;
            }
            if (price > shippingPriceInfo.StartPrice && price < shippingPriceInfo.EndPrice && shippingPriceInfo.StartPrice != 0 &&
                shippingPriceInfo.EndPrice != 0)
            {
                freight += shippingPriceInfo.Freight / 100 * price;
            }
            return freight;
        }

        /// <summary>
        /// 计算按区域运费
        /// </summary>
        /// <param name="templates"></param>
        /// <param name="number"></param>
        /// <param name="weight"></param>
        /// <param name="price"></param>
        /// <param name="volume"></param>
        /// <param name="freight"></param>
        /// <param name="rId"></param>
        /// <returns></returns>
        private decimal CalculateFreight(int templates, int number, int weight, decimal price, decimal volume, decimal freight, int rId)
        {
            /*
            var kmShippingTemplates = _shippingTemplatesRepository.GetDbSetNoTracking().FirstOrDefault(m => m.TemplateId == templates);

            //获得该运费模板的配送地区信息
            var shippingPriceInfo = (from t in _shippingTemplatesRepository.GetDbSetNoTracking()
                                     join tg in _shippingRegionsGroupsRepository.GetDbSetNoTracking() on t.TemplateId equals tg.TemplateId
                                     join ta in _shippingAppointRegions.GetDbSetNoTracking() on t.TemplateId equals ta.TemplateId
                                     join trg in _shippingRegionsRepository.GetDbSetNoTracking() on tg.GroupId equals trg.GroupId
                                     join tra in _shippingRegionsRepository.GetDbSetNoTracking() on ta.AppId equals tra.AppId
                                     select new { t, tg, ta, trg, tra }).Where(m => m.t.TemplateId == templates)
                                                                    .Where(m => m.ta.TemplateId == templates)
                                                                    .Where(m => m.tg.TemplateId == templates)
                                                                    .Where(m => m.tra.TemplateId == templates)
                                                                    .Where(m => m.trg.TemplateId == templates).ToList();

            var regionsIdList = shippingPriceInfo.Select(m => m.trg.RegionId).ToList();

            var appRegionsIdList = shippingPriceInfo.Select(m => m.tra.RegionId).ToList();

            if (appRegionsIdList.Contains(rId))
            {
                var firstOrDefault = shippingPriceInfo.FirstOrDefault(m => m.trg.RegionId == rId);

                var info = firstOrDefault?.ta;

                if (info != null && info.Type == (int)AppointType.Number)
                {
                    if (number >= info.MeetNum)
                    {
                        freight = 0;
                        return freight;
                    }
                }
                if (info != null && info.Type == (int)AppointType.Price)
                {
                    if (price >= info.MeetPrice)
                    {
                        freight = 0;
                        return freight;
                    }
                }
                if (info != null && info.Type == (int)AppointType.NumPrice)
                {
                    if (price >= info.MeetPrice && number >= info.MeetNum)
                    {
                        freight = 0;
                        return freight;
                    }
                }
            }
            var type = kmShippingTemplates?.ValuationMethod;

            if (type == (int)ValuationMethod.Number)
            {
                #region 按件计

                if (regionsIdList.Contains(rId))
                {
                    var firstOrDefault = shippingPriceInfo.FirstOrDefault(m => m.trg.RegionId == rId);

                    var info = firstOrDefault?.tg;
                    if (number <= info?.DefaultNumber)
                    {
                        freight = info.Price;
                    }
                    else
                    {
                        var num = number - info?.DefaultNumber;

                        for (var i = 0; i < num;)
                        {
                            freight += info.AddPrice;

                            num = num - info.AddNumber;
                        }
                        if (info != null)
                        {
                            freight += info.Price;
                        }
                    }
                }
                else
                {
                    if (number <= kmShippingTemplates?.DefaultNumber)
                    {
                        freight = kmShippingTemplates.Price;
                    }
                    else
                    {
                        var num = number - kmShippingTemplates?.DefaultNumber;
                        for (var i = 0; i < num;)
                        {
                            freight += kmShippingTemplates.AddPrice;

                            num = num - kmShippingTemplates.AddNumber;
                        }
                        freight += kmShippingTemplates.Price;
                    }
                }

                #endregion
            }
            else if (type == (int)ValuationMethod.Weight)
            {
                #region 按重量

                if (regionsIdList.Contains(rId))
                {
                    var firstOrDefault = shippingPriceInfo.FirstOrDefault(m => m.trg.RegionId == rId);

                    var info = firstOrDefault?.tg;
                    if (weight <= info?.DefaultNumber)
                    {
                        freight = info.Price;
                    }
                    else
                    {
                        var num = weight - info?.DefaultNumber;
                        for (var i = 0; i < num;)
                        {
                            freight += info.AddPrice;

                            num = num - info.AddNumber;
                        }

                        if (info != null) freight += info.Price;
                    }
                }
                else
                {
                    if (weight <= kmShippingTemplates?.DefaultNumber)
                    {
                        freight = kmShippingTemplates.Price;
                    }
                    else
                    {
                        var num = weight - kmShippingTemplates?.DefaultNumber;
                        for (var i = 0; i < num;)
                        {
                            freight += kmShippingTemplates.AddPrice;

                            num = num - kmShippingTemplates.AddNumber;
                        }

                        freight += kmShippingTemplates.Price;
                    }
                }

                #endregion
            }
            else
            {
                freight = 0;
            }

            return freight;
            */

            var kmShippingTemplates = _shippingTemplatesRepository.GetDbSetNoTracking().FirstOrDefault(m => m.TemplateId == templates);
            if (kmShippingTemplates == null)
            {
                return 0M;
            }

            //获得该运费模板的配送地区信息
            var shippingPriceInfo = (from t in _shippingTemplatesRepository.GetDbSetNoTracking()
                    join tg in _shippingRegionsGroupsRepository.GetDbSetNoTracking() on t.TemplateId equals tg.TemplateId
                    join trg in _shippingRegionsRepository.GetDbSetNoTracking() on tg.GroupId equals trg.GroupId
                    select new { t, tg, trg }).Where(m => m.t.TemplateId == templates)
                .Where(m => m.tg.TemplateId == templates).ToList();

            //获取用户配送地址所在区域的id去查找匹配的运费模板
            var userAddressInfo = _regionsRepository.GetDbSetNoTracking().Where(m => m.RegionId == rId).FirstOrDefault();
            var firstOrDefault = new LF_ShippingRegionsGroups();
            // 先拿第三级
            firstOrDefault = shippingPriceInfo.Where(m => m.trg.RegionId == userAddressInfo.RegionId).FirstOrDefault()?.tg ?? null;
            if (firstOrDefault == null)
            {
                //拿第二级
                firstOrDefault = shippingPriceInfo.Where(m => m.trg.RegionId == userAddressInfo.CityId).FirstOrDefault()?.tg ?? null;
                if (firstOrDefault == null)
                {
                    //拿第一级
                    firstOrDefault = shippingPriceInfo.Where(m => m.trg.RegionId == userAddressInfo.ProvinceId).FirstOrDefault()?.tg ?? null;
                }
            }

            var type = kmShippingTemplates?.ValuationMethod;

            if (type == (int)ValuationMethod.Number)
            {
                #region 按件计

                if (firstOrDefault != null)
                {
                    if (number <= firstOrDefault.DefaultNumber)
                    {
                        freight = firstOrDefault.Price;
                    }
                    else
                    {
                        var num = number - firstOrDefault.DefaultNumber;
                        if (firstOrDefault.AddNumber != 0)
                        {
                            for (var i = 0; i < num;)
                            {
                                freight += firstOrDefault.AddPrice;

                                num = num - firstOrDefault.AddNumber;
                            }
                        }
                        freight += firstOrDefault.Price;
                    }
                }
                else
                {
                    if (number <= kmShippingTemplates.DefaultNumber)
                    {
                        freight = kmShippingTemplates.Price;
                    }
                    else
                    {
                        var num = number - kmShippingTemplates.DefaultNumber;
                        if (kmShippingTemplates.AddNumber != 0)
                        {
                            for (var i = 0; i < num;)
                            {
                                freight += kmShippingTemplates.AddPrice;

                                num = num - kmShippingTemplates.AddNumber;
                            }
                        }
                        freight += kmShippingTemplates.Price;
                    }
                }

                #endregion
            }
            else if (type == (int)ValuationMethod.Weight)
            {
                #region 按重量

                if (firstOrDefault != null)
                {
                    if (weight <= firstOrDefault.DefaultNumber)
                    {
                        freight = firstOrDefault.Price;
                    }
                    else
                    {
                        var num = weight - firstOrDefault.DefaultNumber;
                        if (firstOrDefault.AddNumber != 0)
                        {
                            for (var i = 0; i < num;)
                            {
                                freight += firstOrDefault.AddPrice;

                                num = num - firstOrDefault.AddNumber;
                            }
                        }

                        freight += firstOrDefault.Price;
                    }
                }
                else
                {
                    if (weight <= kmShippingTemplates.DefaultNumber)
                    {
                        freight = kmShippingTemplates.Price;
                    }
                    else
                    {
                        var num = weight - kmShippingTemplates.DefaultNumber;
                        if (kmShippingTemplates.AddNumber != 0)
                        {
                            for (var i = 0; i < num;)
                            {
                                freight += kmShippingTemplates.AddPrice;

                                num = num - kmShippingTemplates.AddNumber;
                            }
                        }

                        freight += kmShippingTemplates.Price;
                    }
                }

                #endregion
            }
            else
            {
                #region 按体积

                if (firstOrDefault != null)
                {
                    if (volume <= firstOrDefault.DefaultNumber)
                    {
                        freight = firstOrDefault.Price;
                    }
                    else
                    {
                        var num = volume - firstOrDefault.DefaultNumber;
                        if (firstOrDefault.AddNumber != 0)
                        {
                            for (var i = 0; i < num;)
                            {
                                freight += firstOrDefault.AddPrice;

                                num = num - firstOrDefault.AddNumber;
                            }
                        }

                        freight += firstOrDefault.Price;
                    }
                }
                else
                {
                    if (volume <= kmShippingTemplates.DefaultNumber)
                    {
                        freight = kmShippingTemplates.Price;
                    }
                    else
                    {
                        var num = volume - kmShippingTemplates.DefaultNumber;
                        if (kmShippingTemplates.AddNumber != 0)
                        {
                            for (var i = 0; i < num;)
                            {
                                freight += kmShippingTemplates.AddPrice;

                                num = num - kmShippingTemplates.AddNumber;
                            }
                        }
                        freight += kmShippingTemplates.Price;
                    }
                }

                #endregion
            }

            return freight;

        }

        /// <summary>
        /// 获得配送地区价格表
        /// </summary>
        /// <param name="id">运费模板id</param>
        /// <returns>配送地区价格表</returns>
        public List<ShippingRegionsGroups> GetRegionsGroupsList(int id)
        {
            return (from rg in _shippingRegionsGroupsRepository.GetDbSetNoTracking()
                    select new ShippingRegionsGroups
                    {
                        GroupId = rg.GroupId,
                        TemplateId = rg.TemplateId,
                        DefaultNumber = rg.DefaultNumber,
                        Price = rg.Price,
                        AddNumber = rg.AddNumber,
                        AddPrice = rg.AddPrice
                    }).Where(m => m.TemplateId == id)
                .OrderByDescending(m => m.GroupId)
                .ToList();
        }
    }
}
