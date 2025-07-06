using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extentions
{
    public static class EnumGetDescription
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);
                foreach (int value in values)
                {
                    if (value == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memoryInfo = type.GetMember(type.GetEnumName(value));
                        var descriptionAttr = memoryInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                        if (descriptionAttr != null)
                        {
                            return descriptionAttr.Description;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string GetDescriptionDynamic<T>(this T e, params object[] args) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);
                foreach (int value in values)
                {
                    if (value == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memoryInfo = type.GetMember(type.GetEnumName(value));
                        var descriptionAttr = memoryInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                        if (descriptionAttr != null)
                        {
                            if (args.Length > 0)
                            {
                                return string.Format(descriptionAttr.Description,args);
                            }
                            return descriptionAttr.Description;
                        }
                    }
                }
            }
            return e.ToString();
        }
    }
}
