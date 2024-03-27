namespace Lab5.Application.Cantracts.Users;

public interface IAdminService
{
    Task SetNewPassword(string password);
    Task<BaseResult> LoginAdmin(string password);
    Task<BaseResult> AddUser(string userName, string password);
    Task Logout();
}