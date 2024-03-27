using Lab5.Application.Cantracts.Users;
using Lab5.Application.Models;
using Lab5.Application.Models.People;

namespace Lab5.Application;

public class CurrentUserManager : ICurrentUserService
{
    public User? User { get; set; }
    public Card? Card { get; set; }
}