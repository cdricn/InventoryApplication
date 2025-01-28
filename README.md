# Inventory Management Application
A simple inventory management application created in C# Console Application connected to an MSSQL server database.

## How to connect your database
- Open the project solution, go to server explorer, and click ``Connect to Database``.
- Fill in your database details and press OK.
- Click properties and copy the Connection String.
- Open Program.cs and go to line 13
- Change the value of this line to the Connection String of your database.
``string connectionString = "Data Source=Adrian\\MSSQLSERVER01;Initial Catalog=InventoryCRUD;Integrated Security=True;Trust Server Certificate=True";``
to
``string connectionString = "Data Source=YourDatabase;Initial Catalog=YourTable;Integrated Security=True;Trust Server Certificate=True";``

## Problems
- If getting "keyword not supported" error, try removing the 'Trust Server Certificate=True' from connection string.
- If getting a certificate issue, check Trust Server Certificate when filling in database details.
