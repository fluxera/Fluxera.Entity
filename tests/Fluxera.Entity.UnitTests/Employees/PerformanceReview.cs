namespace Fluxera.Entity.UnitTests.Employees
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class PerformanceReview : Entity<PerformanceReview, string>
	{
		public string ReviewProtocol { get; set; }
	}
}
