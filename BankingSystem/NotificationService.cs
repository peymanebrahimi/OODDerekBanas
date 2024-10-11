namespace BankingSystem;

public class NotificationService
{
    public void SendTransactionNotification(Customer customer, Transaction transaction)
    {
        Console.WriteLine($"Email sent to {customer.Email}: Transaction {transaction.TransactionID} of {transaction.Amount} executed on {transaction.Date}.");
    }

    public void SendLowBalanceAlert(Customer customer, Account account)
    {
        if (account.Balance < 100)
        {
            Console.WriteLine($"Email sent to {customer.Email}: Low balance alert for account {account.AccountNumber}. Current balance: {account.Balance}.");
        }
    }

    public void SendLoanApprovalNotification(Customer customer, LoanApplication loanApplication)
    {
        if (loanApplication.Status == "Approved")
        {
            Console.WriteLine($"Email sent to {customer.Email}: Your loan application {loanApplication.ApplicationID} has been approved.");
        }
    }

    public void SendLoanRejectionNotification(Customer customer, LoanApplication loanApplication)
    {
        if (loanApplication.Status == "Rejected")
        {
            Console.WriteLine($"Email sent to {customer.Email}: Your loan application {loanApplication.ApplicationID} has been rejected.");
        }
    }
}