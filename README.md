Simple Inventory Management System
=================================

A small console-based C# app to manage products (name, price, quantity).
Built for practicing OOP, collections, user I/O, and Git workflows.

Features
--------
- Add a product (with validation and duplicate-name check)
- View all products
- Edit a product (rename, price, quantity)
- Delete a product
- Search for a product

Tech
----
- .NET 8.0
- C#

Project Structure
-----------------
```
/SimpleInventoryManagementSystem
  ├── Domain/
  │   ├── Product.cs
  │   └── Inventory.cs
  ├── Program.cs
  └── README.md
```

Getting Started
---------------
- Clone
  ```
    git clone https://github.com/Mohammad-Mans/simple-inventory-management-system.git
    cd SimpleInventoryManagementSystem
  ```
- Run
  ```
   dotnet run
  ```
Usage
-----
When the app starts, choose from the menu:

--- Menu Options ---
1) Add product
2) View all products
3) Edit a product
4) Delete a product
5) Search for a product
0) Exit

Sample session
--------------
```
Choose an option: 1
Enter product name: Apple
Enter price: 1.25
Enter quantity: 10
Product added successfully.

Choose an option: 2
Name | Price | Quantity
Apple | 1.25 | 10

Choose an option: 3
Enter the product name to edit: Apple
Editing 'Apple' (Price: 1.25, Quantity: 10)
Press Enter to keep the current value.
New name [Apple]:
New price [1.25]: 1.5
New quantity [10]:
Product edited successfully.
```
--------------
## :stars: Acknowledgment
Special thanks to [**Foothill Technology Solutions**](https://www.foothillsolutions.com/) for the opportunity to work on this project during my internship. The experience and knowledge gained have been invaluable.
