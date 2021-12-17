namespace Fluxera.Entity.UnitTests
{
	using FluentAssertions;
	using Fluxera.Entity.UnitTests.EmployeeAggregate;
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
			Employee employee = new Employee
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
			Employee employee = new Employee
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
