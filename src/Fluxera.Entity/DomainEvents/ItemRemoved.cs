namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///		A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot}"/>
	///		has been removed to the data storage.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	[PublicAPI]
	public sealed class ItemRemoved<TAggregateRoot> : IDomainEvent 
		where TAggregateRoot : AggregateRoot<TAggregateRoot>
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
