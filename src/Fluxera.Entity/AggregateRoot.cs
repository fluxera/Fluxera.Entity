namespace Fluxera.Entity
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for all aggregate root entities.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public abstract class AggregateRoot<TAggregateRoot, TKey> : Entity<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
	}
}
