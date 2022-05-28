﻿namespace Fluxera.Entity.UnitTests.EmployeeAggregate
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Employee : AggregateRoot<Employee, EmployeeId>
	{
		[DomainSignature]
		public string Name { get; set; }

		[DomainSignature]
		public int EmployeeNumber { get; set; }

		public decimal Salary { get; set; }

		public IList<PerformanceReview> PerformanceReviews { get; set; }

		public void GiveRaise(decimal raiseAmount)
		{
			Guard.Against.NegativeOrZero(raiseAmount, nameof(raiseAmount));

			this.Salary += raiseAmount;

			this.DomainEvents.Add(new SalaryRaisedEvent(this.Salary));
		}
	}

	public record EmployeeId(string Value) : IStronglyTypedId<EmployeeId, string>
	{
		/// <inheritdoc />
		public int CompareTo(EmployeeId other)
		{
			return string.Compare(this.Value, other.Value, StringComparison.Ordinal);
		}

		/// <inheritdoc />
		public virtual bool Equals(EmployeeId other)
		{
			return other != null && this.Value.Equals(other.Value);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return this.Value != null ? this.Value.GetHashCode() : 0;
		}
	}
}
