namespace Fluxera.DomainEvents.UnitTests.Employees
{
	using Fluxera.StronglyTypedId;

	public sealed class EmployeeId : StronglyTypedId<EmployeeId, string>
	{
		/// <inheritdoc />
		public EmployeeId(string value) : base(value)
		{
		}
	}
}
