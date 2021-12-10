namespace Fluxera.Entity.DomainEvents
{
	using JetBrains.Annotations;

	/// <summary>
	///     An interface for identifying domain events.
	/// </summary>
	[PublicAPI]
	public interface IDomainEvent
	{
		string DisplayName => this.GetType().Name;
	}
}
