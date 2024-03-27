namespace Lab5.Application;

public class NoCardException : Exception
{
    public NoCardException(string message)
        : base(message)
    { }

    public NoCardException(string message, Exception innerException)
        : base(message, innerException)
    { }

    public NoCardException()
    { }
}