namespace Fluxera.Entity.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Entity.UnitTests.Employees;
	using NUnit.Framework;

	[TestFixture]
	public class EntityWithGuidKeyTransientTests
	{
		[Test]
		public void ShouldReturnTrueForTransientEntityForDefaultValue()
		{
			EmployeeGuid employee = new EmployeeGuid
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
			EmployeeGuid employee = new EmployeeGuid
			{
				ID = Guid.Empty,
				Name = "James Bond",
				EmployeeNumber = 8051007,
				Salary = 1_000_000,
			};

			employee.IsTransient.Should().BeTrue();
		}
	}
}
