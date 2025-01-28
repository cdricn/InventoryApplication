using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProductManagement;
using static ProductManagement.ProductManager;

class Program
{
    static void Main(string[] args)
    {
        // Get Connection String from the connected database
        // If getting "keyword not supported" error, remove 'Trust Server Certificate=True' from connection string
        string connectionString = "Data Source=Adrian\\MSSQLSERVER01;Initial Catalog=InventoryCRUD;Integrated Security=True;Trust Server Certificate=True";
        ProductManager productManager = new ProductManager(connectionString);

        Choices(productManager);
    }

    private static void Choices(ProductManager productManager)
    {

        // UI Start
        Console.WriteLine("\nWelcome to Inventory Management App");
        Console.WriteLine("\nUse the Up and Down key to navigate and press the Enter key to select an option:");

        ConsoleKeyInfo key;
        (int left, int top) = Console.GetCursorPosition();
        bool isSelected = false;
        int selector = 1;
        string arrow = "->";

        // Options UI
        while (!isSelected)
        {
            // Prevent choices from being displayed again
            Console.SetCursorPosition(left, top);

            Console.WriteLine($" {(selector == 1 ? arrow : "  ")} View all products");
            Console.WriteLine($" {(selector == 2 ? arrow : "  ")} Get inventory price");
            Console.WriteLine($" {(selector == 3 ? arrow : "  ")} Add a product");
            Console.WriteLine($" {(selector == 4 ? arrow : "  ")} Update a product");
            Console.WriteLine($" {(selector == 5 ? arrow : "  ")} Remove a product");

            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selector--;
                    break;
                case ConsoleKey.DownArrow:
                    selector++;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;

            }

            // Make the selector loop through the options
            if (selector > 5)
            {
                selector = 1;
            }
            else if (selector < 1)
            {
                selector = 5;
            }
        }

        // Use class
        switch (selector)
        {
            case 1:
                Console.WriteLine("\nListing all Products...");
                productManager.ListProducts();
                break;
            case 2:
                Console.WriteLine("\nTotal Price:");
                break;
            case 3:
                Console.WriteLine("\nAdd a new product:");
                productManager.AddProduct();
                break;
            case 4:
                Console.WriteLine("\nUpdate product quantity:");
                productManager.UpdateProduct();
                break;
            case 5:
                Console.WriteLine("\nRemove a product:");
                Console.WriteLine("\nCurrent Product list: ");
                productManager.ListProducts();

                productManager.RemoveProduct();

                break;
        }
    
        // Ask user if they want to exit program
        Console.WriteLine("\nDo you want to exit?: y/n");
        string cont = Console.ReadLine() ?? String.Empty;
        switch (cont)
        {
            case "y":
                break;
            case "n":
                Console.Clear();
                Choices(productManager);
                break;
            default:
                Console.WriteLine("Exiting program");
                break;
        }
    }

    // Open connection with database
    private static void OpenSqlConnection(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Successfully connected to the server!");
        }
    }
}