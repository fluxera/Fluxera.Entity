namespace Fluxera.Entity.UnitTests
{
	using FluentAssertions;
	using Fluxera.Entity.UnitTests.EmployeeAggregate;
	using NUnit.Framework;

	[TestFixture]
	public class DomainSignatureTests
	{
		[Test]
		public void EqualsShouldReturnFalseForEqualNonTransientEntitiesWithDifferentIdentifiers()
		{
			Employee employeeOne = new Employee
			{
				ID = new EmployeeId("1111"),
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			Employee employeeTwo = new Employee
			{
				ID = new EmployeeId("9999"),
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 2_000_000,
			};

			employeeOne.Equals(employeeTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnFalseForNotEqualTransientEntities()
		{
			Employee employeeOne = new Employee
			{
				Name = "Jason Bourne",
				EmployeeNumber = 8723456,
				Salary = 2_000_000,
			};

			Employee employeeTwo = new Employee
			{
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 2_000_000,
			};

			employeeOne.Equals(employeeTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnTrueForEqualNonTransientEntitiesWithSameIdentifiers()
		{
			Employee employeeOne = new Employee
			{
				ID = new EmployeeId("1111"),
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			Employee employeeTwo = new Employee
			{
				ID = new EmployeeId("1111"),
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 2_000_000,
			};

			employeeOne.Equals(employeeTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForEqualTransientEntities()
		{
			Employee employeeOne = new Employee
			{
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			Employee employeeTwo = new Employee
			{
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 2_000_000,
			};

			employeeOne.Equals(employeeTwo).Should().BeTrue();
		}
	}
}
