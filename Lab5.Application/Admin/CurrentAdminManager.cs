using Lab5.Application.Cantracts.Users;
using Lab5.Application.Models.People;

namespace Lab5.Application;

public class CurrentAdminManager : ICurrentAdminService
{
    public Admin? Admin { get; set; }
}