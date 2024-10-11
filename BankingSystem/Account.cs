namespace BankingSystem;

public abstract class Account
{
    public int AccountNumber { get; private set; }
    public Customer Owner { get; private set; }
    public decimal Balance { get; protected set; }

    protected Account(int accountNumber, Customer owner)
    {
        AccountNumber = accountNumber;
        Owner = owner;
    }

    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);
    public abstract void DisplayInfo();
}