namespace Fluxera.Entity.DomainEvents
{
	using System;
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
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ItemUpdated{TAggregateRoot,TKey}" /> type.
		/// </summary>
		/// <param name="beforeUpdateItem">The underlying item of this event.</param>
		/// <param name="afterUpdateItem">The underlying item of this event.</param>
		public ItemUpdated(TAggregateRoot beforeUpdateItem, TAggregateRoot afterUpdateItem)
		{
			this.BeforeUpdateItem = beforeUpdateItem;
			this.AfterUpdateItem = afterUpdateItem;
		}

		/// <summary>
		///     Gets the item before the update.
		/// </summary>
		public TAggregateRoot BeforeUpdateItem { get; }

		/// <summary>
		///     Gets the updated item.
		/// </summary>
		public TAggregateRoot AfterUpdateItem { get; }
	}
}
