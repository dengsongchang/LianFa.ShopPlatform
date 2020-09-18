using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;

namespace LianFa.ShopPlatform.Code.KuaiDi100
{
    /// <summary>
    /// 快递查询
    /// </summary>
    public class KuaiDiQuery
    {
        /// <summary>
        /// 根据快递名称获取快递100接口名称
        /// </summary>
        /// <param name="typeCom">物流公司</param>
        /// <returns></returns>
        public static string GetKuaidi100(string typeCom)
        {
            switch (typeCom)
            {
                case "AAE全球专递":
                    typeCom = "aae";
                    break;
                case "安捷快递":
                    typeCom = "anjiekuaidi";
                    break;
                case "安信达快递":
                    typeCom = "anxindakuaixi";
                    break;
                case "百福东方":
                    typeCom = "baifudongfang";
                    break;
                case "彪记快递":
                    typeCom = "biaojikuaidi";
                    break;
                case "BHT":
                    typeCom = "bht";
                    break;
                case "希伊艾斯快递":
                    typeCom = "cces";
                    break;
                case "中国东方":
                    typeCom = "Coe";
                    break;
                case "长宇物流":
                    typeCom = "changyuwuliu";
                    break;
                case "大田物流":
                    typeCom = "datianwuliu";
                    break;
                case "德邦物流":
                    typeCom = "debangwuliu";
                    break;
                case "DPEX":
                    typeCom = "dpex";
                    break;
                case "DHL":
                    typeCom = "dhl";
                    break;
                case "D速快递":
                    typeCom = "dsukuaidi";
                    break;
                case "fedex":
                    typeCom = "fedex";
                    break;
                case "飞康达物流":
                    typeCom = "feikangda";
                    break;
                case "凤凰快递":
                    typeCom = "fenghuangkuaidi";
                    break;
                case "港中能达物流":
                    typeCom = "ganzhongnengda";
                    break;
                case "广东邮政物流":
                    typeCom = "guangdongyouzhengwuliu";
                    break;
                case "汇通快运":
                    typeCom = "huitongkuaidi";
                    break;
                case "恒路物流":
                    typeCom = "hengluwuliu";
                    break;
                case "华夏龙物流":
                    typeCom = "huaxialongwuliu";
                    break;
                case "佳怡物流":
                    typeCom = "jiayiwuliu";
                    break;
                case "京广速递":
                    typeCom = "jinguangsudikuaijian";
                    break;
                case "急先达":
                    typeCom = "jixianda";
                    break;
                case "佳吉物流":
                    typeCom = "jiajiwuliu";
                    break;
                case "加运美":
                    typeCom = "jiayunmeiwuliu";
                    break;
                case "快捷速递":
                    typeCom = "kuaijiesudi";
                    break;
                case "联昊通物流":
                    typeCom = "lianhaowuliu";
                    break;
                case "龙邦物流":
                    typeCom = "longbanwuliu";
                    break;
                case "民航快递":
                    typeCom = "minghangkuaidi";
                    break;
                case "配思货运":
                    typeCom = "peisihuoyunkuaidi";
                    break;
                case "全晨快递":
                    typeCom = "quanchenkuaidi";
                    break;
                case "全际通物流":
                    typeCom = "quanjitong";
                    break;
                case "全日通快递":
                    typeCom = "quanritongkuaidi";
                    break;
                case "全一快递":
                    typeCom = "quanyikuaidi";
                    break;
                case "盛辉物流":
                    typeCom = "shenghuiwuliu";
                    break;
                case "速尔物流":
                    typeCom = "suer";
                    break;
                case "盛丰物流":
                    typeCom = "shengfengwuliu";
                    break;
                case "天地华宇":
                    typeCom = "tiandihuayu";
                    break;
                case "天天快递":
                    typeCom = "tiantian";
                    break;
                case "TNT":
                    typeCom = "tnt";
                    break;
                case "UPS":
                    typeCom = "ups";
                    break;
                case "万家物流":
                    typeCom = "wanjiawuliu";
                    break;
                case "文捷航空速递":
                    typeCom = "wenjiesudi";
                    break;
                case "伍圆速递":
                    typeCom = "wuyuansudi";
                    break;
                case "万象物流":
                    typeCom = "wanxiangwuliu";
                    break;
                case "新邦物流":
                    typeCom = "xinbangwuliu";
                    break;
                case "信丰物流":
                    typeCom = "xinfengwuliu";
                    break;
                case "星晨急便":
                    typeCom = "xingchengjibian";
                    break;
                case "鑫飞鸿物流":
                    typeCom = "xinhongyukuaidi";
                    break;
                case "亚风速递":
                    typeCom = "yafengsudi";
                    break;
                case "一邦速递":
                    typeCom = "yibangwuliu";
                    break;
                case "优速物流":
                    typeCom = "youshuwuliu";
                    break;
                case "远成物流":
                    typeCom = "yuanchengwuliu";
                    break;
                case "圆通速递":
                    typeCom = "yuantong";
                    break;
                case "源伟丰快递":
                    typeCom = "yuanweifeng";
                    break;
                case "元智捷诚快递":
                    typeCom = "yuanzhijiecheng";
                    break;
                case "越丰物流":
                    typeCom = "yuefengwuliu";
                    break;
                case "韵达快递":
                    typeCom = "yunda";
                    break;
                case "源安达":
                    typeCom = "yuananda";
                    break;
                case "运通快递":
                    typeCom = "yuntongkuaidi";
                    break;
                case "宅急送":
                    typeCom = "zhaijisong";
                    break;
                case "中铁快运":
                    typeCom = "zhongtiewuliu";
                    break;
                case "中通速递":
                    typeCom = "zhongtong";
                    break;
                case "中邮物流":
                    typeCom = "zhongyouwuliu";
                    break;
                case "顺丰":
                    typeCom = "	shunfengen";
                    break;
                case "顺丰速递":
                    typeCom = "shunfeng";
                    break;
                case "国通快递":
                    typeCom = "guotongkuaidi";
                    break;
            }

            return typeCom;
        }

