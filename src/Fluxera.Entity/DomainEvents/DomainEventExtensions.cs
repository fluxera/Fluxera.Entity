namespace Fluxera.Entity.DomainEvents
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;

	public static class DomainEventExtensions
	{
		public static bool IsDomainEventHandler(this Type type)
		{
			Guard.Against.Null(type, nameof(type));

			bool isEventHandler = type.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType
				&& ((x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))
					|| (x.GetGenericTypeDefinition() == typeof(ICommittedDomainEventHandler<>))));

			return isEventHandler;
		}
	}
}
