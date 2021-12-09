namespace Fluxera.Entity
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     An attribute to indicate which property(s) describe the unique signature of an entity. 
	/// </summary>
	/// <remarks>
	///     This is intended for use with <see cref="Entity{TEntity}" /> and <see cref="AggregateRoot{TAggregateRoot}" />.
	/// </remarks>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false)]
	public sealed class DomainSignatureAttribute : Attribute
	{
	}
}
