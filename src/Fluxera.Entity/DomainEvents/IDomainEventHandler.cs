namespace Fluxera.Entity.DomainEvents
{
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MediatR;

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
		Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);

		/// <inheritdoc />
		Task INotificationHandler<TDomainEvent>.Handle(TDomainEvent notification, CancellationToken cancellationToken)
		{
			return this.HandleAsync(notification, cancellationToken);
		}
	}

	/// <summary>
	///		A wrapper class for a synchronous domain event handler.
	/// </summary>
	/// <typeparam name="TDomainEvent">The domain event type</typeparam>
	[PublicAPI]
	public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
		where TDomainEvent : class, IDomainEvent
	{
		/// <inheritdoc />
		Task IDomainEventHandler<TDomainEvent>.HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			Handle(domainEvent);

			return Task.CompletedTask;
		}

		/// <summary>
		///		Override in a derived class for the handler logic
		/// </summary>
		/// <param name="domainEvent">The domain event to handle.</param>
		protected abstract void Handle(TDomainEvent domainEvent);
	}
}
