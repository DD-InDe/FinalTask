using System.Net.Http.Json;
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
}