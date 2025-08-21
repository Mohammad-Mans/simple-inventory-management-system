namespace SimpleInventoryManagementSystem.Domain;

public class Inventory
{
    private List<Product> _products = new();

    public bool AddProduct(string name, decimal price, int quantity, out string? error)
    {
        error = null;

        if (string.IsNullOrWhiteSpace(name))
        {
            error = "Name must be non-empty.";
            return false;
        }

        if (price < 0)
        {
            error = "Price must be non-negative.";
            return false;
        }

        if (quantity < 0)
        {
            error = "Quantity must be non-negative.";
            return false;
        }

        var trimmed = name.Trim();

        foreach (var p in _products)
        {
            if (string.Equals(p.Name, trimmed, StringComparison.OrdinalIgnoreCase))
            {
                error = "A product with that name already exists.";
                return false;
            }
        }

        _products.Add(new Product(trimmed, price, quantity));
        return true;
    }

    public IReadOnlyList<Product> GetProducts() => _products;

    public bool FindByName(string name, out Product? product)
    {
        product = null;
        if (string.IsNullOrWhiteSpace(name)) return false;

        foreach (var p in _products)
        {
            if (string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                product = p;
                return true;
            }
        }

        return false;
    }

    public bool EditProduct(
        Product product,
        string? newName,
        decimal? newPrice,
        int? newQuantity,
        out string? error)
    {
        error = null;

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var trimmed = newName.Trim();

            foreach (var p in _products)
            {
                if (!ReferenceEquals(p, product) &&
                    string.Equals(p.Name, trimmed, StringComparison.OrdinalIgnoreCase))
                {
                    error = "Another product with that name already exists.";
                    return false;
                }
            }

            product.Name = trimmed;
        }

        if (newPrice.HasValue)
        {
            if (newPrice.Value < 0)
            {
                error = "Price must be non-negative.";
                return false;
            }

            product.Price = newPrice.Value;
        }

        if (newQuantity.HasValue)
        {
            if (newQuantity.Value < 0)
            {
                error = "Quantity must be non-negative.";
                return false;
            }

            product.Quantity = newQuantity.Value;
        }

        return true;
    }
}