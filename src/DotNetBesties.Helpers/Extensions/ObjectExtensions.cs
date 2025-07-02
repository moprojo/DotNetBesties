using System;
using System.Text.Json;

namespace DotNetBesties.Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static T? As<T>(this object obj) where T : class
        {
            return obj as T;
        }

        public static T Cast<T>(this object obj) where T : struct
        {
            return (T)obj;
        }

        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException("Deserialization failed.");
        }

        public static bool IsPrimitive(this object obj)
        {
            return obj.GetType().IsPrimitive || obj is string || obj is decimal;
        }
    }
}