namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///		See: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
	/// </summary>
	[UsedImplicitly]
	internal sealed class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IServiceProvider serviceProvider;

		public DomainEventDispatcher(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public async Task DispatchAsync(dynamic domainEvent, bool dispatchCommitted)
		{
			Type eventType = domainEvent.GetType();

			Type eventHandlerType = dispatchCommitted
				? typeof(ICommittedDomainEventHandler<>).MakeGenericType(eventType)
				: typeof(IDomainEventHandler<>).MakeGenericType(eventType);

			IList<dynamic> handlers = this.serviceProvider.GetServices(eventHandlerType).ToList()!;
			foreach (dynamic handler in handlers)
			{
				await handler.HandleAsync(domainEvent).ConfigureAwait(false);
			}
		}
	}
}
