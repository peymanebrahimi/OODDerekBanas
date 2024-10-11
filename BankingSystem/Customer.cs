using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem;


/*
 Certainly! Let’s break down the problem space for this more sophisticated banking system using Domain-Driven Design (DDD) principles.

Problem Space:
The banking system needs to manage multiple aspects of banking operations, covering various contexts like customer management, account operations, transactions, loans, notifications, and security. Here's an overview of each context and its responsibilities:

Customer Context:

Manage Customer Data: Store and manage information about customers such as name, email, and their associated accounts.

Add New Customers: Allow for the addition of new customers to the banking system.

Customer Display: Provide methods to display customer details.

Account Context:

Manage Different Types of Accounts: Handle multiple account types (savings, checking, loans).

Deposit and Withdraw Operations: Provide functionalities for depositing and withdrawing money from accounts.

Account Display: Allow account details to be displayed, including balance and owner information.

Transaction Context:

Handle Transactions: Manage transactions between accounts, including deposits, withdrawals, and transfers.

Track Transaction History: Maintain a record of all transactions for auditing and tracking purposes.

Transaction Display: Provide methods to display details of transactions.

Loan Context:

Loan Applications: Manage loan applications from customers, including details like requested amount, interest rate, and status (pending, approved, rejected).

Loan Approval/Rejection: Provide mechanisms to approve or reject loan applications based on certain criteria.

Loan Account Management: Handle loan accounts once loans are approved, including repayments.

Notification Context:

Send Notifications: Notify customers about various activities related to their accounts, such as transaction alerts, low balance alerts, loan approval/rejection notifications.

Low Balance Alerts: Monitor account balances and notify customers when balances fall below a specified threshold.

Security Context:

User Authentication: Handle the registration and authentication of users to secure the banking system.

Authorization: Ensure that only authenticated users can perform certain operations within the system.

Interactions:
The interactions between these contexts ensure the system's functionality:

Customer and Account Contexts: Customers can have multiple accounts of various types. Adding a customer should allow associating accounts with that customer.

Account and Transaction Contexts: Transactions involve source and target accounts, managing fund transfers, and updating account balances.

Transaction and Notification Contexts: After a transaction, notifications are sent to customers regarding the details of the transaction.

Loan and Notification Contexts: When a loan application is approved or rejected, a notification is sent to the customer.

Security and Other Contexts: Security ensures that only authenticated users can access customer and account data, perform transactions, or apply for loans.

This structure ensures a comprehensive and cohesive system, handling a wide range of banking operations while maintaining clear boundaries and interactions between different contexts.
 */
public class Customer
{
    public int CustomerID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public List<Account> Accounts { get; private set; }

    public Customer(int customerId, string name, string email)
    {
        CustomerID = customerId;
        Name = name;
        Email = email;
        Accounts = new List<Account>();
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"CustomerID: {CustomerID}, Name: {Name}, Email: {Email}");
    }

    public void AddAccount(Account account)
    {
        Accounts.Add(account);
    }
}

//add events

//Domain Event Handler
//Integration Event Handler

//Simple Event Publisher