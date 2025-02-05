namespace Fluxera.Entity.UnitTests
{
	using FluentAssertions;
	using Fluxera.Entity.UnitTests.Employees;
	using NUnit.Framework;

	[TestFixture]
	public class EntityWithStringKeyTransientTests
	{
		[Test]
		public void ShouldReturnTrueForTransientEntityForDefaultValue()
		{
			Employee employee = new Employee
			{
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			employee.IsTransient.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForTransientEntityForEmpty()
		{
			EmployeeStringId employee = new EmployeeStringId
			{
				ID = string.Empty,
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			employee.IsTransient.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForTransientEntityForNull()
		{
			Employee employee = new Employee
			{
				ID = null,
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			employee.IsTransient.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForTransientEntityForOnlyWhitespace()
		{
			EmployeeStringId employee = new EmployeeStringId
			{
				ID = "     ",
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			employee.IsTransient.Should().BeTrue();
		}
	}
}
