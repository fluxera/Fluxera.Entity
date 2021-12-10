namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class PerformanceReview : Entity<PerformanceReview>
	{
		public string ReviewProtocol { get; set; }
	}
}
