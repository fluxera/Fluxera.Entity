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
		private readonly MediatRServiceConfiguration configuration;

		/// <summary>
		///     Initializes a new instance of the <see cref="DomainEventHandlerBuilder" /> type.
		/// </summary>
		/// <param name="configuration"></param>
		public DomainEventHandlerBuilder(MediatRServiceConfiguration configuration)
		{
			this.configuration = configuration;
		}


		public IDomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies)
		{
			assemblies ??= Enumerable.Empty<Assembly>();

			this.configuration.RegisterServicesFromAssemblies(assemblies.ToArray());

			return this;
		}

		public IDomainEventHandlerBuilder AddDomainEventHandlers(Assembly assembly)
		{
			assembly = Guard.Against.Null(assembly);

			this.configuration.RegisterServicesFromAssembly(assembly);

			return this;
		}
	}
}
