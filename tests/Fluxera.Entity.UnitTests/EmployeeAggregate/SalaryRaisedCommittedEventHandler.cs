namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using System.Threading.Tasks;
	using DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public class SalaryRaisedCommittedEventHandler : ICommittedDomainEventHandler<SalaryRaisedEvent>
	{
		/// <inheritdoc />
		public Task HandleAsync(SalaryRaisedEvent domainEvent)
		{
			domainEvent.HandlerNames.Add(nameof(SalaryRaisedCommittedEventHandler));
			return Task.CompletedTask;
		}
	}
}