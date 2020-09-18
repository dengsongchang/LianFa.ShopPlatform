using System.Collections.Generic;
using LianFa.ShopPlatform.DataBase;

namespace LianFa.ShopPlatform.Model.Response.Client.Cart
{
    /// <summary>
    /// 购物车 响应类
    /// </summary>
    public class CartResponse
    {
        /// <summary>
        /// 商品总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 满减
        /// </summary>
        public int FullCut { get; set; }
        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 购物车信息
        /// </summary>
        public CartInfo CartInfo { get; set; }
    }
    /// <summary>
    /// 购物车信息类
    /// </summary>
    public class CartInfo
    {
        private bool _isselected = true;//是否选中
        private List<CartProductInfo> _cartproductlist = new List<CartProductInfo>();//购物车商品列表
        private List<CartSuitInfo> _cartsuitlist = new List<CartSuitInfo>();//购物车套装列表
        private List<CartFullSendInfo> _cartfullsendlist = new List<CartFullSendInfo>();//购物车满赠列表
        private List<CartFullCutInfo> _cartfullcutlist = new List<CartFullCutInfo>();//购物车满减列表
        private List<LF_OrderProducts> _selectedorderproductlist = new List<LF_OrderProducts>();//选中的订单商品列表
        private List<LF_OrderProducts> _remainedorderproductlist = new List<LF_OrderProducts>();//剩余的订单商品列表

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isselected; }
            set { _isselected = value; }
        }
        /// <summary>
        /// 购物车商品列表
        /// </summary>
        public List<CartProductInfo> CartProductList
        {
            get { return _cartproductlist; }
            set { _cartproductlist = value; }
        }
        /// <summary>
        /// 购物车套装列表
        /// </summary>
        public List<CartSuitInfo> CartSuitList
        {
            get { return _cartsuitlist; }
            set { _cartsuitlist = value; }
        }
        /// <summary>
        /// 购物车满赠列表
        /// </summary>
        public List<CartFullSendInfo> CartFullSendList
        {
            get { return _cartfullsendlist; }
            set { _cartfullsendlist = value; }
        }
        /// <summary>
        /// 购物车满减列表
        /// </summary>
        public List<CartFullCutInfo> CartFullCutList
        {
            get { return _cartfullcutlist; }
            set { _cartfullcutlist = value; }
        }
        /// <summary>
        /// 选中的订单商品列表
        /// </summary>
        public List<LF_OrderProducts> SelectedOrderProductList
        {
            get { return _selectedorderproductlist; }
            set { _selectedorderproductlist = value; }
        }
        /// <summary>
        /// 剩余的订单商品列表
        /// </summary>
        public List<LF_OrderProducts> RemainedOrderProductList
        {
            get { return _remainedorderproductlist; }
            set { _remainedorderproductlist = value; }
        }
    }

    /// <summary>
    /// 购物车商品信息类
    /// </summary>
    public class CartProductInfo
    {
        private bool _isselected = true;//是否选中
        private int _isCostPrice;//是否特价
        private LF_OrderProducts _orderproductinfo;//商品信息
        private List<LF_OrderProducts> _giftlist = null;//赠品列表
        private bool _failure = false;//是否失效

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isselected; }
            set { _isselected = value; }
        }
        /// <summary>
        /// 是否特价
        /// </summary>
        public int IsCostPrice
        {
            get { return _isCostPrice; }
            set { _isCostPrice = value; }
        }
        /// <summary>
        /// 商品信息
        /// </summary>
        public LF_OrderProducts OrderProductInfo
        {
            get { return _orderproductinfo; }
            set { _orderproductinfo = value; }
        }
        /// <summary>
        /// 赠品列表
        /// </summary>
        public List<LF_OrderProducts> GiftList
        {
            get { return _giftlist; }
            set { _giftlist = value; }
        }
        /// <summary>
        /// 是否失效
        /// </summary>
        public bool Failure
        {
            get { return _failure; }
            set { _failure = value; }
        }
    }

    /// <summary>
    /// 购物车套装信息类
    /// </summary>
    public class CartSuitInfo
    {
        private bool _isselected = true;//是否选中
        private int _pmid;//套装促销活动id
        private int _buycount;//购买数量
        private decimal _suitprice;//套装价格
        private decimal _suitamount;//套装合计
        private decimal _productamount;//商品合计
        private decimal _discount;//折扣
        private List<CartProductInfo> _cartproductlist;//购物车商品列表

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isselected; }
            set { _isselected = value; }
        }
        /// <summary>
        /// 套装促销活动id
        /// </summary>
        public int PmId
        {
            get { return _pmid; }
            set { _pmid = value; }
        }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyCount
        {
            get { return _buycount; }
            set { _buycount = value; }
        }
        /// <summary>
        /// 套装价格
        /// </summary>
        public decimal SuitPrice
        {
            get { return _suitprice; }
            set { _suitprice = value; }
        }
        /// <summary>
        /// 套装合计
        /// </summary>
        public decimal SuitAmount
        {
            get { return _suitamount; }
            set { _suitamount = value; }
        }
        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount
        {
            get { return _productamount; }
            set { _productamount = value; }
        }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        /// <summary>
        /// 购物车商品列表
        /// </summary>
        public List<CartProductInfo> CartProductList
        {
            get { return _cartproductlist; }
            set { _cartproductlist = value; }
        }
    }

    /// <summary>
    /// 购物车满赠信息类
    /// </summary>
    public class CartFullSendInfo
    {
        private bool _isenough = false;//是否达到满赠促销活动的金额
        private LF_OrderProducts _fullsendminororderproductinfo = null;//满赠赠品
        private List<CartProductInfo> _fullsendmaincartproductlist;//满赠主商品列表
        private decimal _mainproductamount;//主商品合计

        /// <summary>
        /// 是否达到满赠促销活动的金额
        /// </summary>
        public bool IsEnough
        {
            get { return _isenough; }
            set { _isenough = value; }
        }

        /// <summary>
        /// 满赠赠品
        /// </summary>
        public LF_OrderProducts FullSendMinorOrderProductInfo
        {
            get { return _fullsendminororderproductinfo; }
            set { _fullsendminororderproductinfo = value; }
        }
        /// <summary>
        /// 满赠主商品列表
        /// </summary>
        public List<CartProductInfo> FullSendMainCartProductList
        {
            get { return _fullsendmaincartproductlist; }
            set { _fullsendmaincartproductlist = value; }
        }
        /// <summary>
        /// 主商品合计
        /// </summary>
        public decimal MainProductAmount
        {
            get { return _mainproductamount; }
            set { _mainproductamount = value; }
        }
    }

    /// <summary>
    /// 购物车满减信息类
    /// </summary>
    public class CartFullCutInfo
    {
        private bool _isenough = false;//是否达到满减促销活动的金额
        private int _limitmoney = 0;//限制金额
        private int _cutmoney = 0;//减小金额
        private List<CartProductInfo> _fullcutcartproductlist;//满减商品列表
        private decimal _productamount;//商品合计

        /// <summary>
        /// 是否达到满减促销活动的金额
        /// </summary>
        public bool IsEnough
        {
            get { return _isenough; }
            set { _isenough = value; }
        }
        /// <summary>
        /// 限制金额
        /// </summary>
        public int LimitMoney
        {
            get { return _limitmoney; }
            set { _limitmoney = value; }
        }
        /// <summary>
        /// 减小金额
        /// </summary>
        public int CutMoney
        {
            get { return _cutmoney; }
            set { _cutmoney = value; }
        }
        /// <summary>
        /// 满减商品列表
        /// </summary>
        public List<CartProductInfo> FullCutCartProductList
        {
            get { return _fullcutcartproductlist; }
            set { _fullcutcartproductlist = value; }
        }
        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount
        {
            get { return _productamount; }
            set { _productamount = value; }
        }

    }

    /// <summary>
    /// 购物车门店信息类
    /// </summary>
    public class NewCartInfo
    {
        /// <summary>
        /// 购物车门店列表
        /// </summary>
        public List<CartStoreInfo> CartStoreList { get; set; }

    }

    /// <summary>
    /// 购物车门店信息类
    /// </summary>
    public class CartStoreInfo
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int SId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string SName { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 购物车门店商品信息列表
        /// </summary>
        public List<CartStoreProductInfo> CartStoreProductList { get; set; }

        /// <summary>
        /// 选中的订单商品列表
        /// </summary>
        public List<LF_OrderProducts> SelectedOrderProductList { get; set; }
        /// <summary>
        /// 剩余的订单商品列表
        /// </summary>
        public List<LF_OrderProducts> RemainedOrderProductList { get; set; }
    }

    /// <summary>
    /// 购物车门店商品信息类
    /// </summary>
    public class CartStoreProductInfo
    {
        private bool _isselected = true;//是否选中
        private LF_OrderProducts _orderproductinfo;//商品信息
        private List<LF_OrderProducts> _giftlist = null;//赠品列表
        private bool _failure = false;//是否失效

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isselected; }
            set { _isselected = value; }
        }
        /// <summary>
        /// 商品信息
        /// </summary>
        public LF_OrderProducts OrderProductInfo
        {
            get { return _orderproductinfo; }
            set { _orderproductinfo = value; }
        }
        /// <summary>
        /// 赠品列表
        /// </summary>
        public List<LF_OrderProducts> GiftList
        {
            get { return _giftlist; }
            set { _giftlist = value; }
        }
        /// <summary>
        /// 是否失效
        /// </summary>
        public bool Failure
        {
            get { return _failure; }
            set { _failure = value; }
        }
    }

}
