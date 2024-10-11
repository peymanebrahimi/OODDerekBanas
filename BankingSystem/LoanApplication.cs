namespace BankingSystem;

public class LoanApplication
{
    public int ApplicationID { get; private set; }
    public Customer Applicant { get; private set; }
    public decimal RequestedAmount { get; private set; }
    public decimal InterestRate { get; private set; }
    public DateTime ApplicationDate { get; private set; }
    public string Status { get; private set; }  // e.g., Pending, Approved, Rejected

    public LoanApplication(int applicationID, Customer applicant, decimal requestedAmount, decimal interestRate)
    {
        ApplicationID = applicationID;
        Applicant = applicant;
        RequestedAmount = requestedAmount;
        InterestRate = interestRate;
        ApplicationDate = DateTime.Now;
        Status = "Pending";
    }

    public void Approve()
    {
        Status = "Approved";
        Console.WriteLine($"Loan application {ApplicationID} approved.");
        // Raise domain event
        var loanApprovedEvent = new LoanApprovedEvent(ApplicationID, RequestedAmount, InterestRate);
        EventPublisher.Raise(loanApprovedEvent);
    }

    public void Reject()
    {
        Status = "Rejected";
        Console.WriteLine($"Loan application {ApplicationID} rejected.");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"ApplicationID: {ApplicationID}, Applicant: {Applicant.Name}, Amount: {RequestedAmount}, InterestRate: {InterestRate}, Date: {ApplicationDate}, Status: {Status}");
    }
}