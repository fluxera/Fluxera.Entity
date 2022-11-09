namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     See: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
	/// </summary>
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
		public virtual Task DispatchAsync(IDomainEvent domainEvent)
		{
			return this.DispatchAsync(domainEvent, false);
		}

		/// <inheritdoc />
		public virtual Task DispatchCommittedAsync(IDomainEvent domainEvent)
		{
			return this.DispatchAsync(domainEvent, true);
		}

		/// <summary>
		///     Dispatches the given domain event to registered domain event handlers.
		/// </summary>
		/// <param name="domainEvent"></param>
		/// <param name="dispatchCommitted"></param>
		/// <returns></returns>
		protected async Task DispatchAsync(IDomainEvent domainEvent, bool dispatchCommitted)
		{
			Type eventType = domainEvent.GetType();

			Type eventHandlerType = dispatchCommitted
				? typeof(ICommittedDomainEventHandler<>).MakeGenericType(eventType)
				: typeof(IDomainEventHandler<>).MakeGenericType(eventType);

			IList<dynamic> handlers = this.serviceProvider.GetServices(eventHandlerType).ToList();
			foreach(dynamic handler in handlers)
			{
				await handler.HandleAsync((dynamic)domainEvent).ConfigureAwait(false);
			}
		}
	}
}
