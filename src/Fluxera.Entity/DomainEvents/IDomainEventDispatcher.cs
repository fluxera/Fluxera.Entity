namespace Fluxera.Entity.DomainEvents
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a domain event dispatcher.
	/// </summary>
	/// <remarks>
	///     A repository or other structure is responsible for adding this dispatch behavior
	/// </remarks>
	[PublicAPI]
	public interface IDomainEventDispatcher
	{
		/// <summary>
		///     Dispatches the given domain event to it's corresponding handlers.
		/// </summary>
		/// <param name="domainEvent">The domain event to handle.</param>
		Task DispatchAsync(IDomainEvent domainEvent);
	}
}
