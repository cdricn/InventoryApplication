using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static ProductManagement.ProductManager;

namespace ProductManagement
{
    internal class ProductManager
    {
        private readonly string _connectionString;

        public ProductManager(String connectionString)
        {
            _connectionString = connectionString;
        }

        public class Product
        {
            public Guid ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }

        public void AddProduct()
        {
            Console.WriteLine("\nInput the name of the product: ");
            string productName = Console.ReadLine() ?? String.Empty;
            int quantity;
            double price;

            // Check if quantity is in proper format
            Console.WriteLine("\nInput the quantity of the product: ");
            quantity = int.Parse(Console.ReadLine() ?? String.Empty);

            // Check if price is in proper format
            Console.WriteLine("\nInput the price of the product: ");
            price = double.Parse(Console.ReadLine() ?? String.Empty);

            // Execute query
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = $"INSERT INTO Products(ProductID, ProductName, QuantityInStock, Price) VALUES (newid(), @ProductName, @QuantityInStock, @Price)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@QuantityInStock", quantity);
                        command.Parameters.AddWithValue("@Price", price);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine("\nProduct added successfully!");
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
                Console.WriteLine("Action failed when adding product from table" + "\n " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred." + "\n " + e);
            }
        }
        
        public void RemoveProduct()
        {
            Console.WriteLine("\nType the ID of the product you want to remove:");

            // Execute delete query
            try
            {
                Guid ProductID = Guid.Parse(Console.ReadLine() ?? String.Empty);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", ProductID);

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
            Console.WriteLine("\nEnter ProductID: ");
            Guid id = Guid.Parse(Console.ReadLine() ?? String.Empty);

            Console.WriteLine("\nEnter new item quantity: ");
            int quantity = Int32.Parse(Console.ReadLine() ?? String.Empty);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = $"UPDATE Products SET QuantityInStock = @Quantity WHERE ProductID = {id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void ListProducts()
        {
            List<Product> items = new List<Product>();

            // Execute query
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT ProductID, ProductName, QuantityInStock, Price FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop through and put each entry in List items
                        while (reader.Read())
                        {
                            items.Add(new Product
                            {
                                ProductID = (Guid)reader["ProductID"],
                                ProductName = (string)reader["ProductName"],
                                Quantity = (int)reader["QuantityInStock"],
                                Price = (double)reader["Price"],
                            });
                        }
                    }
                }

                connection.Close();
            }

            // Display List items
            foreach (Product item in items)
            {
                Console.WriteLine($"ID: {item.ProductID} | Name: {item.ProductName} |" +
                    $" Quantity: {item.Quantity} | Price: {item.Price}");
            }

        }

        public void GetTotalValue()
        {

        }
    }
}
