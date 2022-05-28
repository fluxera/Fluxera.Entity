namespace Fluxera.Entity
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for strongly types IDs.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public interface IStronglyTypedId<T, out TKey> : IComparable<T>, IEquatable<T>
	{
		/// <summary>
		///     Gets the underlying value of the strongly typed ID.
		/// </summary>
		public TKey Value { get; }
	}
}
