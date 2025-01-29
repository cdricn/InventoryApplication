# Inventory Management Application
A simple inventory management application created in C# Console Application connected to an MSSQL server database.

## How to connect your database
- Open the project solution, go to server explorer, and click ``Connect to Database``.
- Fill in your database details and press OK.
- Click properties and copy the Connection String.
- Open Program.cs and go to line 13
- Change the value of this line to the Connection String of your database. 
<br />``string connectionString = "Data Source=Adrian\\MSSQLSERVER01;Initial Catalog=InventoryCRUD;Integrated Security=True;Trust Server Certificate=True";``
<br />to
<br />``string connectionString = "Data Source=YourDatabase;Initial Catalog=YourTable;Integrated Security=True;Trust Server Certificate=True";``

## Problems
- If getting "keyword not supported" error, try removing the 'Trust Server Certificate=True' from the connection string.
- If getting a certificate issue when connecting the database, check Trust Server Certificate when filling in database details.

## Images:
Main Page
![image](https://github.com/user-attachments/assets/e3ff4e84-c56f-44fb-8e89-2f7d3ad2a637)
View products
![image](https://github.com/user-attachments/assets/a2064969-da49-4607-b80d-e1cdcfdbf91b)
Get total inventory price
![image](https://github.com/user-attachments/assets/a00d9a84-4828-4816-94a1-baf0904313a4)
Add a new product
![image](https://github.com/user-attachments/assets/8fddcdf3-e6ca-475e-91e9-7e340f3143cc)
Update a product
![image](https://github.com/user-attachments/assets/7f9c3c47-28d4-4284-aa92-d6b59f74432a)
Remove a product
![image](https://github.com/user-attachments/assets/29df3694-3261-48b3-855d-427769d28195)
Products table after removing an entry and exiting program
![image](https://github.com/user-attachments/assets/b43c10a4-e139-48a8-a067-16f236e20720)

