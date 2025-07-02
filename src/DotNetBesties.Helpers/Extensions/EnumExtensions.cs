using System;

namespace DotNetBesties.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsDefined<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        public static string ToDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            var fieldInfo = typeof(TEnum).GetField(value.ToString());
            if(fieldInfo == null) return value.ToString();

            var attributes = fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if(attributes.Length > 0)
            {
                return ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description;
            }

            return value.ToString();
        }

        public static string GetEnumName<TEnum>(TEnum value) where TEnum : Enum
        {
            return Enum.GetName(typeof(TEnum), value);
        }
    }
}