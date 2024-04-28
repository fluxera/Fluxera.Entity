namespace Fluxera.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A default implementation of the <see cref="IDomainEventDispatcher" /> contract that
	///     dispatches domain events in-memory using a <see cref="IServiceProvider" /> instance.
	/// </summary>
	/// <remarks>
	///     See: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
	/// </remarks>
	[PublicAPI]
	public class DefaultDomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IServiceProvider serviceProvider;

		/// <summary>
		///     Initializes a new instance of the <see cref="DefaultDomainEventDispatcher" /> type.
		/// </summary>
		/// <param name="serviceProvider"></param>
		public DefaultDomainEventDispatcher(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public virtual async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
		{
			Type eventType = domainEvent.GetType();
			Type eventHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

			IEnumerable<dynamic> handlers = this.serviceProvider.GetServices(eventHandlerType);
			foreach(dynamic handler in handlers)
			{
				if(handler != null)
				{
					await handler.HandleAsync((dynamic)domainEvent, cancellationToken).ConfigureAwait(false);
				}
			}
		}
	}
}
