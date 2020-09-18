using System;

namespace LianFa.ShopPlatform.Model.Response.Client.Order
{
    /// <summary>
    /// 用户配送地址信息类
    /// </summary>
    public class ShipAddressInfo
    {
        private int _said;//配送地址id
        private int _uid;//用户id
        private int _regionid;//区域id
        private int _isdefault;//是否是默认地址
        private string _alias;//别名
        private string _consignee;//收货人
        private string _mobile;//收货人手机
        private string _phone;//收货人固定电话
        private string _email;//收货人邮箱
        private string _zipcode;//邮政编码
        private string _address;//地址

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SAId
        {
            get { return _said; }
            set { _said = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 是否是默认地址
        /// </summary>
        public int IsDefault
        {
            get { return _isdefault; }
            set { _isdefault = value; }
        }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get { return _consignee; }
            set { _consignee = value; }
        }
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// 收货人邮箱
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value.TrimEnd(); }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }


    /// <summary>
    /// 完整用户配送地址信息类
    /// </summary>
    public class FullShipAddressInfo : ShipAddressInfo
    {
        private int _provinceid;//省id
        private string _provincename;//省名称
        private int _cityid;//市id
        private string _cityname;//市名称
        private int _countyid;//县或区id
        private string _countyname;//县或区名称


        /// <summary>
        /// 省id
        /// </summary>
        public int ProvinceId
        {
            get { return _provinceid; }
            set { _provinceid = value; }
        }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName
        {
            get { return _provincename; }
            set { _provincename = value.TrimEnd(); }
        }
        /// <summary>
        /// 市id
        /// </summary>
        public int CityId
        {
            get { return _cityid; }
            set { _cityid = value; }
        }
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName
        {
            get { return _cityname; }
            set { _cityname = value.TrimEnd(); }
        }
        /// <summary>
        /// 县或区id
        /// </summary>
        public int CountyId
        {
            get { return _countyid; }
            set { _countyid = value; }
        }
        /// <summary>
        /// 县或区名称
        /// </summary>
        public string CountyName
        {
            get { return _countyname; }
            set { _countyname = value.TrimEnd(); }
        }
    }

    /// <summary>
    /// 配送区域信息类
    /// </summary>
    public class DeliverAreaInfo
    {
        private int _dareaid;//配送区域id
        private int _regionid;//区域id
        private string _address;//详细地址
        private int _provinceid;//省id
        private string _provincename;//省名称
        private int _cityid;//市id
        private string _cityname;//市名称
        private int _countyid;//县或区id
        private string _countyname;//县或区名称

        /// <summary>
        /// 配送区域id
        /// </summary>
        public int DAreaId
        {
            get { return _dareaid; }
            set { _dareaid = value; }
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 省id
        /// </summary>
        public int ProvinceId
        {
            get { return _provinceid; }
            set { _provinceid = value; }
        }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName
        {
            get { return _provincename; }
            set { _provincename = value.TrimEnd(); }
        }
        /// <summary>
        /// 市id
        /// </summary>
        public int CityId
        {
            get { return _cityid; }
            set { _cityid = value; }
        }
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName
        {
            get { return _cityname; }
            set { _cityname = value.TrimEnd(); }
        }
        /// <summary>
        /// 县或区id
        /// </summary>
        public int CountyId
        {
            get { return _countyid; }
            set { _countyid = value; }
        }
        /// <summary>
        /// 县或区名称
        /// </summary>
        public string CountyName
        {
            get { return _countyname; }
            set { _countyname = value.TrimEnd(); }
        }
    }

    /// <summary>
    /// 配送员信息类
    /// </summary>
    public class DeliverManInfo : DeliverAreaInfo
    {
        private string _avatar;//配送员头像
        private string _nickname;//配送员名称
        private string _realname;//配送员真实姓名
        private string _mobile;//配送员电话
        private int _diliveryid;//配送员Id
        //private int _dareaid;//配送员所属区域ID

        /// <summary>
        /// 配送员头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value.TrimEnd(); }
        }
        /// <summary>
        /// 配送员名称
        /// </summary>
        public string Nickname
        {
            get
            {
                return _nickname;
            }

            set
            {
                _nickname = value.TrimEnd();
            }
        }
        /// <summary>
        /// 配送员真实姓名
        /// </summary>
        public string Realname
        {
            get
            {
                return _realname;
            }

            set
            {
                _realname = value.TrimEnd();
            }
        }
        /// <summary>
        /// 配送员电话
        /// </summary>
        public string Mobile
        {
            get
            {
                return _mobile;
            }

            set
            {
                _mobile = value.TrimEnd();
            }
        }
        /// <summary>
        /// 配送员Id
        /// </summary>
        public int Diliveryid
        {
            get
            {
                return _diliveryid;
            }

            set
            {
                _diliveryid = value;
            }
        }
        /// <summary>
        /// 配送员所属区域ID
        /// </summary>
        //public int Dareaid
        //{
        //    get
        //    {
        //        return _dareaid;
        //    }

        //    set
        //    {
        //        _dareaid = value;
        //    }
        //}
    }



    /// <summary>
    /// 配送中心显示模型类
    /// </summary>
    public class DiliveryListModel
    {
        private int _diliveryid;//配送员Id
        private int _oid;//订单Id
        private int _orderstate;//订单状态
        private string _nickname;//买家名称
        private string _avatar;//买家头像
        private string _productname;//商品名称
        private decimal _orderamount;//订单总价
        private int _productcount;//订单商品总数
        private DateTime _shiptime;//分配配送员到该订单的时间


        /// <summary>
        /// 配送员Id
        /// </summary>
        public int Diliveryid
        {
            get { return _diliveryid; }
            set { _diliveryid = value; }
        }

        /// <summary>
        /// 订单Id
        /// </summary>
        public int Oid
        {
            get { return _oid; }
            set { _oid = value; }
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int Orderstate
        {
            get { return _orderstate; }
            set { _orderstate = value; }
        }

        /// <summary>
        /// 买家名称
        /// </summary>
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value.TrimEnd(); }
        }

        /// <summary>
        /// 买家头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value.TrimEnd(); }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Productname
        {
            get { return _productname; }
            set { _productname = value.TrimEnd(); }
        }

        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal Orderamount
        {
            get { return _orderamount; }
            set { _orderamount = value; }
        }

        /// <summary>
        /// 商品总数
        /// </summary>
        public int Productcount
        {
            get { return _productcount; }
            set { _productcount = value; }
        }

        /// <summary>
        /// 分配配送员到该订单的时间
        /// </summary>
        public DateTime Shiptime
        {
            get { return _shiptime; }
            set { _shiptime = value; }
        }

    }


    /// <summary>
    /// 用户默认收货地址
    /// </summary>
    public class UserDefaultAddresses
    {
        /// <summary>
        /// 收货地址id
        /// </summary>
        public int SAId { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话 
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Addresses { get; set; }

        /// <summary>
        /// 栋
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// 室
        /// </summary>
        public string Room { get; set; }
    }
}
