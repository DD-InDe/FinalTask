namespace Database.APIModels;

public class LogInModel(string login, string password)
{
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
}