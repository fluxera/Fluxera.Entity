[![Build Status](https://dev.azure.com/fluxera/Foundation/_apis/build/status/GitHub/fluxera.Fluxera.Entity?branchName=main)](https://dev.azure.com/fluxera/Foundation/_build/latest?definitionId=64&branchName=main)

# Fluxera.Entity
An aggregate root and entity objects library.

This library helps in implementing **Entity**, **Aggregate Root**  and **Domain Events**.
The base class for entities implement **Equality** and **Uniquness** based on selected 
attributes of the entity instead of the references. 

## Equality and Uniquness

The equality check consist of two major steps:

1.) When comparing two entities for **Equality** at first we check if the entites are both not
transient and their IDs are equal. In this case the entites are considered equal, even if
their values are different.

2.) If the first step did not signal **Equality** we check if the entities are both transient
and their domain signature attributes are equal. You define which attributes of the entity make 
up their domain signature by adding the ```[DomainSignature]``` attribute to the corresponding 
properties. The attributes are then picked up by the default implementation using reflection.
If you do not want to use the default implementation you can override the ```GetEqualityComponents()```
method an return the values to use manually.

## Domain Events

This library provides the infrastructure to implement, register and dispatch domain events from
entities to loosely-coupled domain event handlers. You can add events to the ```DomainEvents```
collection of an entity and implement two different types of domain event handlers for it.

This library provides the nessessary dispatcher service which can be used to integrate the event
dispatching in a **Repository** implementation. 

### ```IDomainEventHandler```

A domain event handler of this type will be executed **before** a **Repository** adds or updates
the entity in the storage.

### ```ICommittedDomainEventHandler```

A domain event handler of this type will be executed **after** a **Repository** adds or updates
the entity in the storage.

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
// A domain event support.
services.AddDomainEvents(builder =>
{
	builder
		.AddDomainEventHandlers<SalaryRaisedEventHandler>()
		.AddDomainEventHandlers<AdditionalSalaryRaisedEventHandler>()
		.AddDomainEventHandlers<SalaryRaisedCommittedEventHandler>();
});

IDomainEventDispatcher dispatcher = /* Get the dispatcher ... */;

SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);

await dispatcher.DispatchAsync(salaryRaisedEvent);
await dispatcher.DispatchCommittedAsync(salaryRaisedEvent);
```

## References

Jimmy Bogard - [A better domain events pattern](https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/)