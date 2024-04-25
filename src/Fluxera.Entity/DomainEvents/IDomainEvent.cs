namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     A marker interface for domain events.
	/// </summary>
	[PublicAPI]
	public interface IDomainEvent : INotification
	{
		/// <summary>
		///     Gets the name of the event.
		/// </summary>
		string DisplayName => this.GetType().Name.Replace("DomainEvent", string.Empty);
	}
}
