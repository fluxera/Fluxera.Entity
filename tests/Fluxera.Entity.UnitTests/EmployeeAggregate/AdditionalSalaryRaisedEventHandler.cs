namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using System.Threading;
	using System.Threading.Tasks;
	using DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public class AdditionalSalaryRaisedEventHandler : IDomainEventHandler<SalaryRaisedEvent>
	{
		/// <inheritdoc />
		public Task HandleAsync(SalaryRaisedEvent domainEvent, CancellationToken cancellationToken)
		{
			domainEvent.HandlerNames.Add(nameof(AdditionalSalaryRaisedEventHandler));
			return Task.CompletedTask;
		}
	}
}
