namespace Fluxera.Entity.DomainEvents
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///		A service contract for a dynamic domain event dispatcher. 
	/// </summary>
	/// <remarks>
	///		A repository or other data access structure is responsible for adding
	///		this dispatch behavior to the data access.
	/// </remarks>
	[PublicAPI]
	public interface IDomainEventDispatcher
	{
		/// <summary>
		///		Dispatches the given domain event to it's corresponding handlers.
		/// </summary>
		/// <param name="domainEvent">The domain event to handle.</param>
		/// <param name="dispatchCommitted">
		///		If <c>true</c>, dispatches to committed event handlers that run after the entity was stored. <br/>
		///		If <c>false</c>, dispatches to event handlers that that run before the entity was stored.
		/// </param>
		Task DispatchAsync(dynamic domainEvent, bool dispatchCommitted);
	}
}
