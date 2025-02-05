﻿namespace Fluxera.Entity
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Text.Json.Serialization;
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for entities.
	/// </summary>
	/// <typeparam name="TEntity">The entity type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public abstract class Entity<TEntity, TKey>
		where TEntity : Entity<TEntity, TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		private readonly IList<IDomainEvent> domainEvents = [];

		/// <summary>
		///     The unique ID of the entity.
		/// </summary>
		[Key]
		[JsonPropertyOrder(int.MinValue)]
		public virtual TKey ID { get; set; }

		/// <summary>
		///     The domain events of this entity.
		/// </summary>
		[JsonIgnore]
		[IgnoreDataMember]
		public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

		/// <summary>
		///     Gets a flag, if the entity instance is transient (not stored to the storage).
		/// </summary>
		[JsonIgnore]
		[IgnoreDataMember]
		public virtual bool IsTransient
		{
			get
			{
				bool isTransient = Equals(this.ID, default(TKey));

				if(typeof(TKey) == typeof(string))
				{
					if(!isTransient)
					{
						isTransient = string.IsNullOrWhiteSpace(this.ID as string);
					}
				}

				return isTransient;
			}
		}

		/// <summary>
		///     Adds the given event to the list of raised domain events.
		/// </summary>
		/// <param name="domainEvent"></param>
		public void RaiseDomainEvent(IDomainEvent domainEvent)
		{
			this.domainEvents.Add(domainEvent);
		}

		/// <summary>
		///     Clears the list of raised domain events.
		/// </summary>
		public void ClearDomainEvents()
		{
			this.domainEvents.Clear();
		}

		/// <summary>
		///     Checks two instances of <see cref="Entity{TEntity,TKey}" /> are equal.
		/// </summary>
		/// <param name="left">The left item of the operator.</param>
		/// <param name="right">The left item of the operator.</param>
		/// <returns>True, if the instances are equal.</returns>
		public static bool operator ==(Entity<TEntity, TKey> left, Entity<TEntity, TKey> right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		///     Checks two instances of <see cref="Entity{TEntity,TKey}" /> are not equal.
		/// </summary>
		/// <param name="left">The left item of the operator.</param>
		/// <param name="right">The left item of the operator.</param>
		/// <returns>True, if the instances are equal.</returns>
		public static bool operator !=(Entity<TEntity, TKey> left, Entity<TEntity, TKey> right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is null)
			{
				return false;
			}

			if(ReferenceEquals(this, obj))
			{
				return true;
			}

			if(obj is not Entity<TEntity, TKey> other)
			{
				return false;
			}

			if(this.HasSameNonDefaultIdentifierAs(other))
			{
				return true;
			}

			// Since the IDs aren't the same, both of them must be transient to
			// compare domain signatures; because if one is transient and the
			// other is a persisted entity, then they cannot be the same object.
			return this.GetType() == other.GetUnProxiedType()
				&& this.IsTransient
				&& other.IsTransient
				&& this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				// It is possible for two objects to return the same hash code based on
				// identically valued properties, even if they are of different types,
				// so we include the value object type in the hash calculation
				int hashCode = this.GetType().GetHashCode();

				foreach(object component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						hashCode = hashCode * HashMultiplier ^ component.GetHashCode();
					}
				}

				return hashCode;
			}
		}

		/// <summary>
		///     Some OR mappers may create dynamic proxies , so this method
		///     gets into the proxied object to get its actual type.
		/// </summary>
		/// <returns></returns>
		public virtual Type GetUnProxiedType()
		{
			return this.GetType();
		}

		/// <summary>
		///     Gets all components of the entity that are used for equality (domain signature). <br />
		///     The default implementation get all properties via reflection. One
		///     can at any time override this behavior with a manual or custom implementation.
		///     <br /><br />
		///     To add properties to the domain signature you should decorate the appropriate property(s)
		///     with [DomainSignature] and they will be compared automatically.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		protected virtual IEnumerable<object> GetEqualityComponents()
		{
			PropertyAccessor[] propertyAccessors = PropertyAccessor.GetPropertyAccessors(this.GetType(),
				property =>
				{
					bool isDomainSignatureAttribute = property.IsDefined(typeof(DomainSignatureAttribute), true);
					if(isDomainSignatureAttribute)
					{
						if(property.Name is nameof(this.ID) or nameof(DomainEvents) or nameof(this.IsTransient))
						{
							throw new InvalidOperationException($"The property {property.Name} can't belong to the domain signature.");
						}
					}

					return isDomainSignatureAttribute;
				});

			foreach(PropertyAccessor accessor in propertyAccessors)
			{
				object value = accessor.Invoke(this);
				yield return value;
			}
		}

		/// <summary>
		///     Returns true if self and the provided entity have the same ID values
		///     and the IDs are not of the default ID value.
		/// </summary>
		private bool HasSameNonDefaultIdentifierAs(Entity<TEntity, TKey> compareTo)
		{
			return !this.IsTransient && !compareTo.IsTransient && Equals(this.ID, compareTo.ID);
		}
	}
}
