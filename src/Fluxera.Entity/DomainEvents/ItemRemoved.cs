namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///     A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot, TKey}" />
	///     has been removed to the data storage.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public sealed class ItemRemoved<TAggregateRoot, TKey> : IDomainEvent
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ItemRemoved{TAggregateRoot,TKey}" /> type.
		/// </summary>
		/// <param name="item">The underlying item of this event.</param>
		/// <param name="id">The id of the underlying item of this event.</param>
		public ItemRemoved(TAggregateRoot item, TKey id)
		{
			this.Item = item;
			this.ID = id;
		}

		/// <summary>
		///     Gets the underlying item.
		/// </summary>
		public TAggregateRoot Item { get; }

		/// <summary>
		///     Gets the id of underlying item.
		/// </summary>
		public TKey ID { get; }
	}
}
