using Lab5.Application.Models.People;

namespace Lab5.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task AddUser(string userName, string password);
    Task<User?> FindUserByName(string userName);
}