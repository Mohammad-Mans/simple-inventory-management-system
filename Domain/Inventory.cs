namespace SimpleInventoryManagementSystem.Domain;

public class Inventory
{
    private List<Product> _products = new();

    public bool AddProduct(string name, decimal price, int quantity)
    {
        if (!string.IsNullOrEmpty(name) && price >= 0 && quantity >= 0)
        {
            _products.Add(new Product(name, price, quantity));
            return true;
        }
        return false;
    }
    public IReadOnlyList<Product> GetProducts() => _products;
}