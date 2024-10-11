namespace BankingSystem;

public class BalanceLowEventHandler
{
    private readonly NotificationService _notificationService;

    public BalanceLowEventHandler(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Handle(BalanceLowEvent balanceLowEvent)
    {
        // Handle the low balance event (e.g., send a notification)
        var account = bankingSystem.Accounts.First(a => a.AccountNumber == balanceLowEvent.AccountNumber);
        _notificationService.SendLowBalanceAlert(account.Owner, account);
    }
}