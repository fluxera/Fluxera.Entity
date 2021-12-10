namespace Fluxera.Entity
{
	using System;
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class EntityExtensions
	{
		public static bool IsTransient<TEntity>(this Entity<TEntity> entity)
			where TEntity : Entity<TEntity>
		{
			Guard.Against.Null(entity, nameof(entity));

			return string.IsNullOrWhiteSpace(entity.ID);
		}

		/// <summary>
		///		Checks if the given type is a <see cref="Entity{TEntity}" />.
		/// </summary>
		/// <param name="type">The type to check.</param>
		/// <returns><c>true</c> if the type is an entity; <c>false</c> otherwise.</returns>
		public static bool IsEntity(this Type type)
		{
			try
			{
				bool isSubclassOf = type.IsSubclassOfRawGeneric(typeof(Entity<>));
				return isSubclassOf && !type.IsInterface && !type.IsAbstract;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///		Checks if the given type is a <see cref="AggregateRoot{TAggregateRoot}" />.
		/// </summary>
		/// <param name="type">The type to check.</param>
		/// <returns><c>true</c> if the type is an aggregate root; <c>false</c> otherwise.</returns>
		public static bool IsAggregateRoot(this Type type)
		{
			try
			{
				bool isSubclassOf = type.IsSubclassOfRawGeneric(typeof(AggregateRoot<>));
				return isSubclassOf && !type.IsInterface && !type.IsAbstract;
			}
			catch
			{
				return false;
			}
		}

		private static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
		{
			while (toCheck != null && toCheck != typeof(object))
			{
				Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if (generic == cur)
				{
					return true;
				}

				toCheck = toCheck.BaseType;
			}

			return false;
		}
	}
}
