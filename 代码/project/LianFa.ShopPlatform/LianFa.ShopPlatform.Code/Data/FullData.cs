using System;
using System.Collections.Generic;
using System.Linq;

namespace LianFa.ShopPlatform.Code.Data
{
    /// <summary>
    /// 填充为空的区间数据
    /// </summary>
    public static class FullData
    {

        /// <summary>
        /// 填充为空的区间数据
        /// </summary>
        /// <returns></returns>
        public static Data FullDataValue(DateTime startTime, DateTime endTime, Data data)
        {
            if (data.DataList == null)
                return data;
            var list = data.DataList;
            data.DataList = new List<DataDetail>();
            for (var i = startTime; i <= endTime; i = i.AddDays(1))
            {
                if (list.Any(x => x.Date == i.ToString("yyyy-MM-dd")))
                {
                    var firstOrDefault = list.FirstOrDefault(x => x.Date == i.ToString("yyyy-MM-dd"));
                    if (firstOrDefault != null)
                        data.DataList.Add(new DataDetail
                        {
                            Date = i.ToString("yyyy-MM-dd"),
                            Value = firstOrDefault.Value
                        });
                    else
                        data.DataList.Add(new DataDetail
                        {
                            Date = i.ToString("yyyy-MM-dd"),
                            Value = 0
                        });
                }
                else
                {
                    data.DataList.Add(new DataDetail
                    {
                        Date = i.ToString("yyyy-MM-dd"),
                        Value = 0
                    });
                }
            }
            return data;
        }

        /// <summary>
        /// 填充为空的区间数据
        /// </summary>
        /// <returns></returns>
        public static Data FullDataValue(this Data data, DateTime startTime, DateTime endTime)
        {
            if (data.DataList == null)
                return data;
            var list = data.DataList;
            data.DataList = new List<DataDetail>();
            for (var i = startTime; i <= endTime; i = i.AddDays(1))
            {
                if (list.Any(x => x.Date == i.ToString("yyyy-MM-d")))
                {
                    var firstOrDefault = list.FirstOrDefault(x => x.Date == i.ToString("yyyy-MM-d"));
                    if (firstOrDefault != null)
                        data.DataList.Add(new DataDetail
                        {
                            Date = i.ToString("yyyy-MM-dd"),
                            Value = firstOrDefault.Value
                        });
                    else
                        data.DataList.Add(new DataDetail
                        {
                            Date = i.ToString("yyyy-MM-dd"),
                            Value = 0
                        });
                }
                else
                {
                    data.DataList.Add(new DataDetail
                    {
                        Date = i.ToString("yyyy-MM-dd"),
                        Value = 0
                    });
                }
            }
            return data;
        }

        /// <summary>
        /// 填充为空的区间数据
        /// </summary>
        /// <returns></returns>
        public static Data FullDataValueForMonth(this Data data, DateTime startTime, DateTime endTime)
        {
            if (data.DataList == null)
                return data;
            var list = data.DataList;
            data.DataList = new List<DataDetail>();
            for (var i = startTime; i <= endTime; i = i.AddMonths(1))
            {
                if (list.Any(x => x.Date == i.ToString("yyyy-MM")))
                {
                    var firstOrDefault = list.FirstOrDefault(x => x.Date == i.ToString("yyyy-MM"));
                    if (firstOrDefault != null)
                        data.DataList.Add(new DataDetail
                        {
                            Date = i.ToString("yyyy-MM"),
                            Value = firstOrDefault.Value
                        });
                    else
                        data.DataList.Add(new DataDetail
                        {
                            Date = i.ToString("yyyy-MM"),
                            Value = 0
                        });
                }
                else
                {
                    data.DataList.Add(new DataDetail
                    {
                        Date = i.ToString("yyyy-MM"),
                        Value = 0
                    });
                }
            }
            return data;
        }
    }
    /// <summary>
    /// 查询数据 请求类
    /// </summary>
    public class QueryDataRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
    /// <summary>
    /// 查询数据 响应类
    /// </summary>
    public class QueryDataResponse
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<Data> List { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 数据列名
        /// </summary>
        public string DataName { get; set; }

        /// <summary>
        /// 数据列
        /// </summary>
        public IList<DataDetail> DataList { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public decimal DataValue { get; set; }


    }
    /// <summary>
    /// 数据详情
    /// </summary>
    public class DataDetail
    {
        /// <summary>
        /// 值
        /// </summary>
        public Decimal Value { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Date { get; set; }
    }
}