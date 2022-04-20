namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public sealed class DomainEventHandlerBuilder
	{
		private readonly IServiceCollection services;

		public DomainEventHandlerBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers()
		{
			this.AddDomainEventHandlers(Assembly.GetExecutingAssembly());
			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies)
		{
			assemblies ??= Enumerable.Empty<Assembly>();

			foreach(Assembly assembly in assemblies)
			{
				this.AddDomainEventHandlers(assembly);
			}

			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers(Func<IEnumerable<Assembly>> assembliesFactory)
		{
			assembliesFactory ??= Enumerable.Empty<Assembly>;

			IEnumerable<Assembly> assemblies = assembliesFactory.Invoke();
			this.AddDomainEventHandlers(assemblies);

			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandler<TEventHandler>()
		{
			this.AddDomainEventHandlers(typeof(TEventHandler));

			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Type> types)
		{
			types ??= Enumerable.Empty<Type>();

			foreach(Type type in types)
			{
				this.AddDomainEventHandlers(type);
			}

			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers(Func<IEnumerable<Type>> typesFactory)
		{
			Guard.Against.Null(this.services, nameof(this.services));

			typesFactory ??= Enumerable.Empty<Type>;

			IEnumerable<Type> types = typesFactory.Invoke();
			this.AddDomainEventHandlers(types);

			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers(Assembly assembly)
		{
			Guard.Against.Null(assembly, nameof(assembly));

			this.AddDomainEventHandlers(assembly.GetTypes().AsEnumerable());

			return this;
		}

		public DomainEventHandlerBuilder AddDomainEventHandlers(Type type)
		{
			Guard.Against.Null(type, nameof(type));

			bool isEventHandler = type.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType
				&& ((x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))
					|| (x.GetGenericTypeDefinition() == typeof(ICommittedDomainEventHandler<>))));

			if(isEventHandler && !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface)
			{
				IEnumerable<Type> eventHandlerInterfaceTypes = type.GetInterfaces().Where(x => x.GetTypeInfo().IsGenericType
					&& ((x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))
						|| (x.GetGenericTypeDefinition() == typeof(ICommittedDomainEventHandler<>))));

				foreach(Type eventHandlerInterfaceType in eventHandlerInterfaceTypes)
				{
					this.services.AddTransient(eventHandlerInterfaceType, type);
				}
			}

			return this;
		}
	}
}
