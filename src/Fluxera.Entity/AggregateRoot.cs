namespace Fluxera.Entity
{
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for all aggregate root entities. Uses <see cref="string"/> as type for the ID.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	[PublicAPI]
	public abstract class AggregateRoot<TAggregateRoot> : Entity<TAggregateRoot>
		where TAggregateRoot : AggregateRoot<TAggregateRoot>
	{
	}

	/// <summary>
	///     A base class for all aggregate root entities.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public abstract class AggregateRoot<TAggregateRoot, TKey> : Entity<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
	}
}
