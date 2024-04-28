[![Build Status](https://dev.azure.com/fluxera/Foundation/_apis/build/status/GitHub/fluxera.Fluxera.Entity?branchName=main&stageName=BuildAndTest)](https://dev.azure.com/fluxera/Foundation/_build/latest?definitionId=86&branchName=main)

# Fluxera.Entity 
An aggregate root and entity objects library.

This library helps in implementing **Entity**, **Aggregate Root**  and **Domain Events**.
The base class for entities implement **Equality** and **Uniqueness** based on selected 
attributes of the entity instead of the references. 

## Equality and Uniqueness

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
dispatching in a **Repository** implementation. A ```IDomainEventHandler``` implementation handles 
a published domain event.


## Configuration

Two different publishing infrastructures exist:

- A default implementation using a custom domain event dispatcher.
    - Add the ```Fluxera.DomainEvents``` package to use this implementation.

- A MediatR based implementation using the ```IPublisher``` of the MediatR library.
    - Add the ```Fluxera.DomainEvents.MediatR``` package to use this implementation.

You can **NOT** use both packages at the same time.

### Default implementation (```Fluxera.DomainEvents```)

```C#
// A domain event support.
services.AddDomainEvents();

// Add domain event handlers.
services.AddDomainEventHandler<SalaryRaisedEventHandler>();

IDomainEventDispatcher dispatcher = /* Get the dispatcher ... */;

SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);

await dispatcher.DispatchAsync(salaryRaisedEvent);
```

### MediatR implementation (```Fluxera.DomainEvents.MediatR```)

```C#
// A domain event support.
services.AddDomainEvents();

// Add domain event handlers by configuring MediatR.
// This will automatically register all domain event handlers in the given assembly.
services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

IDomainEventDispatcher dispatcher = /* Get the dispatcher ... */;

SalaryRaisedEvent salaryRaisedEvent = new SalaryRaisedEvent(100_000);

await dispatcher.DispatchAsync(salaryRaisedEvent);
```

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
	public Task HandleAsync(SalaryRaisedEvent domainEvent, CancellationToken cancellationToken = default)
	{
		// Do something ...
		return Task.CompletedTask;
	}
}
```

## Future

With the upcoming v9.0 release in november 2024 the domain events libraries will
be moved to a separate repository, because it can be used with your own entities
and repositories.

## References

Jimmy Bogard - [A better domain events pattern](https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/)