// See https://aka.ms/new-console-template for more information

using BankingSystem;

Console.WriteLine("Hello, World!");
// Create the banking system
BankingSys bankingSystem = new BankingSys();

// Register and authenticate users
bankingSystem.AuthenticationService.Register("alice", "password123");
bankingSystem.AuthenticationService.Register("bob", "password456");

if (!bankingSystem.AuthenticationService.Authenticate("alice", "password123"))
{
    return;
}

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

// Apply for a loan
bankingSystem.ApplyForLoan(customer1, 3000, 5.0m);


// Example transaction to trigger domain event
bankingSystem.ExecuteTransaction(savingsAccount, checkingAccount, 950); // Assuming this causes low balance

// Example loan approval to trigger integration event
bankingSystem.ApplyForLoan(customer1, 3000, 5.0m);


// List all customers, accounts, and transactions
Console.WriteLine("Customers:");
bankingSystem.ListCustomers();

Console.WriteLine("Accounts:");
bankingSystem.ListAccounts();

Console.WriteLine("Transactions:");
bankingSystem.ListTransactions();