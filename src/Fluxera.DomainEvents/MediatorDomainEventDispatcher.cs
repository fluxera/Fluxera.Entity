namespace Fluxera.DomainEvents
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.DomainEvents.Abstractions;
	using global::Mediator;
	using JetBrains.Annotations;

	/// <summary>
	///     A default implementation of the <see cref="IDomainEventDispatcher" /> contract that
	///     dispatches domains events in-memory using the <see cref="IPublisher" />.
	/// </summary>
	[PublicAPI]
	public class MediatorDomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IPublisher publisher;

		/// <summary>
		///     Initializes a new instance of the <see cref="MediatorDomainEventDispatcher" /> type.
		/// </summary>
		/// <param name="publisher"></param>
		public MediatorDomainEventDispatcher(IPublisher publisher)
		{
			this.publisher = publisher;
		}

		/// <inheritdoc />
		public virtual async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
		{
			await this.publisher.Publish(domainEvent, cancellationToken);
		}
	}
}
