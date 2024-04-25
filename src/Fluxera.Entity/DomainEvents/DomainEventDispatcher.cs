namespace Fluxera.Entity.DomainEvents
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     A default implementation of the <see cref="IDomainEventDispatcher" /> contract that
	///     dispatches domains events in-memory using the <see cref="IPublisher" />.
	/// </summary>
	[PublicAPI]
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IPublisher publisher;

		/// <summary>
		///     Initializes a new instance of the <see cref="DomainEventDispatcher" /> type.
		/// </summary>
		/// <param name="publisher"></param>
		public DomainEventDispatcher(IPublisher publisher)
		{
			this.publisher = publisher;
		}

		/// <inheritdoc />
		public virtual async Task DispatchAsync(IDomainEvent domainEvent)
		{
			await this.publisher.Publish(domainEvent);
		}
	}
}
