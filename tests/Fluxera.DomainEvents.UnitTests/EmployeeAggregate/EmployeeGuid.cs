namespace Fluxera.DomainEvents.UnitTests.EmployeeAggregate
{
	using System;
	using Fluxera.Entity;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EmployeeGuid : AggregateRoot<EmployeeGuid, Guid>
	{
		[DomainSignature]
		public string Name { get; set; }

		[DomainSignature]
		public int EmployeeNumber { get; set; }

		public decimal Salary { get; set; }
	}
}
