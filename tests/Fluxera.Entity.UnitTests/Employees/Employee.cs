﻿namespace Fluxera.Entity.UnitTests.Employees
{
	using System.Collections.Generic;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Employee : Entity<Employee, EmployeeId>
	{
		[DomainSignature]
		public string Name { get; set; }

		[DomainSignature]
		public int EmployeeNumber { get; set; }

		public decimal Salary { get; set; }

		public IList<PerformanceReview> PerformanceReviews { get; set; }

		public void GiveRaise(decimal raiseAmount)
		{
			Guard.Against.NegativeOrZero(raiseAmount, nameof(raiseAmount));

			this.Salary += raiseAmount;

			this.RaiseDomainEvent(new SalaryRaisedEvent(this.Salary));
		}
	}
}
