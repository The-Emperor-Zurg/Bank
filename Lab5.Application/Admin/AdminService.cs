using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Lab5.Application.Models.People;
using Newtonsoft.Json;

namespace Lab5.Application;

public class AdminService : IAdminService
{
    private readonly IUserRepository _repository;
    private CurrentAdminManager _admin;
    public AdminService(CurrentAdminManager currentAdminService, IUserRepository repository)
    {
        _admin = currentAdminService;
        _repository = repository;
    }

    public async Task SetNewPassword(string password)
    {
        if (_admin.Admin is null)
        {
            throw new NoAdminException("No admin");
        }

        _admin.Admin.SetNewPassword(password);
        string newJson = JsonConvert.SerializeObject(_admin.Admin, Formatting.Indented);
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "..", "AdminData.json");
        await File.WriteAllTextAsync(path, newJson);
    }

    public async Task<BaseResult> LoginAdmin(string password)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "..", "AdminData.json");
        string json = await File.ReadAllTextAsync(path);
        Admin? admin = JsonConvert.DeserializeObject<Admin>(json);

        if (admin?.Password != password)
        {
            return BaseResult.Unluck;
        }

        _admin.Admin = admin;
        return BaseResult.Success;
    }

    public async Task<BaseResult> AddUser(string userName, string password)
    {
        Task<User?> user = _repository.FindUserByName(userName);
        if (await user is not null)
        {
            return BaseResult.Unluck;
        }

        await _repository.AddUser(userName, password);

        return BaseResult.Success;
    }

    public Task Logout()
    {
        _admin.Admin = null;

        return Task.CompletedTask;
    }
}