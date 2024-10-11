namespace BankingSystem;

public class Transaction
{
    public int TransactionID { get; private set; }
    public Account SourceAccount { get; private set; }
    public Account TargetAccount { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }

    public Transaction(int transactionID, Account sourceAccount, Account targetAccount, decimal amount)
    {
        TransactionID = transactionID;
        SourceAccount = sourceAccount;
        TargetAccount = targetAccount;
        Amount = amount;
        Date = DateTime.Now;
    }

    public void Execute()
    {
        SourceAccount.Withdraw(Amount);
        TargetAccount.Deposit(Amount);
        Console.WriteLine($"Transaction {TransactionID} executed: {Amount} transferred from {SourceAccount.AccountNumber} to {TargetAccount.AccountNumber}");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"TransactionID: {TransactionID}, From Account: {SourceAccount.AccountNumber}, To Account: {TargetAccount.AccountNumber}, Amount: {Amount}, Date: {Date}");
    }
}