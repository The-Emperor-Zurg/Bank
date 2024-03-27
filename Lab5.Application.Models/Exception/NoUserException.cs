namespace Lab5.Application;

public class NoUserException : Exception
{
    public NoUserException(string message)
        : base(message)
    { }

    public NoUserException(string message, Exception innerException)
        : base(message, innerException)
    { }

    public NoUserException()
    { }
}