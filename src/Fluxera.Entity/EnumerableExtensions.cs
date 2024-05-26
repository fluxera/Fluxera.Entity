#if NET6_0
namespace Fluxera.Entity
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;

	internal static class EnumerableExtensions
	{
		/// <summary>
		///     Converts the enumerable to a read-only collection.
		/// </summary>
		/// <typeparam name="T">The type of the elements.</typeparam>
		/// <param name="enumerable">The collection to write-protect.</param>
		/// <returns>A write-protected collection.</returns>
		public static IReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> enumerable)
		{
			enumerable = Guard.ThrowIfNull(enumerable);

			return new ReadOnlyCollection<T>(enumerable.ToList());
		}
	}
}
#endif
