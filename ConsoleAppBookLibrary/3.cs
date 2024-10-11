using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBookLibrary;
// Customer entity in Customer Context
public class Customer
{
    public int CustomerID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Customer(int customerId, string name, string email)
    {
        CustomerID = customerId;
        Name = name;
        Email = email;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"CustomerID: {CustomerID}, Name: {Name}, Email: {Email}");
    }
}
// Account entity in Account Context
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

// SavingsAccount class inheriting from Account
public class SavingsAccount : Account
{
    public SavingsAccount(int accountNumber, Customer owner)
        : base(accountNumber, owner) { }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"{amount} deposited to Savings Account {AccountNumber}");
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"{amount} withdrawn from Savings Account {AccountNumber}");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[SavingsAccount] AccountNumber: {AccountNumber}, Owner: {Owner.Name}, Balance: {Balance}");
    }
}

// CheckingAccount class inheriting from Account
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
// Transaction entity in Transaction Context
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
// Notification service in Notification Context
public class NotificationService
{
    public void SendTransactionNotification(Customer customer, Transaction transaction)
    {
        Console.WriteLine($"Email sent to {customer.Email}: Transaction {transaction.TransactionID} of {transaction.Amount} executed on {transaction.Date}.");
    }

    public void SendLowBalanceAlert(Customer customer, Account account)
    {
        if (account.Balance < 100) // Threshold for low balance alert
        {
            Console.WriteLine($"Email sent to {customer.Email}: Low balance alert for account {account.AccountNumber}. Current balance: {account.Balance}.");
        }
    }
}
public class BankingSystem
{
    private List<Customer> customers;
    private List<Account> accounts;
    private List<Transaction> transactions;
    private NotificationService notificationService;

    public BankingSystem()
    {
        customers = new List<Customer>();
        accounts = new List<Account>();
        transactions = new List<Transaction>();
        notificationService = new NotificationService();
    }

    public void AddCustomer(Customer customer)
    {
        customers.Add(customer);
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public void ExecuteTransaction(Account sourceAccount, Account targetAccount, decimal amount)
    {
        var transaction = new Transaction(transactions.Count + 1, sourceAccount, targetAccount, amount);
        transaction.Execute();
        transactions.Add(transaction);
        notificationService.SendTransactionNotification(sourceAccount.Owner, transaction);
        notificationService.SendTransactionNotification(targetAccount.Owner, transaction);
    }

    public void ListCustomers()
    {
        foreach (var customer in customers)
        {
            customer.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void ListAccounts()
    {
        foreach (var account in accounts)
        {
            account.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void ListTransactions()
    {
        foreach (var transaction in transactions)
        {
            transaction.DisplayInfo();
            Console.WriteLine();
        }
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        // Create the banking system
        BankingSystem bankingSystem = new BankingSystem();

        // Add some customers
        var customer1 = new Customer(1, "Alice", "alice@example.com");
        var customer2 = new Customer(2, "Bob", "bob@example.com");
        bankingSystem.AddCustomer(customer1);
        bankingSystem.AddCustomer(customer2);

        // Add some accounts
        var savingsAccount = new SavingsAccount(1001, customer1);
        var checkingAccount = new CheckingAccount(1002, customer2);
        bankingSystem.AddAccount(savingsAccount);
        bankingSystem.AddAccount(checkingAccount);

        // Execute some transactions
        bankingSystem.ExecuteTransaction(savingsAccount, checkingAccount, 500);

        // List all customers, accounts, and transactions
        Console.WriteLine("Customers:");
        bankingSystem.ListCustomers();

        Console.WriteLine("Accounts:");
        bankingSystem.ListAccounts();

        Console.WriteLine("Transactions:");
        bankingSystem.ListTransactions();
    }
}

