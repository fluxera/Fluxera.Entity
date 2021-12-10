namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using System.Collections.Generic;
	using DomainEvents;
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
