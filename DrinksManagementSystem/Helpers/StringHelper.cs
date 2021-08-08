using System;

namespace DrinksManagementSystem.Helpers
{
    public static class StringHelpers
    {
        public static string ToLowerCamelCase(this string value)
        {
            return value?.Length > 1 ? char.ToLowerInvariant(value[0]) + value.Substring(1) : value;
        }

        public static TEnum ToEnum<TEnum>(this string value, TEnum? fallbackValue = null) where TEnum : struct, Enum
        {
            // ReSharper disable once InvertIf
            if (string.IsNullOrEmpty(value))
            {
                if (fallbackValue != null)
                {
                    return (TEnum) fallbackValue;
                }

                return default(TEnum);
            }

            return Enum.TryParse<TEnum>(value, out var result) ? result : default(TEnum);
        }
    }
}