namespace Fluxera.DomainEvents.UnitTests.EmployeeAggregate
{
	using System.Collections.Generic;
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;

	[PublicAPI]
	public class SalaryRaisedEvent : IDomainEvent
	{
		public SalaryRaisedEvent(decimal newSalary)
		{
			this.NewSalary = newSalary;
			this.HandlerNames = new List<string>();
		}

		public decimal NewSalary { get; }

		public IList<string> HandlerNames { get; }
	}
}
