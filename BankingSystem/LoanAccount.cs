namespace BankingSystem;

public class LoanAccount : Account
{
    public decimal LoanAmount { get; private set; }
    public decimal InterestRate { get; private set; }

    public LoanAccount(int accountNumber, Customer owner, decimal loanAmount, decimal interestRate)
        : base(accountNumber, owner)
    {
        LoanAmount = loanAmount;
        InterestRate = interestRate;
        Balance = loanAmount;  // Initially, balance is the loan amount
    }

    public override void Deposit(decimal amount)
    {
        Balance -= amount;
        Console.WriteLine($"{amount} paid towards Loan Account {AccountNumber}. Remaining balance: {Balance}");
    }

    public override void Withdraw(decimal amount)
    {
        Console.WriteLine("Withdrawals are not allowed from a loan account.");
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[LoanAccount] AccountNumber: {AccountNumber}, Owner: {Owner.Name}, LoanAmount: {LoanAmount}, InterestRate: {InterestRate}, Balance: {Balance}");
    }
}