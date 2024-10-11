namespace BankingSystem;

public static class EventPublisher
{
    public static event Action<DomainEvent> DomainEventRaised;
    public static event Action<IntegrationEvent> IntegrationEventRaised;

    public static void Raise(DomainEvent domainEvent)
    {
        DomainEventRaised?.Invoke(domainEvent);
    }

    public static void Raise(IntegrationEvent integrationEvent)
    {
        IntegrationEventRaised?.Invoke(integrationEvent);
    }
}