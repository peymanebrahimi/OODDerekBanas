namespace BankingSystem;

public class CheckingAccount : Account
{
    public CheckingAccount(int accountNumber, Customer owner)
        : base(accountNumber, owner) { }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"{amount} deposited to Checking Account {AccountNumber}");
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"{amount} withdrawn from Checking Account {AccountNumber}");

            if (Balance < 100) // Assuming 100 is the threshold for low balance
            {
                // Raise domain event
                var balanceLowEvent = new BalanceLowEvent(AccountNumber, Balance);
                EventPublisher.Raise(balanceLowEvent);
            }
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[CheckingAccount] AccountNumber: {AccountNumber}, Owner: {Owner.Name}, Balance: {Balance}");
    }
}