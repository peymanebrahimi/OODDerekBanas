namespace BankingSystem;

public class IntegrationEvent
{
    public DateTime OccurredOn { get; private set; }

    public IntegrationEvent()
    {
        OccurredOn = DateTime.Now;
    }
}