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
		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			services.AddDomainEventHandlers(Assembly.GetExecutingAssembly());
			return services;
		}

		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
		{
			Guard.Against.Null(services, nameof(services));

			assemblies ??= Enumerable.Empty<Assembly>();

			foreach(Assembly assembly in assemblies)
			{
				services.AddDomainEventHandlers(assembly);
			}

			return services;
		}

		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Func<IEnumerable<Assembly>> assembliesFactory)
		{
			Guard.Against.Null(services, nameof(services));

			assembliesFactory ??= (Enumerable.Empty<Assembly>);

			IEnumerable<Assembly> assemblies = assembliesFactory.Invoke();
			services.AddDomainEventHandlers(assemblies);

			return services;
		}

		public static IServiceCollection AddDomainEventHandlers<TEventHandler>(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			services.AddDomainEventHandlers(typeof(TEventHandler));

			return services;
		}
		
		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, IEnumerable<Type> types)
		{
			Guard.Against.Null(services, nameof(services));

			types ??= Enumerable.Empty<Type>();

			foreach(Type type in types)
			{
				services.AddDomainEventHandlers(type);
			}

			return services;
		}

		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Func<IEnumerable<Type>> typesFactory)
		{
			Guard.Against.Null(services, nameof(services));

			typesFactory ??= (Enumerable.Empty<Type>);

			IEnumerable<Type> types = typesFactory.Invoke();
			services.AddDomainEventHandlers(types);

			return services;
		}

		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Assembly assembly)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(assembly, nameof(assembly));

			services.AddDomainEventHandlers(assembly.GetTypes().AsEnumerable());

			return services;
		}

		public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Type type)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(type, nameof(type));

			// Register domain event dispatcher.
			services.TryAddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

			bool isEventHandler = type.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType 
				&& (x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>) 
					|| x.GetGenericTypeDefinition() == typeof(ICommittedDomainEventHandler<>)));

			if (isEventHandler && !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface)
			{
				IEnumerable<Type> eventHandlerInterfaceTypes = type.GetInterfaces().Where(x => x.GetTypeInfo().IsGenericType 
					&& (x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>) 
						|| x.GetGenericTypeDefinition() == typeof(ICommittedDomainEventHandler<>)));

				foreach (Type eventHandlerInterfaceType in eventHandlerInterfaceTypes)
				{
					services.AddTransient(eventHandlerInterfaceType, type);
				}
			}

			return services;
		}
	}
}
