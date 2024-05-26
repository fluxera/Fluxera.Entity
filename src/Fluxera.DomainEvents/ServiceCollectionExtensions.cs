namespace Fluxera.DomainEvents
{
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using System.Collections.Generic;
	using System;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the domain events services. The domain events dispatcher is registered scoped.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEvents(this IServiceCollection services)
		{
			services = Guard.ThrowIfNull(services);

			// Register the default domain event dispatcher.
			services.AddDomainEventDispatcher<DefaultDomainEventDispatcher>();

			return services;
		}

		/// <summary>
		///     Adds the provided domain dispatcher service as scoped.
		/// </summary>
		/// <typeparam name="TDispatcher"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEventDispatcher<TDispatcher>(this IServiceCollection services)
			where TDispatcher : DefaultDomainEventDispatcher
		{
			services = Guard.ThrowIfNull(services);

			services.RemoveAll<IDomainEventDispatcher>();
			services.AddScoped<IDomainEventDispatcher, TDispatcher>();

			return services;
		}

		/// <summary>
		///		Adds a domain event handler.
		/// </summary>
		/// <typeparam name="TDomainEventHandler"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEventHandler<TDomainEventHandler>(this IServiceCollection services)
		{
			services = Guard.ThrowIfNull(services);

			Type type = typeof(TDomainEventHandler);

			bool isEventHandler = type.GetInterfaces().Any(x =>
				x.GetTypeInfo().IsGenericType &&
				x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

			if(isEventHandler && !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface)
			{
				IEnumerable<Type> eventHandlerInterfaceTypes = type.GetInterfaces().Where(x =>
					x.GetTypeInfo().IsGenericType &&
					x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

				foreach(Type eventHandlerInterfaceType in eventHandlerInterfaceTypes)
				{
					services.AddTransient(eventHandlerInterfaceType, type);
				}
			}

			return services;
		}
	}
}
