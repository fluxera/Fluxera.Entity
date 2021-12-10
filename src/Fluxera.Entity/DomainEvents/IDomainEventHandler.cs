﻿namespace Fluxera.Entity.DomainEvents
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for implementing domain event handlers, which must be registered
	///		using dependency injection. Handlers of this type must be executed before
	///		storing the entity to a storage.
	/// </summary>
	/// <remarks>
	///		See: <see cref="ServiceCollectionExtensions.AddDomainEventHandlers(IServiceCollection)"/>.
	/// </remarks>
	/// <typeparam name="TDomainEvent">The type of the domain event to handle.</typeparam>
	[PublicAPI]
	public interface IDomainEventHandler<in TDomainEvent> 
		where TDomainEvent : class, IDomainEvent
	{
		/// <summary>
		///		Handles the given domain event asynchronously.
		/// </summary>
		/// <param name="domainEvent">The event to handle.</param>
		Task HandleAsync(TDomainEvent domainEvent);
	}
}