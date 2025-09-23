namespace SimpleInventoryManagementSystem.Domain.Exceptions;

public sealed class ProductNotFoundException(string name)
    : InventoryException($"Product '{name}' was not found.");