namespace Fluxera.DomainEvents.UnitTests.Employees
{
	using System;
	using Fluxera.Entity;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EmployeeGuid : Entity<EmployeeGuid, Guid>
	{
		[DomainSignature]
		public string Name { get; set; }

		[DomainSignature]
		public int EmployeeNumber { get; set; }

		public decimal Salary { get; set; }
	}
}
