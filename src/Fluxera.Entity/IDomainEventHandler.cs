namespace Fluxera.Entity
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Contract for implementing handlers of domain events, which must be registered inside the kernel.
	/// </summary>
	/// <typeparam name="TDomainEvent"></typeparam>
	[PublicAPI]
	public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : class, IDomainEvent
	{
		Task HandleAsync(TDomainEvent domainEvent);
	}
}
