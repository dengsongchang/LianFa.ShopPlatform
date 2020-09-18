using System;

namespace LianFa.ShopPlatform.Code.Data
{
    public static class DateTimeHelpers
    {
        /// <summary>
        /// 获取差值
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetBeforeDateTime(this DateTime time)
        {
            var now = DateTime.Now;
            TimeSpan ts1 = new TimeSpan(time.Ticks);

            TimeSpan ts2 = new TimeSpan(now.Ticks);

            TimeSpan ts = ts1.Subtract(ts2).Duration();

            if (Math.Floor(ts.TotalDays) > 365)
            {
                return Math.Floor(ts.TotalDays) / 365 + "年前";
            }
            else if (Math.Floor(ts.TotalDays) > 30)
            {
                return Math.Floor(ts.TotalDays) / 30 + "月前";
            }
            else if (Math.Floor(ts.TotalDays) > 1)
            {
                return Math.Floor(ts.TotalDays) + "天前";
            }
            else if (Math.Floor(ts.TotalHours) > 1)
            {
                return Math.Floor(ts.TotalHours) + "小时前";
            }
            else if (Math.Floor(ts.TotalMinutes) > 1)
            {
                return Math.Floor(ts.TotalMinutes) + "分钟前";
            }
            else
            {
                return Math.Floor(ts.TotalSeconds) + "秒前";
            }
        }
        /// <summary>  
        /// 当天的23点59分59秒  
        /// </summary>  
        /// <param name="datetime">要取得上个月最后一天的当前时间</param>  
        /// <returns></returns>  
        public static DateTime LastTimeOfDay(this DateTime datetime)
        {
            return DateTime.Parse(datetime.AddDays(1).ToShortDateString()).AddSeconds(-1);
        }
        /// <summary>  
        /// 当天的0点0分0秒  
        /// </summary>  
        /// <param name="datetime">要取得上个月最后一天的当前时间</param>  
        /// <returns></returns>  
        public static DateTime FistTimeOfDay(this DateTime datetime)
        {
            return DateTime.Parse(datetime.ToShortDateString());
        }
    }
}
