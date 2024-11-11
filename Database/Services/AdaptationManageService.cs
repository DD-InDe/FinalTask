using System.Net.Http.Json;
using System.Text.Json;
using Database.Models;

namespace Database.Services;

public abstract class AdaptationManageService : ApiBase
{
    public static async Task<bool> AddModuleAndCollaborations(List<Collaboration> collaborations)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/AdaptationManage/Collaborations", collaborations);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<List<Module>> GetModulesByPosition(int positionId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/AdaptationManage/Modules/positionId={positionId}");
            return await JsonSerializer.DeserializeAsync<List<Module>>(await message.Content.ReadAsStreamAsync(),
                Options) ?? new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddAdaptationProgram(List<AdaptationProgramModule> adaptationProgramModules)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/AdaptationManage/AdaptationPrograms",
                adaptationProgramModules);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}