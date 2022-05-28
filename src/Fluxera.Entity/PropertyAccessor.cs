﻿namespace Fluxera.Entity
{
	using System;
	using System.Collections.Concurrent;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	internal sealed class PropertyAccessor
	{
		private static readonly ConcurrentDictionary<Type, PropertyAccessor[]> PropertyAccessorsMap = new ConcurrentDictionary<Type, PropertyAccessor[]>();

		private static readonly MethodInfo CallInnerDelegateMethod = typeof(PropertyAccessor).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static)!;

		private PropertyAccessor(string propertyName, Func<object, object> getterFunc)
		{
			Guard.Against.NullOrWhiteSpace(propertyName, nameof(propertyName));
			Guard.Against.Null(getterFunc, nameof(getterFunc));

			this.PropertyName = propertyName;
			this.GetterFunc = getterFunc;
		}

		public string PropertyName { get; }

		private Func<object, object> GetterFunc { get; }

		public object Invoke(object target)
		{
			return this.GetterFunc.Invoke(target);
		}

		public static PropertyAccessor[] GetPropertyAccessors(Type type, Predicate<PropertyInfo> predicate)
		{
			return PropertyAccessorsMap
				.GetOrAdd(type, _ => type
					.GetProperties()
					.Where(predicate.Invoke)
					.Select(property =>
					{
						MethodInfo getMethod = property.GetMethod;
						Type declaringType = property.DeclaringType;
						Type propertyType = property.PropertyType;

						Type getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringType, propertyType);
						Delegate getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);
						MethodInfo callInnerGenericMethodWithTypes = CallInnerDelegateMethod.MakeGenericMethod(declaringType, propertyType);
						Func<object, object> getter = (Func<object, object>)callInnerGenericMethodWithTypes.Invoke(null, new object[] { getMethodDelegate });

						return new PropertyAccessor(property.Name, getter);
					}).ToArray());
		}

		// Called via reflection.
		private static Func<object, object> CallInnerDelegate<T, TResult>(Func<T, TResult> func)
		{
			return instance => func.Invoke((T)instance);
		}
	}
}
