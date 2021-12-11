namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class PerformanceReview : Entity<PerformanceReview, string>
	{
		public string ReviewProtocol { get; set; }
	}
}
