using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraUtility
{
    public static class EnumUtility 
    {
        /// <summary>
        /// Get an enumerable list of all enum values of a particular type.
        /// </summary>
        public static IEnumerable<T> GetEnumValues<T>() where T : Enum => (T[])Enum.GetValues(typeof(T));
        public static T[] GetEnumValuesArray<T>() where T : Enum => (T[])Enum.GetValues(typeof(T));

        public static TTo ConvertToEnumOnName<TFrom, TTo>(this TFrom enumVal) where TFrom : Enum where TTo : Enum
            => ConvertEnumOnName<TFrom, TTo>(enumVal);          

        public static TTo ConvertEnumOnName<TFrom, TTo> (TFrom enumVal) where TFrom : Enum where TTo : Enum 
            => GetEnumValues<TTo>().First(e => e.ToString() == enumVal.ToString());

        /// <summary>
        /// Returns the next enum value. If not possible, then returns the first value.
        /// </summary>
        public static T GetNextValue<T>(this T enumVal) where T : Enum
        {
            var values = GetEnumValuesArray<T>();
            var index = Array.IndexOf(values, enumVal) +1;
            return (values.Length == index) ? values[0] : values[index];
        }
    }
    
}