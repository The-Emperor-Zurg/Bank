using Lab5.Application.Models.People;

namespace Lab5.Application.Cantracts.Users;

public interface ICurrentAdminService
{
    Admin? Admin { get; }
}