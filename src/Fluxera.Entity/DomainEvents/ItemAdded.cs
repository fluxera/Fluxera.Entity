namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///     A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot, TKey}" />
	///     has been added to the data storage.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public sealed class ItemAdded<TAggregateRoot, TKey> : IDomainEvent
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ItemAdded{TAggregateRoot,TKey}" /> type.
		/// </summary>
		/// <param name="item">The underlying item of this event.</param>
		public ItemAdded(TAggregateRoot item)
		{
			this.Item = item;
		}

		/// <summary>
		///     Gets the underlying item.
		/// </summary>
		public TAggregateRoot Item { get; }
	}
}
