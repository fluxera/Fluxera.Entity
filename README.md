[![Build Status](https://dev.azure.com/fluxera/Foundation/_apis/build/status/GitHub/fluxera.Fluxera.Entity?branchName=main)](https://dev.azure.com/fluxera/Foundation/_build/latest?definitionId=64&branchName=main)

# Fluxera.Entity
An aggregate root and entity objects library.

This library helps in implementing **Entity**, **Aggregate Root**  and **Domain Events**.

## Usage

```C#
public class Employee : AggregateRoot<Employee>
{
	[DomainSignature]
	public string Name { get; set; }

	[DomainSignature]
	public int EmployeeNumber { get; set; }

	public decimal Salary { get; set; }

	public void GiveRaise(decimal raiseAmount)
	{
		Guard.Against.NegativeOrZero(raiseAmount, nameof(raiseAmount));

		this.Salary += raiseAmount;

		this.DomainEvents.Add(new SalaryRaisedEvent(this.Salary));
	}
}
```

```C#
public class SalaryRaisedEvent : IDomainEvent
{
	public SalaryRaisedEvent(decimal newSalary)
	{
		this.NewSalary = newSalary;
		this.HandlerNames = new List<string>();
	}

	public decimal NewSalary { get; }
}
```

```C#
public class SalaryRaisedEventHandler : IDomainEventHandler<SalaryRaisedEvent>
{
	public Task HandleAsync(SalaryRaisedEvent domainEvent)
	{
		// Do something ...
		return Task.CompletedTask;
	}
}
```

```C#
public class SalaryRaisedEventHandler : ICommittedDomainEventHandler<SalaryRaisedEvent>
{
	public Task HandleAsync(SalaryRaisedEvent domainEvent)
	{
		// Do something ...
		return Task.CompletedTask;
	}
}
```

```C#
// Register the dispatcher and the domain event handlers.
services.AddDomainEventHandlers<SalaryRaisedEventHandler>();
services.AddDomainEventHandlers<SalaryRaisedCommittedEventHandler>();

IDomainEventDispatcher dispatcher = /* Get the dispatcher from container... */;

SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);

await dispatcher.DispatchAsync(salaryRaisedEvent, false);
await dispatcher.DispatchAsync(salaryRaisedEvent, true);
```
