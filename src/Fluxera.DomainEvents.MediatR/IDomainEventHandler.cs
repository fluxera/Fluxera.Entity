namespace Fluxera.DomainEvents.MediatR
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.DomainEvents.Abstractions;
	using global::MediatR;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for implementing domain event handlers, which must be registered
	///     using dependency injection. Handlers of this type are executed before
	///     committing changes to a storage.
	/// </summary>
	/// <typeparam name="TDomainEvent">The domain event type.</typeparam>
	[PublicAPI]
	public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
		where TDomainEvent : class, IDomainEvent
	{
		/// <summary>
		///     Handles the given domain event asynchronously.
		/// </summary>
		/// <param name="domainEvent">The domain event to handle.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

		/// <inheritdoc />
		Task INotificationHandler<TDomainEvent>.Handle(TDomainEvent notification, CancellationToken cancellationToken)
		{
			return this.HandleAsync(notification, cancellationToken);
		}
	}
}
