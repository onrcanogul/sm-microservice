namespace Shared.Base.Exceptions;

public class NotFoundException(string message = "Not Found") : Exception(message);