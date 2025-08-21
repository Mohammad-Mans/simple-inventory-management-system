namespace SimpleInventoryManagementSystem.Domain;

public class Product
{
    public string Name { get; set; }

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set => _price = value < 0 ? 0 : value;
    }

    private int _quantity;

    public int Quantity
    {
        get => _quantity;
        set => _quantity = value < 0 ? 0 : value;
    }
    
    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}