using Lab5.Application.Models;
using Lab5.Application.Models.People;

namespace Lab5.Application.Cantracts.Users;

public interface ICurrentUserService
{
    User? User { get; }
    Card? Card { get; }
}