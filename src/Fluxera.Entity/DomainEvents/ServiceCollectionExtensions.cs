namespace Fluxera.Entity.DomainEvents
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the domain events services.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configureHandlers"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEvents(this IServiceCollection services, Action<DomainEventHandlerBuilder> configureHandlers)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(configureHandlers, nameof(configureHandlers));

			// Register domain event dispatcher.
			services.TryAddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

			// Configure the domain event handlers.
			configureHandlers.Invoke(new DomainEventHandlerBuilder(services));

			return services;
		}
	}
}
