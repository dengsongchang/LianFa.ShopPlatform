using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianFa.ShopPlatform.Code.Deppon.Models.Response
{
    /// <summary>
    /// 标准订单查询响应类
    /// </summary>
    public class QueryOrderResponse
    {
        public string backSignBill { get; set; }
        public string businessNetworkNo { get; set; }
        public string cargoName { get; set; }
        public int codPrice { get; set; }
        public string codType { get; set; }
        public int codValue { get; set; }
        public string deliveryPrice { get; set; }
        public string deliveryType { get; set; }
        public int insurancePrice { get; set; }
        public int insuranceValue { get; set; }
        public string logisticCompanyID { get; set; }
        public string logisticID { get; set; }
        public string mailNo { get; set; }
        public string payType { get; set; }
        public Receiver receiver { get; set; }
        public Sender sender { get; set; }
        public string smsNotify { get; set; }
        public int smsNotifyPrice { get; set; }
        public string toNetworkNo { get; set; }
        public int totalNumber { get; set; }
        public int totalPrice { get; set; }
        public int totalVolume { get; set; }
        public int totalWeight { get; set; }
        public string vistReceive { get; set; }
        public int vistReceivePrice { get; set; }
    }

    public class Receiver
    {
        public string address { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string province { get; set; }
    }

    public class Sender
    {
        public string address { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }

}
