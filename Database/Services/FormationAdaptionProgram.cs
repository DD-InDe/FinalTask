using System.Text.Json;
using Database.Models;

namespace Database.Services;

public class FormationAdaptionProgram : ApiBase
{
    public static async Task<List<Module>?> GetModulesList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + "api/FormationProgram/Modules");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Module>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static async Task<List<Collaboration>?> GetEmployeeCollaborationsList(int id)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Collaborations/{id}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Collaboration>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static async Task<List<ModuleStatus>?> GetModuleStatusesList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/ModuleStatuses");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<ModuleStatus>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}