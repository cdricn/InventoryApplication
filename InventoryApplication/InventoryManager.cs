using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProductManagement
{
    internal class ProductManager
    {
        private readonly string _connectionString;

        public ProductManager(String connectionString)
        {
            _connectionString = connectionString;
        }

        // Checks if quantity is a positive integer and returns value
        private int CheckQuantity()
        {
            int quantity;
            Console.WriteLine("\nEnter the quantity of the product: ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                {
                    break;
                }
                else { Console.WriteLine("\nEnter a valid integer. Please try again:"); }
            }
            return quantity;
        }

        public void AddProduct()
        {
            // Declare variables
            string productName;
            int quantity;
            double price;

            // Get and checks if the name is not a null or empty string
            Console.WriteLine("\nEnter the name of the product: ");
            while (true)
            {
                productName = (Console.ReadLine() ?? String.Empty).Trim();
                if (string.IsNullOrEmpty(productName))
                {
                    Console.WriteLine("\nPlease Enter a name for product: ");
                }
                else { break; }
            }

            // Calls CheckQuantity function and stores in quantity
            quantity = CheckQuantity();

            // Get and checks if price is a valid positive number
            Console.WriteLine("\nEnter the price of the product: ");
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out price) && price > 0)
                {
                    break;
                }
                else { Console.WriteLine("\nInput valid number. Please try again:"); }
            }

            // Placed code in try catch in case of error
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Insert entry into Products table
                    string query = $"INSERT INTO Products(ProductID, ProductName, QuantityInStock, Price) VALUES (newid(), @ProductName, @QuantityInStock, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar);
                    command.Parameters["@ProductName"].Value = productName;

                    command.Parameters.Add("@QuantityInStock", SqlDbType.Int);
                    command.Parameters["@QuantityInStock"].Value = quantity;

                    command.Parameters.Add("@Price", SqlDbType.Float);
                    command.Parameters["@Price"].Value = price;

                    command.ExecuteNonQuery();

                    Console.WriteLine("\nProduct added successfully!");
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Action failed when inserting a product to table" + "\n " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred." + "\n " + e);
            }
        }
        
        public void RemoveProduct()
        {
            Console.WriteLine("\nEnter the ID of the product you want to remove:");

            // Placed code in try catch in case of error
            try
            {
                Guid ProductID = Guid.Parse(Console.ReadLine() ?? String.Empty);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Delete the entry from Products table that matches the input GUID
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Parameterize query
                    command.Parameters.Add("@ProductID", SqlDbType.UniqueIdentifier);
                    command.Parameters["@ProductID"].Value = ProductID;

                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if query removed an entry
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nProduct removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nProduct ID does not exist.");
                    }

                    connection.Close();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid product ID format. Please enter a valid GUID." + "\n " + e);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Action failed when deleting product from table" + "\n " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred." + "\n " + e);
            }
        }
        
        public void UpdateProduct()
        {
            Guid id;
            int quantity;

            // Placed code in try catch in case of error
            try
            {
                Console.WriteLine("\nEnter ProductID: ");
                id = Guid.Parse(Console.ReadLine() ?? String.Empty);

                // Calls CheckQuantity function and stores in quantity
                quantity = CheckQuantity();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = $"UPDATE Products SET QuantityInStock = @QuantityInStock WHERE ProductID = '{id}'";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.Add("@QuantityInStock", SqlDbType.Int);
                    command.Parameters["@QuantityInStock"].Value = quantity;

                    command.ExecuteNonQuery();

                    Console.WriteLine("\nProduct updated successfully!");

                    connection.Close();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid product ID format. Please enter a valid GUID." + "\n " + e);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Action failed when updating product from table" + "\n " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred." + "\n " + e);
            }
        }

        public void ListProducts()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Select all entries from Products table
                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop through the entries and display them in console
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["ProductID"]} | Name: {reader["ProductName"]} |" +
                                $" Quantity: {reader["QuantityInStock"]} | Price: {reader["Price"]}");
                        }
                    }
                }

                connection.Close();
            }

        }

        public void GetTotalValue()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Select the quantity and price columns from the Products table and sum them
                string query = "SELECT SUM(QuantityInStock * Price) AS TotalPrice FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"\nTotal price of inventory: {reader["TotalPrice"]}");
                        }
                    }
                }
            }
        }
    }
}
