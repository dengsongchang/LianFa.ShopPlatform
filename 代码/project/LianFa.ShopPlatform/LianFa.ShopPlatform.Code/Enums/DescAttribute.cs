using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuCheng.Util.Core.Enums;

namespace LianFa.ShopPlatform.Code.Enums
{
    /// <summary>
    /// 用于对Enum进行描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DescAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        public DescAttribute(string desc)
        {
            Desc = desc;
        }

        public static string GetDesc(Type enumType, int eunmValue)
        {
            var dict = ToDict(enumType);
            var intType = eunmValue;
            return !dict.ContainsKey(eunmValue) ? "" : dict[intType];
        }

        public static string GetDesc<T>(T t)
        {
            var enumType = t.GetType();
            var dict = ToDict(enumType);
            var enumName = Enum.Format(enumType, t, "G");
            var intType = (int)Enum.Parse(enumType, enumName);
            return !dict.ContainsKey(intType) ? enumName : dict[intType];
        }

        /// <summary>
        /// 将一个枚举类型转化了[值:描述]字典
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDict(Type enumType)
        {
            var result = new Dictionary<int, string>();
            if (!enumType.IsEnum)
                throw new InvalidOperationException("不允许对非枚举类型进行转化操作");

            var fieldInfos = enumType.GetFields();
            if (fieldInfos.Length == 0)
                return result;

            foreach (var info in fieldInfos)
            {
                if (!info.FieldType.IsEnum)
                    continue;
                var intValue = (int)Enum.Parse(enumType, info.Name);
                var descs = info.GetCustomAttributes(typeof(DescAttribute), true);
                if (descs.Length != 1)
                    throw new InvalidOperationException("不允许对多于或少于一个DescAttribute枚举进行转化");

                result.Add(intValue, ((DescAttribute)descs[0]).Desc);
            }

            return result;
        }

        public static IEnumerable<string> GetDescs(Type t, string[] values)
        {
            var descs = ToDict(t);
            var result = new List<string>();
            foreach (var val in values)
            {
                var intVal = 0;
                if (int.TryParse(val, out intVal))
                    result.Add(descs[intVal]);
            }
            return result;
        }

        public static string ToOptionListStr(Type enumType, int selectedValue)
        {
            var sb = new StringBuilder();
            var oplist = ToDict(enumType);
            foreach (var d in oplist)
            {
                if (d.Key == selectedValue)
                {
                    sb.Append("<option value=\"" + d.Key + "\" selected=\"true\">" + d.Value + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + d.Key + "\">" + d.Value + "</option>");
                }
            }
            return sb.ToString();
        }

        public static List<SelectListItem> GetSelectList(Type enumType, int selectedValue, bool add = true)
        {
            var list = new List<SelectListItem>();
            if (add)
            {
                list.Add(new SelectListItem()
                {
                    Text = "全部",
                    Value = "-1"
                });
            }
            var oplist = ToDict(enumType);
            list.AddRange(oplist.Select(d => new SelectListItem()
            {
                Text = d.Value,
                Value = d.Key.ToString(),
                Selected = d.Key == selectedValue
            }));

            return list;
        }
    }
}
