namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Fluxera.Guards;

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
			Guard.Against.NegativeOrZero(raiseAmount, nameof(raiseAmount));

			this.Salary += raiseAmount;

			this.RaiseDomainEvent(new SalaryRaisedEvent(this.Salary));
		}
	}
}
