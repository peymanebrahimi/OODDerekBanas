namespace BankingSystem;

public class DomainEvent
{
    public DateTime OccurredOn { get; private set; }

    public DomainEvent()
    {
        OccurredOn = DateTime.Now;
    }
}