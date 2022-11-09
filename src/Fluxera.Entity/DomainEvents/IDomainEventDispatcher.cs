namespace Fluxera.Entity.DomainEvents
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A service contract for a domain event dispatcher.
	/// </summary>
	/// <remarks>
	///     A repository or other data-access structure is responsible for adding
	///     this dispatch behavior to the data-access layer.
	/// </remarks>
	[PublicAPI]
	public interface IDomainEventDispatcher
	{
		/// <summary>
		///     Dispatches the given domain event to it's corresponding handlers. <br />
		///     Dispatches to event handlers that that run before changes are committed.
		/// </summary>
		/// <param name="domainEvent">The domain event to handle.</param>
		Task DispatchAsync(IDomainEvent domainEvent);

		/// <summary>
		///     Dispatches the given domain event to it's corresponding handlers. <br />
		///     Dispatches to committed event handlers that run after changes are committed.
		/// </summary>
		/// <param name="domainEvent">The domain event to handle.</param>
		Task DispatchCommittedAsync(IDomainEvent domainEvent);
	}
}
