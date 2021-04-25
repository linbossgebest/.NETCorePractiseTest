using System;
using System.Collections.Generic;
using System.Text;

namespace Sample03.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 判断是否为可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullType(this Type type)
        {
            if (type.IsGenericType)
            {
                var definition = type.GetGenericTypeDefinition();

                if (definition != null && definition == typeof(Nullable<>))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
