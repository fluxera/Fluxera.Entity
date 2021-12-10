namespace Fluxera.Entity.UnitTests
{
	using System.Threading.Tasks;
	using DomainEvents;
	using EmployeeAggregate;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class DomainEventsTests
	{
		private ServiceProvider serviceProvider;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();

			// Register the dispatcher and the domain event handlers.
			services.AddDomainEventHandlers<SalaryRaisedEventHandler>();
			services.AddDomainEventHandlers<AdditionalSalaryRaisedEventHandler>();
			services.AddDomainEventHandlers<SalaryRaisedCommittedEventHandler>();

			this.serviceProvider = services.BuildServiceProvider();
		}

		[Test]
		public async Task ShouldExecuteDomainHandlers()
		{
			IDomainEventDispatcher dispatcher = this.serviceProvider.GetRequiredService<IDomainEventDispatcher>();

			SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);
			await dispatcher.DispatchAsync(salaryRaisedEvent, false);

			salaryRaisedEvent.HandlerNames.Count.Should().Be(2);
			salaryRaisedEvent.HandlerNames.Should().Contain(nameof(SalaryRaisedEventHandler), nameof(AdditionalSalaryRaisedEventHandler));
		}

		[Test]
		public async Task ShouldExecuteCommittedDomainHandlers()
		{
			IDomainEventDispatcher dispatcher = this.serviceProvider.GetRequiredService<IDomainEventDispatcher>();

			SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);
			await dispatcher.DispatchAsync(salaryRaisedEvent, true);

			salaryRaisedEvent.HandlerNames.Count.Should().Be(1);
			salaryRaisedEvent.HandlerNames.Should().Contain(nameof(SalaryRaisedCommittedEventHandler));
		}

		[Test]
		public async Task ShouldExecuteDomainHandlersFromEntity()
		{
			IDomainEventDispatcher dispatcher = this.serviceProvider.GetRequiredService<IDomainEventDispatcher>();

			Employee employee = new Employee
			{
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};
			employee.GiveRaise(25_000);

			foreach(IDomainEvent domainEvent in employee.DomainEvents)
			{
				await dispatcher.DispatchAsync(domainEvent, false);
			}

			foreach(IDomainEvent domainEvent in employee.DomainEvents)
			{
				((SalaryRaisedEvent)domainEvent).HandlerNames.Count.Should().Be(2);
				((SalaryRaisedEvent)domainEvent).HandlerNames.Should().Contain(nameof(SalaryRaisedEventHandler), nameof(AdditionalSalaryRaisedEventHandler));
			}

		}
	}
}
