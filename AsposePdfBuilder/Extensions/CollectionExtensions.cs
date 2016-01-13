using System.Collections.Generic;
using System.Linq;

namespace AsposePdfBuilder.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Tests whether the IEnumerable is null or empty, assumes that this won't result in multiple enumerations of the IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="enumerable">IEnumerable to test</param>
        /// <returns>True if null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Tests whether the IEnumerable contains elements doesn't worry if the IEnumerable is null, assumes that this won't result in multiple enumerations of the IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="enumerable">IEnumerable to test</param>
        /// <returns>True if not empty, false otherwise</returns>
        public static bool IsAny<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        /// Ensures that the Enumerable is always at least an empty list
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="enumerable">IEnumerable to 'make safe'</param>
        /// <returns>An Enumerable.Empty of type T if enumerable is null, otherwise enumerable itself</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }
    }
}
