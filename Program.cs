using SimpleInventoryManagementSystem.Domain;
using SimpleInventoryManagementSystem.Domain.Exceptions;

internal class Program
{
    public static void Main()
    {
        var inventory = new Inventory();
        while (true)
        {
            PrintMenu();
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    return;

                case "1":
                    AddProduct(inventory);
                    break;

                case "2":
                    ViewAllProducts(inventory);
                    break;

                case "3":
                    EditProduct(inventory);
                    break;

                case "4":
                    DeleteProduct(inventory);
                    break;

                case "5":
                    SearchProduct(inventory);
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("--- Menu Options ---");
        Console.WriteLine("1) Add product");
        Console.WriteLine("2) View all products");
        Console.WriteLine("3) Edit a product");
        Console.WriteLine("4) Delete a product");
        Console.WriteLine("5) Search for a product");
        Console.WriteLine("0) Exit");
    }

    private static void AddProduct(Inventory inventory)
    {
        var name = ReadNonEmpty("Enter product name: ");
        var price = ReadNonNegativeDecimal("Enter price: ");
        var quantity = ReadNonNegativeInt("Enter quantity: ");

        try
        {
            var addedProduct = inventory.AddProduct(name, price, quantity);
            Console.WriteLine(
                $"Product added successfully:\nName: {addedProduct.Name}, Price: {addedProduct.Price}, Quantity: {addedProduct.Quantity}");
        }
        catch (InventoryException ex)
        {
            Console.WriteLine($"Failed to add product. {ex.Message}");
        }
    }


    private static void ViewAllProducts(Inventory inventory)
    {
        var items = inventory.GetProducts();
        if (items.Count == 0)
            Console.WriteLine("No products in inventory.");
        else
        {
            Console.WriteLine("Name | Price | Quantity");
            foreach (var p in items)
            {
                Console.WriteLine($"{p.Name} | {p.Price} | {p.Quantity}");
            }
        }
    }

    private static void EditProduct(Inventory inventory)
    {
        var targetName = ReadNonEmpty("Enter the product name to edit: ");
        if (!inventory.FindByName(targetName, out var product))
        {
            Console.WriteLine("Not found.");
            return;
        }

        Console.WriteLine($"Editing '{product!.Name}' (Price: {product.Price}, Quantity: {product.Quantity})");
        Console.WriteLine("Press Enter to keep the current value.");

        var newName = ReadOptionalName("New name", product.Name);
        var newPrice = ReadOptionalNonNegativeDecimal("New price", product.Price);
        var newQty = ReadOptionalNonNegativeInt("New quantity", product.Quantity);

        try
        {
            var editedProduct = inventory.EditProduct(product, newName, newPrice, newQty);
            Console.WriteLine(
                $"Product was edited successfully, new values are:\nName: {editedProduct.Name}, Price: {editedProduct.Price}, Quantity: {editedProduct.Quantity}");
        }
        catch (InventoryException ex)
        {
            Console.WriteLine($"Failed to edit product. {ex.Message}");
        }
    }

    private static void DeleteProduct(Inventory inventory)
    {
        var name = ReadNonEmpty("Enter the product name to delete: ");
        try
        {
            inventory.DeleteProduct(name);
            Console.WriteLine("Product deleted successfully.");
        }
        catch (InventoryException ex)
        {
            Console.WriteLine($"Failed to delete product. {ex.Message}");
        }
    }

    private static void SearchProduct(Inventory inventory)
    {
        var name = ReadNonEmpty("Enter the product name to search: ");

        if (inventory.FindByName(name, out var product))
        {
            Console.WriteLine("Name | Price | Quantity");
            Console.WriteLine($"{product!.Name} | {product.Price} | {product.Quantity}");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    private static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var s = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
            Console.WriteLine("Invalid input. Please enter a non-empty value.");
        }
    }

    private static decimal ReadNonNegativeDecimal(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}");
            var s = Console.ReadLine();
            if (decimal.TryParse(s, out var v) && v >= 0) return v;
            Console.WriteLine("Invalid. Please enter a non-negative decimal.");
        }
    }

    private static int ReadNonNegativeInt(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}");
            var s = Console.ReadLine();
            if (int.TryParse(s, out var v) && v >= 0) return v;
            Console.WriteLine("Invalid. Please enter a non-negative integer.");
        }
    }

    private static string? ReadOptionalName(string label, string current)
    {
        Console.Write($"{label} [{current}]: ");
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
    }

    private static decimal? ReadOptionalNonNegativeDecimal(string label, decimal current)
    {
        while (true)
        {
            Console.Write($"{label} [{current}]: ");
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (decimal.TryParse(s, out var v) && v >= 0) return v;
            Console.WriteLine("Invalid. Enter a non-negative decimal, or press Enter to keep.");
        }
    }

    private static int? ReadOptionalNonNegativeInt(string label, int current)
    {
        while (true)
        {
            Console.Write($"{label} [{current}]: ");
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (int.TryParse(s, out var v) && v >= 0) return v;
            Console.WriteLine("Invalid. Enter a non-negative integer, or press Enter to keep.");
        }
    }
}