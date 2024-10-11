using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBookLibrary2;
using System;
using System.Collections.Generic;

// Define the ILibraryItem interface
public interface ILibraryItem
{
    string Title { get; }
    string Author { get; }
    string ISBN { get; }
    void DisplayInfo();
}

// Define the abstract Book class implementing ILibraryItem
public abstract class Book : ILibraryItem
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }

    protected Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
    }

    public abstract void DisplayInfo();
}

// Define the EBook class inheriting from Book
public class EBook : Book
{
    public double FileSize { get; set; }

    public EBook(string title, string author, string isbn, double fileSize)
        : base(title, author, isbn)
    {
        FileSize = fileSize;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[EBook] Title: {Title}, Author: {Author}, ISBN: {ISBN}, File Size: {FileSize}MB");
    }
}

// Define the PrintedBook class inheriting from Book
public class PrintedBook : Book
{
    public int PageCount { get; set; }

    public PrintedBook(string title, string author, string isbn, int pageCount)
        : base(title, author, isbn)
    {
        PageCount = pageCount;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[PrintedBook] Title: {Title}, Author: {Author}, ISBN: {ISBN}, Page Count: {PageCount}");
    }
}

// Define the LibraryMember class
public class LibraryMember
{
    public string Name { get; set; }
    public int MemberID { get; set; }

    public LibraryMember(string name, int memberId)
    {
        Name = name;
        MemberID = memberId;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, MemberID: {MemberID}");
    }
}

// Define the LibraryTransaction class
public class LibraryTransaction
{
    public LibraryMember Member { get; set; }
    public ILibraryItem Item { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }

    public LibraryTransaction(LibraryMember member, ILibraryItem item)
    {
        Member = member;
        Item = item;
        BorrowedDate = DateTime.Now;
    }

    public void ReturnItem()
    {
        ReturnedDate = DateTime.Now;
        Console.WriteLine($"{Member.Name} returned {Item.Title} on {ReturnedDate}");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Member: {Member.Name}, Item: {Item.Title}, Borrowed on: {BorrowedDate}, Returned on: {ReturnedDate?.ToString() ?? "Not Returned Yet"}");
    }
}

// Define custom exception
public class LibraryException : Exception
{
    public LibraryException(string message) : base(message) { }
}

// Define the Library class
public class Library
{
    private List<ILibraryItem> items;
    private List<LibraryMember> members;
    private List<LibraryTransaction> transactions;

    public Library()
    {
        items = new List<ILibraryItem>();
        members = new List<LibraryMember>();
        transactions = new List<LibraryTransaction>();
    }

    public void AddItem(ILibraryItem item)
    {
        if (item == null)
        {
            throw new LibraryException("Cannot add null item to the library.");
        }
        items.Add(item);
    }

    public void AddMember(LibraryMember member)
    {
        if (member == null)
        {
            throw new LibraryException("Cannot add null member to the library.");
        }
        members.Add(member);
    }

    public void BorrowItem(LibraryMember member, ILibraryItem item)
    {
        if (member == null || item == null)
        {
            throw new LibraryException("Member and item must not be null.");
        }
        if (!items.Contains(item))
        {
            throw new LibraryException($"{item.Title} is not available in the library.");
        }
        transactions.Add(new LibraryTransaction(member, item));
        Console.WriteLine($"{member.Name} borrowed {item.Title}");
    }

    public void ReturnItem(LibraryMember member, ILibraryItem item)
    {
        if (member == null || item == null)
        {
            throw new LibraryException("Member and item must not be null.");
        }
        var transaction = transactions.Find(t => t.Member == member && t.Item == item && !t.ReturnedDate.HasValue);
        if (transaction != null)
        {
            transaction.ReturnItem();
        }
        else
        {
            throw new LibraryException($"{member.Name} has not borrowed {item.Title}");
        }
    }

    public void ListItems()
    {
        foreach (var item in items)
        {
            item.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void ListMembers()
    {
        foreach (var member in members)
        {
            member.DisplayInfo();
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

// Factory class to create books
public static class BookFactory
{
    public static Book CreateBook(string type, string title, string author, string isbn, double sizeOrPageCount)
    {
        if (type == "EBook")
        {
            return new EBook(title, author, isbn, sizeOrPageCount);
        }
        else if (type == "PrintedBook")
        {
            return new PrintedBook(title, author, isbn, (int)sizeOrPageCount);
        }
        else
        {
            throw new LibraryException("Invalid book type.");
        }
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        // Create the library
        Library library = new Library();

        // Add some items using the factory
        library.AddItem(BookFactory.CreateBook("EBook", "Digital Fortress", "Dan Brown", "1234567890", 2.5));
        library.AddItem(BookFactory.CreateBook("PrintedBook", "1984", "George Orwell", "0987654321", 328));

        // Add some members
        LibraryMember member1 = new LibraryMember("Alice", 1);
        LibraryMember member2 = new LibraryMember("Bob", 2);
        library.AddMember(member1);
        library.AddMember(member2);

        // Borrow items
        library.BorrowItem(member1, library.SearchByISBN("1234567890"));
        library.BorrowItem(member2, library.SearchByISBN("0987654321"));

        // List items, members, and transactions
        Console.WriteLine("Library Items:");
        library.ListItems();

        Console.WriteLine("Library Members:");
        library.ListMembers();

        Console.WriteLine("Library Transactions:");
        library.ListTransactions();

        // Return items
        library.ReturnItem(member1, library.SearchByISBN("1234567890"));

        // List transactions after return
        Console.WriteLine("\nLibrary Transactions after return:");
        library.ListTransactions();
    }
}

