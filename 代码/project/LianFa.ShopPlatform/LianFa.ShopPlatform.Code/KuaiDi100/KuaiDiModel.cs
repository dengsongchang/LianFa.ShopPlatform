using System.Collections.Generic;

namespace LianFa.ShopPlatform.Code.KuaiDi100
{
    /// <summary>
    /// 快递模型类
    /// </summary>
    public class KuaiDiModel
    {
        /// <summary>
        /// message
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string nu { get; set; }

        /// <summary>
        /// ischeck
        /// </summary>
        public int ischeck { get; set; }

        /// <summary>
        /// condition
        /// </summary>
        public string condition { get; set; }

        /// <summary>
        /// 快递公司代号
        /// </summary>
        public string com { get; set; }

        /// <summary>
        /// 查询结果状态： 0：物流单暂无结果， 200：查询成功， 2：接口出现异常，
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 快递单当前的状态 ：　 0：在途，即货物处于运输过程中；1：揽件，货物已由快递公司揽收并且产生了第一条跟踪信息；2：疑难，货物寄送过程出了问题；3：签收，收件人已签收；4：退签，即货物由于用户拒签、超区等原因退回，而且发件人已经签收；5：派件，即快递正在进行同城派件；6：退回，货物正处于退回发件人的途中；
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 快递主信息
        /// </summary>
        public List<KuaiDiInfoBody> data { get; set; }

    }

    /// <summary>
    /// 快递信息
    /// </summary>
    public class KuaiDiInfoBody
    {

        /// <summary>
        /// 信息时间
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// 信息时间
        /// </summary>
        public string ftime { get; set; }

        /// <summary>
        /// 信息内容
        /// </summary>
        public string context { get; set; }

    }
}
