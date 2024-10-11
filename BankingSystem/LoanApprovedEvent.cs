namespace BankingSystem;

public class LoanApprovedEvent : IntegrationEvent
{
    public int LoanAccountNumber { get; private set; }
    public decimal LoanAmount { get; private set; }
    public decimal InterestRate { get; private set; }

    public LoanApprovedEvent(int loanAccountNumber, decimal loanAmount, decimal interestRate)
    {
        LoanAccountNumber = loanAccountNumber;
        LoanAmount = loanAmount;
        InterestRate = interestRate;
    }
}