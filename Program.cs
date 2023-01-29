using Microsoft.Data.SqlClient;
using dluznik3;
using System.Drawing;

new wrogram().Run();

public class wrogram
{
    public void Run()
    {
        SettleUpApp app = new SettleUpApp();
        /*
         * -1. login
         * 0. Register
         * 1. Create a transaction
         * 2. Get transactions for user
         * 4. Get user by ID
         * 5. Get user by username
         * 6. Exit
         */
        string username = " ";
        string password = " ";
        Console.WriteLine("Welcome to SettleUpApp!");
        Console.WriteLine("#######################");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
        Console.WriteLine("#######################");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 1)
        {
            Console.WriteLine("Please enter your username:");
            Console.WriteLine();
            username = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            password = Console.ReadLine();
            if (app.Login(username, password))
            {
                Console.WriteLine("Login successful!");
                Console.WriteLine("#######################");
                while (true)
                {

                    int option = NewMethod1();
                    if (option == 1)
                    {
                        NewMethod(app);
                    }
                    else if (option == 2)
                        NewMethod2(app);
                    else if (option == 4)
                    {
                        NewMethod4(app);
                    }
                    else if (option == 5)
                    {
                        username = NewMethod5(app);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Login failed!");
            }
        }
        else if (choice == 2)
        {
            NewMethod3(app, out username, out password);
            Console.WriteLine("Registration successful!");
            Console.WriteLine("#######################");
            while (true)
            {
                int option = NewMethod1();
                if (option == 1)
                {
                    NewMethod(app);
                }
                else if (option == 2)
                    NewMethod2(app);
                else if (option == 4)
                {
                    NewMethod4(app);
                }
                else if (option == 5)
                {
                    username = NewMethod5(app);
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Goodbye!");
        }
        
        
        
    }

    private static string NewMethod5(SettleUpApp app)
    {
        string username;
        Console.WriteLine("Enter username:");
        username = Console.ReadLine();
        var user = app.GetUserByUsername(username);
        Console.WriteLine($"User ID: {user.ID}");
        Console.WriteLine($"Username: {user.username}");
        Console.WriteLine($"Password: {user.password}");
        Console.WriteLine("succes!");
        Console.WriteLine("");
        Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
        return username;
    }

    private static void NewMethod4(SettleUpApp app)
    {
        Console.WriteLine("Enter user ID:");
        int userId = int.Parse(Console.ReadLine());
        var user = app.GetUserById(userId);
        Console.WriteLine($"User ID: {user.ID}");
        Console.WriteLine($"Username: {user.username}");
        Console.WriteLine($"Password: {user.password}");
        Console.WriteLine("succes!");
        Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
    }

    private static void NewMethod3(SettleUpApp app, out string username, out string password)
    {
        Console.WriteLine("Enter username:");
        username = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Enter password:");
        password = Console.ReadLine();
        app.CreateUser(username, password);
        Console.WriteLine("succes!");
        Console.WriteLine("");
        Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
    }

    private static void NewMethod2(SettleUpApp app)
    {
        Console.WriteLine("Enter user ID:");
        int userId = int.Parse(Console.ReadLine());
        var transactions = app.GetTransactionsForUser(userId);
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"Transaction ID: {transaction.ID}");
            Console.WriteLine($"Giver ID: {transaction.GID}");
            Console.WriteLine($"Receiver ID: {transaction.RID}");
            Console.WriteLine($"Amount: {transaction.Amount}");
            Console.WriteLine($"Date: {transaction.Date}");
            Console.WriteLine("succes!");
            Console.WriteLine("");
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
        }
    }

    private static int NewMethod1()
    {
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("Welcome to SettleUpApp!");
        Console.WriteLine("1. Create a transaction");
        Console.WriteLine("2. Get transactions for user");
        Console.WriteLine("3. Create user");
        Console.WriteLine("4. Get user by ID");
        Console.WriteLine("5. Get user by username");
        Console.WriteLine("6. Exit");
        int option = int.Parse(Console.ReadLine());
        return option;
    }

    private static void NewMethod(SettleUpApp app)
    {
        Console.WriteLine("Enter giver ID:");
        int giverId = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter receiver ID:");
        int receiverId = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter amount:");
        decimal amount = decimal.Parse(Console.ReadLine());
        Console.WriteLine($"giver ID: {giverId} receiver ID:{receiverId} amount:{amount} date: {DateTime.UtcNow} ");
        app.CreateTransaction(giverId, receiverId, amount);
        
        Console.WriteLine("succes!");
        Console.WriteLine("");
        Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
    }
}



