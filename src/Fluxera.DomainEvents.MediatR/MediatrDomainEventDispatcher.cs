namespace Fluxera.DomainEvents.MediatR
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.DomainEvents.Abstractions;
	using global::MediatR;
	using JetBrains.Annotations;

	/// <summary>
	///     A default implementation of the <see cref="IDomainEventDispatcher" /> contract that
	///     dispatches domains events in-memory using the <see cref="IPublisher" />.
	/// </summary>
	[PublicAPI]
	public class MediatrDomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IPublisher publisher;

		/// <summary>
		///     Initializes a new instance of the <see cref="MediatrDomainEventDispatcher" /> type.
		/// </summary>
		/// <param name="publisher"></param>
		public MediatrDomainEventDispatcher(IPublisher publisher)
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
