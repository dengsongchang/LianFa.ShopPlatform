using System;
using System.Collections.Generic;

namespace LianFa.ShopPlatform.Model.Response.Admin.AfterSalesService
{
    /// <summary>
    /// 退货列表响应类
    /// </summary>
    public class AdminAfterSalesServiceListResponse
    {
        /// <summary>
        /// 退货列表
        /// </summary>
        public List<ReturnApplyListingList> ReturnApplyListingList { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 退货列表
    /// </summary>
    public class ReturnApplyListingList
    {
        /// <summary>
        /// 售后id
        /// </summary>
        public int AsSid { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public int Oid { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string Osn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 退款数量
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int AsType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int AsState { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        public string AsTypeDec { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string AsStateDec { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyTimec { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
    }

    /// <summary>
    ///售后服务受影响行响应类
    /// </summary>
    public class AdminAfterSalesServiceRowResponse
    {
        /// <summary>
        /// 受影响行数
        /// </summary>
        public int Row { get; set; }
    }

    /// <summary>
    /// 获取发货订单基本信息响应
    /// </summary>
    public class AdminGetDeliveryListResponse
    {
        /// <summary>
        /// 获取发货订单基本信息
        /// </summary>
        public List<DeliveryList> DeliveryList { get; set; }

        /// <summary>
        /// 物流公司信息
        /// </summary>
        public List<ShipCompaniesList> ShipCompaniesList { get; set; }
    }

    /// <summary>
    /// 配送公司列表 
    /// </summary>
    public class ShipCompaniesList
    {
        /// <summary>  
    	/// 配送公司id  
    	/// </summary>
        public int ShipCoId { get; set; }

        /// <summary>  
        /// 配送公司名称  
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 获取发货订单基本信息类
    /// </summary>
    public class DeliveryList
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int oid { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string buyerRemark { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<ProductList> productList { get; set; }
    }

    /// <summary>
    /// 订单商品列表类
    /// </summary>
    public class ProductList
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商城价格
        /// </summary>
        public decimal ShopPrice { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShowImg { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 真实购买数量
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal Subtotal { get; set; }
    }
}
