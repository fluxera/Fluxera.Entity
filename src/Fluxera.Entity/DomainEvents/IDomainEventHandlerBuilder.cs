namespace Fluxera.Entity.DomainEvents
{
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///     A builder for configuring the domain event handlers.
	/// </summary>
	[PublicAPI]
	public interface IDomainEventHandlerBuilder
	{
		/// <summary>
		///     Add the domain handlers available in the given assemblies.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		IDomainEventHandlerBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies);

		/// <summary>
		///     Add the domain handlers available in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		IDomainEventHandlerBuilder AddDomainEventHandlers(Assembly assembly);

		/// <summary>
		///     Adds the provided domain dispatcher service as scoped.
		/// </summary>
		/// <typeparam name="TDispatcher"></typeparam>
		/// <returns></returns>
		IDomainEventHandlerBuilder AddDomainEventDispatcher<TDispatcher>()
			where TDispatcher : class, IDomainEventDispatcher;
	}
}
