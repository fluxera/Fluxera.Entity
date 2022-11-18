namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A default implementation of the <see cref="IDomainEventDispatcher" /> contract that
	///     dispatches domains events in-memory using a <see cref="IServiceProvider" /> instance.
	/// </summary>
	/// <remarks>
	///     See: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
	/// </remarks>
	[PublicAPI]
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IServiceProvider serviceProvider;

		/// <summary>
		///     Initializes a new instance of the <see cref="DomainEventDispatcher" /> type.
		/// </summary>
		/// <param name="serviceProvider"></param>
		public DomainEventDispatcher(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public async Task DispatchAsync(IDomainEvent domainEvent)
		{
			Type eventType = domainEvent.GetType();
			Type eventHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

			IList<dynamic> handlers = this.serviceProvider.GetServices(eventHandlerType).ToList();
			foreach(dynamic handler in handlers)
			{
				await handler.HandleAsync((dynamic)domainEvent).ConfigureAwait(false);
			}
		}
	}
}
