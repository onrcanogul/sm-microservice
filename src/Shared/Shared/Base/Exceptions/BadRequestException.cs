namespace Shared.Base.Exceptions;

public class BadRequestException(string message) : Exception(message);