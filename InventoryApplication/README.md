# Inventory Management Application
A simple inventory management application created in C# Console Application that does CRUD operations in an MSSQL server database.

## How to use
- To connect the program to your database, open the project solution, and connect to your database.
- Open Program.cs and go to line 13
- Change the string to the Connection String of your database
``string connectionString = "Data Source=Adrian\\MSSQLSERVER01;Initial Catalog=InventoryCRUD;Integrated Security=True;Trust Server Certificate=True";``
to
``string connectionString = "Data Source=YourDatabase;Initial Catalog=YourTable;Integrated Security=True;Trust Server Certificate=True";``
