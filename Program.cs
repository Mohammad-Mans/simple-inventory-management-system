using SimpleInventoryManagementSystem.Domain;

class Program
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

        bool success = inventory.AddProduct(name, price, quantity);

        Console.WriteLine(success
            ? "Product added successfully"
            : "Failed to add product.");
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
}