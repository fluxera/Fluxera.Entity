namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	///  <summary>
	/// 	A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot, TKey}"/>
	/// 	has been removed to the data storage.
	///  </summary>
	///  <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	///  <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public sealed class ItemRemoved<TAggregateRoot, TKey> : IDomainEvent 
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		public ItemRemoved(TAggregateRoot item, string id)
		{
			this.Item = item;
			this.ID = id;
		}

		public TAggregateRoot Item { get; }

		public string ID { get; }
	}
}
