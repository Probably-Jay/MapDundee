using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraUtility
{

    public static class LinqExtensions
    {
        public static string ToListString<T>(this IEnumerable<T> collection, Func<T, string> toStringFunction)
        {
            const string Seperator = ", ";

            var sb = new StringBuilder();

            foreach (var item in collection)
            {
                sb.Append(toStringFunction(item));
                sb.Append(Seperator);
            }

            return sb.Remove(sb.Length - Seperator.Length, Seperator.Length).ToString();
        }

        public static IEnumerable<T> TakeRandomSubset<T>(this IEnumerable<T> collection, int subsetSize)
            => subsetSize switch
            {
                0 => Enumerable.Empty<T>(),
                _ => collection.OrderBy(x => UnityEngine.Random.value).Take(subsetSize),
            };

        public static T TakeRandom<T>(this IEnumerable<T> collection) => collection.ElementAt(UnityEngine.Random.Range(0, collection.Count()));    


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || collection.IsEmpty();
        public static bool IsNullOrEmpty<T>(this IList<T> collection) => collection == null || collection.IsEmpty();

        public static bool IsEmpty<T>(this IEnumerable<T> collection) => collection.Count() == 0;
        public static bool IsEmpty<T>(this IList<T> collection) => collection.Count == 0;


        /// <returns>Equivalent to <see cref="Enumerable.ElementAt"/> mod the collection size.</returns>
        public static T ElementAtOrWrap<T>(this IEnumerable<T> collection, int index) => collection.ElementAt(index % collection.Count());
        /// <inheritdoc cref="ElementAtOrWrap{T}(IEnumerable{T}, int)"/>
        public static T ElementAtOrWrap<T>(this IList<T> collection, int index) => collection[index % collection.Count];


        //}  public static void ForEach<T>(this IList<T> collection, Action<T, int> action) 
        //        => collection
        //        .Select((T element, int index) => new { Element = element, Index = index })
        //        .ToList()
        //        .ForEach(obj => action(obj.Element, obj.Index));
        //}

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int i = 0;
            foreach (T item in collection)
            {
                action(item, i);
                i++;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }

        public static bool HasIndex<T>(this IList<T> collection, int index) => index < collection.Count;

    }

}