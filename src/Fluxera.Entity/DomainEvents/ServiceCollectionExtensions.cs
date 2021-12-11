namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
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
