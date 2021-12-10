﻿namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///		A domain event that indicates that a new item of <see cref="AggregateRoot{TAggregateRoot}"/>
	///		has been updated in the data storage.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
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