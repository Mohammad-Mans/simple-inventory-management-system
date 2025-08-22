namespace SimpleInventoryManagementSystem.Domain;

public class InventoryException(string message) : Exception(message);

public class InvalidProductException(string message) : InventoryException(message);

public class ProductNameAlreadyExistsException(string name)
    : InventoryException($"A product named '{name}' already exists.");

public class ProductNotFoundException(string name) : InventoryException($"Product '{name}' was not found.");

public class Inventory
{
    private List<Product> _products = new();

    private static void ValidateProduct(string? name, decimal? price, int? quantity)
    {
        if (name is not null && string.IsNullOrWhiteSpace(name))
            throw new InvalidProductException("Name must be non-empty.");

        if (price is < 0)
            throw new InvalidProductException("Price must be non-negative.");

        if (quantity is < 0)
            throw new InvalidProductException("Quantity must be non-negative.");
    }

    private void EnsureNotDuplicate(string name, Product? except = null)
    {
        foreach (var p in _products)
        {
            if (except is not null && p == except) continue;
            if (string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase))
                throw new ProductNameAlreadyExistsException(name);
        }
    }

    public Product AddProduct(string name, decimal price, int quantity)
    {
        ValidateProduct(name, price, quantity);

        var trimmed = name.Trim();
        EnsureNotDuplicate(trimmed);

        var product = new Product(trimmed, price, quantity);
        _products.Add(product);
        return product;
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

    public Product EditProduct(
        Product product,
        string? newName,
        decimal? newPrice,
        int? newQuantity)
    {
        var nameToValidate = string.IsNullOrWhiteSpace(newName) ? null : newName;

        ValidateProduct(nameToValidate, newPrice, newQuantity);

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var trimmed = newName.Trim();
            EnsureNotDuplicate(trimmed, product);
            product.Name = trimmed;
        }

        if (newPrice.HasValue) product.Price = newPrice.Value;
        if (newQuantity.HasValue) product.Quantity = newQuantity.Value;

        return product;
    }

    public void DeleteProduct(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidProductException("Name must be non-empty.");

        var product = _products
            .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

        if (product != null)
        {
            _products.Remove(product);
            return;
        }

        throw new ProductNotFoundException(name);
    }
}