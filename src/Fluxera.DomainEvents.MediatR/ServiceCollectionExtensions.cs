namespace Fluxera.DomainEvents.MediatR
{
	using Fluxera.DomainEvents.Abstractions;
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
		///     Adds the domain events services. The domain events dispatcher is registered scoped.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEvents(this IServiceCollection services)
		{
			services = Guard.ThrowIfNull(services);

			// Register the default domain event dispatcher.
			services.AddDomainEventDispatcher<MediatrDomainEventDispatcher>();

			return services;
		}

		/// <summary>
		///     Adds the provided domain dispatcher service as scoped.
		/// </summary>
		/// <typeparam name="TDispatcher"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDomainEventDispatcher<TDispatcher>(this IServiceCollection services)
			where TDispatcher : MediatrDomainEventDispatcher
		{
			services = Guard.ThrowIfNull(services);

			services.RemoveAll<IDomainEventDispatcher>();
			services.AddScoped<IDomainEventDispatcher, TDispatcher>();

			return services;
		}
	}
}
