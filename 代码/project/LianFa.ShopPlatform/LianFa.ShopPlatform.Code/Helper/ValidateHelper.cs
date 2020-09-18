using System.Text.RegularExpressions;

namespace LianFa.ShopPlatform.Code.Helper
{
    /// <summary>
    /// 验证帮助类
    /// </summary>
    public class ValidateHelper
    {
        //固话号正则表达式
        private static readonly Regex Phoneregex = new Regex(@"^(\d{3,4}-?)?\d{7,8}$");

        /// <summary>
        /// 是否为固话号
        /// </summary>
        public static bool IsPhone(string s)
        {
            return string.IsNullOrEmpty(s) || Phoneregex.IsMatch(s);
        }

        /// <summary>
        /// 手机有效性
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public static bool IsValidMobile(string mobile)
        {
            mobile = mobile.Trim();
            //大陆手机正则表达式
            Regex DaLuRegex = new Regex(@"^(13|15|17|18|19)\d{9}$", RegexOptions.None);
            //香港手机正则表达式
            Regex HongKongRegex = new Regex(@"^([5|6|8|9])\d{7}$", RegexOptions.None);
            //澳门手机正则表达式
            Regex AoMenRegex = new Regex(@"^[0][9]\d{8}$", RegexOptions.None);

            Match d = DaLuRegex.Match(mobile);
            Match h = HongKongRegex.Match(mobile);
            Match a = AoMenRegex.Match(mobile);
            if (d.Success || h.Success || a.Success)
            {
                return true;
            }
            return false;
        }


    }
}
