namespace Fluxera.Entity.UnitTests
{
	using System;
	using System.Data.SqlTypes;
	using EmployeeAggregate;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EntityExtensionsTests
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

			employee.IsTransient().Should().BeTrue();
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

			employee.IsTransient().Should().BeTrue();
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

			employee.IsTransient().Should().BeTrue();
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

			employee.IsTransient().Should().BeTrue();
		}

		[Test]
		[TestCase(typeof(Employee), true)]
		[TestCase(typeof(PerformanceReview), true)]
		[TestCase(typeof(object), false)]
		public void IsEntityShouldReturnExpectedValue(Type type, bool expected)
		{
			type.IsEntity().Should().Be(expected);
		}

		[Test]
		[TestCase(typeof(Employee), true)]
		[TestCase(typeof(PerformanceReview), false)]
		[TestCase(typeof(object), false)]
		public void IsAggregateRootShouldReturnExpectedValue(Type type, bool expected)
		{
			type.IsAggregateRoot().Should().Be(expected);
		}
	}
}
