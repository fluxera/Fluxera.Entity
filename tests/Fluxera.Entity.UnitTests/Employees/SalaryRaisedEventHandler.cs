namespace Fluxera.Entity.UnitTests.Employees
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public class SalaryRaisedEventHandler : IDomainEventHandler<SalaryRaisedEvent>
	{
		/// <inheritdoc />
		public ValueTask HandleAsync(SalaryRaisedEvent domainEvent, CancellationToken cancellationToken)
		{
			domainEvent.HandlerNames.Add(nameof(SalaryRaisedEventHandler));
			return ValueTask.CompletedTask;
		}
	}
}