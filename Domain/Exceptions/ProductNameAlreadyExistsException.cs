namespace SimpleInventoryManagementSystem.Domain.Exceptions;

public class ProductNameAlreadyExistsException(string name)
    : InventoryException($"A product named '{name}' already exists.");