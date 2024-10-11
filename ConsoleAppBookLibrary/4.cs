using ConsoleAppBookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBookLibrary4;
// Customer service interface
public interface ICustomerService
{
    Customer CreateCustomer(string name, string email);
    Customer GetCustomer(int customerId);
}

// Customer service implementation
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer CreateCustomer(string name, string email)
    {
        var customer = new Customer(_customerRepository.GetNextId(), name, email);
        _customerRepository.AddCustomer(customer);
        return customer;
    }

    public Customer GetCustomer(int customerId)
    {
        return _customerRepository.GetCustomer(customerId);
    }
}
// Account service interface
public interface IAccountService
{
    Account CreateAccount(string accountType, Customer owner);
    Account GetAccount(int accountNumber);
    void Deposit(int accountNumber, decimal amount);
    void Withdraw(int accountNumber, decimal amount);
}

// Account service implementation
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account CreateAccount(string accountType, Customer owner)
    {
        Account account = accountType switch
        {
            "Savings" => new SavingsAccount(_accountRepository.GetNextAccountNumber(), owner),
            "Checking" => new CheckingAccount(_accountRepository.GetNextAccountNumber(), owner),
            "Business" => new BusinessAccount(_accountRepository.GetNextAccountNumber(), owner),
            _ => throw new ArgumentException("Invalid account type.")
        };

        _accountRepository.AddAccount(account);
        return account;
    }

    public Account GetAccount(int accountNumber)
    {
        return _accountRepository.GetAccount(accountNumber);
    }

    public void Deposit(int accountNumber, decimal amount)
    {
        var account = _accountRepository.GetAccount(accountNumber);
        account.Deposit(amount);
    }

    public void Withdraw(int accountNumber, decimal amount)
    {
        var account = _accountRepository.GetAccount(accountNumber);
        account.Withdraw(amount);
    }
}

// BusinessAccount class inheriting from Account
public class BusinessAccount : Account
{
    public BusinessAccount(int accountNumber, Customer owner)
        : base(accountNumber, owner) { }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"{amount} deposited to Business Account {AccountNumber}");
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"{amount} withdrawn from Business Account {AccountNumber}");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[BusinessAccount] AccountNumber: {AccountNumber}, Owner: {Owner.Name}, Balance: {Balance}");
    }
}
// Loan service interface
public interface ILoanService
{
    Loan ApplyForLoan(Customer customer, decimal amount);
    void MakeRepayment(int loanId, decimal amount);
}

// Loan service implementation
public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public Loan ApplyForLoan(Customer customer, decimal amount)
    {
        var loan = new Loan(_loanRepository.GetNextLoanId(), customer, amount);
        _loanRepository.AddLoan(loan);
        return loan;
    }

    public void MakeRepayment(int loanId, decimal amount)
    {
        var loan = _loanRepository.GetLoan(loanId);
        loan.MakeRepayment(amount);
    }
}

// Loan entity
public class Loan
{
    public int LoanId { get; private set; }
    public Customer Borrower { get; private set; }
    public decimal PrincipalAmount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public DateTime LoanDate { get; private set; }

    public Loan(int loanId, Customer borrower, decimal principalAmount)
    {
        LoanId = loanId;
        Borrower = borrower;
        PrincipalAmount = principalAmount;
        RemainingAmount = principalAmount;
        LoanDate = DateTime.Now;
    }

    public void MakeRepayment(decimal amount)
    {
        RemainingAmount -= amount;
        Console.WriteLine($"{Borrower.Name} made a repayment of {amount}. Remaining balance: {RemainingAmount}");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"LoanID: {LoanId}, Borrower: {Borrower.Name}, Principal: {PrincipalAmount}, Remaining: {RemainingAmount}, Loan Date: {LoanDate}");
    }
}
// Notification service interface
public interface INotificationService
{
    void SendTransactionNotification(Customer customer, string message);
    void SendLowBalanceAlert(Customer customer, Account account);
    void SendLoanApprovalNotification(Customer customer, Loan loan);
}

// Notification service implementation
public class NotificationService : INotificationService
{
    public void SendTransactionNotification(Customer customer, string message)
    {
        Console.WriteLine($"Email sent to {customer.Email}: {message}");
    }

    public void SendLowBalanceAlert(Customer customer, Account account)
    {
        if (account.Balance < 100)
        {
            Console.WriteLine($"Email sent to {customer.Email}: Low balance alert for account {account.AccountNumber}. Current balance: {account.Balance}");
        }
    }

    public void SendLoanApprovalNotification(Customer customer, Loan loan)
    {
        Console.WriteLine($"Email sent to {customer.Email}: Your loan of {loan.PrincipalAmount} has been approved. Loan ID: {loan.LoanId}");
    }
}
public class BankingSystem
{
    private readonly ICustomerService _customerService;
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;
    private readonly ILoanService _loanService;
    private readonly INotificationService _notificationService;

    public BankingSystem(ICustomerService customerService, IAccountService accountService, ITransactionService transactionService, ILoanService loanService, INotificationService notificationService)
    {
        _customerService = customerService;
        _accountService = accountService;
        _transactionService = transactionService;
        _loanService = loanService;
        _notificationService = notificationService;
    }

    public void RegisterCustomer(string name, string email)
    {
        var customer = _customerService.CreateCustomer(name, email);
        Console.WriteLine($"{customer.Name} has been registered with ID: {customer.CustomerID}");
    }

    public void OpenAccount(int customerId, string accountType)
    {
        var customer = _customerService.GetCustomer(customerId);
        var account = _accountService.CreateAccount(accountType, customer);
        Console.WriteLine($"{accountType} account opened for {customer.Name} with Account Number: {account.AccountNumber}");
    }

    public void Execute
