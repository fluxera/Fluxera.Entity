namespace Fluxera.Entity
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     An attribute to indicate which property(s) describe the unique signature of an entity.
	/// </summary>
	/// <remarks>
	///     This is intended for use with <see cref="Entity{TEntity,TKey}" /> types.
	/// </remarks>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DomainSignatureAttribute : Attribute
	{
	}
}
