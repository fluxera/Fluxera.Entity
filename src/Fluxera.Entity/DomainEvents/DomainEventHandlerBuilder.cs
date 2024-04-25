namespace Fluxera.Entity.DomainEvents
{
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
	internal sealed class DomainEventHandlerBuilder : IDomainEventHandlerBuilder
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

		public IDomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies)
		{
			assemblies ??= Enumerable.Empty<Assembly>();

			this.services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
			});

			return this;
		}

		public IDomainEventHandlerBuilder AddDomainEventHandlers(Assembly assembly)
		{
			assembly = Guard.Against.Null(assembly);

			this.services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(assembly);
			});

			return this;
		}


		public IDomainEventHandlerBuilder AddDomainEventDispatcher<TDispatcher>()
			where TDispatcher : class, IDomainEventDispatcher
		{
			this.services.AddDomainEventDispatcher<TDispatcher>();

			return this;
		}
	}
}
