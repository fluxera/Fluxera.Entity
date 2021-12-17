namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///     A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot, TKey}" />
	///     has been updated in the data storage.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public sealed class ItemUpdated<TAggregateRoot, TKey> : IDomainEvent
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ItemUpdated{TAggregateRoot,TKey}" /> type.
		/// </summary>
		/// <param name="item">The underlying item of this event.</param>
		public ItemUpdated(TAggregateRoot item)
		{
			this.Item = item;
		}

		/// <summary>
		///     Gets the underlying item.
		/// </summary>
		public TAggregateRoot Item { get; }
	}
}
