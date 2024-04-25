namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Reflection;
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
		///     Adds the domain events services. The domain events dispatcher is register as scoped.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configureHandlers"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEvents(this IServiceCollection services, Action<IDomainEventHandlerBuilder> configureHandlers)
		{
			Guard.Against.Null(services);
			Guard.Against.Null(configureHandlers);

			// Configure MediatR services.
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetCallingAssembly());

				// Configure the domain event handlers.
				configureHandlers.Invoke(new DomainEventHandlerBuilder(services, cfg));
			});

			// Register domain event dispatcher.
			services.AddDomainEventDispatcher<DomainEventDispatcher>();

			return services;
		}

		/// <summary>
		///     Adds the provided domain dispatcher service as scoped.
		/// </summary>
		/// <typeparam name="TDispatcher"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEventDispatcher<TDispatcher>(this IServiceCollection services)
			where TDispatcher : class, IDomainEventDispatcher
		{
			services.RemoveAll<IDomainEventDispatcher>();
			services.AddScoped<IDomainEventDispatcher, TDispatcher>();

			return services;
		}
	}
}
