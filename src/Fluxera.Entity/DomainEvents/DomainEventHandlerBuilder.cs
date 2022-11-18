namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A builder for configuring the domain event handlers.
	/// </summary>
	[PublicAPI]
	public sealed class DomainEventHandlerBuilder
	{
		private readonly IServiceCollection services;

		/// <summary>
		///     Initializes a new instance of the <see cref="DomainEventHandlerBuilder" /> type.
		/// </summary>
		/// <param name="services"></param>
		public DomainEventHandlerBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		/// <summary>
		///     Add the domain handlers available in the given assemblies.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		public DomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies)
		{
			assemblies ??= Enumerable.Empty<Assembly>();

			foreach(Assembly assembly in assemblies)
			{
				this.AddDomainEventHandlers(assembly);
			}

			return this;
		}


		/// <summary>
		///     Add the domain handlers available in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public DomainEventHandlerBuilder AddDomainEventHandlers(Assembly assembly)
		{
			Guard.Against.Null(assembly, nameof(assembly));

			this.AddDomainEventHandlers(assembly.GetTypes().AsEnumerable());

			return this;
		}

		/// <summary>
		///     Add the domain handlers from the given types.
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		public DomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Type> types)
		{
			types ??= Enumerable.Empty<Type>();

			foreach(Type type in types)
			{
				this.AddDomainEventHandlers(type);
			}

			return this;
		}

		/// <summary>
		///     Add the given domain handler.
		/// </summary>
		/// <typeparam name="TEventHandler"></typeparam>
		/// <returns></returns>
		public DomainEventHandlerBuilder AddDomainEventHandler<TEventHandler>() where TEventHandler : IDomainEventHandler
		{
			this.AddDomainEventHandlers(typeof(TEventHandler));

			return this;
		}

		/// <summary>
		///     Add the given domain handler type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public DomainEventHandlerBuilder AddDomainEventHandlers(Type type)
		{
			Guard.Against.Null(type, nameof(type));

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
					this.services.AddTransient(eventHandlerInterfaceType, type);
				}
			}

			return this;
		}

		/// <summary>
		///     Adds the provided domain dispatcher service.
		/// </summary>
		/// <typeparam name="TDispatcher"></typeparam>
		/// <returns></returns>
		public DomainEventHandlerBuilder AddDomainEventDispatcher<TDispatcher>()
			where TDispatcher : class, IDomainEventDispatcher
		{
			this.services.AddDomainEventDispatcher<TDispatcher>();

			return this;
		}
	}
}
