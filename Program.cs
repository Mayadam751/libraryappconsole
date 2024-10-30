using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsCheckedOut { get; private set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsCheckedOut = false;
    }

    public void CheckOut()
    {
        if (IsCheckedOut)
            throw new InvalidOperationException("The book is already checked out.");

        IsCheckedOut = true;
    }

    public void Return()
    {
        if (!IsCheckedOut)
            throw new InvalidOperationException("The book is not checked out.");

        IsCheckedOut = false;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Available: {!IsCheckedOut}";
    }
}


public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        if (books.Exists(b => b.ISBN == book.ISBN))
            throw new InvalidOperationException("A book with this ISBN already exists.");

        books.Add(book);
    }

    public void RemoveBook(string isbn)
    {
        var book = books.Find(b => b.ISBN == isbn);
        if (book == null)
            throw new KeyNotFoundException("The book was not found in the library.");

        books.Remove(book);
    }

    public void CheckOutBook(string isbn)
    {
        var book = books.Find(b => b.ISBN == isbn);
        if (book == null)
            throw new KeyNotFoundException("The book was not found in the library.");

        book.CheckOut();
    }

    public void ReturnBook(string isbn)
    {
        var book = books.Find(b => b.ISBN == isbn);
        if (book == null)
            throw new KeyNotFoundException("The book was not found in the library.");

        book.Return();
    }

    public void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available in the library.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nLibrary Menu:");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. View all books");
            Console.WriteLine("4. Check out a book");
            Console.WriteLine("5. Return a book");
            Console.WriteLine("6. Exit the system");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();
                        Console.Write("Enter ISBN: ");
                        string isbn = Console.ReadLine();
                        library.AddBook(new Book(title, author, isbn));
                        Console.WriteLine("Book added successfully.");
                        break;

                    case "2":
                        Console.Write("Enter ISBN to remove: ");
                        library.RemoveBook(Console.ReadLine());
                        Console.WriteLine("Book removed successfully.");
                        break;

                    case "3":
                        library.DisplayBooks();
                        break;

                    case "4":
                        Console.Write("Enter ISBN to check out: ");
                        library.CheckOutBook(Console.ReadLine());
                        Console.WriteLine("Book checked out successfully.");
                        break;

                    case "5":
                        Console.Write("Enter ISBN to return: ");
                        library.ReturnBook(Console.ReadLine());
                        Console.WriteLine("Book returned successfully.");
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
