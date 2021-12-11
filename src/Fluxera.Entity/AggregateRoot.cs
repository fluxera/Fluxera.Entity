namespace Fluxera.Entity
{
	using Fluxera.ComponentModel.Annotations;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for all aggregate root entities. Uses <see cref="string"/> as type for the ID.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	[PublicAPI]
	public abstract class AggregateRoot<TAggregateRoot> : Entity<TAggregateRoot, string>
		where TAggregateRoot : AggregateRoot<TAggregateRoot>
	{
		[Ignore]
		public override bool IsTransient => string.IsNullOrWhiteSpace(this.ID);
	}

	/// <summary>
	///     A base class for all aggregate root entities.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public abstract class AggregateRoot<TAggregateRoot, TKey> : Entity<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		public static bool operator ==(AggregateRoot<TAggregateRoot, TKey>? left, AggregateRoot<TAggregateRoot, TKey>? right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(AggregateRoot<TAggregateRoot, TKey>? left, AggregateRoot<TAggregateRoot, TKey>? right)
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
