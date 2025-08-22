namespace SimpleInventoryManagementSystem.Domain;

public class Inventory
{
    private List<Product> _products = new();

    private static bool ValidateProduct(string? name, decimal? price, int? quantity, out string? error)
    {
        if (name is not null && string.IsNullOrWhiteSpace(name))
        {
            error = "Name must be non-empty.";
            return false;
        }

        if (price is < 0)
        {
            error = "Price must be non-negative.";
            return false;
        }

        if (quantity is < 0)
        {
            error = "Quantity must be non-negative.";
            return false;
        }

        error = null;
        return true;
    }

    private bool IsDuplicate(string name, Product? except = null)
    {
        foreach (var p in _products)
        {
            if (except is not null && ReferenceEquals(p, except)) continue;
            if (string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)) return true;
        }

        return false;
    }
    
    public bool AddProduct(string name, decimal price, int quantity, out string? error)
    {
        if (!ValidateProduct(name, price, quantity, out error)) return false;

        var trimmed = name.Trim();

        if (IsDuplicate(trimmed))
        {
            error = "A product with that name already exists.";
            return false;
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
        var nameToValidate = string.IsNullOrWhiteSpace(newName) ? null : newName;

        if (!ValidateProduct(nameToValidate, newPrice, newQuantity, out error)) return false;

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var trimmed = newName.Trim();
            if (IsDuplicate(trimmed, product))
            {
                error = "Another product with that new name already exists.";
                return false;
            }

            product.Name = trimmed;
        }

        if (newPrice.HasValue) product.Price = newPrice.Value;
        if (newQuantity.HasValue) product.Quantity = newQuantity.Value;

        return true;
    }

    public bool DeleteProduct(string name, out string? error)
    {
        if (!ValidateProduct(name, null, null, out error)) return false;

        for (int i = 0; i < _products.Count; i++)
        {
            var p = _products[i];
            if (string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                _products.RemoveAt(i);
                return true;
            }
        }

        error = "Product not found.";
        return false;
    }
}