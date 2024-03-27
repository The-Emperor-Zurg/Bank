namespace Lab5.Application.Models;

public class Card
{
    public Card(long userId, long id, string pinCode, decimal balance = 0)
    {
        UserId = userId;
        ID = id;
        PinCode = pinCode;
        Balance = balance;
    }

    public long UserId { get; init; }
    public long ID { get; init; }
    public string PinCode { get; init; }
    public decimal Balance { get; private set; }

    public void ChangeBalace(decimal cash)
    {
        Balance += cash;
    }
}