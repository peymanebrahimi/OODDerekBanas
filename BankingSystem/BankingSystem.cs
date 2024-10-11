namespace BankingSystem;

public class BankingSystem
{
    private List<Customer> customers;
    private List<Account> accounts;
    private List<Transaction> transactions;
    private List<LoanApplication> loanApplications;
    private NotificationService notificationService;
    public AuthenticationService AuthenticationService;

    public BankingSystem()
    {
        customers = new List<Customer>();
        accounts = new List<Account>();
        transactions = new List<Transaction>();
        loanApplications = new List<LoanApplication>();
        notificationService = new NotificationService();
        AuthenticationService = new AuthenticationService();

        // Subscribe event handlers
        EventPublisher.DomainEventRaised += domainEvent =>
        {
            if (domainEvent is BalanceLowEvent balanceLowEvent)
            {
                new BalanceLowEventHandler(notificationService).Handle(balanceLowEvent);
            }
        };

        EventPublisher.IntegrationEventRaised += integrationEvent =>
        {
            if (integrationEvent is LoanApprovedEvent loanApprovedEvent)
            {
                new LoanApprovedEventHandler(notificationService).Handle(loanApprovedEvent);
            }
        };
    }

    public void AddCustomer(Customer customer)
    {
        customers.Add(customer);
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
        account.Owner.AddAccount(account);
    }

    public void ExecuteTransaction(Account sourceAccount, Account targetAccount, decimal amount)
    {
        if (sourceAccount == null || targetAccount == null)
        {
            Console.WriteLine("Invalid account information.");
            return;
        }

        var transaction = new Transaction(transactions.Count + 1, sourceAccount, targetAccount, amount);
        transaction.Execute();
        transactions.Add(transaction);
        notificationService.SendTransactionNotification(sourceAccount.Owner, transaction);
        notificationService.SendTransactionNotification(targetAccount.Owner, transaction);
    }

    public void ApplyForLoan(Customer customer, decimal amount, decimal interestRate)
    {
        var loanApplication = new LoanApplication(loanApplications.Count + 1, customer, amount, interestRate);
        loanApplications.Add(loanApplication);
        Console.WriteLine($"Loan application {loanApplication.ApplicationID} submitted for {customer.Name}");

        // Simple approval logic
        if (amount <= 5000) // example condition
        {
            loanApplication.Approve();
            notificationService.SendLoanApprovalNotification(customer, loanApplication);
            var loanAccount = new LoanAccount(accounts.Count + 1, customer, amount, interestRate);
            AddAccount(loanAccount);
        }
        else
        {
            loanApplication.Reject();
            notificationService.SendLoanRejectionNotification(customer, loanApplication);
        }
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