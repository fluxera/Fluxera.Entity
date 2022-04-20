namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///     An interface for identifying domain events.
	/// </summary>
	[PublicAPI]
	public interface IDomainEvent
	{
		/// <summary>
		///     Gets the name of the event.
		/// </summary>
		string DisplayName => this.GetType().Name;
	}
}
