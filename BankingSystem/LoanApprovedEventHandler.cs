namespace BankingSystem;

public class LoanApprovedEventHandler
{
    private readonly NotificationService _notificationService;

    public LoanApprovedEventHandler(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Handle(LoanApprovedEvent loanApprovedEvent)
    {
        // Handle the loan approved event (e.g., send a notification)
        var account = bankingSystem.Accounts.First(a => a.AccountNumber == loanApprovedEvent.LoanAccountNumber);
        _notificationService.SendLoanApprovalNotification(account.Owner, loanApprovedEvent.LoanAmount, loanApprovedEvent.InterestRate);
    }
}