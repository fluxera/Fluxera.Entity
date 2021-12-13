namespace Fluxera.Entity
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using ComponentModel.Annotations;
	using DomainEvents;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for all entities. Uses <see cref="string" /> as type for the ID.
	/// </summary>
	/// <typeparam name="TEntity">The entity type.</typeparam>
	[PublicAPI]
	public abstract class Entity<TEntity> : Entity<TEntity, string>
		where TEntity : Entity<TEntity>
	{
		[Ignore]
		public override bool IsTransient => string.IsNullOrWhiteSpace(this.ID);
	}

	/// <summary>
	///     A base class for all entities.
	/// </summary>
	/// <typeparam name="TEntity">The entity type.</typeparam>
	/// <typeparam name="TKey">The ID type.</typeparam>
	[PublicAPI]
	public abstract class Entity<TEntity, TKey> : INotifyPropertyChanging, INotifyPropertyChanged
		where TEntity : Entity<TEntity, TKey>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		/// <inheritdoc />
		public event PropertyChangingEventHandler? PropertyChanging;

		/// <inheritdoc />
		public event PropertyChangedEventHandler? PropertyChanged;

		protected Entity()
		{
			this.Init();
		}

		/// <summary>
		///     The unique ID of the entity.
		/// </summary>
		public virtual TKey? ID { get; set; }

		/// <summary>
		///     The domain events of this entity.
		/// </summary>
		[Ignore]
		public ICollection<IDomainEvent> DomainEvents { get; } = new List<IDomainEvent>();

		/// <summary>
		///		Gets a flag, if the entity instance is transient (not stored to the storage).
		/// </summary>
		[Ignore] 
		public virtual bool IsTransient => Equals(this.ID, default);

		public static bool operator ==(Entity<TEntity, TKey>? left, Entity<TEntity, TKey>? right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(Entity<TEntity, TKey>? left, Entity<TEntity, TKey>? right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if(obj is null)
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if(obj is not Entity<TEntity, TKey> other)
			{
				return false;
			}

			if (this.HasSameNonDefaultIdentifierAs(other))
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

				foreach(object? component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						hashCode = (hashCode * HashMultiplier) ^ component.GetHashCode();
					}
				}

				return hashCode;
			}
		}

		/// <summary>
		///		Some OR mappers may create dynamic proxies , so this method
		///		gets into the proxied object to get its actual type.
		/// </summary>
		/// <returns></returns>
		public virtual Type GetUnProxiedType()
		{
			return this.GetType();
		}

		/// <summary>
		///		Gets all components of the entity that are used for equality (domain signature). <br/>
		///		The default implementation get all properties via reflection. One
		///		can at any time override this behavior with a manual or custom implementation.
		///		<br/><br/>
		///		To add properties to the domain signature you should decorate the appropriate property(s)
		///		with [DomainSignature] and they will be compared automatically.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		protected virtual IEnumerable<object?> GetEqualityComponents()
		{
			PropertyAccessor[] propertyAccessors = PropertyAccessor.GetPropertyAccessors(this.GetType(), 
				property =>
				{
					bool isDomainSignatureAttribute = property.IsDefined(typeof(DomainSignatureAttribute), true);
					if(isDomainSignatureAttribute)
					{
						if(property.Name is nameof(this.ID) or nameof(this.DomainEvents) or nameof(this.IsTransient))
						{
							throw new InvalidOperationException($"The property {property.Name} cannot belong to the domain signature.");
						}
					}
					return isDomainSignatureAttribute;
				});

			foreach(PropertyAccessor accessor in propertyAccessors)
			{
				object? value = accessor.Invoke(this);
				yield return value;
			}
		}

		protected void SetAndNotify<T>(ref T? field, T? value, [CallerMemberName] string propertyName = null!)
		{
			if(!Equals(field, value))
			{
				OnPropertyChanging(propertyName);
				field = value;
				OnPropertyChanged(propertyName);
			}
		}

		protected virtual void OnPropertyChanging(string propertyName)
		{
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		///     Returns true if self and the provided entity have the same ID values
		///     and the IDs are not of the default ID value.
		/// </summary>
		private bool HasSameNonDefaultIdentifierAs(Entity<TEntity, TKey> compareTo)
		{
			return !this.IsTransient && !compareTo.IsTransient && Equals(this.ID, compareTo.ID);
		}

		private void Init()
		{
			this.ID = default;
		}
	}
}
