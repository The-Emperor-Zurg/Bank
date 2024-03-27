namespace Lab5.Application.Models.People;

public class Admin
{
    public Admin(string password)
    {
        Password = password;
    }

    public string Password { get; private set; }

    public void SetNewPassword(string newPassword)
    {
        Password = newPassword;
    }
}