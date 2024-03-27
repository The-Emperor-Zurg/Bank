namespace Lab5.Application;

public class NoAdminException : Exception
{
    public NoAdminException(string message)
        : base(message)
    { }

    public NoAdminException()
    { }

    public NoAdminException(string message, Exception innerException)
        : base(message, innerException)
    { }
}