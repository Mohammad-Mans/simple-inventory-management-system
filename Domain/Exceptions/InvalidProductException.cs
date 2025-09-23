namespace SimpleInventoryManagementSystem.Domain.Exceptions;

public sealed class InvalidProductException(string message) : InventoryException(message);