namespace Fluxera.DomainEvents.UnitTests.EmployeeAggregate
{
	using Fluxera.Entity;
	using JetBrains.Annotations;

	[PublicAPI]
	public class PerformanceReview : Entity<PerformanceReview, string>
	{
		public string ReviewProtocol { get; set; }
	}
}
