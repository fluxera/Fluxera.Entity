namespace Fluxera.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Fluxera.ComponentModel.Annotations;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;
#if NET6_0
	using Fluxera.Utilities.Extensions;
#endif

	/// <summary>
	///     A base class for aggregate roots.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public abstract class AggregateRoot<TAggregateRoot, TKey> : Entity<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		private readonly IList<IDomainEvent> domainEvents = new List<IDomainEvent>();

		/// <summary>
		///     The domain events of this entity.
		/// </summary>
		[Ignore]
		[IgnoreDataMember]
		public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

		/// <summary>
		///     Adds the given event to the list of raised domain events.
		/// </summary>
		/// <param name="domainEvent"></param>
		public void RaiseDomainEvent(IDomainEvent domainEvent)
		{
			this.domainEvents.Add(domainEvent);
		}

		/// <summary>
		///     Clears the list of raised domain events.
		/// </summary>
		public void ClearDomainEvents()
		{
			this.domainEvents.Clear();
		}
	}
}
