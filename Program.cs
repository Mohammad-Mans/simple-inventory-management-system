using SimpleInventoryManagementSystem.Domain;

class Program
{
    public static void Main()
    {
        var inventory = new Inventory();
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
}