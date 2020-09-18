using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HuCheng.Util.Core.Datas.Repositories;

namespace LianFa.ShopPlatform.Model.Request.Admin.Orders
{
    /// <summary>
    /// 后台订单列表请求类
    /// </summary>
    public class AdminOrdertListRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string oSn { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 区Id
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string consigNee { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int orderState { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 销售门店编号
        /// </summary>
        public string SellStoreSn { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel page { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public int orderSoirce { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UId { get; set; }
    }

    /// <summary>
    /// 后台订单信息请求类
    /// </summary>
    public class AdminOrderInfoRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "订单ID大小不能小于1")]
        public int oid { get; set; }
    }
    /// <summary>
    /// 订单物流请类
    /// </summary>
    public class AdminOrderLogisticsRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int Oid { get; set; }

        /// <summary>
        /// 商品订单物流id
        /// </summary>
        public int OLId { get; set; }
    }
    /// <summary>
    /// 后台操作订单请求类
    /// </summary>
    public class AdminOperateOrderActionRequest
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int oid { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int actionType { get; set; }

        /// <summary>
        /// 行动
        /// </summary>
        public string actionDes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShipCom { get; set; }
    }

    /// <summary>
    /// 删除订单请求类
    /// </summary>
    public class AdminDelOrdesRequest
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public List<int> oid { get; set; }
    }

    /// <summary>
    /// 导出订单请求类
    /// </summary>
    public class AdminExportAdminOrdersListRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string oSn { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 区Id
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 销售门店编号
        /// </summary>
        public string SellStoreSn { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string consigNee { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int orderState { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string productsName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public int orderSoirce { get; set; }
    }

    /// <summary>
    /// 后台导入快递格式信息
    /// </summary>
    public class AdminUploadShipSnInfo
    {
        /// <summary>  
        /// 订单编号  
        /// </summary>
        public string 订单编号 { get; set; }

        /// <summary>  
        /// 快递公司  
        /// </summary>
        public string 快递公司 { get; set; }

        /// <summary>  
        /// 快递单号  
        /// </summary>
        public string 快递单号 { get; set; }
    }
}
