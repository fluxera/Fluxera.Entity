namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	///  <summary>
	/// 	A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot, TKey}"/>
	/// 	has been added to the data storage.
	///  </summary>
	///  <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	///  <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public sealed class ItemAdded<TAggregateRoot, TKey> : IDomainEvent 
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		public ItemAdded(TAggregateRoot item)
		{
			this.Item = item;
		}

		public TAggregateRoot Item { get; }
	}
}
