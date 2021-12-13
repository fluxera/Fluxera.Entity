namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	///  <summary>
	/// 	A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot, TKey}"/>
	/// 	has been updated in the data storage.
	///  </summary>
	///  <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	///  <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public sealed class ItemUpdated<TAggregateRoot, TKey> : IDomainEvent 
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		public ItemUpdated(TAggregateRoot item)
		{
			this.Item = item;
		}

		public TAggregateRoot Item { get; }
	}

	///  <summary>
	/// 	A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot}"/>
	/// 	has been updated in the data storage. This event uses a <see cref="string"/> for the ID type.
	///  </summary>
	///  <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	[PublicAPI]
	public sealed class ItemUpdated<TAggregateRoot> : IDomainEvent
		where TAggregateRoot : AggregateRoot<TAggregateRoot>
	{
		public ItemUpdated(TAggregateRoot item)
		{
			this.Item = item;
		}

		public TAggregateRoot Item { get; }
	}
}
