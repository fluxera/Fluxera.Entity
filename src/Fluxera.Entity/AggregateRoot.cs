namespace Fluxera.Entity
{
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for all aggregate root entities. Uses <see cref="string"/> as type for the ID.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	[PublicAPI]
	public class AggregateRoot<TAggregateRoot> : Entity<TAggregateRoot>
		where TAggregateRoot : AggregateRoot<TAggregateRoot>
	{
		public static bool operator ==(AggregateRoot<TAggregateRoot>? left, AggregateRoot<TAggregateRoot>? right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(AggregateRoot<TAggregateRoot>? left, AggregateRoot<TAggregateRoot>? right)
		{
			return !(left == right);
		}

		// ReSharper disable once RedundantOverriddenMember
		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		// ReSharper disable once RedundantOverriddenMember
		/// <inheritdoc />
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
