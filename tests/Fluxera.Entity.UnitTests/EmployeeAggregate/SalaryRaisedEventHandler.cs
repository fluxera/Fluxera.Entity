namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using System.Threading.Tasks;
	using DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public class SalaryRaisedEventHandler : IDomainEventHandler<SalaryRaisedEvent>
	{
		/// <inheritdoc />
		public Task HandleAsync(SalaryRaisedEvent domainEvent)
		{
			domainEvent.HandlerNames.Add(nameof(SalaryRaisedEventHandler));
			return Task.CompletedTask;
		}
	}
}