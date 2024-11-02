using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Database.APIModels;
using Database.Models;

namespace Database.Services;

public class AuthorizationService:ApiBase
{
    public static async Task<Employee?> LogIn(LogInModel model)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/Authorization/LogIn", model);
            if (message.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<Employee>(await message.Content.ReadAsStreamAsync(),Options);
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 
}