        /// <summary>
        /// 根据物流单号查询物流信息状态
        /// </summary>
        /// <param name="typeCom">物流公司</param>
        /// <param name="epxno">快递单号</param>
        /// <returns></returns>
        public static KuaiDiModel KuaiDiQueryByNo(string typeCom, string epxno)
        {
            //根据快递名称获取快递100接口名称
            typeCom = GetKuaidi100(typeCom);

            var url = "http://poll.kuaidi100.com/poll/query.do?";

            //参数
            var param = "{\"com\":\"" + typeCom + "\",\"num\":\"" + epxno + "\",\"from\":\"\",\"to\":\"\"}";

            //TODO:暂先写在代码中,后期移到配置中
            const string customer = "6B80341D9A3CE05C909549BE3149E22E";
            const string key = "zmVPQPjV2871";

            var md5 = new MD5CryptoServiceProvider();
            byte[] inBytes = Encoding.GetEncoding("UTF-8").GetBytes(param + key + customer);
            byte[] outBytes = md5.ComputeHash(inBytes);
            var outString = outBytes.Aggregate("", (current, t) => current + t.ToString("x2"));

            string sign = outString.ToUpper();
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("param", param);
            parameters.Add("customer", customer);
            parameters.Add("sign", sign);

            var buffer = new StringBuilder();

            //如果需要POST数据     
            if (parameters.Count != 0)
            {
                var i = 0;
                foreach (var pKey in parameters.Keys)
                {
                    buffer.AppendFormat(i > 0 ? "&{0}={1}" : "{0}={1}", pKey, parameters[pKey]);
                    i++;
                }
            }

            url += buffer.ToString();
            var result = WebHelper.GetJsonRequestData(url, "get", null);
            return result.ToObject<KuaiDiModel>();
        }
    }
}
