namespace Fluxera.Entity.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Entity.UnitTests.EmployeeAggregate;
	using NUnit.Framework;

	[TestFixture]
	public class EntityExtensionsTests
	{
		[Test]
		[TestCase(typeof(Employee), true)]
		[TestCase(typeof(PerformanceReview), true)]
		[TestCase(typeof(object), false)]
		public void IsEntityShouldReturnExpectedValue(Type type, bool expected)
		{
			type.IsEntity().Should().Be(expected);
		}
	}
}
