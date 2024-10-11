namespace BankingSystem;

public class BalanceLowEvent : DomainEvent
{
    public int AccountNumber { get; private set; }
    public decimal Balance { get; private set; }

    public BalanceLowEvent(int accountNumber, decimal balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
    }
}