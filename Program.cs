using SimpleInventoryManagementSystem.Domain;

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

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    public static void PrintMenu()
    {
        Console.WriteLine("--- Menu Options ---");
        Console.WriteLine("1) Add product");
        Console.WriteLine("2) View all products");
        Console.WriteLine("3) Edit a product");
        Console.WriteLine("0) Exit");
    }

    public static void AddProduct(Inventory inventory)
    {
        string name;
        while (true)
        {
            Console.Write("Enter product name: ");
            name = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(name))
                break;

            Console.WriteLine("Invalid name. Please enter a non-empty product name.");
        }

        decimal price;
        while (true)
        {
            Console.Write("Enter price: ");
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out price) && price >= 0)
                break;

            Console.WriteLine("Invalid price. Please enter a non-negative number.");
        }

        int quantity;
        while (true)
        {
            Console.Write("Enter quantity: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out quantity) && quantity >= 0)
                break;

            Console.WriteLine("Invalid quantity. Please enter a non-negative integer.");
        }

        bool success = inventory.AddProduct(name, price, quantity, out var error);
        if (success)
            Console.WriteLine("Product added successfully.");
        else
            Console.WriteLine($"Failed to add product. {error}");
    }


    public static void ViewAllProducts(Inventory inventory)
    {
        var items = inventory.GetProducts();
        if (items.Count == 0)
            Console.WriteLine("No products in inventory.");
        else
        {
            Console.WriteLine("Name | Price | Quantity");
            foreach (Product p in items)
            {
                Console.WriteLine($"{p.Name} | {p.Price} | {p.Quantity}");
            }
        }
    }

    private static void EditProduct(Inventory inventory)
    {
        Console.Write("Enter the product name to edit: ");
        var targetName = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(targetName) ||
            !inventory.FindByName(targetName, out var product))
        {
            Console.WriteLine("Not found.");
            return;
        }

        Console.WriteLine($"Editing '{product!.Name}' (Price: {product.Price}, Quantity: {product.Quantity})");
        Console.WriteLine("Press Enter to keep the current value.");

        Console.Write($"New name [{product.Name}]: ");
        var nameInput = Console.ReadLine();
        string? newName = string.IsNullOrWhiteSpace(nameInput) ? null : nameInput.Trim();

        decimal? newPrice = null;
        while (true)
        {
            Console.Write($"New price [{product.Price}]: ");
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) break;
            if (decimal.TryParse(s, out var v) && v >= 0)
            {
                newPrice = v;
                break;
            }

            Console.WriteLine("Invalid. Enter a non‑negative decimal, or press Enter to keep.");
        }

        int? newQty = null;
        while (true)
        {
            Console.Write($"New quantity [{product.Quantity}]: ");
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) break;
            if (int.TryParse(s, out var v) && v >= 0)
            {
                newQty = v;
                break;
            }

            Console.WriteLine("Invalid. Enter a non‑negative integer, or press Enter to keep.");
        }

        bool success = inventory.EditProduct(product, newName, newPrice, newQty, out var error);
        Console.WriteLine(success ? "Product edited successfully." : $"Failed to edit product. {error}");
    }
}