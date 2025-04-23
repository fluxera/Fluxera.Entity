namespace Fluxera.DomainEvents.UnitTests.Employees
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.DomainEvents.Mediator;
	using JetBrains.Annotations;

	[PublicAPI]
	public class AdditionalSalaryRaisedEventHandler : IDomainEventHandler<SalaryRaisedEvent>
	{
		/// <inheritdoc />
		public ValueTask HandleAsync(SalaryRaisedEvent domainEvent, CancellationToken cancellationToken)
		{
			domainEvent.HandlerNames.Add(nameof(AdditionalSalaryRaisedEventHandler));
			return ValueTask.CompletedTask;
		}
	}
}
