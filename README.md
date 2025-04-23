[![Build Status](https://dev.azure.com/fluxera/Foundation/_apis/build/status/GitHub/fluxera.Fluxera.Entity?branchName=main&stageName=BuildAndTest)](https://dev.azure.com/fluxera/Foundation/_build/latest?definitionId=86&branchName=main)

# Fluxera.Entity 
An aggregate root and entity objects library.

This library helps in implementing **Entity**, **Aggregate Root**  and **Domain Events**.
The base class for entities implement **Equality** and **Uniqueness** based on selected 
attributes of the entity instead of the references. 

## Equality and Uniqueness

The equality check consist of two major steps:

1.) When comparing two entities for **Equality** at first we check if the entities are both not
transient and their IDs are equal. In this case the entities are considered equal, even if
their values are different.

2.) If the first step did not signal **Equality** we check if the entities are both transient
and their domain signature attributes are equal. You define which attributes of the entity make 
up their domain signature by adding the ```[DomainSignature]``` attribute to the corresponding 
properties. The attributes are then picked up by the default implementation using reflection.
If you do not want to use the default implementation you can override the ```GetEqualityComponents()```
method a return the values to use manually.

## Domain Events

This library provides the infrastructure to implement, register and dispatch domain events from
entities to loosely-coupled domain event handlers. You can add events to the ```DomainEvents```
collection of an entity and implement two different types of domain event handlers for it.

This library provides the necessary dispatcher service which can be used to integrate the event
dispatching in a **Repository** implementation. A ```IDomainEventHandler``` implementation handles 
a published domain event.


## Configuration

Two different publishing infrastructures exist:

- A Mediator-based implementation using a custom domain event dispatcher.
    - Add the ```Fluxera.DomainEvents``` package to use this implementation.

### Mediator implementation (```Fluxera.DomainEvents```)

```C#

// Register the Mediator in your application startup.
// Add the domain event handlers.
services.AddMediator();
```

```xml
<!-- Add the Mediator.SourceGenerator to your application project file -->
<PackageReference Include="Mediator.SourceGenerator" Version="3.0.*-*">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

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
