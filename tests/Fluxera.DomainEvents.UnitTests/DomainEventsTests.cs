namespace Fluxera.DomainEvents.UnitTests
{
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.DomainEvents.Abstractions;
	using Fluxera.DomainEvents.Mediator;
	using Fluxera.DomainEvents.UnitTests.Employees;
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

			// A domain event support.
			services.AddDomainEvents();

			// Add the domain event handlers.
			services.AddMediator();

			this.serviceProvider = services.BuildServiceProvider();
		}

		[TearDown]
		public void TearDown()
		{
			this.serviceProvider.Dispose();
		}

		[Test]
		public async Task ShouldExecuteDomainHandlers()
		{
			IDomainEventDispatcher dispatcher = this.serviceProvider.GetRequiredService<IDomainEventDispatcher>();

			SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);
			await dispatcher.DispatchAsync(salaryRaisedEvent);

			salaryRaisedEvent.HandlerNames.Count.Should().Be(2);
			salaryRaisedEvent.HandlerNames.Should().Contain(nameof(SalaryRaisedEventHandler), nameof(AdditionalSalaryRaisedEventHandler));
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
				await dispatcher.DispatchAsync(domainEvent);
			}

			foreach(IDomainEvent domainEvent in employee.DomainEvents)
			{
				((SalaryRaisedEvent)domainEvent).HandlerNames.Count.Should().Be(2);
				((SalaryRaisedEvent)domainEvent).HandlerNames.Should().Contain(nameof(SalaryRaisedEventHandler), nameof(AdditionalSalaryRaisedEventHandler));
			}
		}
	}
}
