namespace Domain.Primitives;

public abstract class AgregateRoot
{
    private readonly List<DomainEvents> _domainEvents = new List<DomainEvents>();

    public ICollection<DomainEvents> GetDomainEventsCollection() => _domainEvents;

    protected void Raise(DomainEvents domainEvents)
    {
        _domainEvents.Add(domainEvents);
    }
}