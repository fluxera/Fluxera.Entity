namespace Fluxera.DomainEvents.UnitTests.EmployeeAggregate
{
	using System.Collections.Generic;
	using Fluxera.Entity;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EmployeeStringId : Entity<EmployeeStringId, string>
	{
		[DomainSignature]
		public string Name { get; set; }

		[DomainSignature]
		public int EmployeeNumber { get; set; }

		public decimal Salary { get; set; }

		public IList<PerformanceReview> PerformanceReviews { get; set; }

		public void GiveRaise(decimal raiseAmount)
		{
			this.Salary += raiseAmount;

			this.RaiseDomainEvent(new SalaryRaisedEvent(this.Salary));
		}
	}
}
